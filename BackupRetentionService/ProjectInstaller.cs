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
    }
}
