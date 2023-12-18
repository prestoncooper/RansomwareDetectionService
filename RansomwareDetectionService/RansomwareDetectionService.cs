using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.ServiceProcess;
using System.Text;
using System.Configuration;
using System.IO;
using System.Threading;
using RansomwareDetection.DetectionLib;


/*
BSD License:
Copyright (c) 2016, Preston Cooper – HESD Ransomware Detection Service

All rights reserved.
Redistribution and use in source and binary forms, with or without modification, are permitted provided that the following conditions are met:
1. Redistributions of source code must retain the above copyright notice, this list of conditions and the following disclaimer.
2. Redistributions in binary form must reproduce the above copyright notice, this list of conditions and the following disclaimer in the documentation and/or other materials provided with the distribution.
3. Neither the name of the copyright holder nor the names of its contributors may be used to endorse or promote products derived from this software without specific prior written permission.
THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
*/



namespace RansomwareDetection
{
    /// <summary>
    /// Ransomware Detection Main Service Class
    /// </summary>
    public partial class RansomwareDetectionService : ServiceBase
    {
        #region "Variables"


        /// <summary>
        /// Default Service Interval
        /// </summary>
        const long ServiceIntervalDefault = 120000; 

        /// <summary>
        /// Timer Object
        /// </summary>
        private System.Timers.Timer _t;

        private static int intRunCount = 0;
        
       
        Thread CompareThread;
        Thread FindFilesThread;
        Thread AuditFilesThread;


        /// <summary>
        /// if service is shutting down boolean variable
        /// </summary>
        private static bool blShuttingDown = false;
        
       

        private static DataTable dtCompareConfig;
        private static DataTable dtFileFiltersConfig;
        private static DataTable dtFindFilesConfig;
        private static DataTable dtAuditFilesConfig;
        private static DataTable dtSignaturesConfig;
    
        /// <summary>
        /// Event Log Class
        /// </summary>
        private static System.Diagnostics.EventLog _evt;
        
        //private static bool blRunning = false;

        #endregion

        #region "Properties"

        

        private long _serviceInterval = ServiceIntervalDefault;
        /// <summary>
        /// Service Interval Property is the time interval used to fire the timer
        /// </summary>
        public long ServiceInterval
        {
            get
            {
                string str = ConfigurationManager.AppSettings["ServiceInterval"];
                if (!String.IsNullOrEmpty(str))
                {
                    long.TryParse(str, out _serviceInterval);
                }
                else
                {
                    _serviceInterval = ServiceIntervalDefault;
                    SaveAppSetting("ServiceInterval", _serviceInterval.ToString());
                }
                return _serviceInterval;
            }

            set
            {
                _serviceInterval = value;
                if (_serviceInterval < 1000)
                {
                    _serviceInterval = ServiceIntervalDefault;
                }
                SaveAppSetting("ServiceInterval", _serviceInterval.ToString());

            }
        }


        

        
        private string _emailTo = "";
        public string EmailTo
        {
            get
            {
                string _emailTo = Common.FixNullstring(ConfigurationManager.AppSettings["EmailTo"]);
                return _emailTo;
            }
            set
            {
                _emailTo = value;

                SaveAppSetting("EmailTo", _emailTo);

            }
        }


        private string _emailFrom = "";
        public string EmailFrom
        {
            get
            {
                string _emailFrom = Common.FixNullstring(ConfigurationManager.AppSettings["EmailFrom"]);
                return _emailFrom;
            }
            set
            {
                _emailFrom = value;

                SaveAppSetting("EmailFrom", _emailFrom);

            }
        }


        private string _smtpServer = "";
        public string SMTPServer
        {
            get
            {
                string _smtpServer = Common.FixNullstring(ConfigurationManager.AppSettings["SMTPServer"]);
                return _smtpServer;
            }
            set
            {
                _smtpServer = value;

                SaveAppSetting("SMTPServer", _smtpServer);

            }
        }

        private int _smtpPort = 25;
        public int SMTPPort
        {
            get
            {
                int.TryParse( ConfigurationManager.AppSettings["SMTPPort"],out _smtpPort);
                return _smtpPort;
            }
            set
            {
                _smtpPort = value;
                SaveAppSetting("SMTPPort", _smtpPort.ToString());
            }
        }

        private bool _smtpUseSSL = false;
        public bool SMTPUseSSL
        {
            get
            {
                _smtpUseSSL = Common.FixNullbool(ConfigurationManager.AppSettings["SMTPUseSSL"]);
                return _smtpUseSSL;
            }
            set
            {
                _smtpUseSSL = value;
                SaveAppSetting("SMTPUseSSL", _smtpUseSSL.ToString());
            }
        }


        public static bool _smtpUseDefaultCredentials = true;
        /// <summary>
        /// SMTP Use SSL to connect to SMTP Host
        /// </summary>
        public bool SMTPUseDefaultCredentials
        {
            get
            {
                _smtpUseDefaultCredentials = Common.FixNullbool(ConfigurationManager.AppSettings["SMTPUseDefaultCredentials"]);
                return _smtpUseDefaultCredentials;
            }
            set
            {
                _smtpUseDefaultCredentials = value;
                SaveAppSetting("SMTPUseDefaultCredentials", _smtpUseDefaultCredentials.ToString());
            }
        }

        public static string _smtpUsername = "";
        /// <summary>
        /// SMTP host server Username
        /// </summary>
        public string SMTPUsername
        {
            get
            {
                _smtpUsername = Common.FixNullstring(ConfigurationManager.AppSettings["SMTPUsername"]);
                return _smtpUsername;
            }
            set
            {
                _smtpUsername = value;
                SaveAppSetting("SMTPUsername", _smtpUsername);
            }
        }

        public static string _smtpPassword = "";
        /// <summary>
        /// SMTP host server Password
        /// </summary>
        public string SMTPPassword
        {
            get
            {
                _smtpPassword= Common.FixNullstring(ConfigurationManager.AppSettings["SMTPPassword"]);
                return _smtpPassword;
            }
            set
            {
                _smtpPassword = value;
                SaveAppSetting("SMTPPassword", _smtpPassword);
            }
        }

        #endregion


        #region "Methods"

        /// <summary>
        /// Class Initialization
        /// </summary>
        public RansomwareDetectionService()
        {
            InitializeComponent();
            _evt = Common.GetEventLog;
            
        }

        /// <summary>
        /// Saves App.config application settings and refreshes.
        /// </summary>
        /// <param name="strProperty"></param>
        /// <param name="strValue"></param>
        public static void SaveAppSetting(string strProperty, string strValue)
        {
            System.Configuration.Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            //SAVE ALL OF THE SETTINGS
            config.AppSettings.Settings.Remove(strProperty);
            config.AppSettings.Settings.Add(strProperty, strValue);

            // Save the config file.
            config.Save(ConfigurationSaveMode.Modified);

            // Force a reload of a changed section. 
            ConfigurationManager.RefreshSection("appSettings");


        }

       

        /// <summary>
        /// Initializes Compress configuration table
        /// </summary>
        private void init_dtCompareConfig()
        {
            dtCompareConfig = CompareFolder.init_dtConfig();
        }

        private void init_dtFindFilesConfig()
        {
            dtFindFilesConfig = FindFilesFolder.init_dtConfig();
        }

        private void init_dtFileFiltersConfig()
        {
            dtFileFiltersConfig = FindFileFilter.init_dtConfig();
        }

        private void init_dtAuditFilesConfig()
        {
            dtAuditFilesConfig = AuditFolder.init_dtConfig();
        }

        private void init_dtSignaturesConfig()
        {
            dtSignaturesConfig = AuditFolder.init_dtSignaturesConfig();
        }

        /// <summary>
        /// Service Start Method
        /// </summary>
        /// <param name="args"></param>
        protected override void OnStart(string[] args)
        {
            try
            {
                _t = new System.Timers.Timer();
                blShuttingDown = false;
                _t.Interval = ServiceInterval;
                _t.Elapsed += TimerFired;
                _t.Enabled = true;

                
                init_dtCompareConfig();
                init_dtFindFilesConfig();
                init_dtFileFiltersConfig();
                init_dtAuditFilesConfig();
                init_dtSignaturesConfig();
                dtCompareConfig.ReadXml(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\CompareConfig.xml");
                dtFindFilesConfig.ReadXml(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\FindFilesConfig.xml");
                dtFileFiltersConfig.ReadXml(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\FileFiltersConfig.xml");
                dtAuditFilesConfig.ReadXml(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\AuditFilesConfig.xml");

                dtSignaturesConfig.ReadXml(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\SignaturesConfig.xml");


                //Timer_Execute();

                CompareThread = new Thread(new ThreadStart(CompareExecute));
                FindFilesThread = new Thread(new ThreadStart(FindFilesExecute));
                AuditFilesThread = new Thread(new ThreadStart(AuditFilesExecute));
                
                writeError("RansomwareDetectionService Started", System.Diagnostics.EventLogEntryType.Information, 6000, 60);
                  
            }
            catch (Exception ex)
            {

                _t.Elapsed += TimerFired;
                _t.Enabled = false;
                
                string strErr = ex.Message + ": " + ex.Source + "  " + ex.StackTrace;
                writeError(strErr, System.Diagnostics.EventLogEntryType.Error, 6000, 60);
            }

        }

        /// <summary>
        /// Service Stop Method
        /// </summary>
        protected override void OnStop()
        {
            try
            {
                _t.Enabled = false;
                blShuttingDown = true;
                System.Threading.Thread.Sleep(3000);
                _t.Dispose();
                _t = null;
                long lwait = 0;
                while (CompareThread.IsAlive || FindFilesThread.IsAlive || AuditFilesThread.IsAlive)
                {
                    Thread.Sleep(3000);
                    lwait += 3;
                    if (lwait > 3600)
                    {
                        
                        
                        if (CompareThread.IsAlive)
                        {
                            CompareThread.Abort();
                        }
                        if (FindFilesThread.IsAlive)
                        {
                            FindFilesThread.Abort();
                        }
                        if (AuditFilesThread.IsAlive)
                        {
                            AuditFilesThread.Abort();
                        }
                        Thread.Sleep(3000);
                        break;
                    }
                }
                
                
               
                CompareThread = null;
                FindFilesThread = null;
                if (lwait > 3600)
                {
                    writeError("RansomwareDetectionService Forced Stopped: " + lwait.ToString(), System.Diagnostics.EventLogEntryType.Information, 6000, 60);
                }
                else
                {
                    writeError("RansomwareDetectionService Stopped", System.Diagnostics.EventLogEntryType.Information, 6000, 60);
                }
            }
            catch (Exception ex)
            {
                string strErr = ex.Message + ": " + ex.Source + "  " + ex.StackTrace;
                writeError(strErr, System.Diagnostics.EventLogEntryType.Error, 6000, 60);
            }
            

        }

        /// <summary>
        /// Timer Event Fired Main Code  
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void TimerFired(object sender, System.Timers.ElapsedEventArgs e)
        {
            Timer_Execute();
        }

        /// <summary>
        /// Write Event Log Error
        /// </summary>
        /// <param name="strErrorMessage"></param>
        /// <param name="entrytype"></param>
        /// <param name="eventid"></param>
        /// <param name="category"></param>
        private void writeError(string strErrorMessage, System.Diagnostics.EventLogEntryType entrytype, int eventid, short category)
        {
            //multi threaded so _evt sometimes is not allocated. 
            if (_evt == null)
            {
                _evt = Common.GetEventLog;
            }
            if (strErrorMessage.Length > 31700)
            {
                strErrorMessage = strErrorMessage.Substring(0, 31700) + " ...";
            }
            _evt.WriteEntry(strErrorMessage, entrytype, eventid, category);

        }

        /// <summary>
        /// Checks to see of the current time is within the appropriate time window to execute per RetentionExecutionTime or CompressExecutionTime
        /// </summary>
        /// <param name="strExecutionTime"></param>
        /// <returns></returns>
        public bool TimeToExecute(IFolderConfig folder)
        {
            string strExecutionTime = "";
            bool blTimeStart = false;
            bool blFinalTimeEnd = false;
            TimeSpan timeStart;
            TimeSpan timeEnd;
            TimeSpan FinalTimeEnd;
            int intIntervalSeconds;
            int intIntervalMinutes;
            int intHours = 0;
            int intMinutes = 0;
            try
            {

                strExecutionTime = folder.Time;
                TimeSpan currentTime = DateTime.Now.TimeOfDay;

                
                //Interval in Minutes + 1 second to make sure the time occurs within the window of the serviceinterval
                intIntervalSeconds = (int)((ServiceInterval) / 1000);
                intIntervalSeconds = intIntervalSeconds + 1;
                

                intIntervalMinutes = (int) Math.Floor(((double) intIntervalSeconds / 60));
                
                if (intIntervalSeconds <= 60000)
                {
                    intIntervalMinutes = 0;
                }
                else
                {
                    intIntervalSeconds = intIntervalSeconds % 60;
                }
                
                blTimeStart = TimeSpan.TryParse(strExecutionTime, out timeStart);

                if (blTimeStart)
                {
                    timeEnd = timeStart + new TimeSpan(0, intIntervalMinutes, intIntervalSeconds);
                }
                else
                {
                    //No Time to Start Specified
                    return false;
                }

                blFinalTimeEnd = TimeSpan.TryParse(folder.EndTime, out FinalTimeEnd);

                if (blTimeStart && currentTime >= timeStart && (currentTime <= timeEnd || (folder.IntervalType == IntervalTypes.Hourly )) && (!blFinalTimeEnd || currentTime <= FinalTimeEnd))
                {
                    switch (folder.IntervalType)
                    {
                        case IntervalTypes.Hourly:
                            //Hourly Repetitive Interval

                            //folder.Interval is in Minutes for Hourly
                            
                            int intCurrentMinutes = 0;
                            intHours = (int) (folder.Interval / (long)60);
                            intHours = intHours % 24;
                            
                            intMinutes = (int)((double)folder.Interval % (double)60.0);
                            

                            intCurrentMinutes = (currentTime.Hours % 24) * 60;
                            intCurrentMinutes += currentTime.Minutes % 60;
                            //If reoccurring time remainder is 0 when comparing the current hour and minutes combined into minutes vs the Interval minutes then it is time to execute
                            //Also need to handle the zero remainder when the time is zero hour and zero minutes and add some exceptions (divisors of 60).
                            if (intCurrentMinutes % folder.Interval == 0 && !(currentTime.Minutes == 0 && currentTime.Hours == 0 && !(folder.Interval == 30 || folder.Interval == 20 || folder.Interval == 15 || folder.Interval == 12 || folder.Interval == 10 || folder.Interval == 6 || folder.Interval == 5 || folder.Interval == 4 || folder.Interval == 3 || folder.Interval == 2 || folder.Interval == 1)))
                            {
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                                    
                            
                        default:   //Daily or Monthly the repetitive time doesn't matter, should only execute once
                            return true;
                            
                    }
                              
                }
                else
                {
                    return false;
                }

                
            }
            catch (Exception ex)
            {
                string strErr = ex.Message + ": " + ex.Source + "  " + ex.StackTrace;
                writeError(strErr, System.Diagnostics.EventLogEntryType.Error, 6000, 60);
                return false;
            }
               

        }

        /// <summary>
        /// Nth Day of the Month for example the 3rd Monday of the Month
        /// </summary>
        /// <param name="folder"></param>
        /// <returns></returns>
        private bool NthDayOfMonth(IFolderConfig folder)
        {
            DateTime today = DateTime.Now;
            bool blExecuteToday = false;
            int n = 0;
            n = (int)Math.Abs(folder.Interval);

            if (DayToExecute(folder) && folder.Interval < 0 && folder.IntervalType == IntervalTypes.Monthly)
            {   
                return (today.Day - 1) / 7 == (n - 1);
            }
            return blExecuteToday;
        }

        
        /// <summary>
        /// Day of the Month to Execute for example the 5th day of the month or the 20th of the month
        /// </summary>
        /// <param name="folder"></param>
        /// <returns></returns>
        public bool DayOfMonthToExecute(IFolderConfig folder)
        {
            DateTime today = DateTime.Now;
            bool blExecuteToday = false;
            if (today.Day == folder.Interval && folder.Interval > 0 && folder.IntervalType == IntervalTypes.Monthly)
            {
                blExecuteToday = true;
            }
            return blExecuteToday;
        }


        /// <summary>
        /// Day to Execute for example if Monday is selected and Today is Monday then this returns true
        /// </summary>
        /// <param name="folder"></param>
        /// <returns></returns>
        public bool DayToExecute(IFolderConfig folder)
        {
            DateTime today =DateTime.Now;
            bool blExecuteToday = false;
            
            switch (today.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    if (folder.Monday)
                    {
                        blExecuteToday = true;
                    }
                    break;
                case DayOfWeek.Tuesday:
                    if (folder.Tuesday)
                    {
                        blExecuteToday = true;
                    }
                    break;
                case DayOfWeek.Wednesday:
                    if (folder.Wednesday)
                    {
                        blExecuteToday = true;
                    }
                    break;
                case DayOfWeek.Thursday:
                    if (folder.Thursday)
                    {
                        blExecuteToday = true;
                    }
                    break;
                case DayOfWeek.Friday:
                    if (folder.Friday)
                    {
                        blExecuteToday = true;
                    }
                    break;
                case DayOfWeek.Saturday:
                    if (folder.Saturday)
                    {
                        blExecuteToday = true;
                    }
                    break;
                case DayOfWeek.Sunday:
                    if (folder.Sunday)
                    {
                        blExecuteToday = true;
                    }
                    break;
            }
            
            return blExecuteToday;
        }

        

        /// <summary>
        /// Is Today in the Month specified to execute
        /// </summary>
        /// <param name="folder"></param>
        /// <returns></returns>
        public bool MonthToExecute(IFolderConfig folder)
        {
            DateTime today = DateTime.Now;
            bool blExecuteThisMonth = false;
            switch (today.Month)
            {
                case 1:
                    if ((folder.Months & Month.January) == Month.January)
                    {
                        blExecuteThisMonth= true;
                    }
                    break;
                case 2:
                    if ((folder.Months & Month.February) == Month.February)
                    {
                        blExecuteThisMonth = true;
                    }
                    break;
                case 3:
                    if ((folder.Months & Month.March) == Month.March)
                    {
                        blExecuteThisMonth = true;
                    }
                    break;
                case 4:
                    if ((folder.Months & Month.April) == Month.April)
                    {
                        blExecuteThisMonth = true;
                    }
                    break;
                case 5:
                    if ((folder.Months & Month.May) == Month.May)
                    {
                        blExecuteThisMonth = true;
                    }
                    break;
                case 6:
                    if ((folder.Months & Month.June) == Month.June)
                    {
                        blExecuteThisMonth = true;
                    }
                    break;
                case 7:
                    if ((folder.Months & Month.July) == Month.July)
                    {
                        blExecuteThisMonth = true;
                    }
                    break;
                case 8:
                    if ((folder.Months & Month.August) == Month.August)
                    {
                        blExecuteThisMonth = true;
                    }
                    break;
                case 9:
                    if ((folder.Months & Month.September) == Month.September)
                    {
                        blExecuteThisMonth = true;
                    }
                    break;
                case 10:
                    if ((folder.Months & Month.October) == Month.October)
                    {
                        blExecuteThisMonth = true;
                    }
                    break;
                case 11:
                    if ((folder.Months & Month.November) == Month.November)
                    {
                        blExecuteThisMonth = true;
                    }
                    break;
                case 12:
                    if ((folder.Months & Month.December) == Month.December)
                    {
                        blExecuteThisMonth = true;
                    }
                    break;

            }
            return blExecuteThisMonth;
        }

        public bool ValidateStartDate(IFolderConfig folder)
        {
            bool blExecute = false;
            DateTime today = DateTime.Now;
            if (folder.StartDate != null)
            {
                if (today >= folder.StartDate || folder.StartDate == DateTime.MinValue)
                {
                    blExecute = true;
                }
            }
            else
            {
                blExecute = true;
            }
            return blExecute;
        }

        public bool ValidateEndDate(IFolderConfig folder)
        {
            bool blExecute = false;
            DateTime today = DateTime.Now;
            if (folder.EndDate != null)
            {
                if (today <= folder.EndDate || folder.EndDate == DateTime.MinValue)
                {
                    blExecute = true;
                }
            }
            else
            {
                blExecute = true;
            }
            return blExecute;

        }

        /// <summary>
        /// Checks Multiple Conditions to see if it is the correct Month, Day, and Time to execute
        /// Also, that Today is in between the StartDate and EndDate
        /// </summary>
        /// <param name="folder"></param>
        /// <returns></returns>
        public bool ExecuteTime(IFolderConfig folder)
        {
           
            bool blTime = false;
            if (folder.Enabled)
            {
                if (ValidateStartDate(folder) && ValidateEndDate(folder))
                {
                    if (MonthToExecute(folder))
                    {
                        if (string.IsNullOrEmpty(folder.Time) || TimeToExecute(folder))
                        {
                            if (DayToExecute(folder) || DayOfMonthToExecute(folder) || NthDayOfMonth(folder))
                            {
                                blTime = true;
                            }
                        }
                    }
                }
            }

            return blTime;
        }
        
       

        private System.Object lockCompare = new System.Object();
        /// <summary>
        /// Thread Procedure for Compare Folder algorithm
        /// </summary>
        public void CompareExecute()
        {
            lock (lockCompare)
            {
                try
                {
                    //Retention  Deletes older files based on algorithm
                    foreach (DataRow row in dtCompareConfig.Rows)
                    {
                        CompareFolder CompFolder = new CompareFolder(row);
                        if (ExecuteTime(CompFolder))
                        {
                            CompFolder.SMTPPort = SMTPPort;
                            CompFolder.SMTPServer = SMTPServer;
                            CompFolder.SMTPUseSSL = SMTPUseSSL;
                            CompFolder.SMTPUseDefaultCredentials = SMTPUseDefaultCredentials;
                            CompFolder.SMTPUsername = SMTPUsername;
                            CompFolder.SMTPPassword = SMTPPassword;
                            CompFolder.EmailFrom = EmailFrom;
                            CompFolder.EmailTo = EmailTo;
                            CompFolder.Execute(ref blShuttingDown);
                        }
                    }
                }
                catch (Exception ex)
                {
                    string strErr = ex.Message + ": " + ex.Source + "  " + ex.StackTrace;
                    writeError(strErr, System.Diagnostics.EventLogEntryType.Error, 6000, 60);
                    
                }
                
            }

        }

        private System.Object lockFindFiles = new System.Object();
        /// <summary>
        /// Thread Procedure for Compare Folder algorithm
        /// </summary>
        public void FindFilesExecute()
        {
            lock (lockFindFiles)
            {
                try
                {
                    //Retention  Deletes older files based on algorithm
                    foreach (DataRow row in dtFindFilesConfig.Rows)
                    {
                        FindFilesFolder findFileFolder1 = new FindFilesFolder(row,dtFileFiltersConfig);
                        if (ExecuteTime(findFileFolder1))
                        {
                            findFileFolder1.SMTPPort = SMTPPort;
                            findFileFolder1.SMTPServer = SMTPServer;
                            findFileFolder1.SMTPUseDefaultCredentials = SMTPUseDefaultCredentials;
                            findFileFolder1.SMTPUseSSL = SMTPUseSSL;
                            findFileFolder1.SMTPUsername = SMTPUsername;
                            findFileFolder1.SMTPPassword = SMTPPassword;
                            findFileFolder1.EmailFrom = EmailFrom;
                            findFileFolder1.EmailTo = EmailTo;
                            findFileFolder1.Execute(ref blShuttingDown);
                        }
                    }
                }
                catch (Exception ex)
                {
                    string strErr = ex.Message + ": " + ex.Source + "  " + ex.StackTrace;
                    writeError(strErr, System.Diagnostics.EventLogEntryType.Error, 6000, 60);

                }

            }

        }



        private System.Object lockAudit = new System.Object();
        /// <summary>
        /// Thread Procedure for Compare Folder algorithm
        /// </summary>
        public void AuditFilesExecute()
        {
            lock (lockAudit)
            {
                try
                {
                    //Retention  Deletes older files based on algorithm
                    foreach (DataRow row in dtAuditFilesConfig.Rows)
                    {
                        AuditFolder AudFolder1 = new AuditFolder(row,dtSignaturesConfig);
                        if (ExecuteTime(AudFolder1))
                        {
                            AudFolder1.SMTPPort = SMTPPort;
                            AudFolder1.SMTPServer = SMTPServer;
                            AudFolder1.SMTPUseSSL = SMTPUseSSL;
                            AudFolder1.SMTPUseDefaultCredentials = SMTPUseDefaultCredentials;
                            AudFolder1.SMTPUsername = SMTPUsername;
                            AudFolder1.SMTPPassword = SMTPPassword;
                            AudFolder1.EmailFrom = EmailFrom;
                            AudFolder1.EmailTo = EmailTo;
                            AudFolder1.Execute(ref blShuttingDown);
                        }
                    }
                }
                catch (Exception ex)
                {
                    string strErr = ex.Message + ": " + ex.Source + "  " + ex.StackTrace;
                    writeError(strErr, System.Diagnostics.EventLogEntryType.Error, 6000, 60);

                }

            }

        }

        /// <summary>
        /// This code executes the main code of the service
        /// </summary>
        public void Timer_Execute()
        {
            try
            {             
                //Compare
                if (!(CompareThread.IsAlive || FindFilesThread.IsAlive))
                {
                    CompareThread = new Thread(new ThreadStart(CompareExecute));
                    CompareThread.Start();
                    
                    FindFilesThread = new Thread(new ThreadStart(FindFilesExecute));
                    FindFilesThread.Start();

                    AuditFilesThread = new Thread(new ThreadStart(AuditFilesExecute));
                    AuditFilesThread.Start();

                    intRunCount = 1;
                }
                else
                {
                    intRunCount++;
                    if (intRunCount > 90)
                    {
                        writeError("RansomwareDetectionService: Timer Code has not finished running in the last 90 minutes. This is normal while finding files in a large directory or the process could be hung. ", System.Diagnostics.EventLogEntryType.Information, 6000, 60);
                        intRunCount = 1;
                    }
                }
            }
            catch (Exception ex)
            {
                string strErr = ex.Message + ": " + ex.Source + "  " + ex.StackTrace;
                writeError(strErr, System.Diagnostics.EventLogEntryType.Error, 6000, 60);
            }
            
            
        }

        
        #endregion

    }

    
}
