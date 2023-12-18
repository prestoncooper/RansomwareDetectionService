﻿using System;
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

All rights reserved.
Redistribution and use in source and binary forms, with or without modification, are permitted provided that the following conditions are met:
1. Redistributions of source code must retain the above copyright notice, this list of conditions and the following disclaimer.
2. Redistributions in binary form must reproduce the above copyright notice, this list of conditions and the following disclaimer in the documentation and/or other materials provided with the distribution.
3. Neither the name of the copyright holder nor the names of its contributors may be used to endorse or promote products derived from this software without specific prior written permission.
THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
*/



namespace RansomwareDetection.DetectionLib
{
    
    
    

    /// <summary>
    /// Find File Individual Filter Class
    /// </summary>
    public class FindFileFilter
    {
        #region "Properties"

        public int ID { get; set; }

        public bool Enabled { get; set; }

        public string Title { get; set; }

        public string FileFilter { get; set; }

        public string ExcludeFiles { get; set; }

        public ContentDetectorLib.Common.FileFilterObjectType ObjectType { get; set; }

        public bool DeleteFilesFound { get; set; }

        public string Comment { get; set; }

        #endregion

        public FindFileFilter()
        {
            ID = 0;
            Enabled = false;
            Title = "";
            FileFilter = "";
            ExcludeFiles = "";
            ObjectType = ContentDetectorLib.Common.FileFilterObjectType.File;
            DeleteFilesFound = false;
            Comment = "";
        }

        public FindFileFilter(DataRow row)
        {
            try
            {
                ID = Common.FixNullInt32(row["ID"]);
                Enabled = Common.FixNullbool(row["Enabled"]);
                Title = Common.FixNullstring(row["Title"]);
                FileFilter = Common.FixNullstring(row["FileFilter"]);
                ExcludeFiles = Common.FixNullstring(row["ExcludeFiles"]);
                try
                {
                    ObjectType = (ContentDetectorLib.Common.FileFilterObjectType)System.Enum.Parse(typeof(ContentDetectorLib.Common.FileFilterObjectType), Common.FixNullstring(row["ObjectType"]));
                }
                catch (Exception)
                {
                    ObjectType = ContentDetectorLib.Common.FileFilterObjectType.File;
                }
                DeleteFilesFound = Common.FixNullbool(row["DeleteFilesFound"]);
                Comment = Common.FixNullstring(row["Comment"]);
            }
            catch (Exception)
            {
                ID = 0;
                Enabled = false;
                Title = "";
                FileFilter = "";
                ExcludeFiles = "";
                ObjectType = ContentDetectorLib.Common.FileFilterObjectType.File;
                DeleteFilesFound = false;
                Comment = "";
            }
        }
        /// <summary>
        /// Initializes the config table for file filters
        /// </summary>
        /// <returns></returns>
        public static DataTable init_dtConfig()
        {
            DataTable dtFileFilters;
            dtFileFilters = new DataTable("FileFiltersConfig");


            //Create Primary Key Column
            DataColumn dcID = new DataColumn("ID", typeof(Int32));
            dcID.AllowDBNull = false;
            dcID.Unique = true;
            dcID.AutoIncrement = true;
            dcID.AutoIncrementSeed = 1;
            dcID.AutoIncrementStep = 1;

            //Assign Primary Key
            DataColumn[] columns = new DataColumn[1];
            dtFileFilters.Columns.Add(dcID);
            columns[0] = dtFileFilters.Columns["ID"];
            dtFileFilters.PrimaryKey = columns;


            //Create Columns
            dtFileFilters.Columns.Add(new DataColumn("Enabled", typeof(String)));
            dtFileFilters.Columns.Add(new DataColumn("Title", typeof(String)));
            dtFileFilters.Columns.Add(new DataColumn("FileFilter", typeof(String)));
            dtFileFilters.Columns.Add(new DataColumn("ExcludeFiles", typeof(String)));
            dtFileFilters.Columns.Add(new DataColumn("DeleteFilesFound", typeof(String)));
            dtFileFilters.Columns.Add(new DataColumn("ObjectType", typeof(String)));
            dtFileFilters.Columns.Add(new DataColumn("Comment", typeof(String)));

            dtFileFilters.Columns["Enabled"].DefaultValue = "true";
            dtFileFilters.Columns["Title"].DefaultValue = "";
            dtFileFilters.Columns["FileFilter"].DefaultValue = "";
            dtFileFilters.Columns["ExcludeFiles"].DefaultValue = "";
            dtFileFilters.Columns["ObjectType"].DefaultValue = "File";
            dtFileFilters.Columns["DeleteFilesFound"].DefaultValue = "false";

            dtFileFilters.Columns["Comment"].DefaultValue = "";

            return dtFileFilters;

        }

    }
    public class FindFilesFolder : IFolderConfig
    {
        #region "Variables"

        private string ep = "19C235A4-A313-C4C4-48F4-A5B4DC86EBCC";

        /*
        public System.Collections.Generic.List<ContentDetectorLib.FileResult> AllFiles = null;
        public System.Collections.Generic.List<ContentDetectorLib.FileResult> FilesFound = null;
        public System.Collections.Generic.List<ContentDetectorLib.FileResult> FoldersFound = null;
        public System.Collections.Generic.List<ContentDetectorLib.FileResult> FilesDeleted = null;
        public System.Collections.Generic.List<ContentDetectorLib.FileResult> ResultErrors = null;
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
        /// SMTP host username
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
        /// SMTP host password
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


        private DataTable _fileFilters = null;
        public DataTable FileFilters
        {
            get
            {
                return _fileFilters;
            }
            set
            {
                _fileFilters = value;
            }

        }


        private string _excludeFlders = "";
        /// <summary>
        /// Exclude Folders separate folder names by semicolon, and no back slashes or forward slashes
        /// </summary>
        public string ExcludeFolders
        {
            get
            {
                return _excludeFlders;
            }
            set
            {
                _excludeFlders = value;
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

        private bool _exportFilesFoundToCSV = true;
        
        public bool ExportFilesFoundToCSV
        {
            get
            {
                return _exportFilesFoundToCSV;
            }
            set
            {
                _exportFilesFoundToCSV = value;
            }

        }

        private bool _exportFoldersFoundToCSV = true;

        public bool ExportFoldersFoundToCSV
        {
            get
            {
                return _exportFoldersFoundToCSV;
            }
            set
            {
                _exportFoldersFoundToCSV = value;
            }

        }

        private bool _exportFilesDeletedToCSV = true;

        public bool ExportFilesDeletedToCSV
        {
            get
            {
                return _exportFilesDeletedToCSV;
            }
            set
            {
                _exportFilesDeletedToCSV = value;
            }

        }

        private bool _exportFileErrorsToCSV = true;

        public bool ExportFileErrorsToCSV
        {
            get
            {
                return _exportFileErrorsToCSV;
            }
            set
            {
                _exportFileErrorsToCSV = value;
            }

        }

        #endregion


        #region "Methods"



        /// <summary>
        /// Contructor
        /// </summary>
        /// <param name="row"></param>
        public FindFilesFolder()
        {
            _evt = Common.GetEventLog;
        }




        /// <summary>
        /// Contructor
        /// </summary>
        /// <param name="row"></param>
        public FindFilesFolder(DataRow row, DataTable dtFileFilters)
        {
            _evt = Common.GetEventLog;

            ID = Common.FixNullInt32(row["ID"]);
            Title = Common.FixNullstring(row["Title"]);
            Enabled = Common.FixNullbool(row["Enabled"]);
            Time = Common.FixNullstring(row["Time"]);
            EndTime = Common.FixNullstring(row["EndTime"]);

            try
            {
                IntervalType = (IntervalTypes)System.Enum.Parse(typeof(IntervalTypes), Common.FixNullstring(row["IntervalType"]));
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
            ExcludeFolders = Common.FixNullstring(row["ExcludeFolders"]);

            ExportCSVPath = Common.FixNullstring(row["ExportCSVPath"]);
            ExportFilesFoundToCSV = Common.FixNullbool(row["ExportFilesFoundToCSV"]);
            ExportFoldersFoundToCSV = Common.FixNullbool(row["ExportFoldersFoundToCSV"]);
            ExportFilesDeletedToCSV = Common.FixNullbool(row["ExportFilesDeletedToCSV"]);
            ExportFileErrorsToCSV = Common.FixNullbool(row["ExportFileErrorsToCSV"]);

            Comment = Common.FixNullstring(row["Comment"]);
            DetailedLogging = Common.FixNullbool(row["DetailedLogging"]);
            FileFilters = dtFileFilters;
        }

        /// <summary>
        /// Find Files Class Destructor
        /// </summary>
        ~FindFilesFolder()
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
        /// Initializes the config table for FindFiles
        /// </summary>
        /// <returns></returns>
        public static DataTable init_dtConfig()
        {
            DataTable dtFindFilesConfig;
            dtFindFilesConfig = new DataTable("FindFilesConfig");


            //Create Primary Key Column
            DataColumn dcID = new DataColumn("ID", typeof(Int32));
            dcID.AllowDBNull = false;
            dcID.Unique = true;
            dcID.AutoIncrement = true;
            dcID.AutoIncrementSeed = 1;
            dcID.AutoIncrementStep = 1;

            //Assign Primary Key
            DataColumn[] columns = new DataColumn[1];
            dtFindFilesConfig.Columns.Add(dcID);
            columns[0] = dtFindFilesConfig.Columns["ID"];
            dtFindFilesConfig.PrimaryKey = columns;


            //Create Columns
            dtFindFilesConfig.Columns.Add(new DataColumn("Enabled", typeof(String)));
            dtFindFilesConfig.Columns.Add(new DataColumn("Title", typeof(String)));
            dtFindFilesConfig.Columns.Add(new DataColumn("Time", typeof(String)));
            dtFindFilesConfig.Columns.Add(new DataColumn("EndTime", typeof(String)));
            dtFindFilesConfig.Columns.Add(new DataColumn("IntervalType", typeof(String)));
            dtFindFilesConfig.Columns.Add(new DataColumn("Interval", typeof(String)));
            dtFindFilesConfig.Columns.Add(new DataColumn("Monday", typeof(String)));
            dtFindFilesConfig.Columns.Add(new DataColumn("Tuesday", typeof(String)));
            dtFindFilesConfig.Columns.Add(new DataColumn("Wednesday", typeof(String)));
            dtFindFilesConfig.Columns.Add(new DataColumn("Thursday", typeof(String)));
            dtFindFilesConfig.Columns.Add(new DataColumn("Friday", typeof(String)));
            dtFindFilesConfig.Columns.Add(new DataColumn("Saturday", typeof(String)));
            dtFindFilesConfig.Columns.Add(new DataColumn("Sunday", typeof(String)));
            dtFindFilesConfig.Columns.Add(new DataColumn("January", typeof(String)));
            dtFindFilesConfig.Columns.Add(new DataColumn("February", typeof(String)));
            dtFindFilesConfig.Columns.Add(new DataColumn("March", typeof(String)));
            dtFindFilesConfig.Columns.Add(new DataColumn("April", typeof(String)));
            dtFindFilesConfig.Columns.Add(new DataColumn("May", typeof(String)));
            dtFindFilesConfig.Columns.Add(new DataColumn("June", typeof(String)));
            dtFindFilesConfig.Columns.Add(new DataColumn("July", typeof(String)));
            dtFindFilesConfig.Columns.Add(new DataColumn("August", typeof(String)));
            dtFindFilesConfig.Columns.Add(new DataColumn("September", typeof(String)));
            dtFindFilesConfig.Columns.Add(new DataColumn("October", typeof(String)));
            dtFindFilesConfig.Columns.Add(new DataColumn("November", typeof(String)));
            dtFindFilesConfig.Columns.Add(new DataColumn("December", typeof(String)));
            dtFindFilesConfig.Columns.Add(new DataColumn("DayOfMonth", typeof(String)));
            dtFindFilesConfig.Columns.Add(new DataColumn("FilePathToCheck", typeof(String)));
            dtFindFilesConfig.Columns.Add(new DataColumn("CheckSubFolders", typeof(String)));
            dtFindFilesConfig.Columns.Add(new DataColumn("SendEmailOnFailure", typeof(String)));
            dtFindFilesConfig.Columns.Add(new DataColumn("SendEmailOnSuccess", typeof(String)));
            dtFindFilesConfig.Columns.Add(new DataColumn("ExcludeFolders", typeof(String)));

            dtFindFilesConfig.Columns.Add(new DataColumn("ExportCSVPath", typeof(String)));
            dtFindFilesConfig.Columns.Add(new DataColumn("ExportFilesFoundToCSV", typeof(String)));
            dtFindFilesConfig.Columns.Add(new DataColumn("ExportFoldersFoundToCSV", typeof(String)));
            dtFindFilesConfig.Columns.Add(new DataColumn("ExportFilesDeletedToCSV", typeof(String)));
            dtFindFilesConfig.Columns.Add(new DataColumn("ExportFileErrorsToCSV", typeof(String)));

            dtFindFilesConfig.Columns.Add(new DataColumn("StartDate", typeof(String)));
            dtFindFilesConfig.Columns.Add(new DataColumn("EndDate", typeof(String)));
            dtFindFilesConfig.Columns.Add(new DataColumn("Comment", typeof(String)));
            dtFindFilesConfig.Columns.Add(new DataColumn("DetailedLogging", typeof(String)));

            dtFindFilesConfig.Columns["Enabled"].DefaultValue = "true";
            dtFindFilesConfig.Columns["Time"].DefaultValue = "01:00";
            dtFindFilesConfig.Columns["IntervalType"].DefaultValue = "Daily";
            dtFindFilesConfig.Columns["Interval"].DefaultValue = "0";
            dtFindFilesConfig.Columns["Monday"].DefaultValue = "true";
            dtFindFilesConfig.Columns["Tuesday"].DefaultValue = "true";
            dtFindFilesConfig.Columns["Wednesday"].DefaultValue = "true";
            dtFindFilesConfig.Columns["Thursday"].DefaultValue = "true";
            dtFindFilesConfig.Columns["Friday"].DefaultValue = "true";
            dtFindFilesConfig.Columns["Saturday"].DefaultValue = "true";
            dtFindFilesConfig.Columns["Sunday"].DefaultValue = "true";
            dtFindFilesConfig.Columns["January"].DefaultValue = "true";
            dtFindFilesConfig.Columns["February"].DefaultValue = "true";
            dtFindFilesConfig.Columns["March"].DefaultValue = "true";
            dtFindFilesConfig.Columns["April"].DefaultValue = "true";
            dtFindFilesConfig.Columns["May"].DefaultValue = "true";
            dtFindFilesConfig.Columns["June"].DefaultValue = "true";
            dtFindFilesConfig.Columns["July"].DefaultValue = "true";
            dtFindFilesConfig.Columns["August"].DefaultValue = "true";
            dtFindFilesConfig.Columns["September"].DefaultValue = "true";
            dtFindFilesConfig.Columns["October"].DefaultValue = "true";
            dtFindFilesConfig.Columns["November"].DefaultValue = "true";
            dtFindFilesConfig.Columns["December"].DefaultValue = "true";
            dtFindFilesConfig.Columns["DayOfMonth"].DefaultValue = "0";
            dtFindFilesConfig.Columns["FilePathToCheck"].DefaultValue = "";
            dtFindFilesConfig.Columns["CheckSubFolders"].DefaultValue = "true";
            dtFindFilesConfig.Columns["Comment"].DefaultValue = "";
            dtFindFilesConfig.Columns["SendEmailOnFailure"].DefaultValue = "false";
            dtFindFilesConfig.Columns["SendEmailOnSuccess"].DefaultValue = "false";
            dtFindFilesConfig.Columns["DetailedLogging"].DefaultValue = "false";
            dtFindFilesConfig.Columns["ExcludeFolders"].DefaultValue = "";

            dtFindFilesConfig.Columns["ExportCSVPath"].DefaultValue = "";
            dtFindFilesConfig.Columns["ExportFilesFoundToCSV"].DefaultValue = "false";
            dtFindFilesConfig.Columns["ExportFoldersFoundToCSV"].DefaultValue = "false";
            dtFindFilesConfig.Columns["ExportFilesDeletedToCSV"].DefaultValue = "false";
            dtFindFilesConfig.Columns["ExportFileErrorsToCSV"].DefaultValue = "false";

            dtFindFilesConfig.Columns["StartDate"].DefaultValue = DateTime.Now.ToString("d");
            return dtFindFilesConfig;

        }

       
        
        

        /// <summary>
        /// Executes Find of all file filters
        /// </summary>
        public void Execute(ref bool blShuttingDown)
        {
            List<ContentDetectorLib.FileResult> AllFiles = null;
            List<ContentDetectorLib.FileResult> FilesFound = null;
            List<ContentDetectorLib.FileResult> FoldersFound = null;
            List<ContentDetectorLib.FileResult> FilesDeleted = null;
            List<ContentDetectorLib.FileResult> ResultErrors = null;
            try
            {
                if (Enabled)
                {
                    writeError("Ransomware Detection Service, Files Found Process: Started " + FilePathToCheck, System.Diagnostics.EventLogEntryType.Information, 9000, 90, true);

                    FilesFound = new List<ContentDetectorLib.FileResult>();
                    FoldersFound = new List<ContentDetectorLib.FileResult>();
                    FilesDeleted = new List<ContentDetectorLib.FileResult>();
                    ResultErrors = new List<ContentDetectorLib.FileResult>();

                    if (_fileFilters != null)
                    {

                        //if the service is shutting down we need to stop looping through the source files
                        if (blShuttingDown)
                        {
                            throw new Exception("Shutting Down");
                        }
                        try
                        {
                            //Find All Ransomware Files
                            AllFiles = LongPathFileSearch.FindAllfiles(FilePathToCheck, _fileFilters, CheckSubFolders, ExcludeFolders, ref blShuttingDown);

                            if (AllFiles != null)
                            {
                                //loop through all ransomware files found
                                foreach (ContentDetectorLib.FileResult frFile1 in AllFiles)
                                {
                                    if (frFile1.ObjectType == ContentDetectorLib.Common.FileFilterObjectType.File && frFile1.Deleted == false)
                                    {
                                        //Keep overall list of files found
                                        FilesFound.Add(frFile1);

                                        //Log error for ransomware file found
                                        string strerror = "Ransomware Detection Service, Find Files: Possible Ransomware File Found: " + frFile1.FullPath + " " + frFile1.Comment;
                                        writeError(strerror, System.Diagnostics.EventLogEntryType.Error, 9001, 90, false);
                                    }
                                    else if (frFile1.ObjectType == ContentDetectorLib.Common.FileFilterObjectType.Folder && frFile1.Deleted == false)
                                    {
                                        //Add the Folder to the list
                                        FoldersFound.Add(frFile1);
                                        string strerror = "Ransomware Detection Service, Find Files: Possible Ransomware Folder Found: " + frFile1.FullPath + " " + frFile1.Comment;
                                        writeError(strerror, System.Diagnostics.EventLogEntryType.Error, 9001, 90, false);
                                    }
                                    else if (frFile1.ObjectType == ContentDetectorLib.Common.FileFilterObjectType.None && frFile1.Deleted == false)
                                    {
                                        //Error
                                        ResultErrors.Add(frFile1);
                                        string strerror = "Ransomware Detection Service, Find Files: Error: " + frFile1.FullPath + " " + frFile1.Comment;
                                        writeError(strerror, System.Diagnostics.EventLogEntryType.Error, 9001, 90, false);
                                    }
                                    else if (frFile1.Deleted)
                                    {
                                        //File Deleted
                                        FilesDeleted.Add(frFile1);
                                        string strerror = "Ransomware Detection Service, Find Files: Possible Ransomware File Found and Deleted: " + frFile1.FullPath + " " + frFile1.Comment;
                                        writeError(strerror, System.Diagnostics.EventLogEntryType.Error, 9001, 90, false);

                                    }
                                    else
                                    {
                                        //Keep overall list of files found Unknown Logic Error
                                        ResultErrors.Add(frFile1);

                                        //Log error for ransomware file found
                                        string strerror = "Ransomware Detection Service, Find Files: Possible Ransomware File Found Unknown Error: " + frFile1.FullPath + " " + frFile1.Comment;
                                        writeError(strerror, System.Diagnostics.EventLogEntryType.Error, 9001, 90, false);
                                    }
                                    
                                }
                                AllFiles.Clear();
                            }
                            else
                            {
                                string strerror = "Ransomware Detection Service, File Path Error: " + FilePathToCheck;   
                                writeError(strerror, System.Diagnostics.EventLogEntryType.Error, 9010, 90, false);
                            }
                            

                        }
                        catch (Exception ex)
                        {
                            
                            string strerror = "Ransomware Detection Service, Files Found Error: " + FilePathToCheck + " Error: " + ex.Message + " Source: " + ex.Source + " Stack Trace: " + ex.StackTrace;
                            writeError(strerror, System.Diagnostics.EventLogEntryType.Error, 9002, 90, false);

                        }
                    }



                    //end loop


                    if (Common.DirectoryExists(ExportCSVPath))
                    {
                        ExportCSVPath = Common.WindowsPathClean(ExportCSVPath);

                        if (ExportFilesFoundToCSV)
                        {

                            try
                            {
                                if (Common.FileExists(ExportCSVPath + "\\" + Title.Trim() + "FilesFound.csv"))
                                {
                                    Alphaleonis.Win32.Filesystem.File.Delete(ExportCSVPath + "\\" + Title.Trim() + "FilesFound.csv");
                                }
                                Alphaleonis.Win32.Filesystem.File.WriteAllText(ExportCSVPath + "\\" + Title.Trim() + "FilesFound.csv", ContentDetectorLib.FileResult.FileResultCollectionToCSV(FilesFound));
                            }
                            catch (Exception)
                            {
                                Alphaleonis.Win32.Filesystem.File.WriteAllText(ExportCSVPath + "\\" + Title.Trim() + "FilesFound" + Guid.NewGuid().ToString() + ".csv", ContentDetectorLib.FileResult.FileResultCollectionToCSV(FilesFound));
                            }
                        }

                        if (ExportFoldersFoundToCSV)
                        {
                            try
                            {
                                if (Common.FileExists(ExportCSVPath + "\\" + Title.Trim() + "FoldersFound.csv"))
                                {
                                    Alphaleonis.Win32.Filesystem.File.Delete(ExportCSVPath + "\\" + Title.Trim() + "FoldersFound.csv");
                                }
                                Alphaleonis.Win32.Filesystem.File.WriteAllText(ExportCSVPath + "\\" + Title.Trim() + "FoldersFound.csv", ContentDetectorLib.FileResult.FileResultCollectionToCSV(FoldersFound));
                            }
                            catch (Exception)
                            {
                                Alphaleonis.Win32.Filesystem.File.WriteAllText(ExportCSVPath + "\\" + Title.Trim() + "FoldersFound" + Guid.NewGuid().ToString() + ".csv", ContentDetectorLib.FileResult.FileResultCollectionToCSV(FoldersFound));
                            }
                        }

                        if (ExportFilesDeletedToCSV)
                        {
                            try
                            {
                                if (Common.FileExists(ExportCSVPath + "\\" + Title.Trim() + "FilesDeleted.csv"))
                                {
                                    Alphaleonis.Win32.Filesystem.File.Delete(ExportCSVPath + "\\" + Title.Trim() + "FilesDeleted.csv");
                                }
                                Alphaleonis.Win32.Filesystem.File.WriteAllText(ExportCSVPath + "\\" + Title.Trim() + "FilesDeleted.csv", ContentDetectorLib.FileResult.FileResultCollectionToCSV(FilesDeleted));
                            }
                            catch (Exception)
                            {
                                Alphaleonis.Win32.Filesystem.File.WriteAllText(ExportCSVPath + "\\" + Title.Trim() + "FilesDeleted" + Guid.NewGuid().ToString() + ".csv", ContentDetectorLib.FileResult.FileResultCollectionToCSV(FilesDeleted));
                            }
                        }

                        if (ExportFileErrorsToCSV)
                        {
                            try
                            {
                                if (Common.FileExists(ExportCSVPath + "\\" + Title.Trim() + "FileErrors.csv"))
                                {
                                    Alphaleonis.Win32.Filesystem.File.Delete(ExportCSVPath + "\\" + Title.Trim() + "FileErrors.csv");
                                }
                                Alphaleonis.Win32.Filesystem.File.WriteAllText(ExportCSVPath + "\\" + Title.Trim() + "FileErrors.csv", ContentDetectorLib.FileResult.FileResultCollectionToCSV(ResultErrors));
                            }
                            catch (Exception)
                            {
                                Alphaleonis.Win32.Filesystem.File.WriteAllText(ExportCSVPath + "\\" + Title.Trim() + "FileErrors" + Guid.NewGuid().ToString() + ".csv", ContentDetectorLib.FileResult.FileResultCollectionToCSV(ResultErrors));
                            }
                        }

                    }

                    //Send Summary Email 
                    if (SendEmailOnFailure || SendEmailOnSuccess)
                    {
                        string strBody = "";
                        string strSubject = "";


                        //send email on failure
                        if (FilesFound.Count > 0 || FoldersFound.Count > 0 || FilesDeleted.Count > 0 || ResultErrors.Count > 0)
                        {
                            //On Failure Email
                            StringBuilder sbbody1 = new StringBuilder();
                            string strline = "";
                            strSubject = "Ransomware Detection Service, Files Found - Possible Ransomware Found!: " + (FilesFound.Count + FoldersFound.Count + FilesDeleted.Count + ResultErrors.Count).ToString();
                            strline = @"<strong>Ransomware Detection Service, Possible Ransomware Files Found</strong><br /><br /><ul><li>FilePathToCheck:</strong> " + Common.GetPathToHTMLAnchor(FilePathToCheck) + @"</li>";
                            sbbody1.AppendLine(strline);
                            strline = @"<li>Check Sub Folders: " + CheckSubFolders.ToString() + @"</li></ul>";
                            sbbody1.AppendLine(strline);
                            if (FilesFound.Count > 0 && FilesFound.Count < 200)
                            {
                                sbbody1.AppendLine(@"<br /><br /><strong>Possible Ransomware Files:</strong><br />");
                                //Loop through files found and list them
                                foreach (ContentDetectorLib.FileResult frFile1 in FilesFound)
                                {
                                        sbbody1.AppendLine("<a href=\"#\" style=\"text-decoration:none !important; text-decoration:none;color:black;\">" + "\"" + frFile1.FullPath + "\",FileCreated: " + frFile1.CreationTime.ToString("G") + ",Owner: " + frFile1.Owner + ",Length" + frFile1.Length.ToString() + @"</a><br />");
                                }
                            }
                            if (FoldersFound.Count > 0 && FoldersFound.Count < 200)
                            {
                                sbbody1.AppendLine(@"<br /><br /><strong>Possible Ransomware Folders:</strong><br />");
                                //Loop through folders and list them
                                foreach (ContentDetectorLib.FileResult frFile1 in FoldersFound)
                                {
                                    sbbody1.AppendLine("<a href=\"#\" style=\"text-decoration:none !important; text-decoration:none;color:black;\">" + "\"" + frFile1.FullPath + "\",FileCreated: " + frFile1.CreationTime.ToString("G") + ",Owner: " + frFile1.Owner + @"</a><br />");
                                }
                            }

                            if (FilesDeleted.Count > 0 && FilesDeleted.Count < 200)
                            {
                                sbbody1.AppendLine(@"<br /><br /><strong>Possible Ransomware Files Deleted:</strong><br />");
                                //Loop through files found and list them
                                foreach (ContentDetectorLib.FileResult frFile1 in FilesDeleted)
                                {
                                        sbbody1.AppendLine("<a href=\"#\" style=\"text-decoration:none !important; text-decoration:none;color:black;\">File Deleted: " + "\"" + frFile1.FullPath + "\",FileCreated: " + frFile1.CreationTime.ToString("G") + ",Owner: " + frFile1.Owner + ",Length" + frFile1.Length.ToString() + "</a><br />");
                                }
                            }
                            if (ResultErrors.Count > 0 && ResultErrors.Count < 200)
                            {
                                sbbody1.AppendLine(@"<br /><br /><br /><strong>Errors:</strong><br />");
                                //Loop through Errors and list them
                                foreach (ContentDetectorLib.FileResult frFile2 in ResultErrors)
                                {
                                        sbbody1.AppendLine("<a href=\"#\" style=\"text-decoration:none !important; text-decoration:none;color:black;\">" + "\"" + frFile2.FullPath + "\",Error: " + frFile2.Comment + @"</a><br />");
                                }
                                sbbody1.AppendLine(@"<br />");
                            }
                        
                            strBody = sbbody1.ToString();
                            sbbody1.Clear();
                            if (SendEmailOnFailure)
                            {
                                send_Email(strSubject, strBody);
                            }
                            
                            
                            writeError(strBody, System.Diagnostics.EventLogEntryType.Error, 9004, 90, false);

                        }
                        else
                        {
                            //OnSuccess Email
                            StringBuilder sbbody2 = new StringBuilder();
                            strBody = "";
                            strSubject = "Ransomware Detection Service, File Found: Success! No Ransomware Files Found.";
                            sbbody2.AppendLine(@"Ransomware Detection Service, File Found: Success! - No Ransomware Files Found. <br />");
                            string strline = @"<br /><ul><li>FilePathToCheck: " + FilePathToCheck + @"</li>";
                            sbbody2.AppendLine(strline);

                            sbbody2.AppendLine(@"<li>Check Sub Folders: " + CheckSubFolders.ToString() + @"</li></ul>");
                            strBody = sbbody2.ToString();
                            sbbody2.Clear();
                            //send email on success
                            if (SendEmailOnSuccess)
                            {
                                send_Email(strSubject, strBody);
                            }
                            
                            writeError(strBody, System.Diagnostics.EventLogEntryType.Information, 9003, 90, true);
                        }


                    }
                    

                    writeError("Ransomware Detection Service, Files Found Process: Finished " + FilePathToCheck, System.Diagnostics.EventLogEntryType.Information, 9000, 90, true);

                } //end enabled if

            }
            catch (Exception ex)
            {
                string strErr = ex.Message + " Source: " + ex.Source + " StackTrace: " + ex.StackTrace; 
                writeError(strErr, System.Diagnostics.EventLogEntryType.Error, 9000, 90, false);
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
                        AllFiles = null;
                    }
                    
                    if (FilesFound != null)
                    {
                        if (FilesFound.Count > 0)
                        {
                            FilesFound.Clear();
                        }
                        FilesFound = null;
                    }
                    
                    if (FoldersFound != null)
                    {
                        if (FoldersFound.Count > 0)
                        {
                            FoldersFound.Clear();
                        }
                        FoldersFound = null;
                    }
                    
                    if (FilesDeleted != null)
                    {
                        if (FilesDeleted.Count > 0)
                        {
                            FilesDeleted.Clear();
                        }
                        FilesDeleted = null;
                    }
                    
                    if (ResultErrors != null)
                    {
                        if (ResultErrors.Count > 0)
                        {
                            ResultErrors.Clear();
                        }
                        ResultErrors = null;
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
        private void writeError(string strErrorMessage, System.Diagnostics.EventLogEntryType entrytype, int eventid, short category, bool blIsDetailedLoggingError)
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
        /// Send Email
        /// </summary>
        /// <param name="strSubject"></param>
        /// <param name="strBody"></param>
        private void send_Email(string strSubject, string strBody)
        {

            //Emails regarding restarting the process server
            char[] cdelimiter = { ',' };
            

            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient(_smtpServer);

            mail.From = new MailAddress(_emailFrom);
            _emailTo = _emailTo.Replace(";", ",");
            //comma delimiter support
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
