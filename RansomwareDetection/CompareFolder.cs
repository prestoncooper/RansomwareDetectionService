using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.IO;
using System.Data;
using System.Diagnostics;
using System.Threading;
using System.Net.Mail;

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
namespace RansomwareDetection.DetectionLib
{
    public class CompareFolder : IFolderConfig   
    {
        #region "Variables"

        private string ep = "19C235A4-A313-C4C4-48F4-A5B4DC86EBCC";
        /*
        public System.Collections.Generic.List<Delimon.Win32.IO.FileInfo> AllFiles = null;
        public System.Collections.Generic.List<string> FilesDifferent = null;
        public System.Collections.Generic.List<string> FilesMissing = null;
        */
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

       

        public static string _sourcePath = "";
        /// <summary>
        /// Source File Path for files to be compared with (these files are not expected to change)
        /// </summary>
        public string SourcePath
        {
            get
            {
                return _sourcePath;
            }
            set
            {
                _sourcePath = Common.WindowsPathClean(value);
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

        private bool _exportFilesDifferentToCSV = true;
        /// <summary>
        /// Exclude Folders separate folder names by semicolon
        /// </summary>
        public bool ExportFilesDifferentToCSV
        {
            get
            {
                return _exportFilesDifferentToCSV;
            }
            set
            {
                _exportFilesDifferentToCSV = value;
            }

        }

        private bool _exportFilesMissingToCSV = true;
        /// <summary>
        /// Exclude Folders separate folder names by semicolon
        /// </summary>
        public bool ExportFilesMissingToCSV
        {
            get
            {
                return _exportFilesMissingToCSV = true;
                
            }
            set
            {
                _exportFilesMissingToCSV = true;
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

        private bool _checkMainFolder = false;
        /// <summary>
        /// Check the main folder in FilePathToCheck 
        /// </summary>
        public bool CheckMainFolder
        {
            get
            {
                return _checkMainFolder;
            }

            set
            {
                _checkMainFolder = value;
            }
        }

        private bool _copySourceFiles = false;
        /// <summary>
        /// Copy the source files into the main folder of FilePathToCheck
        /// </summary>
        public bool CopySourceFiles
        {
            get
            {
                return _copySourceFiles;
            }

            set
            {
                _copySourceFiles = value;
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

        private bool _copySourceFilesSubFolders = false;
        /// <summary>
        /// Copy SourcePath files into each sub folder of FilePathToCheck
        /// </summary>
        public bool CopySourceFilesSubFolders
        {
            get
            {
                return _copySourceFilesSubFolders;
            }

            set
            {
                _copySourceFilesSubFolders = value;
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
        public CompareFolder()
        {
            _evt = Common.GetEventLog;
        }




        /// <summary>
        /// Compare Contructor
        /// </summary>
        /// <param name="row"></param>
        public CompareFolder(DataRow row)
        {
            _evt = Common.GetEventLog;

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
            SourcePath = Common.FixNullstring(row["SourcePath"]);
            FilePathToCheck = Common.FixNullstring(row["FilePathToCheck"]);
            CheckMainFolder = Common.FixNullbool(row["CheckMainFolder"]);
            CheckSubFolders = Common.FixNullbool(row["CheckSubFolders"]);
            CopySourceFiles = Common.FixNullbool(row["CopySourceFiles"]);
            CopySourceFilesSubFolders = Common.FixNullbool(row["CopySourceFilesSubFolders"]);

            ExportCSVPath = Common.FixNullstring(row["ExportCSVPath"]);
            ExportFilesDifferentToCSV = Common.FixNullbool(row["ExportFilesDifferentToCSV"]);
            ExportFilesMissingToCSV = Common.FixNullbool(row["ExportFilesMissingToCSV"]);

            SendEmailOnFailure = Common.FixNullbool(row["SendEmailOnFailure"]);
            SendEmailOnSuccess = Common.FixNullbool(row["SendEmailOnSuccess"]);
            ExcludeFolders = Common.FixNullstring(row["ExcludeFolders"]);
            Comment = Common.FixNullstring(row["Comment"]);
            DetailedLogging = Common.FixNullbool(row["DetailedLogging"]);

        }

        /// <summary>
        /// Compare Class Destructor
        /// </summary>
        ~CompareFolder()
        {
            try
            {
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
            DataTable dtCompareConfig;
            dtCompareConfig = new DataTable("CompareConfig");
            

            //Create Primary Key Column
            DataColumn dcID = new DataColumn("ID", typeof(Int32));
            dcID.AllowDBNull = false;
            dcID.Unique = true;
            dcID.AutoIncrement = true;
            dcID.AutoIncrementSeed = 1;
            dcID.AutoIncrementStep = 1;

            //Assign Primary Key
            DataColumn[] columns = new DataColumn[1];
            dtCompareConfig.Columns.Add(dcID);
            columns[0] = dtCompareConfig.Columns["ID"];
            dtCompareConfig.PrimaryKey = columns;


            //Create Columns
            dtCompareConfig.Columns.Add(new DataColumn("Enabled", typeof(String)));
            dtCompareConfig.Columns.Add(new DataColumn("Title", typeof(String)));
            dtCompareConfig.Columns.Add(new DataColumn("Time", typeof(String)));
            dtCompareConfig.Columns.Add(new DataColumn("EndTime", typeof(String)));
            dtCompareConfig.Columns.Add(new DataColumn("IntervalType", typeof(String)));
            dtCompareConfig.Columns.Add(new DataColumn("Interval", typeof(String)));
            dtCompareConfig.Columns.Add(new DataColumn("Monday", typeof(String)));
            dtCompareConfig.Columns.Add(new DataColumn("Tuesday", typeof(String)));
            dtCompareConfig.Columns.Add(new DataColumn("Wednesday", typeof(String)));
            dtCompareConfig.Columns.Add(new DataColumn("Thursday", typeof(String)));
            dtCompareConfig.Columns.Add(new DataColumn("Friday", typeof(String)));
            dtCompareConfig.Columns.Add(new DataColumn("Saturday", typeof(String)));
            dtCompareConfig.Columns.Add(new DataColumn("Sunday", typeof(String)));
            dtCompareConfig.Columns.Add(new DataColumn("January", typeof(String)));
            dtCompareConfig.Columns.Add(new DataColumn("February", typeof(String)));
            dtCompareConfig.Columns.Add(new DataColumn("March", typeof(String)));
            dtCompareConfig.Columns.Add(new DataColumn("April", typeof(String)));
            dtCompareConfig.Columns.Add(new DataColumn("May", typeof(String)));
            dtCompareConfig.Columns.Add(new DataColumn("June", typeof(String)));
            dtCompareConfig.Columns.Add(new DataColumn("July", typeof(String)));
            dtCompareConfig.Columns.Add(new DataColumn("August", typeof(String)));
            dtCompareConfig.Columns.Add(new DataColumn("September", typeof(String)));
            dtCompareConfig.Columns.Add(new DataColumn("October", typeof(String)));
            dtCompareConfig.Columns.Add(new DataColumn("November", typeof(String)));
            dtCompareConfig.Columns.Add(new DataColumn("December", typeof(String)));
            dtCompareConfig.Columns.Add(new DataColumn("DayOfMonth", typeof(String)));
            dtCompareConfig.Columns.Add(new DataColumn("SourcePath", typeof(String)));
            dtCompareConfig.Columns.Add(new DataColumn("FilePathToCheck", typeof(String)));
            dtCompareConfig.Columns.Add(new DataColumn("CheckMainFolder", typeof(String)));
            dtCompareConfig.Columns.Add(new DataColumn("CheckSubFolders", typeof(String)));
            dtCompareConfig.Columns.Add(new DataColumn("CopySourceFiles", typeof(String)));
            dtCompareConfig.Columns.Add(new DataColumn("CopySourceFilesSubFolders", typeof(String)));

            dtCompareConfig.Columns.Add(new DataColumn("ExportCSVPath", typeof(String)));
            dtCompareConfig.Columns.Add(new DataColumn("ExportFilesDifferentToCSV", typeof(String)));
            dtCompareConfig.Columns.Add(new DataColumn("ExportFilesMissingToCSV", typeof(String)));
            
            dtCompareConfig.Columns.Add(new DataColumn("SendEmailOnFailure", typeof(String)));
            dtCompareConfig.Columns.Add(new DataColumn("SendEmailOnSuccess", typeof(String)));
            dtCompareConfig.Columns.Add(new DataColumn("ExcludeFolders", typeof(String)));
            dtCompareConfig.Columns.Add(new DataColumn("StartDate", typeof(String)));
            dtCompareConfig.Columns.Add(new DataColumn("EndDate", typeof(String)));
            
            dtCompareConfig.Columns.Add(new DataColumn("Comment", typeof(String)));
            dtCompareConfig.Columns.Add(new DataColumn("DetailedLogging", typeof(String)));

            dtCompareConfig.Columns["Enabled"].DefaultValue = "true";
            dtCompareConfig.Columns["Time"].DefaultValue = "01:00";
            dtCompareConfig.Columns["IntervalType"].DefaultValue = "Daily";
            dtCompareConfig.Columns["Interval"].DefaultValue = "0";
            dtCompareConfig.Columns["Monday"].DefaultValue = "true";
            dtCompareConfig.Columns["Tuesday"].DefaultValue = "true";
            dtCompareConfig.Columns["Wednesday"].DefaultValue = "true";
            dtCompareConfig.Columns["Thursday"].DefaultValue = "true";
            dtCompareConfig.Columns["Friday"].DefaultValue = "true";
            dtCompareConfig.Columns["Saturday"].DefaultValue = "true";
            dtCompareConfig.Columns["Sunday"].DefaultValue = "true";
            dtCompareConfig.Columns["January"].DefaultValue = "true";
            dtCompareConfig.Columns["February"].DefaultValue = "true";
            dtCompareConfig.Columns["March"].DefaultValue = "true";
            dtCompareConfig.Columns["April"].DefaultValue = "true";
            dtCompareConfig.Columns["May"].DefaultValue = "true";
            dtCompareConfig.Columns["June"].DefaultValue = "true";
            dtCompareConfig.Columns["July"].DefaultValue = "true";
            dtCompareConfig.Columns["August"].DefaultValue = "true";
            dtCompareConfig.Columns["September"].DefaultValue = "true";
            dtCompareConfig.Columns["October"].DefaultValue = "true";
            dtCompareConfig.Columns["November"].DefaultValue = "true";
            dtCompareConfig.Columns["December"].DefaultValue = "true";
            dtCompareConfig.Columns["DayOfMonth"].DefaultValue = "0";
            dtCompareConfig.Columns["SourcePath"].DefaultValue = "";
            dtCompareConfig.Columns["FilePathToCheck"].DefaultValue = "";
            dtCompareConfig.Columns["CheckMainFolder"].DefaultValue = "true";
            dtCompareConfig.Columns["CheckSubFolders"].DefaultValue = "true";

            dtCompareConfig.Columns["ExportCSVPath"].DefaultValue = "";
            dtCompareConfig.Columns["ExportFilesDifferentToCSV"].DefaultValue = "true";
            dtCompareConfig.Columns["ExportFilesMissingToCSV"].DefaultValue = "true";

            dtCompareConfig.Columns["Comment"].DefaultValue = "";
            dtCompareConfig.Columns["CopySourceFiles"].DefaultValue = "false";
            dtCompareConfig.Columns["CopySourceFilesSubFolders"].DefaultValue = "false";
            dtCompareConfig.Columns["SendEmailOnFailure"].DefaultValue = "false";
            dtCompareConfig.Columns["SendEmailOnSuccess"].DefaultValue = "false";
            dtCompareConfig.Columns["DetailedLogging"].DefaultValue = "false";
            dtCompareConfig.Columns["ExcludeFolders"].DefaultValue = "";
            dtCompareConfig.Columns["StartDate"].DefaultValue = DateTime.Now.ToString("d");
            return dtCompareConfig;

        }


         /// <summary>
        /// Executes Compare of all files
        /// </summary>
        public void Execute(ref bool blShuttingDown)
        {
            bool blFileSame = false;
            string SourceFile = "";
            string FileToCheck = "";
            List<Delimon.Win32.IO.FileInfo> AllFiles = null;
            List<ContentDetectorLib.FileResult> FilesDifferent = null;
            List<ContentDetectorLib.FileResult> FilesMissing = null;
            try
            {
                if (Enabled)
                {
                    
                    WriteError("Ransomware Detection Service, File Compare Process: Started " + FilePathToCheck, System.Diagnostics.EventLogEntryType.Information, 8000, 80, true);

                    AllFiles = new List<Delimon.Win32.IO.FileInfo>();
                    FilesDifferent = new List<ContentDetectorLib.FileResult>();
                    FilesMissing = new List<ContentDetectorLib.FileResult>();                    

                    AllFiles = Common.WalkDirectory(SourcePath, ref blShuttingDown);

                    if (AllFiles != null)
                    {
                        //loop through all source files and folders
                        foreach (Delimon.Win32.IO.FileInfo file1 in AllFiles)
                        {
                            
                            //if the service is shutting down we need to stop looping through the source files
                            if (blShuttingDown)
                            {
                                throw new Exception("Shutting Down");
                            }
                            try
                            {

                                //compares source files in the main folder
                                SourceFile = file1.FullName;
                                FileToCheck = Common.WindowsPathCombine(FilePathToCheck, file1.FullName, SourcePath);
                                if (Common.FileExists(FileToCheck) == false && CopySourceFiles)    //If file does not exist and CopySourceFiles is true then run this statement
                                {
                                    //File Missing
                                    FilesMissing.Add(new ContentDetectorLib.FileResult(FileToCheck, "Possible Ransomware Change Detected: File Missing SourceFile: " + SourceFile + " FileToCheck: " + FileToCheck));
                                    //Create Source Folder Structure in FilePathToCheck
                                    if (CheckMainFolder)
                                    {
                                        Common.CreateDestinationFolders(SourcePath, FilePathToCheck);
                                    }
                                    //this only copies the file if it does not exist
                                    //We are coping files for detection of changes we do not want.
                                    Delimon.Win32.IO.File.Copy(SourceFile, FileToCheck,false);
                                    string strErr = "FileCompare: File Compare Failed! (Possible Ransomware Change Detected, user delete or new sub folder) FileToCheck File Did Not Exist Yet, File Copied: " + SourceFile + " Different than FileToCheck: " + FileToCheck;
                                    WriteError(strErr, System.Diagnostics.EventLogEntryType.Error, 8003, 80, false);
                                }
                                else
                                {
                                    //If the file exists then we can compare
                                    if (Common.FileExists(FileToCheck) && CheckMainFolder)
                                    {
                                        //compare file
                                        blFileSame = Compare_Files(SourceFile, FileToCheck);
                                        if (blFileSame == false)
                                        {
                                            //Add file to File Different list for emailing later!
                                            FilesDifferent.Add(new ContentDetectorLib.FileResult(FileToCheck, "File Changed SourceFile: " + SourceFile + " Different than FileToCheck"));

                                            string strErr = "FileCompare: File Compare Failed! (Possible Ransomware Change Detected) SourceFile: " + SourceFile + " Different than FileToCheck: " + FileToCheck;
                                            WriteError(strErr, System.Diagnostics.EventLogEntryType.Error, 8002, 80, false);
                                        }
                                        else
                                        {
                                            string strErr = "FileCompare: File Compare Succeeded SourceFile: " + SourceFile + " Same as FileToCheck: " + FileToCheck;   
                                            WriteError(strErr, System.Diagnostics.EventLogEntryType.Information, 8001, 80, true);
                                        }
                                    }
                                    else
                                    {
                                       
                                        if (CheckMainFolder)//(CopySourceFiles == false && CopySourceFilesSubFolders == true )  
                                        {
                                            FilesMissing.Add(new ContentDetectorLib.FileResult(FileToCheck, "Possible Ransomware Change Detected: FileToCheck Missing: user delete or new sub folder Sourcefile: " + SourceFile));

                                            string strErr = "FileCompare: File Compare Failed! (Possible Ransomware Change Detected, user delete or new sub folder) FileToCheck File Did Not Exist: " + SourceFile + " Different than FileToCheck: " + FileToCheck;
                                            WriteError(strErr, System.Diagnostics.EventLogEntryType.Error, 8004, 80, false);
                                        }
                                    }
                                }


                                bool blIgnoreSourceDirectory = false;

                                //Loops through the first layer of subfolders and compares current source file in each sub folder of FilePathToCheck
                                if (CheckSubFolders && Common.DirectoryExists(FilePathToCheck))
                                {
                                    System.Collections.Generic.List<Delimon.Win32.IO.DirectoryInfo> dirs;
                                    Delimon.Win32.IO.DirectoryInfo dirToCheck = new Delimon.Win32.IO.DirectoryInfo(FilePathToCheck);
                                    dirs = Common.GetDirsInDirectory(dirToCheck);

                                    foreach (Delimon.Win32.IO.DirectoryInfo dir1 in dirs)
                                    {
                                        
                                        blIgnoreSourceDirectory = false;

                                        

                                        //If the service is shutting down we need to stop traversing the folders
                                        if (blShuttingDown)
                                        {
                                            throw new Exception("Ransomware Detection Service Shutting Down");
                                        }
                                        try
                                        {
                                            try
                                            {
                                                if (CheckSubFolders)
                                                {
                                                    //Check if SourceFolder is one of the sub folders.  (would create extra source directories inside a source directory if not ignored)
                                                    if (CheckMainFolder && CheckSubFolders)
                                                    {
                                                        System.Collections.Generic.List<Delimon.Win32.IO.DirectoryInfo> Sourcedirs;
                                                        Delimon.Win32.IO.DirectoryInfo dirSourceToCheck = new Delimon.Win32.IO.DirectoryInfo(SourcePath);
                                                        Sourcedirs = Common.GetDirsInDirectory(dirSourceToCheck);
                                                        foreach (Delimon.Win32.IO.DirectoryInfo sdir1 in Sourcedirs)
                                                        {
                                                        
                                                                if (sdir1.Name == dir1.Name)
                                                                {
                                                                    blIgnoreSourceDirectory = true;
                                                                }
                                                        
                                                        }
                                                        Sourcedirs.Clear();
                                                    }

                                                    //Check if excluded folder is the current folder being checked.
                                                    char[] delimiters = new char[] { ';' };
                                                    string[] strArr_excludedfolders = ExcludeFolders.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
                                                    
                                                    if (blIgnoreSourceDirectory == false && !(strArr_excludedfolders == null || strArr_excludedfolders.Length == 0))
                                                    {
                                                        //loop through excluded folders
                                                        foreach (string strExclude in strArr_excludedfolders)
                                                        {
                                                            if (dir1.Name.ToLower() == strExclude.ToLower())
                                                            {
                                                                blIgnoreSourceDirectory = true;
                                                            }
                                                        }
                                                    }
                                                    if (blIgnoreSourceDirectory)
                                                    {
                                                        continue;
                                                    }
                                                }
                                            }
                                            catch (Exception ex)
                                            {
                                                string strErr = ex.Message + ": " + ex.Source + " Sourcedirs  " + ex.StackTrace;
                                                WriteError(strErr, System.Diagnostics.EventLogEntryType.Error, 8000, 80, false);
                                            }
                                            
                                            

                                            //Check sub folders for contents of sourcepath
                                            SourceFile = file1.FullName;
                                            FileToCheck = Common.WindowsPathCombine(dir1.FullName, file1.FullName, SourcePath);
                                            //Copy to Sub Folders and check the files too
                                            if (CopySourceFilesSubFolders)
                                            {
                                                if (Common.FileExists(FileToCheck) == false)  //file is missing
                                                {
                                                    FilesMissing.Add(new ContentDetectorLib.FileResult(FileToCheck, "File Missing FileToCheck when compared with SourceFile: " + SourceFile));
                                                    //Create Source Folder Structure in each immediate subfolder in FilePathToCheck
                                                    Common.CreateDestinationFolders(SourcePath, dir1.FullName);
                                                    //Only copy the file to a 1st layer sub folder if it does not exist.  (We want to keep source files there for checking of changes)
                                                    Delimon.Win32.IO.File.Copy(SourceFile, FileToCheck,false);
                                                    
                                                    string strErr = "FileCompare: File Compare Failed! FileToCheck in subfolder, File Did Not Exist! (Possible Ransomware, unless user deleted) and File Copied: " + SourceFile + " Different than FileToCheck: " + FileToCheck;
                                                    WriteError(strErr, System.Diagnostics.EventLogEntryType.Error, 8003, 80, false);
                                                }
                                                else  //file exists, contents need to be checked
                                                {
                                                    //compare source file in this subfolder
                                                    blFileSame = Compare_Files(SourceFile, FileToCheck);
                                                    if (blFileSame == false)
                                                    {
                                                        //Add file to list for emailing later!
                                                        FilesDifferent.Add(new ContentDetectorLib.FileResult(FileToCheck, "File Different FilePathToCheck from SourceFile: " + SourceFile));
                                                        
                                                        string strErr = "FileCompare: File Compare Failed! (Possible Ransomware Change Detected) SourceFile: " + SourceFile + " Different than FileToCheck: " + FileToCheck;
                                                        WriteError(strErr, System.Diagnostics.EventLogEntryType.Error, 8002, 80, false);
                                                    }
                                                    else    //File is the same
                                                    {
                                                        string strErr = "FileCompare: File Compare Succeeded SourceFile: " + SourceFile + " Same as FileToCheck: " + FileToCheck;
                                                        WriteError(strErr, System.Diagnostics.EventLogEntryType.Information, 8001, 80, true);
                                                    }
                                                }
                                            }
                                            else       //Do Not Copy Sub folders, but check files
                                            {
                                                if (Common.FileExists(FileToCheck) == false)  //file is missing
                                                {
                                                    FilesMissing.Add(new ContentDetectorLib.FileResult(FileToCheck, "File Missing from FilePathToCheck: SourceFile: " + SourceFile));
                                                    
                                                    string strErr = "FileCompare: File Compare Failed! FileToCheck in subfolder, File Did Not Exist!: " + SourceFile + " Different than FileToCheck: " + FileToCheck;
                                                    WriteError(strErr, System.Diagnostics.EventLogEntryType.Error, 8003, 80, false);
                                                }
                                                else  //file exists, check contents of the file
                                                {
                                                    //compare source file in this subfolder
                                                    blFileSame = Compare_Files(SourceFile, FileToCheck);
                                                    if (blFileSame == false)
                                                    {
                                                        //Add file to list for emailing later!
                                                        FilesDifferent.Add(new ContentDetectorLib.FileResult(FileToCheck, "SourceFile: " + SourceFile + " Different than FileToCheck"));
                                                        
                                                        string strErr = "FileCompare: File Compare Failed! SourceFile: " + SourceFile + " Different than FileToCheck: " + FileToCheck;
                                                        WriteError(strErr, System.Diagnostics.EventLogEntryType.Error, 8002, 80, false);
                                                    }
                                                    else    //file contents are the same
                                                    {
                                                        string strErr = "FileCompare: File Compare Succeeded SourceFile: " + SourceFile + " Same as FileToCheck: " + FileToCheck;
                                                        WriteError(strErr, System.Diagnostics.EventLogEntryType.Information, 8001, 80, true);
                                                    }
                                                }
                                            }
                                        }
                                        catch (Exception ex)
                                        {
                                            string strErr = ex.Message + ": " + ex.Source + " foreach sub folder  " + ex.StackTrace;
                                            WriteError(strErr, System.Diagnostics.EventLogEntryType.Error, 8000, 80, false);
                                            
                                        }
                                        
                                        
                                    } //end for each loop sub folders check/copy

                                    dirs.Clear();

                                }
                                else
                                {
                                    //Directory does not exist error and copy files is not checked
                                    if (Common.DirectoryExists(FilePathToCheck) == false)
                                    {
                                        FilesMissing.Add(new ContentDetectorLib.FileResult(FilePathToCheck, "File Compare Failed Directory for FilePathToCheck Does not Exist"));
                                        //ContentDetectorLib.FileResult result = new ContentDetectorLib.FileResult(FilePathToCheck, "FileCompare: File Compare Failed Directory for FilePathToCheck Does not Exist: " + FilePathToCheck);

                                        string strErr = "FileCompare: File Compare Failed Directory for FilePathToCheck Does not Exist: " + FilePathToCheck;
                                        WriteError(strErr, System.Diagnostics.EventLogEntryType.Error, 8007, 80, false);
                                    }
                                }



                            }
                            catch (Exception ex1)      //unknown error
                            {

                                string strErr = ex1.Message + ": " + ex1.Source + " inside main for each  " + ex1.StackTrace;
                                WriteError(strErr, System.Diagnostics.EventLogEntryType.Error, 8000, 80, false);
                            }
                        }
                        //end loop
                    }



                    if (Common.FixNullstring(ExportCSVPath).Trim() != "" && Common.DirectoryExists(ExportCSVPath))
                    {
                        ExportCSVPath = Common.WindowsPathClean(ExportCSVPath);

                        if (ExportFilesDifferentToCSV)
                        {
                            try
                            {
                                if (Common.FileExists(ExportCSVPath + "\\" + Title + "DifferentFiles.csv"))
                                {
                                    Delimon.Win32.IO.File.Delete(ExportCSVPath + "\\" + Title + "DifferentFiles.csv");
                                }
                                Delimon.Win32.IO.File.WriteAllText(ExportCSVPath + "\\" + Title + "DifferentFiles.csv", ContentDetectorLib.FileResult.FileResultCollectionToCSV(FilesDifferent));
                            }
                            catch (Exception)
                            {
                                Delimon.Win32.IO.File.WriteAllText(ExportCSVPath + "\\" + Title + "DifferentFiles" + Guid.NewGuid().ToString() + ".csv", ContentDetectorLib.FileResult.FileResultCollectionToCSV(FilesDifferent));

                            }
                        }

                        if (ExportFilesMissingToCSV)
                        {
                            try
                            {
                                if (Common.FileExists(ExportCSVPath + "\\" + Title + "MissingFiles.csv"))
                                {
                                    Delimon.Win32.IO.File.Delete(ExportCSVPath + "\\" + Title + "MissingFiles.csv");
                                }
                                Delimon.Win32.IO.File.WriteAllText(ExportCSVPath + "\\" + Title + "MissingFiles.csv", ContentDetectorLib.FileResult.FileResultCollectionToCSV(FilesMissing));
                            }
                            catch (Exception)
                            {
                                Delimon.Win32.IO.File.WriteAllText(ExportCSVPath + "\\" + Title + "MissingFiles" + Guid.NewGuid().ToString() + ".csv", ContentDetectorLib.FileResult.FileResultCollectionToCSV(FilesMissing));

                            }
                        }

                    }

                    //Send Summary Email 
                    if (SendEmailOnFailure || SendEmailOnSuccess)
                    {
                        string strBody = "";
                        string strSubject = "";


                        //send email on failure
                        if ((FilesDifferent.Count > 0 || FilesMissing.Count > 0))
                        {

                            StringBuilder sbbody1 = new StringBuilder();
                            string strline = "";
                            strSubject = "Ransomware Detection Service, File Compare - Files Different!: " + FilesDifferent.Count.ToString() + " Files Missing: " + FilesMissing.Count.ToString();
                            sbbody1.AppendLine(@"Ransomware Detection Service, Possible Ransomware Found<br /><br />");
                            sbbody1.AppendLine(@"<br /><ul><li>SourcePath: " + SourcePath + @"</li>");
                            strline = @"<li>FilePathToCheck: " + Common.GetPathToHTMLAnchor(FilePathToCheck) + @"</li>";
                            sbbody1.AppendLine(strline);
                            strline = @"<li>Check Sub Folders: " + CheckSubFolders.ToString() + @"</li>";
                            sbbody1.AppendLine(strline);
                            strline = @"<li>Check MainFolder Folder: " + CheckMainFolder.ToString() + @"</li></ul>";
                            sbbody1.AppendLine(strline);
                            if (FilesDifferent.Count > 0 && FilesDifferent.Count < 200)
                            {
                                sbbody1.AppendLine(@"<br /><br /><strong>Files Different:</strong><br />");
                                //Loop through files that are different and list them
                                foreach (ContentDetectorLib.FileResult fsFileDiff in FilesDifferent)
                                {
                                    sbbody1.AppendLine("<a href=\"#\" style=\"text-decoration:none !important; text-decoration:none;color:black;\">\"" + fsFileDiff.FullPath + "\"" + @"</a><br />");
                                }
                                sbbody1.AppendLine(@"<br />");
                            }
                            if (FilesMissing.Count > 0 && FilesMissing.Count < 200)
                            {
                                sbbody1.Append(@"<br /><br /><strong>Files Missing:</strong><br />");
                                //Loop through the files that are missing and list them
                                foreach (ContentDetectorLib.FileResult fsFileMissing in FilesMissing)
                                {
                                    sbbody1.AppendLine("<a href=\"#\" style=\"text-decoration:none !important; text-decoration:none;color:black;\">\"" + fsFileMissing.FullPath + "\"" + @"</a><br />");
                                }
                            }


                            strBody = sbbody1.ToString();
                            sbbody1.Clear();
                            if (SendEmailOnFailure)
                            {
                                Send_Email(strSubject, strBody);
                            }
                            

                            WriteError(strBody, System.Diagnostics.EventLogEntryType.Error, 8000, 80, false);

                        }
                        else
                        {
                            StringBuilder sbbody2 = new StringBuilder();
                            strBody = "";
                            strSubject = "Ransomware Detection Service, File Compare: Success! (all files are the same and exist)";
                            sbbody2.AppendLine(@"Ransomware Detection Service, File Compare: Success! - All Files from SourcePath Files are the Same on FilePathToCheck Files Different: " + FilesDifferent.Count.ToString() + " Files Missing: " + FilesMissing.Count.ToString() + @"<br /><br />");
                            sbbody2.AppendLine("SourcePath: " + SourcePath);
                            sbbody2.AppendLine(@"<br /><strong>FilePathToCheck:</strong> " + FilePathToCheck);
                            sbbody2.AppendLine(@"<br />Check MainFolder Folder: " + CheckMainFolder.ToString());
                            sbbody2.AppendLine(@"<br />Check Sub Folders: " + CheckSubFolders.ToString());
                            strBody = sbbody2.ToString();
                            sbbody2.Clear();
                            //send email on success
                            if (SendEmailOnSuccess)
                            {
                                Send_Email(strSubject, strBody);
                            }

                            WriteError(strBody, System.Diagnostics.EventLogEntryType.Information, 8000, 80, true);
                        }


                    }
                    WriteError("Ransomware Detection Service, File Compare Process: Finished " + FilePathToCheck, System.Diagnostics.EventLogEntryType.Information, 8000, 80, true);

                } //end enabled if
                

                
                
            }
            catch (Exception ex)
            {
                WriteError(ex.Message + " source: " + ex.Source + " Stacktrace: " + ex.StackTrace, System.Diagnostics.EventLogEntryType.Error, 8000, 80, false);
            }
            finally
            {
                try
                {
                    if (AllFiles != null)
                    {
                        if (AllFiles.Count > 0)
                        {
                            AllFiles.Clear();
                        }
                    }
                    if (FilesDifferent != null)
                    {
                        if (FilesDifferent.Count > 0)
                        {
                            FilesDifferent.Clear();
                        }
                    }
                    if (FilesMissing != null)
                    {
                        if (FilesMissing.Count > 0)
                        {
                            FilesMissing.Clear();
                        }
                    }
                }
                catch (Exception)
                {

                }

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

            if (strErrorMessage.Length > 31800)
            {
                strErrorMessage = strErrorMessage.Substring(0, 31800) + " ...";
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
        /// Compares two Streams for binary differences
        /// </summary>
        /// <param name="stream1"></param>
        /// <param name="stream2"></param>
        /// <returns></returns>
        private static bool StreamsContentsAreEqual(Stream stream1, Stream stream2)
        {
            const int bufferSize = 2048 * 2;
            var buffer1 = new byte[bufferSize];
            var buffer2 = new byte[bufferSize];

            while (true)
            {
                int count1 = stream1.Read(buffer1, 0, bufferSize);
                int count2 = stream2.Read(buffer2, 0, bufferSize);

                if (count1 != count2)
                {
                    return false;
                }

                if (count1 == 0)
                {
                    return true;
                }

                int iterations = (int)Math.Ceiling((double)count1 / sizeof(Int64));
                for (int i = 0; i < iterations; i++)
                {
                    //Compare the streams in buffer size chunks until there is a failure
                    if (BitConverter.ToInt64(buffer1, i * sizeof(Int64)) != BitConverter.ToInt64(buffer2, i * sizeof(Int64)))
                    {
                        return false;
                    }
                }
            }
        }

        /// <summary>
        /// Binary comparison of two files
        /// </summary>
        /// <param name="fileName1">the file to compare</param>
        /// <param name="fileName2">the other file to compare</param>
        /// <returns>a value indicateing weather the file are identical</returns>
        public static bool Compare_Files(string fileName1, string fileName2)
        {
            Delimon.Win32.IO.FileInfo info1 = new Delimon.Win32.IO.FileInfo(Common.WindowsPathClean(fileName1));
            Delimon.Win32.IO.FileInfo info2 = new Delimon.Win32.IO.FileInfo(Common.WindowsPathClean(fileName2));
            Common.RefreshFileInfo(info1);
            Common.RefreshFileInfo(info2);
            bool blSame = info1.Length == info2.Length;
            if (blSame)
            {
                using (FileStream fs1 = info1.OpenRead())
                using (FileStream fs2 = info2.OpenRead())
                {

                    blSame = StreamsContentsAreEqual(fs1,fs2);
                }
                
            }

            return blSame;
        }


        /// <summary>
        /// Compare Files by MD5 hash  (Not in Use Yet)  MD5 is not fool proof, does not guarantee that the files are exactly the same (also performance is less)
        /// </summary>
        /// <param name="fileName1"></param>
        /// <param name="fileName2"></param>
        /// <returns></returns>
        static bool Compare_Files_MD5(string fileName1, string fileName2)
        {
            bool blSame = false;
            string strMD5_1 = "";
            string strMD5_2 = "";
            fileName1 = Common.WindowsPathClean(fileName1);
            fileName2 = Common.WindowsPathClean(fileName2);

            Delimon.Win32.IO.FileInfo info1 = new Delimon.Win32.IO.FileInfo(fileName1);
            Delimon.Win32.IO.FileInfo info2 = new Delimon.Win32.IO.FileInfo(fileName2);
            Common.RefreshFileInfo(info1);
            Common.RefreshFileInfo(info2);
            blSame = info1.Length == info2.Length;

            if (blSame)
            {
                strMD5_1 = Common.GetMD5HashFromFile(fileName1);
                strMD5_2 = Common.GetMD5HashFromFile(fileName2);
                blSame = strMD5_1 == strMD5_2;
            }
            return blSame;
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
