using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.ServiceProcess;
using System.Configuration;
using System.IO;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Net.Mail;

using RansomwareDetection.DetectionLib;

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

namespace RansomwareDetection
{
    /// <summary>
    /// RansomwareDetectionSystemTray Form
    /// </summary>
    public partial class RansomwareDetectionSystemTray : Form
    {
        #region "Variables"

        private string ep = "19C235A4-A313-C4C4-48F4-A5B4DC86EBCC";
        

        const long ServiceIntervalDefault = 120000; 
        
        /// <summary>
        /// Service Controller for restaring the RansomwareDetectionService
        /// </summary>
        private ServiceController sc;
        private ServiceController scFileServer;



        private DataTable dtCompareConfig;
        private DataTable dtFindFilesConfig;
        private DataTable dtFileFiltersConfig;
        private DataTable dtAuditFilesConfig;
        private DataTable dtSignaturesConfig;

        /// <summary>
        /// Event Log DataSet
        /// </summary>
        private DataSet dsEvents;
        private BindingSource bs;
       

       

        /// <summary>
        /// About Box
        /// </summary>
        private AboutBox frmAboutBox;


        /// <summary>
        /// FolderBrowserDialog for double click events in dgv's cells with folder data
        /// </summary>
        public FolderBrowserDialog FolderBrowserD;
        public System.Windows.Forms.OpenFileDialog FileBrowserD;
        /// <summary>
        /// trayIcon or Notification Area Icon
        /// </summary>
        private NotifyIcon trayIcon;
        /// <summary>
        /// Context Menu for trayIcon
        /// </summary>
        private ContextMenu trayMenu;

        /// <summary>
        /// Event Log Class
        /// </summary>
        private static System.Diagnostics.EventLog _evt;


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
                string str = getAppSetting("ServiceInterval");
                if (!String.IsNullOrEmpty(str))
                {
                    long.TryParse(str, out _serviceInterval);
                }
                else
                {
                    _serviceInterval = ServiceIntervalDefault;
                    //SaveAppSetting("ServiceInterval", _serviceInterval.ToString());
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
                //SaveAppSetting("ServiceInterval", _serviceInterval.ToString());

            }
        }

        private string _smtpServer = "";
        public string SMTPServer 
        {
            get
            {
                string str = getAppSetting("SMTPServer");
                if (!String.IsNullOrEmpty(str))
                {
                    _smtpServer = str;
                }
                return _smtpServer;
            }

            set
            {
                _smtpServer = value;
            }
        
        }

        private int _smtpPort = 25;
        public int SMTPPort
        {
            get
            {
                string str = getAppSetting("SMTPPort");
                if (!String.IsNullOrEmpty(str))
                {
                    int.TryParse(str, out _smtpPort);
                }
                else
                {
                    _smtpPort = 25;
                    
                }
                return _smtpPort;
            }

            set
            {
                _smtpPort = value;
                

            }
        }

        private string _emailFrom = "";
        public string EmailFrom
        {
            get
            {
                string str = getAppSetting("EmailFrom");
                if (!String.IsNullOrEmpty(str))
                {
                    _emailFrom = str;
                }
                return _emailFrom;
            }

            set
            {
                _emailFrom = value;
            }

        }

        private string _emailTo = "";
        public string EmailTo
        {
            get
            {
                string str = getAppSetting("EmailTo");
                if (!String.IsNullOrEmpty(str))
                {
                    _emailTo = str;
                }
                return _emailTo;
            }

            set
            {
                _emailTo = value;
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
                _smtpUseSSL = Common.FixNullbool(getAppSetting("SMTPUseSSL"));
                return _smtpUseSSL;
            }
            set
            {
                _smtpUseSSL = value;
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
                _smtpUseDefaultCredentials = Common.FixNullbool(getAppSetting("SMTPUseDefaultCredentials"));
                return _smtpUseDefaultCredentials;
            }
            set
            {
                _smtpUseDefaultCredentials = value;
            }
        }

        public static string _smtpUsername = "";
        /// <summary>
        /// SMTP host server IP or hostname
        /// </summary>
        public string SMTPUsername
        {
            get
            {

                _smtpUsername = Common.FixNullstring(getAppSetting("SMTPUsername"));
                return _smtpUsername;
            }
            set
            {
                //Encrypt?
                _smtpUsername = value;
            }
        }

        public static string _smtpPassword = "";
        /// <summary>
        /// SMTP host server IP or hostname
        /// </summary>
        public string SMTPPassword
        {
            get
            {
                
                _smtpPassword= Common.FixNullstring(getAppSetting("SMTPPassword"));
                return _smtpPassword;
            }
            set
            {
                //Encrypt?
                _smtpPassword = value;
            }
        }
       
        #endregion

        #region "Methods"
        
        
       

     
       
        /// <summary>
        /// Gets emtpy tables with fields specified for Compare (Detect) tab
        /// </summary>
        private void init_dtCompareConfig()
        {

            dtCompareConfig = CompareFolder.init_dtConfig();
            
        }

        /// <summary>
        /// Gets emtpy tables with fields specified for Find Files and File Filters 
        /// </summary>
        private void init_dtFindFilesConfig()
        {
            dtFindFilesConfig = FindFilesFolder.init_dtConfig();
            dtFileFiltersConfig = FindFileFilter.init_dtConfig();
            
        }

        private void init_dtAuditFilesConfig()
        {
            dtAuditFilesConfig = AuditFolder.init_dtConfig();
            dtSignaturesConfig = AuditFolder.init_dtSignaturesConfig();
        }

        /// <summary>
        /// Destructor
        /// </summary>
        ~RansomwareDetectionSystemTray()
        {
            try
            {
                if (trayIcon != null)
                {
                    trayIcon.Dispose();
                }
                if (sc != null)
                {
                    sc.Dispose();
                }
                if (scFileServer != null)
                {
                    scFileServer.Dispose();
                }
                //dtSyncConfig.Dispose();
                if (dtCompareConfig != null)
                {
                    dtCompareConfig.Dispose();
                }
                if (dtFindFilesConfig != null)
                {
                    dtFindFilesConfig.Dispose();
                }

                if (dtFileFiltersConfig != null)
                {
                    dtFileFiltersConfig.Dispose();
                }

                if (dtAuditFilesConfig != null)
                {
                    dtAuditFilesConfig.Dispose();
                }
                if (dtSignaturesConfig != null)
                {
                    dtSignaturesConfig.Dispose();
                }

                if (bs != null)
                {
                    bs.Dispose();
                }

                if (FolderBrowserD != null)
                {
                    FolderBrowserD.Dispose();
                }

                if (FileBrowserD != null)
                {
                    FileBrowserD.Dispose();
                }

                if (dsEvents != null)
                {
                    dsEvents.Dispose();
                }
                

            }
            catch (Exception)
            {


            }
        }

        


        /// <summary>
        /// RansomwareDetectionSystemTray Constructor
        /// </summary>
        public RansomwareDetectionSystemTray()
        {
            _evt = Common.GetEventLog;
            InitializeComponent();
            eventLogRansomwareDetection = Common.GetEventLog;
            
            //eventLogRansomwareDetection.EnableRaisingEvents = true;
            //eventLogRansomwareDetection.EntryWritten += new System.Diagnostics.EntryWrittenEventHandler(this.eventLogRansomwareDetection_EntryWritten);
            

            sc = new ServiceController("RansomwareDetectionService");
            scFileServer = new ServiceController("LanmanServer");
             // Create a simple tray menu with only one item.
            trayMenu = new ContextMenu();
            trayMenu.MenuItems.Add("Settings", onShowForm);
            trayMenu.MenuItems.Add("Start RansomwareDetectionService", onRestart);
            trayMenu.MenuItems.Add("Restart RansomwareDetectionService", onRestart);
            trayMenu.MenuItems.Add("Stop RansomwareDetectionService", onStop);
            trayMenu.MenuItems.Add("Close Tray", onExit);
            

            // Create a tray icon. In this example we use a
            // standard system icon for simplicity, but you
            // can of course use your own custom icon too.
            trayIcon      = new NotifyIcon();
            trayIcon.Text = "RansomwareDetectionTray";
            try
            {
                trayIcon.Icon = Properties.Resources.IconBlueExternalDrive;//new Icon(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\wood_drive_time_machine.ico", 40, 40);
                this.Icon = Properties.Resources.IconBlueExternalDrive;
            }
            catch (Exception)
            {

                trayIcon.Icon = new Icon(SystemIcons.Application, 40, 40);
            }
           
            // Add menu to tray icon and show it.
            trayIcon.ContextMenu = trayMenu;
            trayIcon.Visible     = true;

            


           
            //Load XML data for Compare (detect)
            init_dtCompareConfig();
            try
            {
                dtCompareConfig.ReadXml(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\CompareConfig.xml");
            }
            catch (Exception)
            {
                dtCompareConfig.WriteXml(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\CompareConfig.xml");
                dtCompareConfig.ReadXml(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\CompareConfig.xml");
            }

            dgvCompare.AutoGenerateColumns = false;
            dgvCompare.DataSource = dtCompareConfig;

            
            //Load XML data for Find Files
            init_dtFindFilesConfig();
            try
            {
                dtFindFilesConfig.ReadXml(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\FindFilesConfig.xml");
            }
            catch (Exception)
            {
                dtFindFilesConfig.WriteXml(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\FindFilesConfig.xml");
                dtFindFilesConfig.ReadXml(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\FindFilesConfig.xml");
            }

            dgvFindFiles.AutoGenerateColumns = false;
            dgvFindFiles.DataSource = dtFindFilesConfig;

            //Load XML data for File Filters
            try
            {
                dtFileFiltersConfig.ReadXml(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\FileFiltersConfig.xml");
            }
            catch (Exception)
            {
                dtFileFiltersConfig.WriteXml(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\FileFiltersConfig.xml");
                dtFileFiltersConfig.ReadXml(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\FileFiltersConfig.xml");
            }
            dgvFileFilters.AutoGenerateColumns = false;
            dgvFileFilters.DataSource = dtFileFiltersConfig;


            //Load XML data for Compare (detect)
            init_dtAuditFilesConfig();
            try
            {
                dtAuditFilesConfig.ReadXml(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\AuditFilesConfig.xml");
            }
            catch (Exception)
            {
                dtAuditFilesConfig.WriteXml(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\AuditFilesConfig.xml");
                dtAuditFilesConfig.ReadXml(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\AuditFilesConfig.xml");
            }

            dgvAudit.AutoGenerateColumns = false;
            dgvAudit.DataSource = dtAuditFilesConfig;

            try
            {
                dtSignaturesConfig.ReadXml(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\SignaturesConfig.xml");
            }
            catch (Exception)
            {
                dtSignaturesConfig.WriteXml(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\SignaturesConfig.xml");
                dtSignaturesConfig.ReadXml(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\SignaturesConfig.xml");
            }

            dgvSignatures.AutoGenerateColumns = false;
            dgvSignatures.DataSource = dtSignaturesConfig;


            txtServiceInterval.Text = ServiceInterval.ToString().Trim();

            lblServiceIntervalMinutes.Text = lblServiceIntervalMinutes.Text = (Math.Round(((double) ServiceInterval) / 1000 / 60, 2)).ToString() + " Minute(s)";
            txtSMTPHost.Text = SMTPServer;
            txtSMTPPort.Text = SMTPPort.ToString();
            txtEmailFrom.Text = EmailFrom;
            txtEmailTo.Text = EmailTo;
            
            txtSMTPPassword.Text = SMTPPassword;
            txtSMTPUsername.Text = SMTPUsername;
            
            chkSMTPUseDefaultCredentials.Checked = SMTPUseDefaultCredentials;
            chkSMTPUseSSL.Checked = SMTPUseSSL;

            FolderBrowserD = new FolderBrowserDialog();
            FileBrowserD = new OpenFileDialog();
            
            getServiceStatus();
            showForm();
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
            if (strErrorMessage.Length > 32765)
            {
                strErrorMessage = strErrorMessage.Substring(0, 32760) + " ...";
            }
            _evt.WriteEntry(strErrorMessage, entrytype, eventid, category);

        }

        /// <summary>
        /// Saves App.config application settings and refreshes.
        /// </summary>
        /// <param name="strProperty"></param>
        /// <param name="strValue"></param>
        private static void saveAppSetting(string strProperty, string strValue)
        {
            System.Configuration.Configuration config = ConfigurationManager.OpenExeConfiguration(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\RansomwareDetectionService.exe");

            //SAVE ALL OF THE SETTINGS
            config.AppSettings.Settings.Remove(strProperty);
            config.AppSettings.Settings.Add(strProperty, strValue);

            // Save the config file.
            config.Save(ConfigurationSaveMode.Modified);

            // Force a reload of a changed section. 
            ConfigurationManager.RefreshSection("appSettings");


        }

        /// <summary>
        /// Gets Application Setting from RansomwareDetectionService.exe.config
        /// </summary>
        /// <param name="strProperty"></param>
        /// <returns></returns>
        private string getAppSetting(string strProperty)
        {
            try
            {
                string strValue = "";
                System.Configuration.Configuration config = ConfigurationManager.OpenExeConfiguration(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\RansomwareDetectionService.exe");
                //System.Configuration.AppSettingsSection appsettings = config.AppSettings;
                //return appsettings.Settings[strProperty].Value.ToString();
                //return config.AppSettings[strProperty];
                strValue = config.AppSettings.Settings[strProperty].Value.ToString();
                return strValue;
            }
            catch (Exception ex)
            {
                string strErr = "RansomwareDetectionServiceTray: " + ex.Message + " Property: " + strProperty + " Source: " + ex.Source + "  StackTrace: " + ex.StackTrace;
                writeError(strErr,EventLogEntryType.Error,5000,50);
                return "";
            }
        }

        /// <summary>
        /// On Form Load Event Handler
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            Visible       = false; // Hide form window.
            ShowInTaskbar = false; // Remove from taskbar.

            base.OnLoad(e);
        }

        /// <summary>
        /// Close Tray Event Handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void onExit(object sender, EventArgs e)
        {
            //CloseRansomwareDetectionSystemTray();

            trayMenu.Dispose();
            trayIcon.Dispose();
            trayIcon = null;
            System.Environment.Exit(0);
        }

        private delegate void d_showTip(int timeout, string tipTitle, string tipText, ToolTipIcon tipIcon);
        
        /// <summary>
        /// ShowBalloonTip invokes the form in case it is called from other thread.
        /// </summary>
        /// <param name="timeout"></param>
        /// <param name="tipTitle"></param>
        /// <param name="tipText"></param>
        /// <param name="tipIcon"></param>
        public void ShowBalloonTip(int timeout,string tipTitle, string tipText, ToolTipIcon tipIcon)
        {
            
            if (this.InvokeRequired)
            {
                d_showTip d = new d_showTip(ShowBalloonTip);

                this.Invoke(d,new object[] {timeout,tipTitle,tipText,tipIcon});
            }
            else
            {
                trayIcon.ShowBalloonTip(timeout, tipTitle, tipText, tipIcon);
                Application.DoEvents();
                
            }

        
        }

        private delegate void d_WriteEntry(string message);
        /// <summary>
        /// WriteEntry invokes the form in case it is called from another thread.
        /// </summary>
        /// <param name="message"></param>
        private void writeEntry(string message)
        {
            if (this.InvokeRequired)
            {
                d_WriteEntry d = new d_WriteEntry(writeEntry);

                this.Invoke(d, new object[] { message });
            }
            else
            {
                _evt.WriteEntry(message);
            }
        }

        /// <summary>
        /// Restarts or Starts RansomwareDetectionService
        /// </summary>
        /// <returns></returns>
        private bool restart()
        {
            bool blSuccess = false;
            string strStatus = "";
            TimeSpan timeout = TimeSpan.FromMilliseconds(120000);
            try
            {
                strStatus = sc.Status.ToString();
                
                //Restart
                if (strStatus == "Running")
                {
                    sc.Stop();
                    sc.WaitForStatus(ServiceControllerStatus.Stopped, timeout);
                    getServiceStatus();
                    Application.DoEvents();
                    if (sc.Status.ToString() == "Stopped")
                    {
                        sc.Start();
                        sc.WaitForStatus(ServiceControllerStatus.Running, timeout);
                        if (sc.Status.ToString() == "Running")
                        {
                            ShowBalloonTip(5000, "Restart", "RansomwareDetectionService Restarted Successfully", ToolTipIcon.Info);
                            blSuccess = true;
                        }
                        else
                        {
                            ShowBalloonTip(5000, "Restart", "RansomwareDetectionService Restart Timed Out", ToolTipIcon.Error);
                        }
                    }
                    else
                    {
                        ShowBalloonTip(5000, "Restart Failed", "RansomwareDetectionService did not stop in time to start back up", ToolTipIcon.Error);
                    }
                } //Already Stopped Just Start
                else if (strStatus == "Stopped")
                {
                    sc.Start();
                    sc.WaitForStatus(ServiceControllerStatus.Running, timeout);
                    if (sc.Status.ToString() == "Running")
                    {
                        ShowBalloonTip(5000, "Start", "RansomwareDetectionService Started Successfully", ToolTipIcon.Info);
                        blSuccess = true;
                    }
                    else
                    {
                        ShowBalloonTip(5000, "Start", "RansomwareDetectionService Start Timed Out", ToolTipIcon.Error);
                    }
                }
                else
                {
                    ShowBalloonTip(5000, "Restart Failed", "RansomwareDetectionService is not in a proper state to restart", ToolTipIcon.Error);

                }
                
            }
            catch (Exception ex)
            {
                ShowBalloonTip(5000, "Restart Failed", "Restart of RansomwareDetectionService Failed!", ToolTipIcon.Error);
                string strErr = "RansomwareDetectionServiceTray: Restart of RansomwareDetectionService Failed! " + ex.Message + " Source: " + ex.Source + "  StackTrace: " + ex.StackTrace;
                writeError(strErr, EventLogEntryType.Error, 5000, 50);
            }
            Application.DoEvents();
            getServiceStatus();
            Application.DoEvents();
            return blSuccess;
        }


        private static string getServiceStartMode(string serviceName)
        {
            //Auto, Manual, Disabled 

            string filter = String.Format("SELECT * FROM Win32_Service WHERE Name = '{0}'", serviceName);

            System.Management.ManagementObjectSearcher query = new System.Management.ManagementObjectSearcher(filter);

            // No match = failed condition
            if (query == null) return "";

            try
            {
                System.Management.ManagementObjectCollection services = query.Get();

                foreach (System.Management.ManagementObject service in services)
                {
                    return service.GetPropertyValue("StartMode").ToString();
                }
            }
            catch (Exception)
            {
                return "";
            }

            return "";
        }

        /// <summary>
        /// Restarts or Starts RansomwareDetectionService
        /// </summary>
        /// <returns></returns>
        private bool startFileServer()
        {
            bool blSuccess = false;
            string strStatus = "";
            TimeSpan timeout = TimeSpan.FromMilliseconds(120000);
            try
            {
                
                

                strStatus = scFileServer.Status.ToString();

                //Restart
                if (strStatus == "Running")
                {
                    ShowBalloonTip(5000, "Start Failed", "File Server Service is already started", ToolTipIcon.Error);
                    
                } //Already Stopped Just Start
                else if (strStatus == "Stopped")
                {

                    


                    scFileServer.Start();
                    scFileServer.WaitForStatus(ServiceControllerStatus.Running, timeout);
                    
                    //Start Dfs Service
                    ServiceController scDfs = null;
                    try
                    {
                        scDfs = new ServiceController("Dfs");
                        if (scDfs.Status == ServiceControllerStatus.Stopped && getServiceStartMode("Dfs") == "Auto")
                        {
                            scDfs.Start();
                            scDfs.WaitForStatus(ServiceControllerStatus.Running, timeout);
                        }
                        
                    }
                    catch (Exception)
                    {


                    }
                    finally
                    {
                        scDfs.Dispose();
                        scDfs = null;
                    }

                    //Start Dfs Service
                    ServiceController scBrowser = null;
                    try
                    {
                        scBrowser = new ServiceController("Browser");
                        string strStartMode = getServiceStartMode("Browser");
                        if (scBrowser.Status == ServiceControllerStatus.Stopped && (strStartMode == "Auto" || strStartMode == "Manual"))
                        {
                            scBrowser.Start();
                            scBrowser.WaitForStatus(ServiceControllerStatus.Running, timeout);
                        }

                    }
                    catch (Exception)
                    {


                    }
                    finally
                    {
                        scBrowser.Dispose();
                        scBrowser = null;
                    }

                    if (scFileServer.Status.ToString() == "Running")
                    {
                        ShowBalloonTip(5000, "Start", "File Server Service Started Successfully", ToolTipIcon.Info);
                        blSuccess = true;
                    }
                    else
                    {
                        ShowBalloonTip(5000, "Start", "File Server Service Start Timed Out", ToolTipIcon.Error);
                    }
                }
                else
                {
                    ShowBalloonTip(5000, "Start Failed", "File Server Service is not in a proper state to restart", ToolTipIcon.Error);

                }
                
            }
            catch (Exception ex)
            {
                ShowBalloonTip(5000, "Start Failed", "Start of File Server Service Failed!", ToolTipIcon.Error);
                string strErr = "RansomwareDetectionServiceTray: Start of File Server Service Failed! " + ex.Message + " Source: " + ex.Source + "  StackTrace: " + ex.StackTrace;
                writeError(strErr, EventLogEntryType.Error, 5000, 50);
            }
            Application.DoEvents();
            getServiceStatus();
            Application.DoEvents();
            return blSuccess;
        }

        /// <summary>
        /// OnRestart Event Handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void onRestart(object sender, EventArgs e)
        {
            //Restart();
            backgroundWorker1.RunWorkerAsync("Restart");
        }

        /// <summary>
        /// Stops RansomwareDetectionService
        /// </summary>
        /// <returns></returns>
        private bool stop()
        {
            bool blSuccess = false;
            string strStatus = "";
            TimeSpan timeout = TimeSpan.FromMilliseconds(120000);
            try
            {
                strStatus = sc.Status.ToString();

                if (strStatus == "Running")
                {
                    sc.Stop();

                    sc.WaitForStatus(ServiceControllerStatus.Stopped, timeout);

                    if (sc.Status.ToString() == "Stopped")
                    {
                        ShowBalloonTip(5000, "Stop", "RansomwareDetectionService Stopped Successfully", ToolTipIcon.Info);
                        blSuccess = true;
                    }
                    else
                    {
                        ShowBalloonTip(5000, "Stop Failed", "RansomwareDetectionService timed out stopping.", ToolTipIcon.Error);
                    }
                }
                else if (strStatus == "Stopped")
                {
                    ShowBalloonTip(5000, "Stop", "RansomwareDetectionService Already Stopped", ToolTipIcon.Info);
                    blSuccess = true;
                }
                else
                {
                    ShowBalloonTip(5000, "Stop Failed", "RansomwareDetectionService is not in a proper state to stop.", ToolTipIcon.Error);

                }
                
            }
            catch (Exception ex)
            {
                ShowBalloonTip(5000, "Stop Failed", "Stop of RansomwareDetectionService Failed.", ToolTipIcon.Error);
                string strErr = "RansomwareDetectionServiceTray: Stop of RansomwareDetectionService Failed. " + ex.Message + " Source: " + ex.Source + "  StackTrace: " + ex.StackTrace;
                writeError(strErr, EventLogEntryType.Error, 5000, 50);
            }
            Application.DoEvents();
            getServiceStatus();
            Application.DoEvents();
            return blSuccess;
        }


        /// <summary>
        /// Stops File Server Service (lanmanserver)
        /// </summary>
        /// <returns></returns>
        private bool stopFileServer()
        {
            bool blSuccess = false;
            string strStatus = "";
            TimeSpan timeout = TimeSpan.FromMilliseconds(120000);
            try
            {
                strStatus = scFileServer.Status.ToString();

                if (strStatus == "Running")
                {
                    ServiceController scBrowser = null;
                    ServiceController scDfs = null;
                    try
                    {
                        scBrowser = new ServiceController("Browser");
                        if (scBrowser.Status == ServiceControllerStatus.Running)
                        {
                            scBrowser.Stop();
                            scBrowser.WaitForStatus(ServiceControllerStatus.Stopped, timeout);
                        }
                        
                    }
                    catch (Exception)
                    {

                        
                    }
                    finally
                    {
                        scBrowser.Dispose();
                        scBrowser = null;
                    }

                    try
                    {
                        scDfs = new ServiceController("Dfs");
                        if (scDfs.Status == ServiceControllerStatus.Running)
                        {
                            scDfs.Stop();
                            scDfs.WaitForStatus(ServiceControllerStatus.Stopped, timeout);
                        }
                    }
                    catch (Exception)
                    {

                        
                    }
                    finally
                    {
                        scDfs.Dispose();
                        scDfs = null;
                    }
                   

                    scFileServer.Stop();

                    scFileServer.WaitForStatus(ServiceControllerStatus.Stopped, timeout);

                    if (scFileServer.Status.ToString() == "Stopped")
                    {
                        ShowBalloonTip(5000, "Stop", "File Server Service Stopped Successfully", ToolTipIcon.Info);
                        blSuccess = true;
                    }
                    else
                    {
                        ShowBalloonTip(5000, "Stop Failed", "File Server Service timed out stopping.", ToolTipIcon.Error);
                    }
                }
                else if (strStatus == "Stopped")
                {
                    ShowBalloonTip(5000, "Stop", "File Server Service Already Stopped", ToolTipIcon.Info);
                    blSuccess = true;
                }
                else
                {
                    ShowBalloonTip(5000, "Stop Failed", "File Server Service is not in a proper state to stop.", ToolTipIcon.Error);

                }
                
            }
            catch (Exception ex)
            {
                ShowBalloonTip(5000, "Stop Failed", "Stop of File Server Service Failed.", ToolTipIcon.Error);
                string strErr = "RansomwareDetectionServiceTray: Stop of File Server Service Failed. " + ex.Message + " Source: " + ex.Source + "  StackTrace: " + ex.StackTrace;
                writeError(strErr, EventLogEntryType.Error, 5000, 50);
            }
            Application.DoEvents();
            getServiceStatus();
            Application.DoEvents();
            return blSuccess;
        }

        /// <summary>
        /// OnStop Event Handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void onStop(object sender, EventArgs e)
        {
            //Stop();
            backgroundWorker1.RunWorkerAsync("Stop");
        }


        /// <summary>
        /// Searches or Refreshes Events DataTable and DGV
        /// </summary>
        private void refreshEventsTab()
        {
            try
            {
                Application.DoEvents();
                //Refresh Events
                dsEvents = new DataSet("EventLog");
                DataTable dtEvents = new DataTable("Events");
                dtEvents.Columns.Add(new DataColumn("Type", typeof(String)));
                dtEvents.Columns.Add(new DataColumn("EventImage", typeof(System.Drawing.Image)));
                dtEvents.Columns.Add(new DataColumn("Time", typeof(System.DateTime)));
                dtEvents.Columns.Add(new DataColumn("Message", typeof(String)));
                dtEvents.Columns.Add(new DataColumn("Source", typeof(String)));
                dtEvents.Columns.Add(new DataColumn("Category", typeof(String)));
                dtEvents.Columns.Add(new DataColumn("EventID", typeof(long)));
                dsEvents.Tables.Add(dtEvents);

                foreach (System.Diagnostics.EventLogEntry entry in eventLogRansomwareDetection.Entries)
                {
                    if (entry.Source == "RansomwareDetection")
                    {
                        addEventLogEntry(entry);
                    }
                }
                bs = new BindingSource(dsEvents, "Events");
                string strFilter = "";
                if (!Common.FixNullbool(chkError.Checked))
                {
                    strFilter = "Type<>'Error'";
                }
                if (!Common.FixNullbool(chkWarning.Checked))
                {
                    if (strFilter.Length > 0)
                    {
                        strFilter += " AND Type<>'Warning'";
                    }
                    else
                    {
                        strFilter = "Type<>'Warning'";
                    }
                }
                if (!Common.FixNullbool(chkInformation.Checked))
                {
                    if (strFilter.Length > 0)
                    {
                        strFilter += " AND Type<>'Information'";
                    }
                    else
                    {
                        strFilter = "Type<>'Information'";
                    }
                }

                if (!string.IsNullOrEmpty(txtSearch.Text))
                {
                    if (strFilter.Length > 0)
                    {
                        strFilter += " AND Message LIKE '%" + txtSearch.Text + "%'";
                    }
                    else
                    {
                        strFilter = "Message LIKE '%" + txtSearch.Text + "%'";
                    }
                }

                if (Common.FixNullstring(strFilter).Length > 0)
                {
                    bs.Filter = strFilter; //+ " AND Source='RansomwareDetection'";
                }
                else
                {
                    bs.Filter = "";
                }

                bs.Sort = "Time DESC";
                dgvEvents.DataSource = bs;
                Application.DoEvents();
                getServiceStatus();
                Application.DoEvents();
            }
            catch (Exception ex)
            {
                string strError = "Search Error: Make sure to not use special characters like *,% etc.  Error: " + ex.Message;
                MessageBox.Show(strError, "RansomwareDetectionSystemTray", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                try
                {
                    dsEvents = new DataSet("EventLog");
                    DataTable dtEvents = new DataTable("Events");
                    dtEvents.Columns.Add(new DataColumn("Type", typeof(String)));
                    dtEvents.Columns.Add(new DataColumn("EventImage", typeof(System.Drawing.Image)));
                    dtEvents.Columns.Add(new DataColumn("Time", typeof(System.DateTime)));
                    dtEvents.Columns.Add(new DataColumn("Message", typeof(String)));
                    dtEvents.Columns.Add(new DataColumn("Source", typeof(String)));
                    dtEvents.Columns.Add(new DataColumn("Category", typeof(String)));
                    dtEvents.Columns.Add(new DataColumn("EventID", typeof(long)));
                    dsEvents.Tables.Add(dtEvents);

                    foreach (System.Diagnostics.EventLogEntry entry in eventLogRansomwareDetection.Entries)
                    {
                        if (entry.Source == "RansomwareDetection")
                        {
                            addEventLogEntry(entry);
                        }
                    }
                    bs = new BindingSource(dsEvents, "Events");

                    bs.Sort = "Time DESC";
                    bs.Filter = "";
                    dgvEvents.DataSource = bs;
                    Application.DoEvents();
                    getServiceStatus();
                    Application.DoEvents();
                }
                catch (Exception)
                {
                   
                }
               
            }
           
        }

        /// <summary>
        /// Shows Settings Form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void onShowForm(object sender, EventArgs e)
        {
            getServiceStatus();
            Visible = true;
            WindowState = FormWindowState.Normal;
            refreshEventsTab();
        }

        private void showForm()
        {
            getServiceStatus();
            Visible = true;
            WindowState = FormWindowState.Normal;
            refreshEventsTab();
        }

        /// <summary>
        /// Hides the settings form instead of closing
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ransomwareDetectionSystemTray_FormClosing(object sender, FormClosingEventArgs e)
        {
                this.Hide();
                e.Cancel = true; // this cancels the close event.
        }

        /// <summary>
        /// On Minimize Hides the settings form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ransomwareDetectionSystemTray_SizeChanged(object sender, EventArgs e)
        {
            if (FormWindowState.Minimized == this.WindowState)
            {
                this.Hide();
            }
        }

        

        
        /// <summary>
        /// Validates Time in string format
        /// </summary>
        /// <param name="strValue"></param>
        /// <returns></returns>
        private bool validateTime(string strValue)
        {
            try
            {
                if (!string.IsNullOrEmpty(strValue))
                {
                    if (!(strValue.Replace(":", "").Trim() == ""))
                    {
                        TimeSpan timeStart;
                        timeStart = TimeSpan.Parse(strValue);
                        
                    }

                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
            
        }

        


        /// <summary>
        /// Service Interval text box validation
        /// </summary>
        /// <returns></returns>
        private bool validateServiceInterval()
        {
            string strValue = txtServiceInterval.Text;
            try
            {
                long lValue = 0;
                lValue = long.Parse(strValue);
                if (lValue > 1209600000 || lValue < 1000)
                {
                    throw new Exception("Time in milliseconds too small or too large");
                }
                else
                {
                    return true;
                }

            }
            catch (Exception)
            {
                return false;
            }

        }

        

        /// <summary>
        /// txt Service Interval validation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtServiceInterval_Validating(object sender, CancelEventArgs e)
        {
            string strValue = txtServiceInterval.Text;
            try
            {
                long lValue = 0;
                lValue = long.Parse(strValue);
                if (lValue > 1209600000 || lValue < 1000)
                {
                    throw new Exception("Time in milliseconds too small or too large");

                }
                else
                {
                    lblServiceIntervalMinutes.Text = (Math.Round(((double) lValue) / 1000 / 60,2)).ToString() + " Minutes";
                }
            }
            catch (Exception)
            {
                e.Cancel = true;
                MessageBox.Show("Incorrect time in milliseconds greater than or equal 1 seconds and less than two weeks", "RansomwareDetectionSystemTray", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

        }

        /// <summary>
        /// txtService
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtServiceInterval_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                MessageBox.Show("Please enter number only..."); 
                e.Handled = true;
            }
        }

        /// <summary>
        /// btnSave Click Event Handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            save();
        }

        /// <summary>
        /// Save Procedure
        /// </summary>
        private void save()
        {
            try
            {
                if (saveValidated())
                {
                    long lValue = 0;
                    
                    long.TryParse(txtServiceInterval.Text, out lValue);
                    ServiceInterval = lValue;
                    

                    SMTPServer = txtSMTPHost.Text;
                    int.TryParse(txtSMTPPort.Text,out _smtpPort);
                    EmailFrom = txtEmailFrom.Text;
                    EmailTo = txtEmailTo.Text;

                    saveAppSetting("ServiceInterval", _serviceInterval.ToString());

                    _smtpServer = txtSMTPHost.Text;
                    saveAppSetting("SMTPServer", _smtpServer);
                    _smtpPort = Common.FixNullInt32(txtSMTPPort.Text, 25);
                    saveAppSetting("SMTPPort", _smtpPort.ToString());
                    _emailFrom = Common.FixNullstring(txtEmailFrom.Text);
                    saveAppSetting("EmailFrom", _emailFrom);
                    _emailTo = Common.FixNullstring(txtEmailTo.Text);
                    saveAppSetting("EmailTo", _emailTo);
                    _smtpUseSSL = Common.FixNullbool(chkSMTPUseSSL.Checked);
                    saveAppSetting("SMTPUseSSL", _smtpUseSSL.ToString());
                    _smtpUseDefaultCredentials = Common.FixNullbool(chkSMTPUseDefaultCredentials.Checked);
                    saveAppSetting("SMTPUseDefaultCredentials", _smtpUseDefaultCredentials.ToString());

                    _smtpUsername = Common.FixNullstring(txtSMTPUsername.Text);
                    saveAppSetting("SMTPUsername", _smtpUsername);

                    _smtpPassword = Common.FixNullstring(txtSMTPPassword.Text);
                    //Encrypt Password
                    if (_smtpPassword.Length > 0)
                    {
                        AES256 aes = new AES256(ep);
                        _smtpPassword = aes.Encrypt(_smtpPassword);
                    }
                    saveAppSetting("SMTPPassword", _smtpPassword);


                    //Save Configuration XML Files
                    dtCompareConfig.WriteXml(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\CompareConfig.xml");
                    dtFindFilesConfig.WriteXml(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\FindFilesConfig.xml");
                    dtFileFiltersConfig.WriteXml(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\FileFiltersConfig.xml");
                    dtAuditFilesConfig.WriteXml(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\AuditFilesConfig.xml");
                    dtSignaturesConfig.WriteXml(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\SignaturesConfig.xml");
                }
            }
            catch (Exception ex)
            {
                string strErr = "RansomwareDetectionServiceTray: " + ex.Message + " Source: " + ex.Source + "  StackTrace: " + ex.StackTrace;
                writeError(strErr, EventLogEntryType.Error, 5000, 50);
               
            }
            
            
            
        }

        /// <summary>
        /// Save Validated Function
        /// </summary>
        /// <returns></returns>
        private bool saveValidated()
        {
            bool blOkToSave = true;

            if (!validateServiceInterval())
            {
                blOkToSave = false;
            }
            

            return blOkToSave;

        }


        /// <summary>
        /// btnSaveApply Event Handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSaveApply_Click(object sender, EventArgs e)
        {
            save();
            //Restart();
            backgroundWorker1.RunWorkerAsync("Restart");
        }

        /// <summary>
        /// Form Load Event Handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ransomwareDetectionSystemTray_Load(object sender, EventArgs e)
        {
            //Set Tool Tips
            System.Windows.Forms.ToolTip ToolTip1 = new System.Windows.Forms.ToolTip();
            ToolTip1.SetToolTip(this.btnSaveApply, "Saves Settings and Restarts Ransomware Detection Service");

            System.Windows.Forms.ToolTip ToolTip2 = new System.Windows.Forms.ToolTip();
            ToolTip2.SetToolTip(this.txtServiceInterval, "Time in milliseconds for the service to execute repeatedly");
            ToolTip2.SetToolTip(this.lblServiceInterval, "Time in milliseconds for the service to execute repeatedly");

            System.Windows.Forms.ToolTip ToolTip3 = new System.Windows.Forms.ToolTip();
            //trayIcon.Visible = false;
        }

        /// <summary>
        /// Cell Event Argument IsNumeric validation whole and negative numbers
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        private bool cellEventArgIsNumeric(ref DataGridViewCellValidatingEventArgs e)
        {

            bool blIsNumeric = true;
            try
            {
                char[] chars = e.FormattedValue.ToString().ToCharArray();
                foreach (char c in chars)
                {
                    if (char.IsDigit(c) == false)
                    {
                        blIsNumeric = false;
                        break;
                    }
                }
            }
            catch (Exception)
            {
                blIsNumeric = false;
            }
            return blIsNumeric;
        }

        /// <summary>
        /// Cell Event Argument IsNumeric validation whole numbers only
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        private bool cellEventArgIsNumeric2(ref DataGridViewCellValidatingEventArgs e)
        {

            bool blIsNumeric = true;
            int i = 0;
            try
            {
                char[] chars = e.FormattedValue.ToString().ToCharArray();
                foreach (char c in chars)
                {
                    i++;
                    if (char.IsDigit(c) == false && !(c =='-' && i == 1))
                    {
                        blIsNumeric = false;
                        break;
                    }
                }
            }
            catch (Exception)
            {
                blIsNumeric = false;
            }
            return blIsNumeric;
        }


        /// <summary>
        /// Cell Event Argument IsTime validation
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        private bool cellEventArgIsTime(ref DataGridViewCellValidatingEventArgs e)
        {

            bool blIsTime = true;
            try
            {
                //^(?:0?[0-9]|1[0-9]|2[0-3]):[0-5][0-9]$
                if (Common.FixNullstring(e.FormattedValue).Length > 0)
                {
                    Regex checktime = new Regex(@"^(?:0?[0-9]|1[0-9]|2[0-3]):[0-5][0-9]$");
                    blIsTime = checktime.IsMatch(e.FormattedValue.ToString());
                }
            }
            catch (Exception)
            {
                blIsTime = false;
            }
            return blIsTime;
        }

       

        
        
        /// <summary>
        /// Compare (Detect) tab validates fields entered
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvCompare_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            try
            {
                if (dgvCompare.Columns[e.ColumnIndex].HeaderText == "SourcePath" || dgvCompare.Columns[e.ColumnIndex].HeaderText == "FilePathToCheck" || dgvCompare.Columns[e.ColumnIndex].HeaderText == "ExportCSVPath"  || dgvCompare.Columns[e.ColumnIndex].HeaderText == "CommandWorkingDirectory")
                {
                    DataGridViewTextBoxCell cell = dgvCompare[e.ColumnIndex, e.RowIndex] as DataGridViewTextBoxCell;
                    if (cell != null)
                    {
                        if (Common.FixNullstring(e.FormattedValue).Length > 0)
                        {
                            if (!Common.DirectoryExists(e.FormattedValue.ToString()))
                            {
                                MessageBox.Show("File Path Path does not exist or permission problem.");
                                e.Cancel = true;
                            }
                        }
                    }
                }
                
                else if (dgvCompare.Columns[e.ColumnIndex].HeaderText == "StartTime")
                {
                    //validates text boxes that are supposed to be a military time 00:00
                    if (!cellEventArgIsTime(ref e))
                    {
                        MessageBox.Show("Time has to be in military time format 00:00 between 00:00 - 23:59");
                        e.Cancel = true;
                    }
                }
                else if (dgvCompare.Columns[e.ColumnIndex].HeaderText == "EndTime")
                {
                    //validates text boxes that are supposed to be a military time 00:00
                    if (!cellEventArgIsTime(ref e))
                    {
                        MessageBox.Show("Time has to be in military time format 00:00 between 00:00 - 23:59");
                        e.Cancel = true;
                    }
                }
                else if (dgvCompare.Columns[e.ColumnIndex].HeaderText == "Interval")
                {
                    if (!cellEventArgIsNumeric2(ref e))
                    {
                        MessageBox.Show("You have to enter numbers only");
                        e.Cancel = true;
                    }
                }
                else if (dgvCompare.Columns[e.ColumnIndex].HeaderText == "CommandTimeout")
                {
                    DataGridViewTextBoxCell cell = dgvCompare[e.ColumnIndex, e.RowIndex] as DataGridViewTextBoxCell;
                    if (cell != null)
                    {
                        if (Common.FixNullstring(e.FormattedValue).Length > 0)
                        {
                            int intctimeout = Common.FixNullInt32(e.FormattedValue.ToString());
                            if (intctimeout < 0 || intctimeout > 2880)
                            {
                                MessageBox.Show("Only positive integer value less than 2880 is allowed.");
                                e.Cancel = true;
                            }


                        }
                    }
                }

            }
            catch (Exception ex)
            {
                string strErr = "RansomwareDetectionServiceTray: " + ex.Message + " Source: " + ex.Source + "  StackTrace: " + ex.StackTrace;
                writeError(strErr, EventLogEntryType.Error, 5000, 50);

            }
        }

        /// <summary>
        /// Find Files tab validates fields entered
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvFindFiles_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            try
            {
                if (dgvFindFiles.Columns[e.ColumnIndex].HeaderText == "FilePathToCheck" || dgvFindFiles.Columns[e.ColumnIndex].HeaderText == "ExportCSVPath")
                {
                    DataGridViewTextBoxCell cell = dgvFindFiles[e.ColumnIndex, e.RowIndex] as DataGridViewTextBoxCell;
                    if (cell != null)
                    {
                        if (Common.FixNullstring(e.FormattedValue).Length > 0)
                        {
                            if (!Common.DirectoryExists(e.FormattedValue.ToString()))
                            {
                                MessageBox.Show("File Path Path does not exist or permission problem.");
                                e.Cancel = true;
                            }
                        }
                    }
                }

                else if (dgvFindFiles.Columns[e.ColumnIndex].HeaderText == "StartTime")
                {
                    //validates text boxes that are supposed to be a military time 00:00
                    if (!cellEventArgIsTime(ref e))
                    {
                        MessageBox.Show("Time has to be in military time format 00:00 between 00:00 - 23:59");
                        e.Cancel = true;
                    }
                }
                else if (dgvFindFiles.Columns[e.ColumnIndex].HeaderText == "EndTime")
                {
                    //validates text boxes that are supposed to be a military time 00:00
                    if (!cellEventArgIsTime(ref e))
                    {
                        MessageBox.Show("Time has to be in military time format 00:00 between 00:00 - 23:59");
                        e.Cancel = true;
                    }
                }
                else if (dgvFindFiles.Columns[e.ColumnIndex].HeaderText == "Interval")
                {
                    if (!cellEventArgIsNumeric2(ref e))
                    {
                        MessageBox.Show("You have to enter numbers only");
                        e.Cancel = true;
                    }
                }
            }
            catch (Exception ex)
            {
                string strErr = "RansomwareDetectionServiceTray: " + ex.Message + " Source: " + ex.Source + "  StackTrace: " + ex.StackTrace;
                writeError(strErr, EventLogEntryType.Error, 5000, 50);

            }

        }


        private void dgvAudit_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            try
            {
                if (dgvAudit.Columns[e.ColumnIndex].HeaderText == "FilePathToCheck" || dgvAudit.Columns[e.ColumnIndex].HeaderText == "ExportCSVPath" || dgvAudit.Columns[e.ColumnIndex].HeaderText == "RestoredFilesPath")
                {
                    DataGridViewTextBoxCell cell = dgvAudit[e.ColumnIndex, e.RowIndex] as DataGridViewTextBoxCell;
                    if (cell != null)
                    {
                        if (Common.FixNullstring(e.FormattedValue).Length > 0)
                        {
                            if (!Common.DirectoryExists(e.FormattedValue.ToString()))
                            {
                                MessageBox.Show("File Path Path does not exist or permission problem.");
                                e.Cancel = true;
                            }
                        }
                    }
                }
                else if (dgvAudit.Columns[e.ColumnIndex].HeaderText == "StartTime")
                {
                    //validates text boxes that are supposed to be a military time 00:00
                    if (!cellEventArgIsTime(ref e))
                    {
                        MessageBox.Show("Time has to be in military time format 00:00 between 00:00 - 23:59");
                        e.Cancel = true;
                    }
                }
                else if (dgvAudit.Columns[e.ColumnIndex].HeaderText == "EndTime")
                {
                    //validates text boxes that are supposed to be a military time 00:00
                    if (!cellEventArgIsTime(ref e))
                    {
                        MessageBox.Show("Time has to be in military time format 00:00 between 00:00 - 23:59");
                        e.Cancel = true;
                    }
                }
                else if (dgvAudit.Columns[e.ColumnIndex].HeaderText == "Interval")
                {
                    if (!cellEventArgIsNumeric2(ref e))
                    {
                        MessageBox.Show("You have to enter numbers only");
                        e.Cancel = true;
                    }
                }
                else if (dgvAudit.Columns[e.ColumnIndex].HeaderText == "FixUnverifiedFilesFromBackup")
                {
                    DataGridViewCheckBoxCell cell = dgvAudit[e.ColumnIndex, e.RowIndex] as DataGridViewCheckBoxCell;
                    if (cell != null)
                    {
                        if (e.FormattedValue.ToString().ToLower() == "true")
                        {

                            MessageBox.Show("Warning:  This option will replace unverified files in the FilePathToCheck from the files in the RestoredFilesPath. Make a current backup of FilePathToCheck folder before using this option. Run this task at least once with FixUnverifiedFilesFromBackup option unchecked for review.  Review the csv files and confirm the unverified files are corrupted. Fix as many unknownfiles as possible.");
                            e.Cancel = false;
                        }



                    }
                }

            }
            catch (Exception ex)
            {
                if (_evt == null)
                {
                    _evt = Common.GetEventLog;
                }
                string strErr = "RansomwareDetectionServiceTray: " + ex.Message + " Source: " + ex.Source + "  StackTrace: " + ex.StackTrace;
                writeError(strErr, EventLogEntryType.Error, 5000, 50);

            }
        }

        

        

        /// <summary>
        /// File Filter tab validates entered text for the File Filter Table
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvFileFilters_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            try
            {
                if (dgvFileFilters.Columns[e.ColumnIndex].HeaderText == "FileFilter")
                {
                    DataGridViewTextBoxCell cell = dgvFileFilters[e.ColumnIndex, e.RowIndex] as DataGridViewTextBoxCell;
                    if (cell != null)
                    {
                        if (Common.FixNullstring(e.FormattedValue).Length > 0)
                        {
                            if (!LongPathFileSearch.VerifyPattern(Common.FixNullstring(e.FormattedValue)))
                            {
                                MessageBox.Show("File Filter is in incorrect format (e.g. *filename*.txt, filename*, *.* )");
                                e.Cancel = true;
                            }
                            
                        }
                    }
                }
                else if (dgvFileFilters.Columns[e.ColumnIndex].HeaderText == "DeleteFilesFound")
                {
                    DataGridViewCheckBoxCell cell = dgvFileFilters[e.ColumnIndex, e.RowIndex] as DataGridViewCheckBoxCell;
                    if (cell != null)
                    {
                        if (e.FormattedValue.ToString().ToLower() == "true")
                        {
                            
                            MessageBox.Show("Warning:  Make sure file filter is very specific, make sure a search has previously completed with no false positive files found by the search, document owner of any ransomware created files and time created for auditing/user training before deletion is run, and be careful because this runs for all directories listed on the Find Files tab.\r\n\r\n Be very careful this searches for all files in potentially all folders matching the file filter and deletes them.");
                            e.Cancel = false;
                        }

                                
                            
                    }

                }

                
            }
            catch (Exception ex)
            {
                string strErr = "RansomwareDetectionServiceTray: " + ex.Message + " Source: " + ex.Source + "  StackTrace: " + ex.StackTrace;
                writeError(strErr, EventLogEntryType.Error, 5000, 50);

            }
        }



        private void dgvSignatures_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            try
            {
                if (dgvSignatures.Columns[e.ColumnIndex].HeaderText == "HexPattern")
                {
                    DataGridViewTextBoxCell cell = dgvSignatures[e.ColumnIndex, e.RowIndex] as DataGridViewTextBoxCell;
                    if (cell != null)
                    {
                        if (Common.FixNullstring(e.FormattedValue).Length > 0)
                        {
                            if (!Common.IsHexString(e.FormattedValue.ToString()))
                            {
                                MessageBox.Show("Only Hexadecimal Value is allowed (0-9 or A-F)");
                                e.Cancel = true;
                            }
                        }
                    }
                }
                else if (dgvSignatures.Columns[e.ColumnIndex].HeaderText == "ByteOffset")
                {
                    DataGridViewTextBoxCell cell = dgvSignatures[e.ColumnIndex, e.RowIndex] as DataGridViewTextBoxCell;
                    if (cell != null)
                    {
                        if (Common.FixNullstring(e.FormattedValue).Length > 0)
                        {
                            int intByteOffset = Common.FixNullInt32(e.FormattedValue.ToString());
                            if (intByteOffset < 0 || intByteOffset > 100)
                            {
                                MessageBox.Show("Only positive integer value less than 100 is allowed.");
                                e.Cancel = true;
                            }


                        }
                    }
                }
                
            }
            catch (Exception ex)
            {
                string strErr = "RansomwareDetectionServiceTray: " + ex.Message + " Source: " + ex.Source + "  StackTrace: " + ex.StackTrace;
                writeError(strErr, EventLogEntryType.Error, 5000, 50);

            }
        }

        /// <summary>
        /// btnStop Click Event Handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStop_Click(object sender, EventArgs e)
        {
            //Stop();
            backgroundWorker1.RunWorkerAsync("Stop");
        }

        /// <summary>
        /// btnStart Click Event Handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStart_Click(object sender, EventArgs e)
        {
            //Restart();
            backgroundWorker1.RunWorkerAsync("Restart");
        }

        /// <summary>
        /// Gets RansomwareDetectionService Status and sets enabled on buttons and label text
        /// </summary>
        private void getServiceStatus()
        {
            try
            {

                //Ransomware Detection Service Status
                sc.Refresh();
                string strStatus = sc.Status.ToString();
                txtServiceStatus.Text = strStatus;
                ServiceControllerStatus cStatus = sc.Status;
                //if (strStatus == "Running" || strStatus == "StartPending")
                if (cStatus == ServiceControllerStatus.Running || cStatus == ServiceControllerStatus.StartPending ||  cStatus == ServiceControllerStatus.ContinuePending)
                {
                    //service running
                    btnStart.Enabled = false;
                    trayMenu.MenuItems[1].Enabled = false;
                    trayMenu.MenuItems[2].Enabled = true;
                    trayMenu.MenuItems[3].Enabled = true;
                    
                    btnStop.Enabled = true;
                    
                    txtServiceStatus.BackColor = Color.DarkGreen;
                }
                else if (cStatus == ServiceControllerStatus.Stopped || cStatus == ServiceControllerStatus.StopPending || cStatus == ServiceControllerStatus.Paused || cStatus == ServiceControllerStatus.PausePending)
                {
                    //service not running
                    trayMenu.MenuItems[1].Enabled = true;
                    trayMenu.MenuItems[2].Enabled = false;
                    trayMenu.MenuItems[3].Enabled = false;
                    
                    btnStart.Enabled = true;
                    btnStop.Enabled = false;
                    txtServiceStatus.BackColor = Color.DarkRed;
                }
                else
                {
                    //unknown status allow all
                    btnStart.Enabled = true;
                    btnStop.Enabled = true;
                    trayMenu.MenuItems[1].Enabled = true;
                    trayMenu.MenuItems[2].Enabled = true;
                    trayMenu.MenuItems[3].Enabled = true;
                    txtServiceStatus.BackColor = Color.DarkRed;
                }




                //File Server Status
                scFileServer.Refresh();
                strStatus = scFileServer.Status.ToString();
                txtFileServerStatus.Text = strStatus;
                cStatus = scFileServer.Status;
                //if (strStatus == "Running" || strStatus == "StartPending")
                if (cStatus == ServiceControllerStatus.Running || cStatus == ServiceControllerStatus.StartPending || cStatus == ServiceControllerStatus.ContinuePending)
                {
                    //service running
                    btnStartFileServer.Enabled = false;
                    btnStopFileServer.Enabled = true;


                    txtFileServerStatus.BackColor = Color.DarkGreen;
                }
                else if (cStatus == ServiceControllerStatus.Stopped || cStatus == ServiceControllerStatus.StopPending || cStatus == ServiceControllerStatus.Paused || cStatus == ServiceControllerStatus.PausePending)
                {
                    //service not running
                    btnStartFileServer.Enabled = true;
                    btnStopFileServer.Enabled = false;
                    txtFileServerStatus.BackColor = Color.DarkRed;
                }
                else
                {
                    //unknown status allow all
                    btnStartFileServer.Enabled = true;
                    btnStopFileServer.Enabled = true;

                    txtFileServerStatus.BackColor = Color.DarkRed;
                }
            }
            catch (Exception ex)
            {
                string strErr = "RansomwareDetectionServiceTray: " + ex.Message + " Source: " + ex.Source + "  StackTrace: " + ex.StackTrace;
                writeError(strErr, EventLogEntryType.Error, 5000, 50);
            }
            

        }

        /// <summary>
        /// Cell FolderBrowser opens folder browser dialog
        /// </summary>
        /// <param name="cell"></param>
        private void cellFolderBrowser(ref DataGridViewTextBoxCell cell)
        {
            try
            {
                if (cell != null)
                {
                    if (Common.DirectoryExists(cell.Value.ToString()))
                    {
                        FolderBrowserD.SelectedPath = cell.Value.ToString();
                    }
                    if (FolderBrowserD.ShowDialog() == DialogResult.OK)
                    {
                        cell.Value = FolderBrowserD.SelectedPath;
                    }
                }
            }
            catch (Exception ex)
            {
                string strErr = "RansomwareDetectionServiceTray: " + ex.Message + " Source: " + ex.Source + "  StackTrace: " + ex.StackTrace;
                writeError(strErr, EventLogEntryType.Error, 5000, 50);
                
            }
           
        }



        /// <summary>
        /// Cell FileBrowser opens file browser dialog
        /// </summary>
        /// <param name="cell"></param>
        private void cellFileBrowser(ref DataGridViewTextBoxCell cell)
        {
            try
            {
                if (cell != null)
                {
                    FileBrowserD.FileName = "";
                    if (Common.FileExists(cell.Value.ToString()))
                    {
                        Delimon.Win32.IO.FileInfo f = new Delimon.Win32.IO.FileInfo(cell.Value.ToString());
                        FileBrowserD.InitialDirectory = f.Directory.FullName;
                        FileBrowserD.FileName = cell.Value.ToString();
                    }
                    if (FileBrowserD.ShowDialog() == DialogResult.OK)
                    {
                        cell.Value = FileBrowserD.FileName;
                    }
                }
            }
            catch (Exception ex)
            {
                if (_evt == null)
                {
                    _evt = Common.GetEventLog;
                }
                string strErr = ex.Message + ": " + ex.Source + "  " + ex.StackTrace;
                _evt.WriteEntry(strErr);

            }

        }


       

        private void dgvAudit_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvAudit.Columns[e.ColumnIndex].HeaderText == "ExportCSVPath" || dgvAudit.Columns[e.ColumnIndex].HeaderText == "FilePathToCheck" || dgvAudit.Columns[e.ColumnIndex].HeaderText == "RestoredFilesPath")
            {
                DataGridViewTextBoxCell cell = dgvAudit[e.ColumnIndex, e.RowIndex] as DataGridViewTextBoxCell;
                cellFolderBrowser(ref cell);
                dgvAudit.RefreshEdit();
            }
        }

        

        /// <summary>
        /// Compare Double Click (Detect) - Opens Folder Browser dialog with SourcePath or FilePathToCheck columns
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvCompare_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvCompare.Columns[e.ColumnIndex].HeaderText == "SourcePath" || dgvCompare.Columns[e.ColumnIndex].HeaderText == "FilePathToCheck" || dgvCompare.Columns[e.ColumnIndex].HeaderText == "ExportCSVPath" || dgvCompare.Columns[e.ColumnIndex].HeaderText == "CommandWorkingDirectory")
            {
                DataGridViewTextBoxCell cell = dgvCompare[e.ColumnIndex, e.RowIndex] as DataGridViewTextBoxCell;
                cellFolderBrowser(ref cell);
                dgvCompare.RefreshEdit();
            }
        }

        /// <summary>
        /// Find Files Double Click - Opens Folder Browser dialog with FilePathToCheck column
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvFindFiles_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvFindFiles.Columns[e.ColumnIndex].HeaderText == "FilePathToCheck" || dgvFindFiles.Columns[e.ColumnIndex].HeaderText == "ExportCSVPath")
            {
                DataGridViewTextBoxCell cell = dgvFindFiles[e.ColumnIndex, e.RowIndex] as DataGridViewTextBoxCell;
                cellFolderBrowser(ref cell);
                dgvFindFiles.RefreshEdit();
            }
        }

        

        /// <summary>
        /// Help Menu About Event Handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void aboutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (frmAboutBox == null)
            {
                frmAboutBox = new AboutBox();
                frmAboutBox.Show();
            }
            else
            {
                frmAboutBox.Dispose();
                frmAboutBox = new AboutBox();
                frmAboutBox.Show();
            }
        }

        /// <summary>
        /// File Menu Exit Event Handler - Hide instead of close
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        /// <summary>
        /// Adds event log entry into row for datatable
        /// </summary>
        /// <param name="entry"></param>
        private void addEventLogEntry(System.Diagnostics.EventLogEntry entry)
        {

            try
            {
                DataRow row = dsEvents.Tables["Events"].NewRow();
                if (entry.EntryType == 0)
                {
                    row["Type"] = "Information";
                }
                else
                {
                    row["Type"] = Enum.GetName(typeof(System.Diagnostics.EventLogEntryType), entry.EntryType).ToString();
                }
                switch (Common.FixNullstring(row["Type"]))
                {
                    case "Error":
                        row["EventImage"] = (System.Drawing.Image)Properties.Resources.ImgError;
                        break;
                    case "Warning":
                        row["EventImage"] = (System.Drawing.Image)Properties.Resources.ImgWarning;
                        break;
                    case "Information":
                        row["EventImage"] = (System.Drawing.Image)Properties.Resources.ImgInformation;
                        break;
                    default:
                        row["EventImage"] = (System.Drawing.Image)Properties.Resources.ImgInformation;
                        break;
                }
                row["Time"] = entry.TimeGenerated;
                row["Message"] = entry.Message;
                row["Source"] = entry.Source;
                row["Category"] = entry.Category;
                row["EventID"] = entry.InstanceId;
                dsEvents.Tables["Events"].Rows.Add(row);
            }
            catch (Exception ex)
            {
                string strErr = "RansomwareDetectionServiceTray: " + ex.Message + " Source: " + ex.Source + "  StackTrace: " + ex.StackTrace;
                writeError(strErr, EventLogEntryType.Error, 5000, 50);
            }
            
        }

        /// <summary>
        /// Event Log EntryWritten Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void eventLogRansomwareDetection_EntryWritten(object sender, System.Diagnostics.EntryWrittenEventArgs e)
        {
            addEventLogEntry(e.Entry);
        }



       

        /// <summary>
        /// Clear Logs button click event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClearLogs_Click(object sender, EventArgs e)
        {
            try
            {

                _evt.Clear();
                dsEvents.Tables["Events"].Rows.Clear();
                _evt.WriteEntry("RanDet Event Log Cleared.");
            }
            catch (Exception)
            {
                if (_evt == null)
                {
                    _evt = Common.GetEventLog;
                }
            }
            
        }

        /// <summary>
        /// Search button click event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRefreshEventLog_Click(object sender, EventArgs e)
        {
            refreshEventsTab();
        }

        /// <summary>
        /// BackgroundWork1 DoWork event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            string strTask = Common.FixNullstring(e.Argument);
            switch (strTask)
            {
                case "Stop":
                    stop();
                    break;
                case "Restart":
                    restart();
                    break;
                case "StartFileServer":
                    startFileServer();
                    break;
                case "StopFileServer":
                    stopFileServer();
                    break;
            }
        }

        /// <summary>
        /// backgroundWork1 DoWork Completed Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Application.DoEvents();
            getServiceStatus();
            Application.DoEvents();
        }


        /// <summary>
        /// Help Menu Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void helpToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            string strdocpath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\RansomwareDetectionServiceDocumentation.pdf";
            if (Common.FileExists(strdocpath))
            {
                System.Diagnostics.Process.Start(strdocpath);
            }
            else
            {
                System.Diagnostics.Process.Start("http://ransomwaredetectionservice.codeplex.com/documentation");
            }
        }

        

        /// <summary>
        /// Opens License File
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void licenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string strdocpath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\License.rtf";
            if (Common.FileExists(strdocpath))
            {
                System.Diagnostics.Process.Start(strdocpath);
            }
        }

        private void websiteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.questiondriven.com");
        }
        

        private void authorPostRegardingRansomwareToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.questiondriven.com/category/ransomware/");
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
            MailMessage mail;
            SmtpClient SmtpServer;
            string[] ToMultiEmail;
            try
            {

            
                mail = new MailMessage();
            
                SmtpServer = new SmtpClient(_smtpServer);

                mail.From = new MailAddress(_emailFrom);

                _emailTo = _emailTo.Replace(";", ",");
                //comma delimiter support for email to
                ToMultiEmail = _emailTo.Split(cdelimiter, StringSplitOptions.RemoveEmptyEntries);
            
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
            catch (Exception ex)
            {

                string strErr = "RansomwareDetectionServiceTray: " + ex.Message + " Source: " + ex.Source + "  StackTrace: " + ex.StackTrace;
                writeError(strErr, EventLogEntryType.Error, 5000, 50);
            }

        }

        
        
        

        private void sendTestEmailToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            
            save();
            send_Email("Test", "Ransomware Detection Email Test");
            
        }

        private void servicesConsoleToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("services.msc");
        }
        

        private void btnSendTestEmail_Click(object sender, EventArgs e)
        {
            
            save();
            send_Email("Test", "Ransomware Detection Email Test");
            
        }
        

        private void ransomwareDetectionServiceCodePlexPageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://ransomwaredetectionservice.codeplex.com");
        }

        

        private void onlineHelpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://ransomwaredetectionservice.codeplex.com/documentation");
        }

        
        private void fileExplorerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("c:\\windows\\explorer.exe");
        }

        private void btnStartFileServer_Click(object sender, EventArgs e)
        {
            backgroundWorker1.RunWorkerAsync("StartFileServer");
        }

        private void btnStopFileServer_Click(object sender, EventArgs e)
        {
            backgroundWorker1.RunWorkerAsync("StopFileServer");
        }

        
       
        #endregion

        














    }
}
