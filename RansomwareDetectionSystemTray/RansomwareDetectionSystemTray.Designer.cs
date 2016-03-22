namespace RansomwareDetection
{
    partial class RansomwareDetectionSystemTray
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            
            if (disposing && (components != null))
            {
                components.Dispose();
                trayIcon.Dispose();
                trayIcon = null;
            }
            base.Dispose(disposing);
        }
        

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RansomwareDetectionSystemTray));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sendTestEmailToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.servicesConsoleToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.fileExplorerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.onlineHelpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.licenseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ransomwareDetectionServiceCodePlexPageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.authorPostRegardingRansomwareToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.websiteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSendTestEmail = new System.Windows.Forms.Button();
            this.txtEmailTo = new System.Windows.Forms.TextBox();
            this.txtSMTPPassword = new System.Windows.Forms.TextBox();
            this.txtSMTPUsername = new System.Windows.Forms.TextBox();
            this.txtEmailFrom = new System.Windows.Forms.TextBox();
            this.lblSMTPUsername = new System.Windows.Forms.Label();
            this.lblSMTPPassword = new System.Windows.Forms.Label();
            this.chkSMTPUseDefaultCredentials = new System.Windows.Forms.CheckBox();
            this.chkSMTPUseSSL = new System.Windows.Forms.CheckBox();
            this.txtSMTPPort = new System.Windows.Forms.TextBox();
            this.lblSMTPPort = new System.Windows.Forms.Label();
            this.lblEmailTo = new System.Windows.Forms.Label();
            this.lblEmailFrom = new System.Windows.Forms.Label();
            this.txtSMTPHost = new System.Windows.Forms.TextBox();
            this.lblSMTPHost = new System.Windows.Forms.Label();
            this.txtServiceStatus = new System.Windows.Forms.TextBox();
            this.lblServiceIntervalMinutes = new System.Windows.Forms.Label();
            this.btnStart = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnSaveApply = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.lblServiceInterval = new System.Windows.Forms.Label();
            this.txtServiceInterval = new System.Windows.Forms.TextBox();
            this.lblTitle = new System.Windows.Forms.Label();
            this.pbRightIcon = new System.Windows.Forms.PictureBox();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.tabEvents = new System.Windows.Forms.TabPage();
            this.lblSearch = new System.Windows.Forms.Label();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.chkInformation = new System.Windows.Forms.CheckBox();
            this.chkWarning = new System.Windows.Forms.CheckBox();
            this.chkError = new System.Windows.Forms.CheckBox();
            this.btnRefreshEventLog = new System.Windows.Forms.Button();
            this.btnClearLogs = new System.Windows.Forms.Button();
            this.dgvEvents = new System.Windows.Forms.DataGridView();
            this.dgvEventImage = new System.Windows.Forms.DataGridViewImageColumn();
            this.dgvEventType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvColEventTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvColEventMessage = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvColEventSource = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvColEventCategory = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvColEventEventID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabCompare = new System.Windows.Forms.TabPage();
            this.dgvCompare = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewCheckBoxColumn1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewComboBoxColumn1 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewCheckBoxColumn2 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewCheckBoxColumn3 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewCheckBoxColumn4 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewCheckBoxColumn5 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewCheckBoxColumn6 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewCheckBoxColumn7 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewCheckBoxColumn8 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewCheckBoxColumn9 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewCheckBoxColumn10 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewCheckBoxColumn11 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewCheckBoxColumn12 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewCheckBoxColumn13 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewCheckBoxColumn14 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewCheckBoxColumn15 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewCheckBoxColumn16 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewCheckBoxColumn17 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewCheckBoxColumn18 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewCheckBoxColumn19 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewCheckBoxColumn20 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewTextBoxColumn11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.chkCheckMainfolder = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.chkCheckSubFolders = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.txtExcludeFolders = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.chkCopySourceFiles = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.chkCopySourceFilesSubFolders = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.chkSendEmail = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.chkSendEmailOnSuccess = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.calendarColumn1 = new System.Windows.Forms.CalendarColumn();
            this.calendarColumn2 = new System.Windows.Forms.CalendarColumn();
            this.dataGridViewTextBoxColumn15 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.chkDetailedLogging = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabFindFiles = new System.Windows.Forms.TabPage();
            this.dgvFindFiles = new System.Windows.Forms.DataGridView();
            this.dgvcoltxtFindFilesID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvcolchkFindFilesEnabled = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dgvcoltxtFindFilesTitle = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvcoltxtFindFilesStartTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvcoltxtFindFilesEndTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvcolDropDownFindFilesIntervalType = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.dgvcoltxtFindFilesInterval = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewCheckBoxColumn22 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewCheckBoxColumn23 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewCheckBoxColumn24 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewCheckBoxColumn25 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewCheckBoxColumn26 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewCheckBoxColumn27 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewCheckBoxColumn28 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewCheckBoxColumn29 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewCheckBoxColumn30 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewCheckBoxColumn31 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewCheckBoxColumn32 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewCheckBoxColumn33 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewCheckBoxColumn34 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewCheckBoxColumn35 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewCheckBoxColumn36 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewCheckBoxColumn37 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewCheckBoxColumn38 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewCheckBoxColumn39 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewCheckBoxColumn40 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dgvcoltxtFindFilesFilePathToCheck = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvcolchkFindFilesCheckSubFolders = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dgvcoltxtFindFilesExcludeFolders = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvcolchkFindFilesSendEmailOnFailure = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dgvcolchkFindFilesSendEmailOnSuccess = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dgvcolcalFindFilesStartDate = new System.Windows.Forms.CalendarColumn();
            this.dgvcolcalFindFilesEndDate = new System.Windows.Forms.CalendarColumn();
            this.dgvcoltxtFindFilesComment = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvcolchkFindFilesDetailedLogging = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.tabFilesToFind = new System.Windows.Forms.TabPage();
            this.dgvFileFilters = new System.Windows.Forms.DataGridView();
            this.dgvcoltxtFileFiltersID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvcolchkFileFiltersEnabled = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dgvcoltxtFileFiltersTitle = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvcoltxtFileFiltersFileFilter = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvFindFilesColtxtExcludeFiles = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dropdownfindfilescolObjectType = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.dgvcolchkFileFiltersDeleteFilesFound = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dgvcoltxtFileFiltersComment = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabAudit = new System.Windows.Forms.TabPage();
            this.dgvAudit = new System.Windows.Forms.DataGridView();
            this.dgvAuditTxtColID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewCheckBoxColumn21 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dgvAuditTxtColTitle = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvAuditTxtColStartTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvAuditTxtColEndTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewComboBoxColumn2 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.dgvAuditTxtColInterval = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewCheckBoxColumn41 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewCheckBoxColumn42 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewCheckBoxColumn43 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewCheckBoxColumn44 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewCheckBoxColumn45 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewCheckBoxColumn46 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewCheckBoxColumn47 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewCheckBoxColumn48 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewCheckBoxColumn49 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewCheckBoxColumn50 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewCheckBoxColumn51 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewCheckBoxColumn52 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewCheckBoxColumn53 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewCheckBoxColumn54 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewCheckBoxColumn55 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewCheckBoxColumn56 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewCheckBoxColumn57 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewCheckBoxColumn58 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewCheckBoxColumn59 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dgvAuditTxtColFilePathToCheck = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewCheckBoxColumn61 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dgvAuditTxtColExcludeFolders = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvAuditTxtColExportCSVPath = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvAuditChkColExportUnVerifiedToCSV = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dgvAuditChkColExportVerifiedToCSV = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dgvAuditChkCol1ExportUnknownToCSV = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewCheckBoxColumn64 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewCheckBoxColumn65 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dgvAuditCalColStartDate = new System.Windows.Forms.CalendarColumn();
            this.dgvAuditCalColEndDate = new System.Windows.Forms.CalendarColumn();
            this.dgvAuditTxtColComment = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewCheckBoxColumn66 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.menuStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbRightIcon)).BeginInit();
            this.tabEvents.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEvents)).BeginInit();
            this.tabCompare.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCompare)).BeginInit();
            this.tabControl.SuspendLayout();
            this.tabFindFiles.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFindFiles)).BeginInit();
            this.tabFilesToFind.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFileFilters)).BeginInit();
            this.tabAudit.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAudit)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(784, 27);
            this.menuStrip1.TabIndex = 19;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sendTestEmailToolStripMenuItem1,
            this.servicesConsoleToolStripMenuItem1,
            this.fileExplorerToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(41, 23);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // sendTestEmailToolStripMenuItem1
            // 
            this.sendTestEmailToolStripMenuItem1.Name = "sendTestEmailToolStripMenuItem1";
            this.sendTestEmailToolStripMenuItem1.Size = new System.Drawing.Size(179, 24);
            this.sendTestEmailToolStripMenuItem1.Text = "Send Test Email";
            this.sendTestEmailToolStripMenuItem1.Click += new System.EventHandler(this.sendTestEmailToolStripMenuItem1_Click);
            // 
            // servicesConsoleToolStripMenuItem1
            // 
            this.servicesConsoleToolStripMenuItem1.Name = "servicesConsoleToolStripMenuItem1";
            this.servicesConsoleToolStripMenuItem1.Size = new System.Drawing.Size(179, 24);
            this.servicesConsoleToolStripMenuItem1.Text = "Services Console";
            this.servicesConsoleToolStripMenuItem1.Click += new System.EventHandler(this.servicesConsoleToolStripMenuItem1_Click);
            // 
            // fileExplorerToolStripMenuItem
            // 
            this.fileExplorerToolStripMenuItem.Name = "fileExplorerToolStripMenuItem";
            this.fileExplorerToolStripMenuItem.Size = new System.Drawing.Size(179, 24);
            this.fileExplorerToolStripMenuItem.Text = "File Explorer";
            this.fileExplorerToolStripMenuItem.Click += new System.EventHandler(this.fileExplorerToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(179, 24);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem1,
            this.onlineHelpToolStripMenuItem,
            this.helpToolStripMenuItem1,
            this.licenseToolStripMenuItem,
            this.ransomwareDetectionServiceCodePlexPageToolStripMenuItem,
            this.authorPostRegardingRansomwareToolStripMenuItem,
            this.websiteToolStripMenuItem});
            this.helpToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(49, 23);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // aboutToolStripMenuItem1
            // 
            this.aboutToolStripMenuItem1.Name = "aboutToolStripMenuItem1";
            this.aboutToolStripMenuItem1.Size = new System.Drawing.Size(359, 24);
            this.aboutToolStripMenuItem1.Text = "&About";
            this.aboutToolStripMenuItem1.Click += new System.EventHandler(this.aboutToolStripMenuItem1_Click);
            // 
            // onlineHelpToolStripMenuItem
            // 
            this.onlineHelpToolStripMenuItem.Name = "onlineHelpToolStripMenuItem";
            this.onlineHelpToolStripMenuItem.Size = new System.Drawing.Size(359, 24);
            this.onlineHelpToolStripMenuItem.Text = "Online Help";
            this.onlineHelpToolStripMenuItem.Click += new System.EventHandler(this.onlineHelpToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem1
            // 
            this.helpToolStripMenuItem1.Name = "helpToolStripMenuItem1";
            this.helpToolStripMenuItem1.Size = new System.Drawing.Size(359, 24);
            this.helpToolStripMenuItem1.Text = "&Help PDF Document";
            this.helpToolStripMenuItem1.Click += new System.EventHandler(this.helpToolStripMenuItem1_Click);
            // 
            // licenseToolStripMenuItem
            // 
            this.licenseToolStripMenuItem.Name = "licenseToolStripMenuItem";
            this.licenseToolStripMenuItem.Size = new System.Drawing.Size(359, 24);
            this.licenseToolStripMenuItem.Text = "License";
            this.licenseToolStripMenuItem.Click += new System.EventHandler(this.licenseToolStripMenuItem_Click);
            // 
            // ransomwareDetectionServiceCodePlexPageToolStripMenuItem
            // 
            this.ransomwareDetectionServiceCodePlexPageToolStripMenuItem.Name = "ransomwareDetectionServiceCodePlexPageToolStripMenuItem";
            this.ransomwareDetectionServiceCodePlexPageToolStripMenuItem.Size = new System.Drawing.Size(359, 24);
            this.ransomwareDetectionServiceCodePlexPageToolStripMenuItem.Text = "Ransomware Detection Service CodePlex Page";
            this.ransomwareDetectionServiceCodePlexPageToolStripMenuItem.Click += new System.EventHandler(this.ransomwareDetectionServiceCodePlexPageToolStripMenuItem_Click);
            // 
            // authorPostRegardingRansomwareToolStripMenuItem
            // 
            this.authorPostRegardingRansomwareToolStripMenuItem.Name = "authorPostRegardingRansomwareToolStripMenuItem";
            this.authorPostRegardingRansomwareToolStripMenuItem.Size = new System.Drawing.Size(359, 24);
            this.authorPostRegardingRansomwareToolStripMenuItem.Text = "Author Post Regarding Ransomware";
            this.authorPostRegardingRansomwareToolStripMenuItem.Click += new System.EventHandler(this.authorPostRegardingRansomwareToolStripMenuItem_Click);
            // 
            // websiteToolStripMenuItem
            // 
            this.websiteToolStripMenuItem.Name = "websiteToolStripMenuItem";
            this.websiteToolStripMenuItem.Size = new System.Drawing.Size(359, 24);
            this.websiteToolStripMenuItem.Text = "Author Site QuestionDriven.com";
            this.websiteToolStripMenuItem.Click += new System.EventHandler(this.websiteToolStripMenuItem_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.btnSendTestEmail);
            this.panel1.Controls.Add(this.txtEmailTo);
            this.panel1.Controls.Add(this.txtSMTPPassword);
            this.panel1.Controls.Add(this.txtSMTPUsername);
            this.panel1.Controls.Add(this.txtEmailFrom);
            this.panel1.Controls.Add(this.lblSMTPUsername);
            this.panel1.Controls.Add(this.lblSMTPPassword);
            this.panel1.Controls.Add(this.chkSMTPUseDefaultCredentials);
            this.panel1.Controls.Add(this.chkSMTPUseSSL);
            this.panel1.Controls.Add(this.txtSMTPPort);
            this.panel1.Controls.Add(this.lblSMTPPort);
            this.panel1.Controls.Add(this.lblEmailTo);
            this.panel1.Controls.Add(this.lblEmailFrom);
            this.panel1.Controls.Add(this.txtSMTPHost);
            this.panel1.Controls.Add(this.lblSMTPHost);
            this.panel1.Controls.Add(this.txtServiceStatus);
            this.panel1.Controls.Add(this.lblServiceIntervalMinutes);
            this.panel1.Controls.Add(this.btnStart);
            this.panel1.Controls.Add(this.btnStop);
            this.panel1.Controls.Add(this.btnSaveApply);
            this.panel1.Controls.Add(this.btnSave);
            this.panel1.Controls.Add(this.lblServiceInterval);
            this.panel1.Controls.Add(this.txtServiceInterval);
            this.panel1.Controls.Add(this.lblTitle);
            this.panel1.Controls.Add(this.pbRightIcon);
            this.panel1.Location = new System.Drawing.Point(0, 30);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(784, 360);
            this.panel1.TabIndex = 20;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(519, 309);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(103, 18);
            this.label1.TabIndex = 49;
            this.label1.Text = "Service Status";
            // 
            // btnSendTestEmail
            // 
            this.btnSendTestEmail.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSendTestEmail.Location = new System.Drawing.Point(265, 290);
            this.btnSendTestEmail.Name = "btnSendTestEmail";
            this.btnSendTestEmail.Size = new System.Drawing.Size(124, 26);
            this.btnSendTestEmail.TabIndex = 10;
            this.btnSendTestEmail.Text = "Send &Test Email";
            this.btnSendTestEmail.UseVisualStyleBackColor = true;
            this.btnSendTestEmail.Click += new System.EventHandler(this.btnSendTestEmail_Click);
            // 
            // txtEmailTo
            // 
            this.txtEmailTo.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEmailTo.Location = new System.Drawing.Point(265, 260);
            this.txtEmailTo.Name = "txtEmailTo";
            this.txtEmailTo.Size = new System.Drawing.Size(252, 24);
            this.txtEmailTo.TabIndex = 9;
            // 
            // txtSMTPPassword
            // 
            this.txtSMTPPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSMTPPassword.Location = new System.Drawing.Point(265, 210);
            this.txtSMTPPassword.Name = "txtSMTPPassword";
            this.txtSMTPPassword.PasswordChar = '*';
            this.txtSMTPPassword.Size = new System.Drawing.Size(252, 24);
            this.txtSMTPPassword.TabIndex = 7;
            // 
            // txtSMTPUsername
            // 
            this.txtSMTPUsername.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSMTPUsername.Location = new System.Drawing.Point(265, 185);
            this.txtSMTPUsername.Name = "txtSMTPUsername";
            this.txtSMTPUsername.Size = new System.Drawing.Size(252, 24);
            this.txtSMTPUsername.TabIndex = 6;
            // 
            // txtEmailFrom
            // 
            this.txtEmailFrom.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEmailFrom.Location = new System.Drawing.Point(265, 235);
            this.txtEmailFrom.Name = "txtEmailFrom";
            this.txtEmailFrom.Size = new System.Drawing.Size(252, 24);
            this.txtEmailFrom.TabIndex = 8;
            // 
            // lblSMTPUsername
            // 
            this.lblSMTPUsername.AutoSize = true;
            this.lblSMTPUsername.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSMTPUsername.Location = new System.Drawing.Point(141, 188);
            this.lblSMTPUsername.Name = "lblSMTPUsername";
            this.lblSMTPUsername.Size = new System.Drawing.Size(127, 18);
            this.lblSMTPUsername.TabIndex = 48;
            this.lblSMTPUsername.Text = "SMTP Username:";
            // 
            // lblSMTPPassword
            // 
            this.lblSMTPPassword.AutoSize = true;
            this.lblSMTPPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSMTPPassword.Location = new System.Drawing.Point(141, 213);
            this.lblSMTPPassword.Name = "lblSMTPPassword";
            this.lblSMTPPassword.Size = new System.Drawing.Size(125, 18);
            this.lblSMTPPassword.TabIndex = 46;
            this.lblSMTPPassword.Text = "SMTP Password:";
            // 
            // chkSMTPUseDefaultCredentials
            // 
            this.chkSMTPUseDefaultCredentials.AutoSize = true;
            this.chkSMTPUseDefaultCredentials.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkSMTPUseDefaultCredentials.Location = new System.Drawing.Point(265, 161);
            this.chkSMTPUseDefaultCredentials.Name = "chkSMTPUseDefaultCredentials";
            this.chkSMTPUseDefaultCredentials.Size = new System.Drawing.Size(218, 21);
            this.chkSMTPUseDefaultCredentials.TabIndex = 5;
            this.chkSMTPUseDefaultCredentials.Text = "SMTP Use Default Credentials";
            this.chkSMTPUseDefaultCredentials.UseVisualStyleBackColor = true;
            // 
            // chkSMTPUseSSL
            // 
            this.chkSMTPUseSSL.AutoSize = true;
            this.chkSMTPUseSSL.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkSMTPUseSSL.Location = new System.Drawing.Point(265, 132);
            this.chkSMTPUseSSL.Name = "chkSMTPUseSSL";
            this.chkSMTPUseSSL.Size = new System.Drawing.Size(124, 21);
            this.chkSMTPUseSSL.TabIndex = 4;
            this.chkSMTPUseSSL.Text = "SMTP Use SSL";
            this.chkSMTPUseSSL.UseVisualStyleBackColor = true;
            // 
            // txtSMTPPort
            // 
            this.txtSMTPPort.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSMTPPort.Location = new System.Drawing.Point(265, 103);
            this.txtSMTPPort.Name = "txtSMTPPort";
            this.txtSMTPPort.Size = new System.Drawing.Size(252, 24);
            this.txtSMTPPort.TabIndex = 3;
            // 
            // lblSMTPPort
            // 
            this.lblSMTPPort.AutoSize = true;
            this.lblSMTPPort.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSMTPPort.Location = new System.Drawing.Point(141, 106);
            this.lblSMTPPort.Name = "lblSMTPPort";
            this.lblSMTPPort.Size = new System.Drawing.Size(86, 18);
            this.lblSMTPPort.TabIndex = 42;
            this.lblSMTPPort.Text = "SMTP Port:";
            // 
            // lblEmailTo
            // 
            this.lblEmailTo.AutoSize = true;
            this.lblEmailTo.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEmailTo.Location = new System.Drawing.Point(141, 263);
            this.lblEmailTo.Name = "lblEmailTo";
            this.lblEmailTo.Size = new System.Drawing.Size(71, 18);
            this.lblEmailTo.TabIndex = 40;
            this.lblEmailTo.Text = "Email To:";
            // 
            // lblEmailFrom
            // 
            this.lblEmailFrom.AutoSize = true;
            this.lblEmailFrom.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEmailFrom.Location = new System.Drawing.Point(141, 238);
            this.lblEmailFrom.Name = "lblEmailFrom";
            this.lblEmailFrom.Size = new System.Drawing.Size(89, 18);
            this.lblEmailFrom.TabIndex = 38;
            this.lblEmailFrom.Text = "Email From:";
            // 
            // txtSMTPHost
            // 
            this.txtSMTPHost.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSMTPHost.Location = new System.Drawing.Point(265, 77);
            this.txtSMTPHost.Name = "txtSMTPHost";
            this.txtSMTPHost.Size = new System.Drawing.Size(252, 24);
            this.txtSMTPHost.TabIndex = 2;
            // 
            // lblSMTPHost
            // 
            this.lblSMTPHost.AutoSize = true;
            this.lblSMTPHost.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSMTPHost.Location = new System.Drawing.Point(141, 80);
            this.lblSMTPHost.Name = "lblSMTPHost";
            this.lblSMTPHost.Size = new System.Drawing.Size(90, 18);
            this.lblSMTPHost.TabIndex = 36;
            this.lblSMTPHost.Text = "SMTP Host:";
            // 
            // txtServiceStatus
            // 
            this.txtServiceStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtServiceStatus.Location = new System.Drawing.Point(519, 330);
            this.txtServiceStatus.Name = "txtServiceStatus";
            this.txtServiceStatus.ReadOnly = true;
            this.txtServiceStatus.Size = new System.Drawing.Size(107, 24);
            this.txtServiceStatus.TabIndex = 15;
            // 
            // lblServiceIntervalMinutes
            // 
            this.lblServiceIntervalMinutes.AutoSize = true;
            this.lblServiceIntervalMinutes.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblServiceIntervalMinutes.Location = new System.Drawing.Point(435, 51);
            this.lblServiceIntervalMinutes.Name = "lblServiceIntervalMinutes";
            this.lblServiceIntervalMinutes.Size = new System.Drawing.Size(82, 18);
            this.lblServiceIntervalMinutes.TabIndex = 32;
            this.lblServiceIntervalMinutes.Text = "1 Minute(s)";
            // 
            // btnStart
            // 
            this.btnStart.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStart.Location = new System.Drawing.Point(438, 326);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 30);
            this.btnStart.TabIndex = 14;
            this.btnStart.Text = "S&tart";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnStop
            // 
            this.btnStop.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStop.Location = new System.Drawing.Point(356, 326);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(75, 30);
            this.btnStop.TabIndex = 13;
            this.btnStop.Text = "Sto&p";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnSaveApply
            // 
            this.btnSaveApply.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSaveApply.Location = new System.Drawing.Point(226, 327);
            this.btnSaveApply.Name = "btnSaveApply";
            this.btnSaveApply.Size = new System.Drawing.Size(124, 29);
            this.btnSaveApply.TabIndex = 12;
            this.btnSaveApply.Text = "Save and &Apply";
            this.btnSaveApply.UseVisualStyleBackColor = true;
            this.btnSaveApply.Click += new System.EventHandler(this.btnSaveApply_Click);
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Location = new System.Drawing.Point(144, 327);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 30);
            this.btnSave.TabIndex = 11;
            this.btnSave.Text = "&Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // lblServiceInterval
            // 
            this.lblServiceInterval.AutoSize = true;
            this.lblServiceInterval.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblServiceInterval.Location = new System.Drawing.Point(141, 54);
            this.lblServiceInterval.Name = "lblServiceInterval";
            this.lblServiceInterval.Size = new System.Drawing.Size(111, 18);
            this.lblServiceInterval.TabIndex = 25;
            this.lblServiceInterval.Text = "Service Interval:";
            // 
            // txtServiceInterval
            // 
            this.txtServiceInterval.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtServiceInterval.Location = new System.Drawing.Point(265, 51);
            this.txtServiceInterval.Name = "txtServiceInterval";
            this.txtServiceInterval.ReadOnly = true;
            this.txtServiceInterval.Size = new System.Drawing.Size(164, 24);
            this.txtServiceInterval.TabIndex = 1;
            this.txtServiceInterval.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtServiceInterval_KeyPress);
            this.txtServiceInterval.Validating += new System.ComponentModel.CancelEventHandler(this.txtServiceInterval_Validating);
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(159, 9);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(480, 29);
            this.lblTitle.TabIndex = 23;
            this.lblTitle.Text = "Ransomware Detection Service Settings";
            // 
            // pbRightIcon
            // 
            this.pbRightIcon.Image = global::RansomwareDetection.Properties.Resources.ImgWoodDriveTime;
            this.pbRightIcon.InitialImage = global::RansomwareDetection.Properties.Resources.ImgWoodDriveTime;
            this.pbRightIcon.Location = new System.Drawing.Point(520, 44);
            this.pbRightIcon.Name = "pbRightIcon";
            this.pbRightIcon.Size = new System.Drawing.Size(118, 127);
            this.pbRightIcon.TabIndex = 34;
            this.pbRightIcon.TabStop = false;
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerReportsProgress = true;
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // tabEvents
            // 
            this.tabEvents.Controls.Add(this.lblSearch);
            this.tabEvents.Controls.Add(this.txtSearch);
            this.tabEvents.Controls.Add(this.chkInformation);
            this.tabEvents.Controls.Add(this.chkWarning);
            this.tabEvents.Controls.Add(this.chkError);
            this.tabEvents.Controls.Add(this.btnRefreshEventLog);
            this.tabEvents.Controls.Add(this.btnClearLogs);
            this.tabEvents.Controls.Add(this.dgvEvents);
            this.tabEvents.Location = new System.Drawing.Point(4, 25);
            this.tabEvents.Name = "tabEvents";
            this.tabEvents.Padding = new System.Windows.Forms.Padding(3);
            this.tabEvents.Size = new System.Drawing.Size(769, 140);
            this.tabEvents.TabIndex = 4;
            this.tabEvents.Text = "Log";
            this.tabEvents.ToolTipText = "Event Log for this service.";
            this.tabEvents.UseVisualStyleBackColor = true;
            // 
            // lblSearch
            // 
            this.lblSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSearch.AutoSize = true;
            this.lblSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSearch.Location = new System.Drawing.Point(470, 12);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new System.Drawing.Size(57, 17);
            this.lblSearch.TabIndex = 42;
            this.lblSearch.Text = "Search:";
            // 
            // txtSearch
            // 
            this.txtSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearch.Location = new System.Drawing.Point(529, 9);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(154, 23);
            this.txtSearch.TabIndex = 22;
            // 
            // chkInformation
            // 
            this.chkInformation.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkInformation.AutoSize = true;
            this.chkInformation.Checked = true;
            this.chkInformation.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkInformation.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkInformation.Location = new System.Drawing.Point(370, 11);
            this.chkInformation.Name = "chkInformation";
            this.chkInformation.Size = new System.Drawing.Size(97, 21);
            this.chkInformation.TabIndex = 21;
            this.chkInformation.Text = "Information";
            this.chkInformation.UseVisualStyleBackColor = true;
            // 
            // chkWarning
            // 
            this.chkWarning.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkWarning.AutoSize = true;
            this.chkWarning.Checked = true;
            this.chkWarning.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkWarning.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkWarning.Location = new System.Drawing.Point(281, 11);
            this.chkWarning.Name = "chkWarning";
            this.chkWarning.Size = new System.Drawing.Size(80, 21);
            this.chkWarning.TabIndex = 20;
            this.chkWarning.Text = "Warning";
            this.chkWarning.UseVisualStyleBackColor = true;
            // 
            // chkError
            // 
            this.chkError.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkError.AutoSize = true;
            this.chkError.Checked = true;
            this.chkError.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkError.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkError.Location = new System.Drawing.Point(216, 10);
            this.chkError.Name = "chkError";
            this.chkError.Size = new System.Drawing.Size(59, 21);
            this.chkError.TabIndex = 19;
            this.chkError.Text = "Error";
            this.chkError.UseVisualStyleBackColor = true;
            // 
            // btnRefreshEventLog
            // 
            this.btnRefreshEventLog.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRefreshEventLog.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRefreshEventLog.Location = new System.Drawing.Point(689, 4);
            this.btnRefreshEventLog.Name = "btnRefreshEventLog";
            this.btnRefreshEventLog.Size = new System.Drawing.Size(79, 30);
            this.btnRefreshEventLog.TabIndex = 23;
            this.btnRefreshEventLog.Text = "Search";
            this.btnRefreshEventLog.UseVisualStyleBackColor = true;
            this.btnRefreshEventLog.Click += new System.EventHandler(this.btnRefreshEventLog_Click);
            // 
            // btnClearLogs
            // 
            this.btnClearLogs.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClearLogs.Location = new System.Drawing.Point(3, 5);
            this.btnClearLogs.Name = "btnClearLogs";
            this.btnClearLogs.Size = new System.Drawing.Size(86, 28);
            this.btnClearLogs.TabIndex = 24;
            this.btnClearLogs.Text = "Clear Log";
            this.btnClearLogs.UseVisualStyleBackColor = true;
            this.btnClearLogs.Click += new System.EventHandler(this.btnClearLogs_Click);
            // 
            // dgvEvents
            // 
            this.dgvEvents.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvEvents.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvEvents.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvEvents.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgvEventImage,
            this.dgvEventType,
            this.dgvColEventTime,
            this.dgvColEventMessage,
            this.dgvColEventSource,
            this.dgvColEventCategory,
            this.dgvColEventEventID});
            this.dgvEvents.Location = new System.Drawing.Point(0, 39);
            this.dgvEvents.Name = "dgvEvents";
            this.dgvEvents.Size = new System.Drawing.Size(768, 99);
            this.dgvEvents.TabIndex = 25;
            // 
            // dgvEventImage
            // 
            this.dgvEventImage.DataPropertyName = "EventImage";
            this.dgvEventImage.HeaderText = "EventImage";
            this.dgvEventImage.Name = "dgvEventImage";
            this.dgvEventImage.Width = 88;
            // 
            // dgvEventType
            // 
            this.dgvEventType.DataPropertyName = "Type";
            this.dgvEventType.HeaderText = "Type";
            this.dgvEventType.Name = "dgvEventType";
            this.dgvEventType.Visible = false;
            this.dgvEventType.Width = 65;
            // 
            // dgvColEventTime
            // 
            this.dgvColEventTime.DataPropertyName = "Time";
            this.dgvColEventTime.HeaderText = "Time";
            this.dgvColEventTime.Name = "dgvColEventTime";
            this.dgvColEventTime.Width = 64;
            // 
            // dgvColEventMessage
            // 
            this.dgvColEventMessage.DataPropertyName = "Message";
            this.dgvColEventMessage.HeaderText = "Message";
            this.dgvColEventMessage.Name = "dgvColEventMessage";
            this.dgvColEventMessage.Width = 90;
            // 
            // dgvColEventSource
            // 
            this.dgvColEventSource.DataPropertyName = "Source";
            this.dgvColEventSource.HeaderText = "Source";
            this.dgvColEventSource.Name = "dgvColEventSource";
            this.dgvColEventSource.Width = 78;
            // 
            // dgvColEventCategory
            // 
            this.dgvColEventCategory.DataPropertyName = "Category";
            this.dgvColEventCategory.HeaderText = "Category";
            this.dgvColEventCategory.Name = "dgvColEventCategory";
            this.dgvColEventCategory.Width = 90;
            // 
            // dgvColEventEventID
            // 
            this.dgvColEventEventID.DataPropertyName = "EventID";
            this.dgvColEventEventID.HeaderText = "EventID";
            this.dgvColEventEventID.Name = "dgvColEventEventID";
            this.dgvColEventEventID.Width = 82;
            // 
            // tabCompare
            // 
            this.tabCompare.Controls.Add(this.dgvCompare);
            this.tabCompare.Location = new System.Drawing.Point(4, 25);
            this.tabCompare.Name = "tabCompare";
            this.tabCompare.Padding = new System.Windows.Forms.Padding(3);
            this.tabCompare.Size = new System.Drawing.Size(769, 140);
            this.tabCompare.TabIndex = 6;
            this.tabCompare.Text = "Compare (Detect Ransomware)";
            this.tabCompare.ToolTipText = "Entrapment Ransomware Detection; Compares files by binary comparison and if files" +
    " are missing or changed then alert email is sent and an error is logged.";
            this.tabCompare.UseVisualStyleBackColor = true;
            // 
            // dgvCompare
            // 
            this.dgvCompare.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvCompare.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCompare.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewCheckBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewTextBoxColumn4,
            this.dataGridViewComboBoxColumn1,
            this.dataGridViewTextBoxColumn5,
            this.dataGridViewCheckBoxColumn2,
            this.dataGridViewCheckBoxColumn3,
            this.dataGridViewCheckBoxColumn4,
            this.dataGridViewCheckBoxColumn5,
            this.dataGridViewCheckBoxColumn6,
            this.dataGridViewCheckBoxColumn7,
            this.dataGridViewCheckBoxColumn8,
            this.dataGridViewCheckBoxColumn9,
            this.dataGridViewCheckBoxColumn10,
            this.dataGridViewCheckBoxColumn11,
            this.dataGridViewCheckBoxColumn12,
            this.dataGridViewCheckBoxColumn13,
            this.dataGridViewCheckBoxColumn14,
            this.dataGridViewCheckBoxColumn15,
            this.dataGridViewCheckBoxColumn16,
            this.dataGridViewCheckBoxColumn17,
            this.dataGridViewCheckBoxColumn18,
            this.dataGridViewCheckBoxColumn19,
            this.dataGridViewCheckBoxColumn20,
            this.dataGridViewTextBoxColumn11,
            this.dataGridViewTextBoxColumn12,
            this.chkCheckMainfolder,
            this.chkCheckSubFolders,
            this.txtExcludeFolders,
            this.chkCopySourceFiles,
            this.chkCopySourceFilesSubFolders,
            this.chkSendEmail,
            this.chkSendEmailOnSuccess,
            this.calendarColumn1,
            this.calendarColumn2,
            this.dataGridViewTextBoxColumn15,
            this.chkDetailedLogging});
            this.dgvCompare.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvCompare.Location = new System.Drawing.Point(3, 3);
            this.dgvCompare.Name = "dgvCompare";
            this.dgvCompare.Size = new System.Drawing.Size(763, 134);
            this.dgvCompare.TabIndex = 16;
            this.dgvCompare.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvCompare_CellDoubleClick);
            this.dgvCompare.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dgvCompare_CellValidating);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "ID";
            this.dataGridViewTextBoxColumn1.HeaderText = "ID";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Width = 46;
            // 
            // dataGridViewCheckBoxColumn1
            // 
            this.dataGridViewCheckBoxColumn1.DataPropertyName = "Enabled";
            this.dataGridViewCheckBoxColumn1.FalseValue = "false";
            this.dataGridViewCheckBoxColumn1.HeaderText = "Enabled";
            this.dataGridViewCheckBoxColumn1.IndeterminateValue = "";
            this.dataGridViewCheckBoxColumn1.Name = "dataGridViewCheckBoxColumn1";
            this.dataGridViewCheckBoxColumn1.TrueValue = "true";
            this.dataGridViewCheckBoxColumn1.Width = 66;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "Title";
            this.dataGridViewTextBoxColumn2.HeaderText = "Title";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.Width = 60;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.DataPropertyName = "Time";
            this.dataGridViewTextBoxColumn3.HeaderText = "StartTime";
            this.dataGridViewTextBoxColumn3.MaxInputLength = 5;
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.Width = 94;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.DataPropertyName = "EndTime";
            this.dataGridViewTextBoxColumn4.HeaderText = "EndTime";
            this.dataGridViewTextBoxColumn4.MaxInputLength = 5;
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ToolTipText = "End Time is required for Hourly Interval Type";
            this.dataGridViewTextBoxColumn4.Width = 89;
            // 
            // dataGridViewComboBoxColumn1
            // 
            this.dataGridViewComboBoxColumn1.DataPropertyName = "IntervalType";
            this.dataGridViewComboBoxColumn1.HeaderText = "IntervalType";
            this.dataGridViewComboBoxColumn1.Items.AddRange(new object[] {
            "Hourly",
            "Daily",
            "Monthly"});
            this.dataGridViewComboBoxColumn1.Name = "dataGridViewComboBoxColumn1";
            this.dataGridViewComboBoxColumn1.Width = 92;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.DataPropertyName = "Interval";
            this.dataGridViewTextBoxColumn5.HeaderText = "Interval";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ToolTipText = resources.GetString("dataGridViewTextBoxColumn5.ToolTipText");
            this.dataGridViewTextBoxColumn5.Width = 79;
            // 
            // dataGridViewCheckBoxColumn2
            // 
            this.dataGridViewCheckBoxColumn2.DataPropertyName = "Monday";
            this.dataGridViewCheckBoxColumn2.FalseValue = "false";
            this.dataGridViewCheckBoxColumn2.HeaderText = "Mon";
            this.dataGridViewCheckBoxColumn2.Name = "dataGridViewCheckBoxColumn2";
            this.dataGridViewCheckBoxColumn2.TrueValue = "true";
            this.dataGridViewCheckBoxColumn2.Width = 41;
            // 
            // dataGridViewCheckBoxColumn3
            // 
            this.dataGridViewCheckBoxColumn3.DataPropertyName = "Tuesday";
            this.dataGridViewCheckBoxColumn3.FalseValue = "false";
            this.dataGridViewCheckBoxColumn3.HeaderText = "Tue";
            this.dataGridViewCheckBoxColumn3.Name = "dataGridViewCheckBoxColumn3";
            this.dataGridViewCheckBoxColumn3.TrueValue = "true";
            this.dataGridViewCheckBoxColumn3.Width = 39;
            // 
            // dataGridViewCheckBoxColumn4
            // 
            this.dataGridViewCheckBoxColumn4.DataPropertyName = "Wednesday";
            this.dataGridViewCheckBoxColumn4.FalseValue = "false";
            this.dataGridViewCheckBoxColumn4.HeaderText = "Wed";
            this.dataGridViewCheckBoxColumn4.Name = "dataGridViewCheckBoxColumn4";
            this.dataGridViewCheckBoxColumn4.TrueValue = "true";
            this.dataGridViewCheckBoxColumn4.Width = 43;
            // 
            // dataGridViewCheckBoxColumn5
            // 
            this.dataGridViewCheckBoxColumn5.DataPropertyName = "Thursday";
            this.dataGridViewCheckBoxColumn5.FalseValue = "false";
            this.dataGridViewCheckBoxColumn5.HeaderText = "Thu";
            this.dataGridViewCheckBoxColumn5.Name = "dataGridViewCheckBoxColumn5";
            this.dataGridViewCheckBoxColumn5.TrueValue = "true";
            this.dataGridViewCheckBoxColumn5.Width = 39;
            // 
            // dataGridViewCheckBoxColumn6
            // 
            this.dataGridViewCheckBoxColumn6.DataPropertyName = "Friday";
            this.dataGridViewCheckBoxColumn6.FalseValue = "false";
            this.dataGridViewCheckBoxColumn6.HeaderText = "Fri";
            this.dataGridViewCheckBoxColumn6.Name = "dataGridViewCheckBoxColumn6";
            this.dataGridViewCheckBoxColumn6.TrueValue = "true";
            this.dataGridViewCheckBoxColumn6.Width = 30;
            // 
            // dataGridViewCheckBoxColumn7
            // 
            this.dataGridViewCheckBoxColumn7.DataPropertyName = "Saturday";
            this.dataGridViewCheckBoxColumn7.FalseValue = "false";
            this.dataGridViewCheckBoxColumn7.HeaderText = "Sat";
            this.dataGridViewCheckBoxColumn7.Name = "dataGridViewCheckBoxColumn7";
            this.dataGridViewCheckBoxColumn7.TrueValue = "true";
            this.dataGridViewCheckBoxColumn7.Width = 35;
            // 
            // dataGridViewCheckBoxColumn8
            // 
            this.dataGridViewCheckBoxColumn8.DataPropertyName = "Sunday";
            this.dataGridViewCheckBoxColumn8.FalseValue = "false";
            this.dataGridViewCheckBoxColumn8.HeaderText = "Sun";
            this.dataGridViewCheckBoxColumn8.Name = "dataGridViewCheckBoxColumn8";
            this.dataGridViewCheckBoxColumn8.TrueValue = "true";
            this.dataGridViewCheckBoxColumn8.Width = 39;
            // 
            // dataGridViewCheckBoxColumn9
            // 
            this.dataGridViewCheckBoxColumn9.DataPropertyName = "January";
            this.dataGridViewCheckBoxColumn9.HeaderText = "Jan";
            this.dataGridViewCheckBoxColumn9.Name = "dataGridViewCheckBoxColumn9";
            this.dataGridViewCheckBoxColumn9.Width = 37;
            // 
            // dataGridViewCheckBoxColumn10
            // 
            this.dataGridViewCheckBoxColumn10.DataPropertyName = "February";
            this.dataGridViewCheckBoxColumn10.HeaderText = "Feb";
            this.dataGridViewCheckBoxColumn10.Name = "dataGridViewCheckBoxColumn10";
            this.dataGridViewCheckBoxColumn10.Width = 38;
            // 
            // dataGridViewCheckBoxColumn11
            // 
            this.dataGridViewCheckBoxColumn11.DataPropertyName = "March";
            this.dataGridViewCheckBoxColumn11.HeaderText = "Mar";
            this.dataGridViewCheckBoxColumn11.Name = "dataGridViewCheckBoxColumn11";
            this.dataGridViewCheckBoxColumn11.Width = 38;
            // 
            // dataGridViewCheckBoxColumn12
            // 
            this.dataGridViewCheckBoxColumn12.DataPropertyName = "April";
            this.dataGridViewCheckBoxColumn12.HeaderText = "Apr";
            this.dataGridViewCheckBoxColumn12.Name = "dataGridViewCheckBoxColumn12";
            this.dataGridViewCheckBoxColumn12.Width = 36;
            // 
            // dataGridViewCheckBoxColumn13
            // 
            this.dataGridViewCheckBoxColumn13.DataPropertyName = "May";
            this.dataGridViewCheckBoxColumn13.HeaderText = "May";
            this.dataGridViewCheckBoxColumn13.Name = "dataGridViewCheckBoxColumn13";
            this.dataGridViewCheckBoxColumn13.Width = 40;
            // 
            // dataGridViewCheckBoxColumn14
            // 
            this.dataGridViewCheckBoxColumn14.DataPropertyName = "June";
            this.dataGridViewCheckBoxColumn14.HeaderText = "June";
            this.dataGridViewCheckBoxColumn14.Name = "dataGridViewCheckBoxColumn14";
            this.dataGridViewCheckBoxColumn14.Width = 45;
            // 
            // dataGridViewCheckBoxColumn15
            // 
            this.dataGridViewCheckBoxColumn15.DataPropertyName = "July";
            this.dataGridViewCheckBoxColumn15.HeaderText = "July";
            this.dataGridViewCheckBoxColumn15.Name = "dataGridViewCheckBoxColumn15";
            this.dataGridViewCheckBoxColumn15.Width = 39;
            // 
            // dataGridViewCheckBoxColumn16
            // 
            this.dataGridViewCheckBoxColumn16.DataPropertyName = "August";
            this.dataGridViewCheckBoxColumn16.HeaderText = "Aug";
            this.dataGridViewCheckBoxColumn16.Name = "dataGridViewCheckBoxColumn16";
            this.dataGridViewCheckBoxColumn16.Width = 39;
            // 
            // dataGridViewCheckBoxColumn17
            // 
            this.dataGridViewCheckBoxColumn17.DataPropertyName = "September";
            this.dataGridViewCheckBoxColumn17.HeaderText = "Sept";
            this.dataGridViewCheckBoxColumn17.Name = "dataGridViewCheckBoxColumn17";
            this.dataGridViewCheckBoxColumn17.Width = 43;
            // 
            // dataGridViewCheckBoxColumn18
            // 
            this.dataGridViewCheckBoxColumn18.DataPropertyName = "October";
            this.dataGridViewCheckBoxColumn18.HeaderText = "Oct";
            this.dataGridViewCheckBoxColumn18.Name = "dataGridViewCheckBoxColumn18";
            this.dataGridViewCheckBoxColumn18.Width = 36;
            // 
            // dataGridViewCheckBoxColumn19
            // 
            this.dataGridViewCheckBoxColumn19.DataPropertyName = "November";
            this.dataGridViewCheckBoxColumn19.HeaderText = "Nov";
            this.dataGridViewCheckBoxColumn19.Name = "dataGridViewCheckBoxColumn19";
            this.dataGridViewCheckBoxColumn19.Width = 39;
            // 
            // dataGridViewCheckBoxColumn20
            // 
            this.dataGridViewCheckBoxColumn20.DataPropertyName = "December";
            this.dataGridViewCheckBoxColumn20.HeaderText = "Dec";
            this.dataGridViewCheckBoxColumn20.Name = "dataGridViewCheckBoxColumn20";
            this.dataGridViewCheckBoxColumn20.Width = 39;
            // 
            // dataGridViewTextBoxColumn11
            // 
            this.dataGridViewTextBoxColumn11.DataPropertyName = "SourcePath";
            this.dataGridViewTextBoxColumn11.HeaderText = "SourcePath";
            this.dataGridViewTextBoxColumn11.Name = "dataGridViewTextBoxColumn11";
            this.dataGridViewTextBoxColumn11.ToolTipText = "Source Files and sub folders to verify against FilePathToCheck and possibly copy";
            this.dataGridViewTextBoxColumn11.Width = 107;
            // 
            // dataGridViewTextBoxColumn12
            // 
            this.dataGridViewTextBoxColumn12.DataPropertyName = "FilePathToCheck";
            this.dataGridViewTextBoxColumn12.HeaderText = "FilePathToCheck";
            this.dataGridViewTextBoxColumn12.Name = "dataGridViewTextBoxColumn12";
            this.dataGridViewTextBoxColumn12.ToolTipText = "File Path to examine and see if corruption, changes to content of the file, or en" +
    "cryption has ruined files (binary compare)";
            this.dataGridViewTextBoxColumn12.Width = 140;
            // 
            // chkCheckMainfolder
            // 
            this.chkCheckMainfolder.DataPropertyName = "CheckMainFolder";
            this.chkCheckMainfolder.HeaderText = "CheckMainFolder";
            this.chkCheckMainfolder.Name = "chkCheckMainfolder";
            this.chkCheckMainfolder.ToolTipText = "Check the Main Folder contents of FilePathToCheck";
            this.chkCheckMainfolder.Width = 123;
            // 
            // chkCheckSubFolders
            // 
            this.chkCheckSubFolders.DataPropertyName = "CheckSubFolders";
            this.chkCheckSubFolders.HeaderText = "CheckSubFolders";
            this.chkCheckSubFolders.Name = "chkCheckSubFolders";
            this.chkCheckSubFolders.ToolTipText = "Check Immediate Sub Folders of FilePathToCheck";
            this.chkCheckSubFolders.Width = 125;
            // 
            // txtExcludeFolders
            // 
            this.txtExcludeFolders.DataPropertyName = "ExcludeFolders";
            this.txtExcludeFolders.HeaderText = "ExcludeFolders";
            this.txtExcludeFolders.Name = "txtExcludeFolders";
            this.txtExcludeFolders.ToolTipText = "Exclude folders from sub folder check and copy separate each folder with semicolo" +
    "n ; no slashes";
            this.txtExcludeFolders.Width = 129;
            // 
            // chkCopySourceFiles
            // 
            this.chkCopySourceFiles.DataPropertyName = "CopySourceFiles";
            this.chkCopySourceFiles.HeaderText = "CopySourceFiles";
            this.chkCopySourceFiles.Name = "chkCopySourceFiles";
            this.chkCopySourceFiles.ToolTipText = "Copy source files to Main Folder in FilePathToCheck path only";
            this.chkCopySourceFiles.Width = 120;
            // 
            // chkCopySourceFilesSubFolders
            // 
            this.chkCopySourceFilesSubFolders.DataPropertyName = "CopySourceFilesSubFolders";
            this.chkCopySourceFilesSubFolders.HeaderText = "CopySourceFilesSubFolders";
            this.chkCopySourceFilesSubFolders.Name = "chkCopySourceFilesSubFolders";
            this.chkCopySourceFilesSubFolders.ToolTipText = "Copies sources files to FilePathToCheck first layer of subfolders if the files do" +
    " not exist in each";
            this.chkCopySourceFilesSubFolders.Width = 192;
            // 
            // chkSendEmail
            // 
            this.chkSendEmail.DataPropertyName = "SendEmailOnFailure";
            this.chkSendEmail.HeaderText = "SendEmailOnFailure";
            this.chkSendEmail.Name = "chkSendEmail";
            this.chkSendEmail.ToolTipText = "On compare failure send summary email regarding changed or missing files each tim" +
    "e this task runs";
            this.chkSendEmail.Width = 143;
            // 
            // chkSendEmailOnSuccess
            // 
            this.chkSendEmailOnSuccess.DataPropertyName = "SendEmailOnSuccess";
            this.chkSendEmailOnSuccess.HeaderText = "SendEmailOnSuccess";
            this.chkSendEmailOnSuccess.Name = "chkSendEmailOnSuccess";
            this.chkSendEmailOnSuccess.ToolTipText = "On success send summary email of summary to notify that the files were compared.";
            this.chkSendEmailOnSuccess.Width = 153;
            // 
            // calendarColumn1
            // 
            this.calendarColumn1.DataPropertyName = "StartDate";
            dataGridViewCellStyle1.Format = "MM/dd/yyyy";
            this.calendarColumn1.DefaultCellStyle = dataGridViewCellStyle1;
            this.calendarColumn1.HeaderText = "StartDate";
            this.calendarColumn1.Name = "calendarColumn1";
            this.calendarColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.calendarColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.calendarColumn1.ToolTipText = "Scheduled Start Date that this task should run";
            this.calendarColumn1.Width = 93;
            // 
            // calendarColumn2
            // 
            this.calendarColumn2.DataPropertyName = "EndDate";
            dataGridViewCellStyle2.Format = "MM/dd/yyyy";
            this.calendarColumn2.DefaultCellStyle = dataGridViewCellStyle2;
            this.calendarColumn2.HeaderText = "EndDate";
            this.calendarColumn2.Name = "calendarColumn2";
            this.calendarColumn2.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.calendarColumn2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.calendarColumn2.ToolTipText = "Scheduled End Date of this compare task";
            this.calendarColumn2.Width = 88;
            // 
            // dataGridViewTextBoxColumn15
            // 
            this.dataGridViewTextBoxColumn15.DataPropertyName = "Comment";
            this.dataGridViewTextBoxColumn15.HeaderText = "Comment";
            this.dataGridViewTextBoxColumn15.Name = "dataGridViewTextBoxColumn15";
            this.dataGridViewTextBoxColumn15.ToolTipText = "Comment for this task";
            this.dataGridViewTextBoxColumn15.Width = 92;
            // 
            // chkDetailedLogging
            // 
            this.chkDetailedLogging.DataPropertyName = "DetailedLogging";
            this.chkDetailedLogging.HeaderText = "DetailedLogging";
            this.chkDetailedLogging.Name = "chkDetailedLogging";
            this.chkDetailedLogging.ToolTipText = "Detailed Error Logging even on successful check of files.";
            this.chkDetailedLogging.Width = 117;
            // 
            // tabControl
            // 
            this.tabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl.Controls.Add(this.tabCompare);
            this.tabControl.Controls.Add(this.tabFindFiles);
            this.tabControl.Controls.Add(this.tabFilesToFind);
            this.tabControl.Controls.Add(this.tabAudit);
            this.tabControl.Controls.Add(this.tabEvents);
            this.tabControl.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl.Location = new System.Drawing.Point(1, 392);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(777, 169);
            this.tabControl.TabIndex = 16;
            // 
            // tabFindFiles
            // 
            this.tabFindFiles.Controls.Add(this.dgvFindFiles);
            this.tabFindFiles.Location = new System.Drawing.Point(4, 25);
            this.tabFindFiles.Name = "tabFindFiles";
            this.tabFindFiles.Padding = new System.Windows.Forms.Padding(3);
            this.tabFindFiles.Size = new System.Drawing.Size(769, 140);
            this.tabFindFiles.TabIndex = 7;
            this.tabFindFiles.Text = "Find Ransomware Files (Off Hours Only)";
            this.tabFindFiles.ToolTipText = "Only run this outside of business hours.  Large file shares can take a long time." +
    "";
            this.tabFindFiles.UseVisualStyleBackColor = true;
            // 
            // dgvFindFiles
            // 
            this.dgvFindFiles.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvFindFiles.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvFindFiles.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgvcoltxtFindFilesID,
            this.dgvcolchkFindFilesEnabled,
            this.dgvcoltxtFindFilesTitle,
            this.dgvcoltxtFindFilesStartTime,
            this.dgvcoltxtFindFilesEndTime,
            this.dgvcolDropDownFindFilesIntervalType,
            this.dgvcoltxtFindFilesInterval,
            this.dataGridViewCheckBoxColumn22,
            this.dataGridViewCheckBoxColumn23,
            this.dataGridViewCheckBoxColumn24,
            this.dataGridViewCheckBoxColumn25,
            this.dataGridViewCheckBoxColumn26,
            this.dataGridViewCheckBoxColumn27,
            this.dataGridViewCheckBoxColumn28,
            this.dataGridViewCheckBoxColumn29,
            this.dataGridViewCheckBoxColumn30,
            this.dataGridViewCheckBoxColumn31,
            this.dataGridViewCheckBoxColumn32,
            this.dataGridViewCheckBoxColumn33,
            this.dataGridViewCheckBoxColumn34,
            this.dataGridViewCheckBoxColumn35,
            this.dataGridViewCheckBoxColumn36,
            this.dataGridViewCheckBoxColumn37,
            this.dataGridViewCheckBoxColumn38,
            this.dataGridViewCheckBoxColumn39,
            this.dataGridViewCheckBoxColumn40,
            this.dgvcoltxtFindFilesFilePathToCheck,
            this.dgvcolchkFindFilesCheckSubFolders,
            this.dgvcoltxtFindFilesExcludeFolders,
            this.dgvcolchkFindFilesSendEmailOnFailure,
            this.dgvcolchkFindFilesSendEmailOnSuccess,
            this.dgvcolcalFindFilesStartDate,
            this.dgvcolcalFindFilesEndDate,
            this.dgvcoltxtFindFilesComment,
            this.dgvcolchkFindFilesDetailedLogging});
            this.dgvFindFiles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvFindFiles.Location = new System.Drawing.Point(3, 3);
            this.dgvFindFiles.Name = "dgvFindFiles";
            this.dgvFindFiles.Size = new System.Drawing.Size(763, 134);
            this.dgvFindFiles.TabIndex = 17;
            this.dgvFindFiles.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvFindFiles_CellDoubleClick);
            this.dgvFindFiles.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dgvFindFiles_CellValidating);
            // 
            // dgvcoltxtFindFilesID
            // 
            this.dgvcoltxtFindFilesID.DataPropertyName = "ID";
            this.dgvcoltxtFindFilesID.HeaderText = "ID";
            this.dgvcoltxtFindFilesID.Name = "dgvcoltxtFindFilesID";
            this.dgvcoltxtFindFilesID.ReadOnly = true;
            this.dgvcoltxtFindFilesID.Width = 46;
            // 
            // dgvcolchkFindFilesEnabled
            // 
            this.dgvcolchkFindFilesEnabled.DataPropertyName = "Enabled";
            this.dgvcolchkFindFilesEnabled.FalseValue = "false";
            this.dgvcolchkFindFilesEnabled.HeaderText = "Enabled";
            this.dgvcolchkFindFilesEnabled.IndeterminateValue = "";
            this.dgvcolchkFindFilesEnabled.Name = "dgvcolchkFindFilesEnabled";
            this.dgvcolchkFindFilesEnabled.TrueValue = "true";
            this.dgvcolchkFindFilesEnabled.Width = 66;
            // 
            // dgvcoltxtFindFilesTitle
            // 
            this.dgvcoltxtFindFilesTitle.DataPropertyName = "Title";
            this.dgvcoltxtFindFilesTitle.HeaderText = "Title";
            this.dgvcoltxtFindFilesTitle.Name = "dgvcoltxtFindFilesTitle";
            this.dgvcoltxtFindFilesTitle.Width = 60;
            // 
            // dgvcoltxtFindFilesStartTime
            // 
            this.dgvcoltxtFindFilesStartTime.DataPropertyName = "Time";
            this.dgvcoltxtFindFilesStartTime.HeaderText = "StartTime";
            this.dgvcoltxtFindFilesStartTime.MaxInputLength = 5;
            this.dgvcoltxtFindFilesStartTime.Name = "dgvcoltxtFindFilesStartTime";
            this.dgvcoltxtFindFilesStartTime.ToolTipText = "When to run or start time for window to run";
            this.dgvcoltxtFindFilesStartTime.Width = 94;
            // 
            // dgvcoltxtFindFilesEndTime
            // 
            this.dgvcoltxtFindFilesEndTime.DataPropertyName = "EndTime";
            this.dgvcoltxtFindFilesEndTime.HeaderText = "EndTime";
            this.dgvcoltxtFindFilesEndTime.MaxInputLength = 5;
            this.dgvcoltxtFindFilesEndTime.Name = "dgvcoltxtFindFilesEndTime";
            this.dgvcoltxtFindFilesEndTime.ToolTipText = "End Time is required for Hourly Interval Type.";
            this.dgvcoltxtFindFilesEndTime.Width = 89;
            // 
            // dgvcolDropDownFindFilesIntervalType
            // 
            this.dgvcolDropDownFindFilesIntervalType.DataPropertyName = "IntervalType";
            this.dgvcolDropDownFindFilesIntervalType.HeaderText = "IntervalType";
            this.dgvcolDropDownFindFilesIntervalType.Items.AddRange(new object[] {
            "Hourly",
            "Daily",
            "Monthly"});
            this.dgvcolDropDownFindFilesIntervalType.Name = "dgvcolDropDownFindFilesIntervalType";
            this.dgvcolDropDownFindFilesIntervalType.Width = 92;
            // 
            // dgvcoltxtFindFilesInterval
            // 
            this.dgvcoltxtFindFilesInterval.DataPropertyName = "Interval";
            this.dgvcoltxtFindFilesInterval.HeaderText = "Interval";
            this.dgvcoltxtFindFilesInterval.Name = "dgvcoltxtFindFilesInterval";
            this.dgvcoltxtFindFilesInterval.ToolTipText = resources.GetString("dgvcoltxtFindFilesInterval.ToolTipText");
            this.dgvcoltxtFindFilesInterval.Width = 79;
            // 
            // dataGridViewCheckBoxColumn22
            // 
            this.dataGridViewCheckBoxColumn22.DataPropertyName = "Monday";
            this.dataGridViewCheckBoxColumn22.FalseValue = "false";
            this.dataGridViewCheckBoxColumn22.HeaderText = "Mon";
            this.dataGridViewCheckBoxColumn22.Name = "dataGridViewCheckBoxColumn22";
            this.dataGridViewCheckBoxColumn22.TrueValue = "true";
            this.dataGridViewCheckBoxColumn22.Width = 41;
            // 
            // dataGridViewCheckBoxColumn23
            // 
            this.dataGridViewCheckBoxColumn23.DataPropertyName = "Tuesday";
            this.dataGridViewCheckBoxColumn23.FalseValue = "false";
            this.dataGridViewCheckBoxColumn23.HeaderText = "Tue";
            this.dataGridViewCheckBoxColumn23.Name = "dataGridViewCheckBoxColumn23";
            this.dataGridViewCheckBoxColumn23.TrueValue = "true";
            this.dataGridViewCheckBoxColumn23.Width = 39;
            // 
            // dataGridViewCheckBoxColumn24
            // 
            this.dataGridViewCheckBoxColumn24.DataPropertyName = "Wednesday";
            this.dataGridViewCheckBoxColumn24.FalseValue = "false";
            this.dataGridViewCheckBoxColumn24.HeaderText = "Wed";
            this.dataGridViewCheckBoxColumn24.Name = "dataGridViewCheckBoxColumn24";
            this.dataGridViewCheckBoxColumn24.TrueValue = "true";
            this.dataGridViewCheckBoxColumn24.Width = 43;
            // 
            // dataGridViewCheckBoxColumn25
            // 
            this.dataGridViewCheckBoxColumn25.DataPropertyName = "Thursday";
            this.dataGridViewCheckBoxColumn25.FalseValue = "false";
            this.dataGridViewCheckBoxColumn25.HeaderText = "Thu";
            this.dataGridViewCheckBoxColumn25.Name = "dataGridViewCheckBoxColumn25";
            this.dataGridViewCheckBoxColumn25.TrueValue = "true";
            this.dataGridViewCheckBoxColumn25.Width = 39;
            // 
            // dataGridViewCheckBoxColumn26
            // 
            this.dataGridViewCheckBoxColumn26.DataPropertyName = "Friday";
            this.dataGridViewCheckBoxColumn26.FalseValue = "false";
            this.dataGridViewCheckBoxColumn26.HeaderText = "Fri";
            this.dataGridViewCheckBoxColumn26.Name = "dataGridViewCheckBoxColumn26";
            this.dataGridViewCheckBoxColumn26.TrueValue = "true";
            this.dataGridViewCheckBoxColumn26.Width = 30;
            // 
            // dataGridViewCheckBoxColumn27
            // 
            this.dataGridViewCheckBoxColumn27.DataPropertyName = "Saturday";
            this.dataGridViewCheckBoxColumn27.FalseValue = "false";
            this.dataGridViewCheckBoxColumn27.HeaderText = "Sat";
            this.dataGridViewCheckBoxColumn27.Name = "dataGridViewCheckBoxColumn27";
            this.dataGridViewCheckBoxColumn27.TrueValue = "true";
            this.dataGridViewCheckBoxColumn27.Width = 35;
            // 
            // dataGridViewCheckBoxColumn28
            // 
            this.dataGridViewCheckBoxColumn28.DataPropertyName = "Sunday";
            this.dataGridViewCheckBoxColumn28.FalseValue = "false";
            this.dataGridViewCheckBoxColumn28.HeaderText = "Sun";
            this.dataGridViewCheckBoxColumn28.Name = "dataGridViewCheckBoxColumn28";
            this.dataGridViewCheckBoxColumn28.TrueValue = "true";
            this.dataGridViewCheckBoxColumn28.Width = 39;
            // 
            // dataGridViewCheckBoxColumn29
            // 
            this.dataGridViewCheckBoxColumn29.DataPropertyName = "January";
            this.dataGridViewCheckBoxColumn29.HeaderText = "Jan";
            this.dataGridViewCheckBoxColumn29.Name = "dataGridViewCheckBoxColumn29";
            this.dataGridViewCheckBoxColumn29.Width = 37;
            // 
            // dataGridViewCheckBoxColumn30
            // 
            this.dataGridViewCheckBoxColumn30.DataPropertyName = "February";
            this.dataGridViewCheckBoxColumn30.HeaderText = "Feb";
            this.dataGridViewCheckBoxColumn30.Name = "dataGridViewCheckBoxColumn30";
            this.dataGridViewCheckBoxColumn30.Width = 38;
            // 
            // dataGridViewCheckBoxColumn31
            // 
            this.dataGridViewCheckBoxColumn31.DataPropertyName = "March";
            this.dataGridViewCheckBoxColumn31.HeaderText = "Mar";
            this.dataGridViewCheckBoxColumn31.Name = "dataGridViewCheckBoxColumn31";
            this.dataGridViewCheckBoxColumn31.Width = 38;
            // 
            // dataGridViewCheckBoxColumn32
            // 
            this.dataGridViewCheckBoxColumn32.DataPropertyName = "April";
            this.dataGridViewCheckBoxColumn32.HeaderText = "Apr";
            this.dataGridViewCheckBoxColumn32.Name = "dataGridViewCheckBoxColumn32";
            this.dataGridViewCheckBoxColumn32.Width = 36;
            // 
            // dataGridViewCheckBoxColumn33
            // 
            this.dataGridViewCheckBoxColumn33.DataPropertyName = "May";
            this.dataGridViewCheckBoxColumn33.HeaderText = "May";
            this.dataGridViewCheckBoxColumn33.Name = "dataGridViewCheckBoxColumn33";
            this.dataGridViewCheckBoxColumn33.Width = 40;
            // 
            // dataGridViewCheckBoxColumn34
            // 
            this.dataGridViewCheckBoxColumn34.DataPropertyName = "June";
            this.dataGridViewCheckBoxColumn34.HeaderText = "June";
            this.dataGridViewCheckBoxColumn34.Name = "dataGridViewCheckBoxColumn34";
            this.dataGridViewCheckBoxColumn34.Width = 45;
            // 
            // dataGridViewCheckBoxColumn35
            // 
            this.dataGridViewCheckBoxColumn35.DataPropertyName = "July";
            this.dataGridViewCheckBoxColumn35.HeaderText = "July";
            this.dataGridViewCheckBoxColumn35.Name = "dataGridViewCheckBoxColumn35";
            this.dataGridViewCheckBoxColumn35.Width = 39;
            // 
            // dataGridViewCheckBoxColumn36
            // 
            this.dataGridViewCheckBoxColumn36.DataPropertyName = "August";
            this.dataGridViewCheckBoxColumn36.HeaderText = "Aug";
            this.dataGridViewCheckBoxColumn36.Name = "dataGridViewCheckBoxColumn36";
            this.dataGridViewCheckBoxColumn36.Width = 39;
            // 
            // dataGridViewCheckBoxColumn37
            // 
            this.dataGridViewCheckBoxColumn37.DataPropertyName = "September";
            this.dataGridViewCheckBoxColumn37.HeaderText = "Sept";
            this.dataGridViewCheckBoxColumn37.Name = "dataGridViewCheckBoxColumn37";
            this.dataGridViewCheckBoxColumn37.Width = 43;
            // 
            // dataGridViewCheckBoxColumn38
            // 
            this.dataGridViewCheckBoxColumn38.DataPropertyName = "October";
            this.dataGridViewCheckBoxColumn38.HeaderText = "Oct";
            this.dataGridViewCheckBoxColumn38.Name = "dataGridViewCheckBoxColumn38";
            this.dataGridViewCheckBoxColumn38.Width = 36;
            // 
            // dataGridViewCheckBoxColumn39
            // 
            this.dataGridViewCheckBoxColumn39.DataPropertyName = "November";
            this.dataGridViewCheckBoxColumn39.HeaderText = "Nov";
            this.dataGridViewCheckBoxColumn39.Name = "dataGridViewCheckBoxColumn39";
            this.dataGridViewCheckBoxColumn39.Width = 39;
            // 
            // dataGridViewCheckBoxColumn40
            // 
            this.dataGridViewCheckBoxColumn40.DataPropertyName = "December";
            this.dataGridViewCheckBoxColumn40.HeaderText = "Dec";
            this.dataGridViewCheckBoxColumn40.Name = "dataGridViewCheckBoxColumn40";
            this.dataGridViewCheckBoxColumn40.Width = 39;
            // 
            // dgvcoltxtFindFilesFilePathToCheck
            // 
            this.dgvcoltxtFindFilesFilePathToCheck.DataPropertyName = "FilePathToCheck";
            this.dgvcoltxtFindFilesFilePathToCheck.HeaderText = "FilePathToCheck";
            this.dgvcoltxtFindFilesFilePathToCheck.Name = "dgvcoltxtFindFilesFilePathToCheck";
            this.dgvcoltxtFindFilesFilePathToCheck.ToolTipText = "File Path to examine recursively and search for ransomware related files.";
            this.dgvcoltxtFindFilesFilePathToCheck.Width = 140;
            // 
            // dgvcolchkFindFilesCheckSubFolders
            // 
            this.dgvcolchkFindFilesCheckSubFolders.DataPropertyName = "CheckSubFolders";
            this.dgvcolchkFindFilesCheckSubFolders.HeaderText = "CheckSubFolders";
            this.dgvcolchkFindFilesCheckSubFolders.Name = "dgvcolchkFindFilesCheckSubFolders";
            this.dgvcolchkFindFilesCheckSubFolders.ToolTipText = "Recursively Check Sub Folders of FilePathToCheck";
            this.dgvcolchkFindFilesCheckSubFolders.Width = 125;
            // 
            // dgvcoltxtFindFilesExcludeFolders
            // 
            this.dgvcoltxtFindFilesExcludeFolders.DataPropertyName = "ExcludeFolders";
            this.dgvcoltxtFindFilesExcludeFolders.HeaderText = "ExcludeFolders";
            this.dgvcoltxtFindFilesExcludeFolders.Name = "dgvcoltxtFindFilesExcludeFolders";
            this.dgvcoltxtFindFilesExcludeFolders.ToolTipText = "Exclude folders from sub folder check and copy separate each folder with semicolo" +
    "n ; no slashes.  All folders at any depth with this name will be excluded";
            this.dgvcoltxtFindFilesExcludeFolders.Width = 129;
            // 
            // dgvcolchkFindFilesSendEmailOnFailure
            // 
            this.dgvcolchkFindFilesSendEmailOnFailure.DataPropertyName = "SendEmailOnFailure";
            this.dgvcolchkFindFilesSendEmailOnFailure.HeaderText = "SendEmailOnFailure";
            this.dgvcolchkFindFilesSendEmailOnFailure.Name = "dgvcolchkFindFilesSendEmailOnFailure";
            this.dgvcolchkFindFilesSendEmailOnFailure.ToolTipText = "On finding ransomware send summary email regarding the files found.";
            this.dgvcolchkFindFilesSendEmailOnFailure.Width = 143;
            // 
            // dgvcolchkFindFilesSendEmailOnSuccess
            // 
            this.dgvcolchkFindFilesSendEmailOnSuccess.DataPropertyName = "SendEmailOnSuccess";
            this.dgvcolchkFindFilesSendEmailOnSuccess.HeaderText = "SendEmailOnSuccess";
            this.dgvcolchkFindFilesSendEmailOnSuccess.Name = "dgvcolchkFindFilesSendEmailOnSuccess";
            this.dgvcolchkFindFilesSendEmailOnSuccess.ToolTipText = "On running send summary email to notify that the files paths were searched.";
            this.dgvcolchkFindFilesSendEmailOnSuccess.Width = 153;
            // 
            // dgvcolcalFindFilesStartDate
            // 
            this.dgvcolcalFindFilesStartDate.DataPropertyName = "StartDate";
            dataGridViewCellStyle3.Format = "MM/dd/yyyy";
            this.dgvcolcalFindFilesStartDate.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvcolcalFindFilesStartDate.HeaderText = "StartDate";
            this.dgvcolcalFindFilesStartDate.Name = "dgvcolcalFindFilesStartDate";
            this.dgvcolcalFindFilesStartDate.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvcolcalFindFilesStartDate.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.dgvcolcalFindFilesStartDate.ToolTipText = "Scheduled Start Date that this task should run";
            this.dgvcolcalFindFilesStartDate.Width = 93;
            // 
            // dgvcolcalFindFilesEndDate
            // 
            this.dgvcolcalFindFilesEndDate.DataPropertyName = "EndDate";
            dataGridViewCellStyle4.Format = "MM/dd/yyyy";
            this.dgvcolcalFindFilesEndDate.DefaultCellStyle = dataGridViewCellStyle4;
            this.dgvcolcalFindFilesEndDate.HeaderText = "EndDate";
            this.dgvcolcalFindFilesEndDate.Name = "dgvcolcalFindFilesEndDate";
            this.dgvcolcalFindFilesEndDate.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvcolcalFindFilesEndDate.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.dgvcolcalFindFilesEndDate.ToolTipText = "Scheduled End Date of this compare task";
            this.dgvcolcalFindFilesEndDate.Width = 88;
            // 
            // dgvcoltxtFindFilesComment
            // 
            this.dgvcoltxtFindFilesComment.DataPropertyName = "Comment";
            this.dgvcoltxtFindFilesComment.HeaderText = "Comment";
            this.dgvcoltxtFindFilesComment.Name = "dgvcoltxtFindFilesComment";
            this.dgvcoltxtFindFilesComment.ToolTipText = "Comment for this task";
            this.dgvcoltxtFindFilesComment.Width = 92;
            // 
            // dgvcolchkFindFilesDetailedLogging
            // 
            this.dgvcolchkFindFilesDetailedLogging.DataPropertyName = "DetailedLogging";
            this.dgvcolchkFindFilesDetailedLogging.HeaderText = "DetailedLogging";
            this.dgvcolchkFindFilesDetailedLogging.Name = "dgvcolchkFindFilesDetailedLogging";
            this.dgvcolchkFindFilesDetailedLogging.ToolTipText = "Detailed error logging of information events.";
            this.dgvcolchkFindFilesDetailedLogging.Width = 117;
            // 
            // tabFilesToFind
            // 
            this.tabFilesToFind.Controls.Add(this.dgvFileFilters);
            this.tabFilesToFind.Location = new System.Drawing.Point(4, 25);
            this.tabFilesToFind.Name = "tabFilesToFind";
            this.tabFilesToFind.Padding = new System.Windows.Forms.Padding(3);
            this.tabFilesToFind.Size = new System.Drawing.Size(769, 140);
            this.tabFilesToFind.TabIndex = 8;
            this.tabFilesToFind.Text = "Find File Filters";
            this.tabFilesToFind.ToolTipText = "Ransomware files to search for via the Find Ransomware Files tab.";
            this.tabFilesToFind.UseVisualStyleBackColor = true;
            // 
            // dgvFileFilters
            // 
            this.dgvFileFilters.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvFileFilters.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvFileFilters.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgvcoltxtFileFiltersID,
            this.dgvcolchkFileFiltersEnabled,
            this.dgvcoltxtFileFiltersTitle,
            this.dgvcoltxtFileFiltersFileFilter,
            this.dgvFindFilesColtxtExcludeFiles,
            this.dropdownfindfilescolObjectType,
            this.dgvcolchkFileFiltersDeleteFilesFound,
            this.dgvcoltxtFileFiltersComment});
            this.dgvFileFilters.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvFileFilters.Location = new System.Drawing.Point(3, 3);
            this.dgvFileFilters.Name = "dgvFileFilters";
            this.dgvFileFilters.Size = new System.Drawing.Size(763, 134);
            this.dgvFileFilters.TabIndex = 18;
            this.dgvFileFilters.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dgvFileFilters_CellValidating);
            // 
            // dgvcoltxtFileFiltersID
            // 
            this.dgvcoltxtFileFiltersID.DataPropertyName = "ID";
            this.dgvcoltxtFileFiltersID.HeaderText = "ID";
            this.dgvcoltxtFileFiltersID.Name = "dgvcoltxtFileFiltersID";
            this.dgvcoltxtFileFiltersID.ReadOnly = true;
            this.dgvcoltxtFileFiltersID.Width = 46;
            // 
            // dgvcolchkFileFiltersEnabled
            // 
            this.dgvcolchkFileFiltersEnabled.DataPropertyName = "Enabled";
            this.dgvcolchkFileFiltersEnabled.FalseValue = "false";
            this.dgvcolchkFileFiltersEnabled.HeaderText = "Enabled";
            this.dgvcolchkFileFiltersEnabled.IndeterminateValue = "";
            this.dgvcolchkFileFiltersEnabled.Name = "dgvcolchkFileFiltersEnabled";
            this.dgvcolchkFileFiltersEnabled.TrueValue = "true";
            this.dgvcolchkFileFiltersEnabled.Width = 66;
            // 
            // dgvcoltxtFileFiltersTitle
            // 
            this.dgvcoltxtFileFiltersTitle.DataPropertyName = "Title";
            this.dgvcoltxtFileFiltersTitle.HeaderText = "Title";
            this.dgvcoltxtFileFiltersTitle.Name = "dgvcoltxtFileFiltersTitle";
            this.dgvcoltxtFileFiltersTitle.Width = 60;
            // 
            // dgvcoltxtFileFiltersFileFilter
            // 
            this.dgvcoltxtFileFiltersFileFilter.DataPropertyName = "FileFilter";
            this.dgvcoltxtFileFiltersFileFilter.HeaderText = "FileFilter";
            this.dgvcoltxtFileFiltersFileFilter.Name = "dgvcoltxtFileFiltersFileFilter";
            this.dgvcoltxtFileFiltersFileFilter.ToolTipText = "File Filter in windows search format (e.g. *, *.*, *file*.txt, file*.txt, file.*)" +
    " ";
            this.dgvcoltxtFileFiltersFileFilter.Width = 86;
            // 
            // dgvFindFilesColtxtExcludeFiles
            // 
            this.dgvFindFilesColtxtExcludeFiles.DataPropertyName = "ExcludeFiles";
            this.dgvFindFilesColtxtExcludeFiles.HeaderText = "ExcludeFiles";
            this.dgvFindFilesColtxtExcludeFiles.Name = "dgvFindFilesColtxtExcludeFiles";
            this.dgvFindFilesColtxtExcludeFiles.ToolTipText = "Semicolon separated list of exact file names to exclude from results. List false " +
    "positive file names from a previous run.";
            this.dgvFindFilesColtxtExcludeFiles.Width = 111;
            // 
            // dropdownfindfilescolObjectType
            // 
            this.dropdownfindfilescolObjectType.DataPropertyName = "ObjectType";
            this.dropdownfindfilescolObjectType.HeaderText = "ObjectType";
            this.dropdownfindfilescolObjectType.Items.AddRange(new object[] {
            "File",
            "Folder",
            "Both"});
            this.dropdownfindfilescolObjectType.Name = "dropdownfindfilescolObjectType";
            this.dropdownfindfilescolObjectType.Width = 87;
            // 
            // dgvcolchkFileFiltersDeleteFilesFound
            // 
            this.dgvcolchkFileFiltersDeleteFilesFound.DataPropertyName = "DeleteFilesFound";
            this.dgvcolchkFileFiltersDeleteFilesFound.HeaderText = "DeleteFilesFound";
            this.dgvcolchkFileFiltersDeleteFilesFound.Name = "dgvcolchkFileFiltersDeleteFilesFound";
            this.dgvcolchkFileFiltersDeleteFilesFound.ToolTipText = "Careful! Delete All Files Found by the File Filter to cleanup and remove ransomwa" +
    "re created files (uncheck this after it is run once)";
            this.dgvcolchkFileFiltersDeleteFilesFound.Width = 124;
            // 
            // dgvcoltxtFileFiltersComment
            // 
            this.dgvcoltxtFileFiltersComment.DataPropertyName = "Comment";
            this.dgvcoltxtFileFiltersComment.HeaderText = "Comment";
            this.dgvcoltxtFileFiltersComment.Name = "dgvcoltxtFileFiltersComment";
            this.dgvcoltxtFileFiltersComment.ToolTipText = "Comment for this task";
            this.dgvcoltxtFileFiltersComment.Width = 92;
            // 
            // tabAudit
            // 
            this.tabAudit.Controls.Add(this.dgvAudit);
            this.tabAudit.Location = new System.Drawing.Point(4, 25);
            this.tabAudit.Name = "tabAudit";
            this.tabAudit.Padding = new System.Windows.Forms.Padding(3);
            this.tabAudit.Size = new System.Drawing.Size(769, 140);
            this.tabAudit.TabIndex = 9;
            this.tabAudit.Text = "Audit Files (Off Hours Only)";
            this.tabAudit.UseVisualStyleBackColor = true;
            // 
            // dgvAudit
            // 
            this.dgvAudit.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvAudit.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAudit.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgvAuditTxtColID,
            this.dataGridViewCheckBoxColumn21,
            this.dgvAuditTxtColTitle,
            this.dgvAuditTxtColStartTime,
            this.dgvAuditTxtColEndTime,
            this.dataGridViewComboBoxColumn2,
            this.dgvAuditTxtColInterval,
            this.dataGridViewCheckBoxColumn41,
            this.dataGridViewCheckBoxColumn42,
            this.dataGridViewCheckBoxColumn43,
            this.dataGridViewCheckBoxColumn44,
            this.dataGridViewCheckBoxColumn45,
            this.dataGridViewCheckBoxColumn46,
            this.dataGridViewCheckBoxColumn47,
            this.dataGridViewCheckBoxColumn48,
            this.dataGridViewCheckBoxColumn49,
            this.dataGridViewCheckBoxColumn50,
            this.dataGridViewCheckBoxColumn51,
            this.dataGridViewCheckBoxColumn52,
            this.dataGridViewCheckBoxColumn53,
            this.dataGridViewCheckBoxColumn54,
            this.dataGridViewCheckBoxColumn55,
            this.dataGridViewCheckBoxColumn56,
            this.dataGridViewCheckBoxColumn57,
            this.dataGridViewCheckBoxColumn58,
            this.dataGridViewCheckBoxColumn59,
            this.dgvAuditTxtColFilePathToCheck,
            this.dataGridViewCheckBoxColumn61,
            this.dgvAuditTxtColExcludeFolders,
            this.dgvAuditTxtColExportCSVPath,
            this.dgvAuditChkColExportUnVerifiedToCSV,
            this.dgvAuditChkColExportVerifiedToCSV,
            this.dgvAuditChkCol1ExportUnknownToCSV,
            this.dataGridViewCheckBoxColumn64,
            this.dataGridViewCheckBoxColumn65,
            this.dgvAuditCalColStartDate,
            this.dgvAuditCalColEndDate,
            this.dgvAuditTxtColComment,
            this.dataGridViewCheckBoxColumn66});
            this.dgvAudit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvAudit.Location = new System.Drawing.Point(3, 3);
            this.dgvAudit.Name = "dgvAudit";
            this.dgvAudit.Size = new System.Drawing.Size(763, 134);
            this.dgvAudit.TabIndex = 17;
            this.dgvAudit.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvAudit_CellDoubleClick);
            this.dgvAudit.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dgvAudit_CellValidating);
            // 
            // dgvAuditTxtColID
            // 
            this.dgvAuditTxtColID.DataPropertyName = "ID";
            this.dgvAuditTxtColID.HeaderText = "ID";
            this.dgvAuditTxtColID.Name = "dgvAuditTxtColID";
            this.dgvAuditTxtColID.ReadOnly = true;
            this.dgvAuditTxtColID.Width = 46;
            // 
            // dataGridViewCheckBoxColumn21
            // 
            this.dataGridViewCheckBoxColumn21.DataPropertyName = "Enabled";
            this.dataGridViewCheckBoxColumn21.FalseValue = "false";
            this.dataGridViewCheckBoxColumn21.HeaderText = "Enabled";
            this.dataGridViewCheckBoxColumn21.IndeterminateValue = "";
            this.dataGridViewCheckBoxColumn21.Name = "dataGridViewCheckBoxColumn21";
            this.dataGridViewCheckBoxColumn21.TrueValue = "true";
            this.dataGridViewCheckBoxColumn21.Width = 66;
            // 
            // dgvAuditTxtColTitle
            // 
            this.dgvAuditTxtColTitle.DataPropertyName = "Title";
            this.dgvAuditTxtColTitle.HeaderText = "Title";
            this.dgvAuditTxtColTitle.Name = "dgvAuditTxtColTitle";
            this.dgvAuditTxtColTitle.Width = 60;
            // 
            // dgvAuditTxtColStartTime
            // 
            this.dgvAuditTxtColStartTime.DataPropertyName = "Time";
            this.dgvAuditTxtColStartTime.HeaderText = "StartTime";
            this.dgvAuditTxtColStartTime.MaxInputLength = 5;
            this.dgvAuditTxtColStartTime.Name = "dgvAuditTxtColStartTime";
            this.dgvAuditTxtColStartTime.Width = 94;
            // 
            // dgvAuditTxtColEndTime
            // 
            this.dgvAuditTxtColEndTime.DataPropertyName = "EndTime";
            this.dgvAuditTxtColEndTime.HeaderText = "EndTime";
            this.dgvAuditTxtColEndTime.MaxInputLength = 5;
            this.dgvAuditTxtColEndTime.Name = "dgvAuditTxtColEndTime";
            this.dgvAuditTxtColEndTime.ToolTipText = "End Time is required for Hourly Interval Type";
            this.dgvAuditTxtColEndTime.Width = 89;
            // 
            // dataGridViewComboBoxColumn2
            // 
            this.dataGridViewComboBoxColumn2.DataPropertyName = "IntervalType";
            this.dataGridViewComboBoxColumn2.HeaderText = "IntervalType";
            this.dataGridViewComboBoxColumn2.Items.AddRange(new object[] {
            "Hourly",
            "Daily",
            "Monthly"});
            this.dataGridViewComboBoxColumn2.Name = "dataGridViewComboBoxColumn2";
            this.dataGridViewComboBoxColumn2.Width = 92;
            // 
            // dgvAuditTxtColInterval
            // 
            this.dgvAuditTxtColInterval.DataPropertyName = "Interval";
            this.dgvAuditTxtColInterval.HeaderText = "Interval";
            this.dgvAuditTxtColInterval.Name = "dgvAuditTxtColInterval";
            this.dgvAuditTxtColInterval.ToolTipText = resources.GetString("dgvAuditTxtColInterval.ToolTipText");
            this.dgvAuditTxtColInterval.Width = 79;
            // 
            // dataGridViewCheckBoxColumn41
            // 
            this.dataGridViewCheckBoxColumn41.DataPropertyName = "Monday";
            this.dataGridViewCheckBoxColumn41.FalseValue = "false";
            this.dataGridViewCheckBoxColumn41.HeaderText = "Mon";
            this.dataGridViewCheckBoxColumn41.Name = "dataGridViewCheckBoxColumn41";
            this.dataGridViewCheckBoxColumn41.TrueValue = "true";
            this.dataGridViewCheckBoxColumn41.Width = 41;
            // 
            // dataGridViewCheckBoxColumn42
            // 
            this.dataGridViewCheckBoxColumn42.DataPropertyName = "Tuesday";
            this.dataGridViewCheckBoxColumn42.FalseValue = "false";
            this.dataGridViewCheckBoxColumn42.HeaderText = "Tue";
            this.dataGridViewCheckBoxColumn42.Name = "dataGridViewCheckBoxColumn42";
            this.dataGridViewCheckBoxColumn42.TrueValue = "true";
            this.dataGridViewCheckBoxColumn42.Width = 39;
            // 
            // dataGridViewCheckBoxColumn43
            // 
            this.dataGridViewCheckBoxColumn43.DataPropertyName = "Wednesday";
            this.dataGridViewCheckBoxColumn43.FalseValue = "false";
            this.dataGridViewCheckBoxColumn43.HeaderText = "Wed";
            this.dataGridViewCheckBoxColumn43.Name = "dataGridViewCheckBoxColumn43";
            this.dataGridViewCheckBoxColumn43.TrueValue = "true";
            this.dataGridViewCheckBoxColumn43.Width = 43;
            // 
            // dataGridViewCheckBoxColumn44
            // 
            this.dataGridViewCheckBoxColumn44.DataPropertyName = "Thursday";
            this.dataGridViewCheckBoxColumn44.FalseValue = "false";
            this.dataGridViewCheckBoxColumn44.HeaderText = "Thu";
            this.dataGridViewCheckBoxColumn44.Name = "dataGridViewCheckBoxColumn44";
            this.dataGridViewCheckBoxColumn44.TrueValue = "true";
            this.dataGridViewCheckBoxColumn44.Width = 39;
            // 
            // dataGridViewCheckBoxColumn45
            // 
            this.dataGridViewCheckBoxColumn45.DataPropertyName = "Friday";
            this.dataGridViewCheckBoxColumn45.FalseValue = "false";
            this.dataGridViewCheckBoxColumn45.HeaderText = "Fri";
            this.dataGridViewCheckBoxColumn45.Name = "dataGridViewCheckBoxColumn45";
            this.dataGridViewCheckBoxColumn45.TrueValue = "true";
            this.dataGridViewCheckBoxColumn45.Width = 30;
            // 
            // dataGridViewCheckBoxColumn46
            // 
            this.dataGridViewCheckBoxColumn46.DataPropertyName = "Saturday";
            this.dataGridViewCheckBoxColumn46.FalseValue = "false";
            this.dataGridViewCheckBoxColumn46.HeaderText = "Sat";
            this.dataGridViewCheckBoxColumn46.Name = "dataGridViewCheckBoxColumn46";
            this.dataGridViewCheckBoxColumn46.TrueValue = "true";
            this.dataGridViewCheckBoxColumn46.Width = 35;
            // 
            // dataGridViewCheckBoxColumn47
            // 
            this.dataGridViewCheckBoxColumn47.DataPropertyName = "Sunday";
            this.dataGridViewCheckBoxColumn47.FalseValue = "false";
            this.dataGridViewCheckBoxColumn47.HeaderText = "Sun";
            this.dataGridViewCheckBoxColumn47.Name = "dataGridViewCheckBoxColumn47";
            this.dataGridViewCheckBoxColumn47.TrueValue = "true";
            this.dataGridViewCheckBoxColumn47.Width = 39;
            // 
            // dataGridViewCheckBoxColumn48
            // 
            this.dataGridViewCheckBoxColumn48.DataPropertyName = "January";
            this.dataGridViewCheckBoxColumn48.HeaderText = "Jan";
            this.dataGridViewCheckBoxColumn48.Name = "dataGridViewCheckBoxColumn48";
            this.dataGridViewCheckBoxColumn48.Width = 37;
            // 
            // dataGridViewCheckBoxColumn49
            // 
            this.dataGridViewCheckBoxColumn49.DataPropertyName = "February";
            this.dataGridViewCheckBoxColumn49.HeaderText = "Feb";
            this.dataGridViewCheckBoxColumn49.Name = "dataGridViewCheckBoxColumn49";
            this.dataGridViewCheckBoxColumn49.Width = 38;
            // 
            // dataGridViewCheckBoxColumn50
            // 
            this.dataGridViewCheckBoxColumn50.DataPropertyName = "March";
            this.dataGridViewCheckBoxColumn50.HeaderText = "Mar";
            this.dataGridViewCheckBoxColumn50.Name = "dataGridViewCheckBoxColumn50";
            this.dataGridViewCheckBoxColumn50.Width = 38;
            // 
            // dataGridViewCheckBoxColumn51
            // 
            this.dataGridViewCheckBoxColumn51.DataPropertyName = "April";
            this.dataGridViewCheckBoxColumn51.HeaderText = "Apr";
            this.dataGridViewCheckBoxColumn51.Name = "dataGridViewCheckBoxColumn51";
            this.dataGridViewCheckBoxColumn51.Width = 36;
            // 
            // dataGridViewCheckBoxColumn52
            // 
            this.dataGridViewCheckBoxColumn52.DataPropertyName = "May";
            this.dataGridViewCheckBoxColumn52.HeaderText = "May";
            this.dataGridViewCheckBoxColumn52.Name = "dataGridViewCheckBoxColumn52";
            this.dataGridViewCheckBoxColumn52.Width = 40;
            // 
            // dataGridViewCheckBoxColumn53
            // 
            this.dataGridViewCheckBoxColumn53.DataPropertyName = "June";
            this.dataGridViewCheckBoxColumn53.HeaderText = "June";
            this.dataGridViewCheckBoxColumn53.Name = "dataGridViewCheckBoxColumn53";
            this.dataGridViewCheckBoxColumn53.Width = 45;
            // 
            // dataGridViewCheckBoxColumn54
            // 
            this.dataGridViewCheckBoxColumn54.DataPropertyName = "July";
            this.dataGridViewCheckBoxColumn54.HeaderText = "July";
            this.dataGridViewCheckBoxColumn54.Name = "dataGridViewCheckBoxColumn54";
            this.dataGridViewCheckBoxColumn54.Width = 39;
            // 
            // dataGridViewCheckBoxColumn55
            // 
            this.dataGridViewCheckBoxColumn55.DataPropertyName = "August";
            this.dataGridViewCheckBoxColumn55.HeaderText = "Aug";
            this.dataGridViewCheckBoxColumn55.Name = "dataGridViewCheckBoxColumn55";
            this.dataGridViewCheckBoxColumn55.Width = 39;
            // 
            // dataGridViewCheckBoxColumn56
            // 
            this.dataGridViewCheckBoxColumn56.DataPropertyName = "September";
            this.dataGridViewCheckBoxColumn56.HeaderText = "Sept";
            this.dataGridViewCheckBoxColumn56.Name = "dataGridViewCheckBoxColumn56";
            this.dataGridViewCheckBoxColumn56.Width = 43;
            // 
            // dataGridViewCheckBoxColumn57
            // 
            this.dataGridViewCheckBoxColumn57.DataPropertyName = "October";
            this.dataGridViewCheckBoxColumn57.HeaderText = "Oct";
            this.dataGridViewCheckBoxColumn57.Name = "dataGridViewCheckBoxColumn57";
            this.dataGridViewCheckBoxColumn57.Width = 36;
            // 
            // dataGridViewCheckBoxColumn58
            // 
            this.dataGridViewCheckBoxColumn58.DataPropertyName = "November";
            this.dataGridViewCheckBoxColumn58.HeaderText = "Nov";
            this.dataGridViewCheckBoxColumn58.Name = "dataGridViewCheckBoxColumn58";
            this.dataGridViewCheckBoxColumn58.Width = 39;
            // 
            // dataGridViewCheckBoxColumn59
            // 
            this.dataGridViewCheckBoxColumn59.DataPropertyName = "December";
            this.dataGridViewCheckBoxColumn59.HeaderText = "Dec";
            this.dataGridViewCheckBoxColumn59.Name = "dataGridViewCheckBoxColumn59";
            this.dataGridViewCheckBoxColumn59.Width = 39;
            // 
            // dgvAuditTxtColFilePathToCheck
            // 
            this.dgvAuditTxtColFilePathToCheck.DataPropertyName = "FilePathToCheck";
            this.dgvAuditTxtColFilePathToCheck.HeaderText = "FilePathToCheck";
            this.dgvAuditTxtColFilePathToCheck.Name = "dgvAuditTxtColFilePathToCheck";
            this.dgvAuditTxtColFilePathToCheck.ToolTipText = "File Path to examine and see if corruption, changes to content of the file, or en" +
    "cryption has ruined files (binary compare)";
            this.dgvAuditTxtColFilePathToCheck.Width = 140;
            // 
            // dataGridViewCheckBoxColumn61
            // 
            this.dataGridViewCheckBoxColumn61.DataPropertyName = "CheckSubFolders";
            this.dataGridViewCheckBoxColumn61.HeaderText = "CheckSubFolders";
            this.dataGridViewCheckBoxColumn61.Name = "dataGridViewCheckBoxColumn61";
            this.dataGridViewCheckBoxColumn61.ToolTipText = "Check Immediate Sub Folders of FilePathToCheck";
            this.dataGridViewCheckBoxColumn61.Width = 125;
            // 
            // dgvAuditTxtColExcludeFolders
            // 
            this.dgvAuditTxtColExcludeFolders.DataPropertyName = "ExcludeFolders";
            this.dgvAuditTxtColExcludeFolders.HeaderText = "ExcludeFolders";
            this.dgvAuditTxtColExcludeFolders.Name = "dgvAuditTxtColExcludeFolders";
            this.dgvAuditTxtColExcludeFolders.ToolTipText = "Exclude folders from sub folder check and copy separate each folder with semicolo" +
    "n ; no slashes";
            this.dgvAuditTxtColExcludeFolders.Width = 129;
            // 
            // dgvAuditTxtColExportCSVPath
            // 
            this.dgvAuditTxtColExportCSVPath.DataPropertyName = "ExportCSVPath";
            this.dgvAuditTxtColExportCSVPath.HeaderText = "ExportCSVPath";
            this.dgvAuditTxtColExportCSVPath.Name = "dgvAuditTxtColExportCSVPath";
            this.dgvAuditTxtColExportCSVPath.Width = 129;
            // 
            // dgvAuditChkColExportUnVerifiedToCSV
            // 
            this.dgvAuditChkColExportUnVerifiedToCSV.DataPropertyName = "ExportUnVerifiedToCSV";
            this.dgvAuditChkColExportUnVerifiedToCSV.HeaderText = "ExportUnVerifiedToCSV";
            this.dgvAuditChkColExportUnVerifiedToCSV.Name = "dgvAuditChkColExportUnVerifiedToCSV";
            this.dgvAuditChkColExportUnVerifiedToCSV.Width = 164;
            // 
            // dgvAuditChkColExportVerifiedToCSV
            // 
            this.dgvAuditChkColExportVerifiedToCSV.DataPropertyName = "ExportVerifiedToCSV";
            this.dgvAuditChkColExportVerifiedToCSV.HeaderText = "ExportVerifiedToCSV";
            this.dgvAuditChkColExportVerifiedToCSV.Name = "dgvAuditChkColExportVerifiedToCSV";
            this.dgvAuditChkColExportVerifiedToCSV.Width = 146;
            // 
            // dgvAuditChkCol1ExportUnknownToCSV
            // 
            this.dgvAuditChkCol1ExportUnknownToCSV.DataPropertyName = "ExportUnknownToCSV";
            this.dgvAuditChkCol1ExportUnknownToCSV.HeaderText = "ExportUnknownToCSV";
            this.dgvAuditChkCol1ExportUnknownToCSV.Name = "dgvAuditChkCol1ExportUnknownToCSV";
            this.dgvAuditChkCol1ExportUnknownToCSV.Width = 156;
            // 
            // dataGridViewCheckBoxColumn64
            // 
            this.dataGridViewCheckBoxColumn64.DataPropertyName = "SendEmailOnFailure";
            this.dataGridViewCheckBoxColumn64.HeaderText = "SendEmailOnFailure";
            this.dataGridViewCheckBoxColumn64.Name = "dataGridViewCheckBoxColumn64";
            this.dataGridViewCheckBoxColumn64.ToolTipText = "On compare failure send summary email regarding changed or missing files each tim" +
    "e this task runs";
            this.dataGridViewCheckBoxColumn64.Width = 143;
            // 
            // dataGridViewCheckBoxColumn65
            // 
            this.dataGridViewCheckBoxColumn65.DataPropertyName = "SendEmailOnSuccess";
            this.dataGridViewCheckBoxColumn65.HeaderText = "SendEmailOnSuccess";
            this.dataGridViewCheckBoxColumn65.Name = "dataGridViewCheckBoxColumn65";
            this.dataGridViewCheckBoxColumn65.ToolTipText = "On success send summary email of summary to notify that the files were compared.";
            this.dataGridViewCheckBoxColumn65.Width = 153;
            // 
            // dgvAuditCalColStartDate
            // 
            this.dgvAuditCalColStartDate.DataPropertyName = "StartDate";
            dataGridViewCellStyle5.Format = "MM/dd/yyyy";
            this.dgvAuditCalColStartDate.DefaultCellStyle = dataGridViewCellStyle5;
            this.dgvAuditCalColStartDate.HeaderText = "StartDate";
            this.dgvAuditCalColStartDate.Name = "dgvAuditCalColStartDate";
            this.dgvAuditCalColStartDate.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvAuditCalColStartDate.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.dgvAuditCalColStartDate.ToolTipText = "Scheduled Start Date that this task should run";
            this.dgvAuditCalColStartDate.Width = 93;
            // 
            // dgvAuditCalColEndDate
            // 
            this.dgvAuditCalColEndDate.DataPropertyName = "EndDate";
            dataGridViewCellStyle6.Format = "MM/dd/yyyy";
            this.dgvAuditCalColEndDate.DefaultCellStyle = dataGridViewCellStyle6;
            this.dgvAuditCalColEndDate.HeaderText = "EndDate";
            this.dgvAuditCalColEndDate.Name = "dgvAuditCalColEndDate";
            this.dgvAuditCalColEndDate.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvAuditCalColEndDate.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.dgvAuditCalColEndDate.ToolTipText = "Scheduled End Date of this compare task";
            this.dgvAuditCalColEndDate.Width = 88;
            // 
            // dgvAuditTxtColComment
            // 
            this.dgvAuditTxtColComment.DataPropertyName = "Comment";
            this.dgvAuditTxtColComment.HeaderText = "Comment";
            this.dgvAuditTxtColComment.Name = "dgvAuditTxtColComment";
            this.dgvAuditTxtColComment.ToolTipText = "Comment for this task";
            this.dgvAuditTxtColComment.Width = 92;
            // 
            // dataGridViewCheckBoxColumn66
            // 
            this.dataGridViewCheckBoxColumn66.DataPropertyName = "DetailedLogging";
            this.dataGridViewCheckBoxColumn66.HeaderText = "DetailedLogging";
            this.dataGridViewCheckBoxColumn66.Name = "dataGridViewCheckBoxColumn66";
            this.dataGridViewCheckBoxColumn66.ToolTipText = "Detailed Error Logging even on successful check of files.";
            this.dataGridViewCheckBoxColumn66.Width = 117;
            // 
            // RansomwareDetectionSystemTray
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "RansomwareDetectionSystemTray";
            this.Text = "RansomwareDetectionSystemTray";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.RansomwareDetectionSystemTray_FormClosing);
            this.Load += new System.EventHandler(this.RansomwareDetectionSystemTray_Load);
            this.SizeChanged += new System.EventHandler(this.RansomwareDetectionSystemTray_SizeChanged);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbRightIcon)).EndInit();
            this.tabEvents.ResumeLayout(false);
            this.tabEvents.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEvents)).EndInit();
            this.tabCompare.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCompare)).EndInit();
            this.tabControl.ResumeLayout(false);
            this.tabFindFiles.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvFindFiles)).EndInit();
            this.tabFilesToFind.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvFileFilters)).EndInit();
            this.tabAudit.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAudit)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem1;
        private System.Diagnostics.EventLog eventLogRansomwareDetection;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtServiceStatus;
        private System.Windows.Forms.Label lblServiceIntervalMinutes;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnSaveApply;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label lblServiceInterval;
        private System.Windows.Forms.TextBox txtServiceInterval;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.PictureBox pbRightIcon;
        protected System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Label lblSMTPHost;
        private System.Windows.Forms.TextBox txtEmailFrom;
        private System.Windows.Forms.Label lblEmailFrom;
        private System.Windows.Forms.TextBox txtSMTPHost;
        private System.Windows.Forms.TextBox txtEmailTo;
        private System.Windows.Forms.Label lblEmailTo;
        private System.Windows.Forms.TextBox txtSMTPPort;
        private System.Windows.Forms.Label lblSMTPPort;
        private System.Windows.Forms.TabPage tabEvents;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.CheckBox chkInformation;
        private System.Windows.Forms.CheckBox chkWarning;
        private System.Windows.Forms.CheckBox chkError;
        private System.Windows.Forms.Button btnRefreshEventLog;
        private System.Windows.Forms.Button btnClearLogs;
        private System.Windows.Forms.DataGridView dgvEvents;
        private System.Windows.Forms.DataGridViewImageColumn dgvEventImage;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvEventType;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvColEventTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvColEventMessage;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvColEventSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvColEventCategory;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvColEventEventID;
        private System.Windows.Forms.TabPage tabCompare;
        private System.Windows.Forms.DataGridView dgvCompare;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabFindFiles;
        private System.Windows.Forms.TabPage tabFilesToFind;
        private System.Windows.Forms.DataGridView dgvFindFiles;
        private System.Windows.Forms.DataGridView dgvFileFilters;
        private System.Windows.Forms.ToolStripMenuItem licenseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem websiteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem authorPostRegardingRansomwareToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sendTestEmailToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem servicesConsoleToolStripMenuItem1;
        private System.Windows.Forms.CheckBox chkSMTPUseSSL;
        private System.Windows.Forms.TextBox txtSMTPPassword;
        private System.Windows.Forms.Label lblSMTPPassword;
        private System.Windows.Forms.CheckBox chkSMTPUseDefaultCredentials;
        private System.Windows.Forms.TextBox txtSMTPUsername;
        private System.Windows.Forms.Label lblSMTPUsername;
        private System.Windows.Forms.Button btnSendTestEmail;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewComboBoxColumn dataGridViewComboBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn2;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn3;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn4;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn5;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn6;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn7;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn8;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn9;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn10;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn11;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn12;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn13;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn14;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn15;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn16;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn17;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn18;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn19;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn20;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn11;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn12;
        private System.Windows.Forms.DataGridViewCheckBoxColumn chkCheckMainfolder;
        private System.Windows.Forms.DataGridViewCheckBoxColumn chkCheckSubFolders;
        private System.Windows.Forms.DataGridViewTextBoxColumn txtExcludeFolders;
        private System.Windows.Forms.DataGridViewCheckBoxColumn chkCopySourceFiles;
        private System.Windows.Forms.DataGridViewCheckBoxColumn chkCopySourceFilesSubFolders;
        private System.Windows.Forms.DataGridViewCheckBoxColumn chkSendEmail;
        private System.Windows.Forms.DataGridViewCheckBoxColumn chkSendEmailOnSuccess;
        private System.Windows.Forms.CalendarColumn calendarColumn1;
        private System.Windows.Forms.CalendarColumn calendarColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn15;
        private System.Windows.Forms.DataGridViewCheckBoxColumn chkDetailedLogging;
        private System.Windows.Forms.ToolStripMenuItem ransomwareDetectionServiceCodePlexPageToolStripMenuItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvcoltxtFindFilesID;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dgvcolchkFindFilesEnabled;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvcoltxtFindFilesTitle;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvcoltxtFindFilesStartTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvcoltxtFindFilesEndTime;
        private System.Windows.Forms.DataGridViewComboBoxColumn dgvcolDropDownFindFilesIntervalType;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvcoltxtFindFilesInterval;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn22;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn23;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn24;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn25;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn26;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn27;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn28;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn29;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn30;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn31;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn32;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn33;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn34;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn35;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn36;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn37;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn38;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn39;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn40;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvcoltxtFindFilesFilePathToCheck;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dgvcolchkFindFilesCheckSubFolders;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvcoltxtFindFilesExcludeFolders;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dgvcolchkFindFilesSendEmailOnFailure;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dgvcolchkFindFilesSendEmailOnSuccess;
        private System.Windows.Forms.CalendarColumn dgvcolcalFindFilesStartDate;
        private System.Windows.Forms.CalendarColumn dgvcolcalFindFilesEndDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvcoltxtFindFilesComment;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dgvcolchkFindFilesDetailedLogging;
        private System.Windows.Forms.ToolStripMenuItem onlineHelpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fileExplorerToolStripMenuItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvcoltxtFileFiltersID;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dgvcolchkFileFiltersEnabled;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvcoltxtFileFiltersTitle;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvcoltxtFileFiltersFileFilter;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvFindFilesColtxtExcludeFiles;
        private System.Windows.Forms.DataGridViewComboBoxColumn dropdownfindfilescolObjectType;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dgvcolchkFileFiltersDeleteFilesFound;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvcoltxtFileFiltersComment;
        private System.Windows.Forms.TabPage tabAudit;
        private System.Windows.Forms.DataGridView dgvAudit;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvAuditTxtColID;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn21;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvAuditTxtColTitle;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvAuditTxtColStartTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvAuditTxtColEndTime;
        private System.Windows.Forms.DataGridViewComboBoxColumn dataGridViewComboBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvAuditTxtColInterval;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn41;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn42;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn43;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn44;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn45;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn46;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn47;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn48;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn49;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn50;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn51;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn52;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn53;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn54;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn55;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn56;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn57;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn58;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn59;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvAuditTxtColFilePathToCheck;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn61;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvAuditTxtColExcludeFolders;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvAuditTxtColExportCSVPath;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dgvAuditChkColExportUnVerifiedToCSV;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dgvAuditChkColExportVerifiedToCSV;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dgvAuditChkCol1ExportUnknownToCSV;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn64;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn65;
        private System.Windows.Forms.CalendarColumn dgvAuditCalColStartDate;
        private System.Windows.Forms.CalendarColumn dgvAuditCalColEndDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvAuditTxtColComment;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn66;
    }
}

