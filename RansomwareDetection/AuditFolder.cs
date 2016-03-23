using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.IO;
using System.Data;
using System.Diagnostics;
using System.Threading;
using System.Net.Mail;


//Used this code project to compare file headers http://www.codeproject.com/Articles/18611/A-small-Content-Detection-Library

//Reference Articles for further review and later features
//http://en.wikipedia.org/wiki/Magic_number_(programming)
//http://stackoverflow.com/questions/1654846/in-c-how-can-i-know-the-file-type-from-a-byte
//http://stackoverflow.com/questions/58510/using-net-how-can-you-find-the-mime-type-of-a-file-based-on-the-file-signature
//http://stackoverflow.com/questions/15300567/alternative-to-findmimefromdata-method-in-urlmon-dll-one-which-has-more-mime-typ
//http://stackoverflow.com/questions/58510/using-net-how-can-you-find-the-mime-type-of-a-file-based-on-the-file-signature
//https://en.wikipedia.org/wiki/Magic_number_(programming)
//http://www.garykessler.net/library/file_sigs.html
//https://msdn.microsoft.com/en-us/library/ms775107(v=vs.85).aspx
//https://en.wikipedia.org/wiki/List_of_file_signatures
//http://asecuritysite.com/forensics/magic
//https://www.nationalarchives.gov.uk/PRONOM/Default.aspx
//https://www.nationalarchives.gov.uk/aboutapps/pronom/droid-signature-files.htm


namespace RansomwareDetection.DetectionLib
{

/*
BSD License:
Copyright (c) 2016, Preston Cooper – HESD Ransomware Detection Service
http://www.questiondriven.com
All rights reserved.
Redistribution and use in source and binary forms, with or without modification, are permitted provided that the following conditions are met:
1. Redistributions of source code must retain the above copyright notice, this list of conditions and the following disclaimer.
2. Redistributions in binary form must reproduce the above copyright notice, this list of conditions and the following disclaimer in the documentation and/or other materials provided with the distribution.
3. Neither the name of the copyright holder nor the names of its contributors may be used to endorse or promote products derived from this software without specific prior written permission.
THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
*/

/*
Delimon.Win32.IO Class License
Copyright © 2012, Johan Delimon
http://bit.ly/delimon 
All rights reserved.

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE. 
*/

    public class AuditFolder : IFolderConfig   
    {
        #region "Variables"

        private string ep = "19C235A4-A313-C4C4-48F4-A5B4DC86EBCC";

        public System.Collections.Generic.List<Delimon.Win32.IO.FileInfo> FilesVerified = null;
        public System.Collections.Generic.List<Delimon.Win32.IO.FileInfo> FilesUnVerified = null;
        public System.Collections.Generic.List<Delimon.Win32.IO.FileInfo> FilesUnknown = null;
        
        /// <summary>
        /// Event Log Class
        /// </summary>
        private static EventLog _evt;

        #endregion

        #region "Properties"

        private int _id = 1;
        public int ID
        {
            get
            {
                return _id;
            }

            set
            {
                _id = value;
            }

        }

        private string _title = "";
        public string Title
        {
            get
            {
                return _title;
            }

            set
            {
                _title = value;
            }

        }

        private string _time = "";
        public string Time
        {
            get
            {
                return _time;
            }

            set
            {
                _time = value;
            }

        }

        private string _endTime = "";
        public string EndTime
        {
            get
            {
                return _endTime;
            }

            set
            {
                _endTime = value;
            }

        }

        private IntervalTypes _intervalType = IntervalTypes.Daily;
        /// <summary>
        /// Interval Type  Hourly, Daily, Monthly
        /// </summary>
        public IntervalTypes IntervalType
        {
            get
            {
                return _intervalType;
            }

            set
            {
                _intervalType = value;
            }

        }

        private long _interval = 0;
        /// <summary>
        /// Interval  Hourly then minutes but start time and end time is required,  Monthly then 1-31 for day in the month or -1 to -5 for specific day in a week ( -1 Mon : runs 1st monday of the month)
        /// </summary>
        public long Interval
        {
            get
            {
                return _interval;
            }

            set
            {
                _interval = value;
            }

        }

        private bool _monday = false;
        public bool Monday
        {
            get
            {
                return _monday;
            }
            set
            {
                _monday = value;
            }
        }

        private bool _tuesday = false;
        public bool Tuesday
        {
            get
            {
                return _tuesday;
            }
            set
            {
                _tuesday = value;
            }
        }

        private bool _wednesday = false;
        public bool Wednesday
        {
            get
            {
                return _wednesday;
            }
            set
            {
                _wednesday = value;
            }
        }

        private bool _thursday = false;
        public bool Thursday
        {
            get
            {
                return _thursday;
            }
            set
            {
                _thursday = value;
            }
        }

        private bool _friday = false;
        public bool Friday
        {
            get
            {
                return _friday;
            }
            set
            {
                _friday = value;
            }
        }

        private bool _saturday = false;
        public bool Saturday
        {
            get
            {
                return _saturday;
            }
            set
            {
                _saturday = value;
            }
        }

        private bool _sunday = false;
        public bool Sunday
        {
            get
            {
                return _sunday;
            }
            set
            {
                _sunday = value;
            }
        }

        private Month _months = Month.January | Month.February | Month.March | Month.April | Month.May | Month.June | Month.July | Month.August | Month.September | Month.October | Month.November | Month.December;
        public Month Months
        {
            get
            {
                return _months;
            }

            set
            {
                _months = value;
            }
        }

        private bool _enabled = false;
        public bool Enabled
        {
            get
            {
                return _enabled;
            }

            set
            {
                _enabled = value;
            }
        }

        private string _comment = "";
        public string Comment
        {
            get
            {
                return _comment;
            }

            set
            {
                _comment = value;
            }

        }

        private DateTime _startDate;
        public DateTime StartDate
        {
            get
            {
                return _startDate;
            }

            set
            {
                _startDate = value;
            }
        }

        private DateTime _endDate;
        public DateTime EndDate
        {
            get
            {
                return _endDate;
            }

            set
            {
                _endDate = value;
            }
        }

       

        

        public static string _FilePathToCheck = "";
        /// <summary>
        /// File Path for files to be compared (expected that these files could possibly be changed or go missing)
        /// </summary>
        public string FilePathToCheck
        {
            get
            {
                return _FilePathToCheck;
            }
            set
            {
                _FilePathToCheck = Common.WindowsPathClean(value);
            }
        }


        private bool _sendEmailOnFailure = false;
        /// <summary>
        /// Send email on failure of binary file comparison
        /// </summary>
        public bool SendEmailOnFailure
        {
            get
            {
                return _sendEmailOnFailure;
            }

            set
            {
                _sendEmailOnFailure = value;
            }
        }


        private bool _sendEmailOnSuccess = false;
        /// <summary>
        /// Send email on success of binary file comparison
        /// </summary>
        public bool SendEmailOnSuccess
        {
            get
            {
                return _sendEmailOnSuccess;
            }

            set
            {
                _sendEmailOnSuccess = value;
            }
        }

        public static string _emailTo = "";
        /// <summary>
        /// Email To field (who will receive the email)
        /// </summary>
        public string EmailTo
        {
            get
            {
                return _emailTo;
            }
            set
            {
                _emailTo = value;
            }
        }

        public static string _emailFrom = "";
        /// <summary>
        /// Email will be sent from this email address
        /// </summary>
        public string EmailFrom
        {
            get
            {
                return _emailFrom;
            }
            set
            {
                _emailFrom = value;
            }
        }

        public static string _smtpServer = "";
        /// <summary>
        /// SMTP host server IP or hostname
        /// </summary>
        public string SMTPServer
        {
            get
            {
                return _smtpServer;
            }
            set
            {
                _smtpServer = value;
            }
        }

        public static int _smtpPort = 25;
        /// <summary>
        /// SMTP Port to be used to connect to SMTP server
        /// </summary>
        public int SMTPPort
        {
            get
            {
                return _smtpPort;
            }
            set
            {
                _smtpPort = value;
            }
        }

        public static bool _smtpUseSSL = false;
        /// <summary>
        /// SMTP Use SSL to connect to SMTP Host
        /// </summary>
        public bool SMTPUseSSL
        {
            get
            {
                return _smtpUseSSL;
            }
            set
            {
                _smtpUseSSL = value;
            }
        }

        public static bool _smtpUseDefaultCredentials = true;
        /// <summary>
        /// SMTP Use default credentials to connect to SMTP Host
        /// </summary>
        public bool SMTPUseDefaultCredentials
        {
            get
            {
                return _smtpUseDefaultCredentials;
            }
            set
            {
                _smtpUseDefaultCredentials = value;
            }
        }

        public static string _smtpUsername = "";
        /// <summary>
        /// SMTP host server username
        /// </summary>
        public string SMTPUsername
        {
            get
            {
                return _smtpUsername;
            }
            set
            {
                _smtpUsername = value;
            }
        }
        

        public static string _smtpPassword = "";
        /// <summary>
        /// SMTP host server password
        /// </summary>
        public string SMTPPassword
        {
            get
            {
                return _smtpPassword;
            }
            set
            {
                _smtpPassword = value;
            }
        }
       

        private bool _checkSubFolders = false;
        /// <summary>
        /// Check each sub folder in FilePathToCheck to have contents of SourcePath files in each
        /// </summary>
        public bool CheckSubFolders
        {
            get
            {
                return _checkSubFolders;
            }

            set
            {
                _checkSubFolders = value;
            }
        }


        private string _exportCSVPath = "";
        /// <summary>
        /// Exclude Folders separate folder names by semicolon
        /// </summary>
        public string ExportCSVPath
        {
            get
            {
                return _exportCSVPath;
            }
            set
            {
                _exportCSVPath = Common.WindowsPathClean(value);
            }

        }

        private bool _exportUnknownToCSV = true;
        /// <summary>
        /// Exclude Folders separate folder names by semicolon
        /// </summary>
        public bool ExportUnknownToCSV
        {
            get
            {
                return _exportUnknownToCSV;
            }
            set
            {
                _exportUnknownToCSV = value;
            }

        }

        private bool _exportVerifiedToCSV = true;
        /// <summary>
        /// Exclude Folders separate folder names by semicolon
        /// </summary>
        public bool ExportVerifiedToCSV
        {
            get
            {
                return _exportVerifiedToCSV;
            }
            set
            {
                _exportVerifiedToCSV = value;
            }

        }

        private bool _exportUnVerifiedToCSV = true;
        /// <summary>
        /// Exclude Folders separate folder names by semicolon
        /// </summary>
        public bool ExportUnVerifiedToCSV
        {
            get
            {
                return _exportUnVerifiedToCSV;
            }
            set
            {
                _exportUnVerifiedToCSV = value;
            }

        }

        private string _excludeFolders = "";
        /// <summary>
        /// Exclude Folders separate folder names by semicolon
        /// </summary>
        public string ExcludeFolders 
        {
            get
            {
                return _excludeFolders;
            }
            set
            {
                _excludeFolders = value;
            }
        
        }

        private bool _detailedLogging = false;
        public bool DetailedLogging
        {
            get
            {
                return _detailedLogging;
            }
            set
            {
                _detailedLogging = value;
            }
        }
        #endregion


         #region "Methods"

        

        /// <summary>
        /// Compare Contructor
        /// </summary>
        /// <param name="row"></param>
        public AuditFolder()
        {
            //FilesVerified = new System.Collections.Generic.List<Delimon.Win32.IO.FileInfo>();
            //FilesUnVerified = new System.Collections.Generic.List<Delimon.Win32.IO.FileInfo>();
            //FilesUnknown = new System.Collections.Generic.List<Delimon.Win32.IO.FileInfo>();
            _evt = Common.GetEventLog;
        }




        /// <summary>
        /// Compare Contructor
        /// </summary>
        /// <param name="row"></param>
        public AuditFolder(DataRow row)
        {
            _evt = Common.GetEventLog;
            //FilesVerified = new System.Collections.Generic.List<Delimon.Win32.IO.FileInfo>();
            //FilesUnVerified = new System.Collections.Generic.List<Delimon.Win32.IO.FileInfo>();
            //FilesUnknown = new System.Collections.Generic.List<Delimon.Win32.IO.FileInfo>();

            ID = Common.FixNullInt32(row["ID"]);
            Title = Common.FixNullstring(row["Title"]);
            Enabled = Common.FixNullbool(row["Enabled"]);
            Time = Common.FixNullstring(row["Time"]);
            EndTime=Common.FixNullstring(row["EndTime"]);

            try
            {
                IntervalType = (IntervalTypes) System.Enum.Parse(typeof(IntervalTypes), Common.FixNullstring(row["IntervalType"]));
            }
            catch (Exception)
            {
                IntervalType = IntervalTypes.Daily;
            }

            Interval = Common.FixNulllong(row["Interval"]);
            Monday = Common.FixNullbool(row["Monday"]);
            Tuesday = Common.FixNullbool(row["Tuesday"]);
            Wednesday = Common.FixNullbool(row["Wednesday"]);
            Thursday = Common.FixNullbool(row["Thursday"]);
            Friday = Common.FixNullbool(row["Friday"]);
            Saturday = Common.FixNullbool(row["Saturday"]);
            Sunday = Common.FixNullbool(row["Sunday"]);
            Months = 0;
            if (Common.FixNullbool(row["January"]))
            {
                Months = Months | Month.January;
            }
            if (Common.FixNullbool(row["February"]))
            {
                Months = Months | Month.February;
            }
            if (Common.FixNullbool(row["March"]))
            {
                Months = Months | Month.March;
            }
            if (Common.FixNullbool(row["April"]))
            {
                Months = Months | Month.April;
            }
            if (Common.FixNullbool(row["May"]))
            {
                Months = Months | Month.May;
            }
            if (Common.FixNullbool(row["June"]))
            {
                Months = Months | Month.June;
            }
            if (Common.FixNullbool(row["July"]))
            {
                Months = Months | Month.July;
            }
            if (Common.FixNullbool(row["August"]))
            {
                Months = Months | Month.August;
            }
            if (Common.FixNullbool(row["September"]))
            {
                Months = Months | Month.September;
            }
            if (Common.FixNullbool(row["October"]))
            {
                Months = Months | Month.October;
            }
            if (Common.FixNullbool(row["November"]))
            {
                Months = Months | Month.November;
            }
            if (Common.FixNullbool(row["December"]))
            {
                Months = Months | Month.December;
            }
            DateTime.TryParse(Common.FixNullstring(row["StartDate"]), out _startDate);
            DateTime.TryParse(Common.FixNullstring(row["EndDate"]), out _endDate);
            
            FilePathToCheck = Common.FixNullstring(row["FilePathToCheck"]);
            CheckSubFolders = Common.FixNullbool(row["CheckSubFolders"]);
       
            SendEmailOnFailure = Common.FixNullbool(row["SendEmailOnFailure"]);
            SendEmailOnSuccess = Common.FixNullbool(row["SendEmailOnSuccess"]);
            ExportCSVPath = Common.FixNullstring(row["ExportCSVPath"]);
            ExportUnknownToCSV = Common.FixNullbool(row["ExportUnknownToCSV"]);
            ExportUnVerifiedToCSV = Common.FixNullbool(row["ExportUnVerifiedToCSV"]);
            ExportVerifiedToCSV = Common.FixNullbool(row["ExportVerifiedToCSV"]);
            ExcludeFolders = Common.FixNullstring(row["ExcludeFolders"]);
            Comment = Common.FixNullstring(row["Comment"]);
            DetailedLogging = Common.FixNullbool(row["DetailedLogging"]);

        }

        /// <summary>
        /// Compare Class Destructor
        /// </summary>
        ~AuditFolder()
        {
            try
            {
                if (FilesVerified != null)
                {
                    if (FilesVerified.Count > 0)
                    {
                        FilesVerified.Clear();
                    }
                }
                FilesVerified = null;
                if (FilesUnVerified != null)
                {
                    if (FilesUnVerified.Count > 0)
                    {
                        FilesUnVerified.Clear();
                    }
                }
                FilesUnVerified = null;


                if (FilesUnknown != null)
                {
                    if (FilesUnknown.Count > 0)
                    {
                        FilesUnknown.Clear();
                    }
                }
                FilesUnknown = null;

                _evt = null;
            }
            catch (Exception)
            {   
            }
           
        }


        /// <summary>
        /// Initializes the Compare config table
        /// </summary>
        /// <returns></returns>
        public static DataTable init_dtConfig()
        {
            DataTable dtAuditConfig;
            dtAuditConfig = new DataTable("AuditConfig");
            

            //Create Primary Key Column
            DataColumn dcID = new DataColumn("ID", typeof(Int32));
            dcID.AllowDBNull = false;
            dcID.Unique = true;
            dcID.AutoIncrement = true;
            dcID.AutoIncrementSeed = 1;
            dcID.AutoIncrementStep = 1;

            //Assign Primary Key
            DataColumn[] columns = new DataColumn[1];
            dtAuditConfig.Columns.Add(dcID);
            columns[0] = dtAuditConfig.Columns["ID"];
            dtAuditConfig.PrimaryKey = columns;


            //Create Columns
            dtAuditConfig.Columns.Add(new DataColumn("Enabled", typeof(String)));
            dtAuditConfig.Columns.Add(new DataColumn("Title", typeof(String)));
            dtAuditConfig.Columns.Add(new DataColumn("Time", typeof(String)));
            dtAuditConfig.Columns.Add(new DataColumn("EndTime", typeof(String)));
            dtAuditConfig.Columns.Add(new DataColumn("IntervalType", typeof(String)));
            dtAuditConfig.Columns.Add(new DataColumn("Interval", typeof(String)));
            dtAuditConfig.Columns.Add(new DataColumn("Monday", typeof(String)));
            dtAuditConfig.Columns.Add(new DataColumn("Tuesday", typeof(String)));
            dtAuditConfig.Columns.Add(new DataColumn("Wednesday", typeof(String)));
            dtAuditConfig.Columns.Add(new DataColumn("Thursday", typeof(String)));
            dtAuditConfig.Columns.Add(new DataColumn("Friday", typeof(String)));
            dtAuditConfig.Columns.Add(new DataColumn("Saturday", typeof(String)));
            dtAuditConfig.Columns.Add(new DataColumn("Sunday", typeof(String)));
            dtAuditConfig.Columns.Add(new DataColumn("January", typeof(String)));
            dtAuditConfig.Columns.Add(new DataColumn("February", typeof(String)));
            dtAuditConfig.Columns.Add(new DataColumn("March", typeof(String)));
            dtAuditConfig.Columns.Add(new DataColumn("April", typeof(String)));
            dtAuditConfig.Columns.Add(new DataColumn("May", typeof(String)));
            dtAuditConfig.Columns.Add(new DataColumn("June", typeof(String)));
            dtAuditConfig.Columns.Add(new DataColumn("July", typeof(String)));
            dtAuditConfig.Columns.Add(new DataColumn("August", typeof(String)));
            dtAuditConfig.Columns.Add(new DataColumn("September", typeof(String)));
            dtAuditConfig.Columns.Add(new DataColumn("October", typeof(String)));
            dtAuditConfig.Columns.Add(new DataColumn("November", typeof(String)));
            dtAuditConfig.Columns.Add(new DataColumn("December", typeof(String)));
            dtAuditConfig.Columns.Add(new DataColumn("DayOfMonth", typeof(String)));
            dtAuditConfig.Columns.Add(new DataColumn("FilePathToCheck", typeof(String)));
            dtAuditConfig.Columns.Add(new DataColumn("CheckSubFolders", typeof(String)));
            dtAuditConfig.Columns.Add(new DataColumn("SendEmailOnFailure", typeof(String)));
            dtAuditConfig.Columns.Add(new DataColumn("SendEmailOnSuccess", typeof(String)));
            dtAuditConfig.Columns.Add(new DataColumn("ExportCSVPath", typeof(String)));
            dtAuditConfig.Columns.Add(new DataColumn("ExportUnVerifiedToCSV", typeof(String)));
            dtAuditConfig.Columns.Add(new DataColumn("ExportVerifiedToCSV", typeof(String)));
            dtAuditConfig.Columns.Add(new DataColumn("ExportUnknownToCSV", typeof(String)));
            dtAuditConfig.Columns.Add(new DataColumn("ExcludeFolders", typeof(String)));
            dtAuditConfig.Columns.Add(new DataColumn("StartDate", typeof(String)));
            dtAuditConfig.Columns.Add(new DataColumn("EndDate", typeof(String)));
            
            dtAuditConfig.Columns.Add(new DataColumn("Comment", typeof(String)));
            dtAuditConfig.Columns.Add(new DataColumn("DetailedLogging", typeof(String)));

            dtAuditConfig.Columns["Enabled"].DefaultValue = "true";
            dtAuditConfig.Columns["Time"].DefaultValue = "01:00";
            dtAuditConfig.Columns["IntervalType"].DefaultValue = "Daily";
            dtAuditConfig.Columns["Interval"].DefaultValue = "0";
            dtAuditConfig.Columns["Monday"].DefaultValue = "true";
            dtAuditConfig.Columns["Tuesday"].DefaultValue = "true";
            dtAuditConfig.Columns["Wednesday"].DefaultValue = "true";
            dtAuditConfig.Columns["Thursday"].DefaultValue = "true";
            dtAuditConfig.Columns["Friday"].DefaultValue = "true";
            dtAuditConfig.Columns["Saturday"].DefaultValue = "true";
            dtAuditConfig.Columns["Sunday"].DefaultValue = "true";
            dtAuditConfig.Columns["January"].DefaultValue = "true";
            dtAuditConfig.Columns["February"].DefaultValue = "true";
            dtAuditConfig.Columns["March"].DefaultValue = "true";
            dtAuditConfig.Columns["April"].DefaultValue = "true";
            dtAuditConfig.Columns["May"].DefaultValue = "true";
            dtAuditConfig.Columns["June"].DefaultValue = "true";
            dtAuditConfig.Columns["July"].DefaultValue = "true";
            dtAuditConfig.Columns["August"].DefaultValue = "true";
            dtAuditConfig.Columns["September"].DefaultValue = "true";
            dtAuditConfig.Columns["October"].DefaultValue = "true";
            dtAuditConfig.Columns["November"].DefaultValue = "true";
            dtAuditConfig.Columns["December"].DefaultValue = "true";
            dtAuditConfig.Columns["DayOfMonth"].DefaultValue = "0";
            dtAuditConfig.Columns["FilePathToCheck"].DefaultValue = "";
            dtAuditConfig.Columns["CheckSubFolders"].DefaultValue = "true";
            dtAuditConfig.Columns["Comment"].DefaultValue = "";
            dtAuditConfig.Columns["SendEmailOnFailure"].DefaultValue = "false";
            dtAuditConfig.Columns["SendEmailOnSuccess"].DefaultValue = "false";
            dtAuditConfig.Columns["DetailedLogging"].DefaultValue = "false";
            dtAuditConfig.Columns["ExcludeFolders"].DefaultValue = "";
            dtAuditConfig.Columns["ExportCSVPath"].DefaultValue = "";
            dtAuditConfig.Columns["ExportUnVerifiedToCSV"].DefaultValue = "true";
            dtAuditConfig.Columns["ExportVerifiedToCSV"].DefaultValue = "true";
            dtAuditConfig.Columns["ExportUnknownToCSV"].DefaultValue = "true";
            dtAuditConfig.Columns["StartDate"].DefaultValue = DateTime.Now.ToString("d");
            return dtAuditConfig;

        }


        /// <summary>
        /// Executes Compare of all files extensions vs file headers
        /// </summary>
        public void Execute(ref bool blShuttingDown)
        {
            
            try
            {
                if (Enabled)
                {
                    
                    WriteError("Ransomware Detection Service, File Audit Process: Started " + FilePathToCheck, System.Diagnostics.EventLogEntryType.Information, 7000, 70, true);

                    FilesVerified = new System.Collections.Generic.List<Delimon.Win32.IO.FileInfo>();
                    FilesUnVerified = new System.Collections.Generic.List<Delimon.Win32.IO.FileInfo>();
                    FilesUnknown = new System.Collections.Generic.List<Delimon.Win32.IO.FileInfo>();


                    try
                    {
                        ContentDetectorLib.ContentDetectorEngine cengine = new ContentDetectorLib.ContentDetectorEngine();
                        Delimon.Win32.IO.DirectoryInfo dFilePathToCheck = new Delimon.Win32.IO.DirectoryInfo(FilePathToCheck);
                        cengine.ContainsFolderVerifyContent(dFilePathToCheck, CheckSubFolders, ref FilesVerified, ref FilesUnVerified, ref FilesUnknown, ref blShuttingDown, ExcludeFolders);
                    }
                    catch (Exception ex)
                    {
                        WriteError(ex.Message + " source: " + ex.Source + " Stacktrace: " + ex.StackTrace, System.Diagnostics.EventLogEntryType.Error, 7000, 70, false);
                        
                    }

                    try
                    {

                    
                        if (Common.FixNullstring(ExportUnVerifiedToCSV).Trim() != "" && Common.DirectoryExists(ExportCSVPath))
                        {
                            ExportCSVPath = Common.WindowsPathClean(ExportCSVPath);
                        
                            if (ExportUnVerifiedToCSV)
                            {
                                StringBuilder sbUnVerified = new StringBuilder();
                                sbUnVerified.AppendLine("\"FullName\",\"Name\",\"Extension\",\"Created\",\"Modified\",\"Size\",\"Owner\"");
                                foreach (Delimon.Win32.IO.FileInfo unVerifiedFile in FilesUnVerified)
                                {
                                    string strline = "\"" + unVerifiedFile.FullName + "\",\"" + unVerifiedFile.Name + "\",\"" + unVerifiedFile.Extension + "\",\"" + unVerifiedFile.CreationTime.ToString("G") + "\",\"" + unVerifiedFile.LastWriteTime.ToString("G") + "\",\"" + unVerifiedFile.Length.ToString() + "\",\"" + LongPathFileSearch.GetFileOwner(unVerifiedFile.FullName) + "\"";
                                    sbUnVerified.AppendLine(strline);
                                }
                                if (Common.FileExists(ExportCSVPath + "\\UnVerifiedFiles.csv"))
                                {
                                    Delimon.Win32.IO.File.Delete(ExportCSVPath + "\\UnVerifiedFiles.csv");
                                }
                                Delimon.Win32.IO.File.WriteAllText(ExportCSVPath + "\\UnVerifiedFiles.csv", sbUnVerified.ToString());
                                sbUnVerified.Clear();
                            }

                            if (ExportVerifiedToCSV)
                            {
                                StringBuilder sbVerified = new StringBuilder();
                                sbVerified.AppendLine("\"FullName\",\"Name\",\"Extension\",\"Created\",\"Modified\",\"Size\",\"Owner\"");
                                foreach (Delimon.Win32.IO.FileInfo VerifiedFile in FilesVerified)
                                {

                                    string strline = "\"" + VerifiedFile.FullName + "\",\"" + VerifiedFile.Name + "\",\"" + VerifiedFile.Extension + "\",\"" + VerifiedFile.CreationTime.ToString("G") + "\",\"" + VerifiedFile.LastWriteTime.ToString("G") + "\",\"" + VerifiedFile.Length.ToString() + "\",\"" + LongPathFileSearch.GetFileOwner(VerifiedFile.FullName) + "\"";
                                    sbVerified.AppendLine(strline);
                                }
                                if (Common.FileExists(ExportCSVPath + "\\VerifiedFiles.csv"))
                                {
                                    Delimon.Win32.IO.File.Delete(ExportCSVPath + "\\VerifiedFiles.csv");
                                }
                                Delimon.Win32.IO.File.WriteAllText(ExportCSVPath + "\\VerifiedFiles.csv", sbVerified.ToString());
                                sbVerified.Clear();
                            }

                            if (ExportUnknownToCSV)
                            {
                                StringBuilder sbUnknown = new StringBuilder();
                                sbUnknown.AppendLine("\"FullName\",\"Name\",\"Extension\",\"Created\",\"Modified\",\"Size\",\"Owner\"");
                                foreach (Delimon.Win32.IO.FileInfo unknownFile in FilesUnknown)
                                {
                                    string strline = "\"" + unknownFile.FullName + "\",\"" + unknownFile.Name + "\",\"" + unknownFile.Extension + "\",\"" + unknownFile.CreationTime.ToString("G") + "\",\"" + unknownFile.LastWriteTime.ToString("G") + "\",\"" + unknownFile.Length.ToString() + "\",\"" + LongPathFileSearch.GetFileOwner(unknownFile.FullName) + "\"";
                                    sbUnknown.AppendLine(strline);
                                }
                                if (Common.FileExists(ExportCSVPath + "\\UnknownFiles.csv"))
                                {
                                    Delimon.Win32.IO.File.Delete(ExportCSVPath + "\\UnknownFiles.csv");
                                }
                                Delimon.Win32.IO.File.WriteAllText(ExportCSVPath + "\\UnknownFiles.csv", sbUnknown.ToString());
                                sbUnknown.Clear();
                            }
                        
                        }
                    }
                    catch (Exception ex)
                    {
                        WriteError(ex.Message + " source: " + ex.Source + " Stacktrace: " + ex.StackTrace, System.Diagnostics.EventLogEntryType.Error, 7000, 70, false);
                    }
                   

                    //Send Summary Email 
                    if (SendEmailOnFailure || SendEmailOnSuccess)
                    {
                        string strBody = "";
                        string strSubject = "";
                   

                        //send email on failure
                        if (FilesUnVerified.Count > 0 )
                        {
                        
                            StringBuilder sbbody1 = new StringBuilder();
                            string strline = "";
                            strSubject = "Ransomware Detection Service, File Audit - File Extensions Not Matching Content Header!: " + FilesUnVerified.Count.ToString();
                            sbbody1.AppendLine("Ransomware Detection Service, Possible Ransomware Affected Files Found<br />\r\n<br />");
                        
                            strline = "<li>FilePathToCheck: " + Common.GetPathToHTMLAnchor(FilePathToCheck) + "</li>";
                            sbbody1.AppendLine(strline);
                            strline = "<li>Check Sub Folders: " + CheckSubFolders.ToString()+ "</li>";
                            sbbody1.AppendLine(strline);
                            strline = "<li>ExportCSVPath: " + Common.GetPathToHTMLAnchor(ExportCSVPath) + "</li>";
                            sbbody1.AppendLine(strline);
                        
                            
                            if (FilesUnVerified.Count > 0)
                            {
                                sbbody1.AppendLine("<br /><br />\r\n<strong>Files UnVerified:</strong><br />");
                                //Loop through files that are different and list them
                                foreach (Delimon.Win32.IO.FileInfo unVerifiedFile in FilesUnVerified)
                                {
                                    sbbody1.AppendLine("<a href=\"#\" style=\"text-decoration:none !important; text-decoration:none;color:black;\">\"" + unVerifiedFile.FullName + "\"</a><br />"); 
                                }
                                sbbody1.AppendLine("<br />");
                            }
                        
                     
                        
                            strBody = sbbody1.ToString();
                            sbbody1.Clear();
                            if (SendEmailOnFailure)
                            {
                                Send_Email(strSubject, strBody);
                            }
                            if (strBody.Length > 32765)
                            {
                                strBody = strBody.Substring(0, 32760) + " ...";
                            }
                        
                            WriteError(strBody, System.Diagnostics.EventLogEntryType.Error, 7000, 70, false);

                        }
                        else
                        {
                            StringBuilder sbbody2 = new StringBuilder();
                            strBody = "";
                            strSubject = "Ransomware Detection Service, File Audit: Success!";
                        
                            sbbody2.AppendLine("Ransomware Detection Service, File Audit: Success! - All Files from FilePathToCheck file extensions match the file header Files Verified:" + FilesVerified.Count.ToString() + "<br /><br />");
                            sbbody2.AppendLine("<br /><strong>FilePathToCheck:</strong> " + FilePathToCheck);
                            sbbody2.AppendLine("<br />Check Sub Folders: " + CheckSubFolders.ToString());
                            strBody = sbbody2.ToString();
                            sbbody2.Clear();
                            //send email on success
                            if (SendEmailOnSuccess)
                            {
                                Send_Email(strSubject, strBody);
                            }
                        
                            WriteError(strBody, System.Diagnostics.EventLogEntryType.Information, 7000, 70, true);
                        }
                    

                    }
                    WriteError("Ransomware Detection Service, File Audit Process: Finished " + FilePathToCheck, System.Diagnostics.EventLogEntryType.Information, 7000, 70, true);
                    }
                
            }
            catch (Exception ex)
            {
                WriteError(ex.Message + " source: " + ex.Source + " Stacktrace: " + ex.StackTrace, System.Diagnostics.EventLogEntryType.Error, 7000, 70, false);
            }
            


        }

        /// <summary>
        /// Write Event Log Error
        /// </summary>
        /// <param name="strErrorMessage"></param>
        /// <param name="entrytype"></param>
        /// <param name="eventid"></param>
        /// <param name="category"></param>
        /// <param name="blIsDetailedLoggingError"></param>
        private void WriteError(string strErrorMessage, System.Diagnostics.EventLogEntryType entrytype, int eventid, short category, bool blIsDetailedLoggingError)
        {
            //multi threaded so _evt sometimes is not allocated. 
            if (_evt == null)
            {
                _evt = Common.GetEventLog;
            }
            if (blIsDetailedLoggingError == false)
            {
                _evt.WriteEntry(strErrorMessage, entrytype, eventid, category);
            }
            else
            {
                if (DetailedLogging)
                {
                    _evt.WriteEntry(strErrorMessage, entrytype, eventid, category);
                }
            }

        }

        


        /// <summary>
        /// Send Email
        /// </summary>
        /// <param name="strSubject"></param>
        /// <param name="strBody"></param>
        public void Send_Email(string strSubject, string strBody)
        {

            //Emails regarding restarting the process server
            char[] cdelimiter = { ',' };
            

            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient(_smtpServer);

            mail.From = new MailAddress(_emailFrom);
            //comma delimiter support for email to
            _emailTo = _emailTo.Replace(";", ",");
            string[] ToMultiEmail = _emailTo.Split(cdelimiter, StringSplitOptions.RemoveEmptyEntries);
            
            if (ToMultiEmail != null)
            {
                //add each email address
                foreach (string strEmailTo in ToMultiEmail)
                {
                    mail.To.Add(strEmailTo);
                }
                mail.IsBodyHtml = true;
                mail.Body = strBody;
                mail.Subject = strSubject;

                SmtpServer.Port = Common.FixNullInt32(_smtpPort);
                SmtpServer.UseDefaultCredentials = _smtpUseDefaultCredentials;
                
                //credential support (username/password) for SMTP Server
                if (_smtpPassword != null && _smtpUsername != null)
                {
                    if (_smtpPassword.Length > 0 && _smtpUsername.Length > 0)
                    {
                        AES256 aes = new AES256(ep);

                        string strpw = aes.Decrypt(_smtpPassword);
                        SmtpServer.Credentials = new System.Net.NetworkCredential(_smtpUsername, strpw);
                        strpw = "";
                    }
                }
                SmtpServer.EnableSsl = _smtpUseSSL;
                SmtpServer.Send(mail);
            }

        }
        
        
       
        #endregion
    }
}
