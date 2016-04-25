using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;


namespace RansomwareDetectionService
{
    [RunInstaller(true)]
    public partial class ProjectInstaller : System.Configuration.Install.Installer
    {
        public ProjectInstaller()
        {
            InitializeComponent();
        }
        public override void Install(IDictionary stateSaver)
        {
            //Forces Service to bring up "Set Service Login" Dialog
            if (this.serviceProcessInstaller1 != null)
            {
                this.serviceProcessInstaller1.Username = null;
                this.serviceProcessInstaller1.Password = null;
                Context.Parameters["USERNAME"] = null;
                Context.Parameters["PASSWORD"] = null;
                Context.Parameters["ACCOUNT"] = "User";
                this.serviceProcessInstaller1.Account = System.ServiceProcess.ServiceAccount.User;
            }
            
            base.Install(stateSaver);
        }

       
        public override void Commit(IDictionary savedState)
        {
            base.Commit(savedState);
            try
            {
                if (System.IO.Directory.Exists("C:\\Program Files (x86)\\Ransomware Detection Service"))
                {
                    System.Diagnostics.Process.Start("C:\\Program Files (x86)\\Ransomware Detection Service\\RansomwareDetectionSystemTray.exe");
                }
                else if (System.IO.Directory.Exists("D:\\Program Files (x86)\\Ransomware Detection Service"))
                {
                    System.Diagnostics.Process.Start("D:\\Program Files (x86)\\Ransomware Detection Service\\RansomwareDetectionSystemTray.exe");
                }
                else if (System.IO.Directory.Exists("E:\\Program Files (x86)\\Ransomware Detection Service"))
                {
                    System.Diagnostics.Process.Start("E:\\Program Files (x86)\\Ransomware Detection Service\\RansomwareDetectionSystemTray.exe");
                }
                else if (System.IO.Directory.Exists("F:\\Program Files (x86)\\Ransomware Detection Service"))
                {
                    System.Diagnostics.Process.Start("F:\\Program Files (x86)\\Ransomware Detection Service\\RansomwareDetectionSystemTray.exe");
                }
                else if (System.IO.Directory.Exists("G:\\Program Files (x86)\\Ransomware Detection Service"))
                {
                    System.Diagnostics.Process.Start("G:\\Program Files (x86)\\Ransomware Detection Service\\RansomwareDetectionSystemTray.exe");
                }
                else if (System.IO.Directory.Exists("H:\\Program Files (x86)\\Ransomware Detection Service"))
                {
                    System.Diagnostics.Process.Start("H:\\Program Files (x86)\\Ransomware Detection Service\\RansomwareDetectionSystemTray.exe");
                }
                else if (System.IO.Directory.Exists("I:\\Program Files (x86)\\Ransomware Detection Service"))
                {
                    System.Diagnostics.Process.Start("I:\\Program Files (x86)\\Ransomware Detection Service\\RansomwareDetectionSystemTray.exe");
                }
                else if (System.IO.Directory.Exists("J:\\Program Files (x86)\\Ransomware Detection Service"))
                {
                    System.Diagnostics.Process.Start("J:\\Program Files (x86)\\Ransomware Detection Service\\RansomwareDetectionSystemTray.exe");
                }
                else if (System.IO.Directory.Exists("K:\\Program Files (x86)\\Ransomware Detection Service"))
                {
                    System.Diagnostics.Process.Start("K:\\Program Files (x86)\\Ransomware Detection Service\\RansomwareDetectionSystemTray.exe");
                }
                else if (System.IO.Directory.Exists("L:\\Program Files (x86)\\Ransomware Detection Service"))
                {
                    System.Diagnostics.Process.Start("L:\\Program Files (x86)\\Ransomware Detection Service\\RansomwareDetectionSystemTray.exe");
                }
                else if (System.IO.Directory.Exists("M:\\Program Files (x86)\\Ransomware Detection Service"))
                {
                    System.Diagnostics.Process.Start("M:\\Program Files (x86)\\Ransomware Detection Service\\RansomwareDetectionSystemTray.exe");
                }
                else if (System.IO.Directory.Exists("N:\\Program Files (x86)\\Ransomware Detection Service"))
                {
                    System.Diagnostics.Process.Start("N:\\Program Files (x86)\\Ransomware Detection Service\\RansomwareDetectionSystemTray.exe");
                }

                System.Diagnostics.Process.Start("http://ransomwaredetectionservice.codeplex.com/documentation");
            }
            catch (Exception)
            {
    
            }
            
        }
    }
}
