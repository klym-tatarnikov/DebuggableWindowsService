using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using System.ServiceProcess;
using System.Threading.Tasks;
using DebuggableWindowsService.Properties;

namespace DebuggableWindowsService
{
    [RunInstaller(true)]
    public partial class ProjectInstaller : Installer
    {
        private readonly ServiceProcessInstaller m_ServiceProcessInstaller;
        private readonly ServiceInstaller m_ServiceInstaller;

        public ProjectInstaller()
        {
            string serviceName;
            string serviceDescription;
            serviceName = Settings.Default.serviceName;
            serviceDescription = Settings.Default.serviceDescription;
            //Installer that installs the process (in this case 'DebuggableWindowsService.exe')
            //There can be only one ServiceProcessInstaller
            m_ServiceProcessInstaller = new ServiceProcessInstaller();
            m_ServiceProcessInstaller.Account = ServiceAccount.LocalSystem;

            //Installer that registers actual Windows Service implementations in the application
            //There may be one or more ServiceInstaller
            m_ServiceInstaller = new ServiceInstaller();
            m_ServiceInstaller.ServiceName = serviceName;
            m_ServiceInstaller.Description = serviceDescription;
            m_ServiceInstaller.StartType = ServiceStartMode.Automatic;
            m_ServiceInstaller.DelayedAutoStart = true;            

            Installers.Add(m_ServiceProcessInstaller);
            Installers.Add(m_ServiceInstaller);

            InitializeComponent();
        }

        protected override void OnBeforeInstall(IDictionary savedState)
        {
            base.OnBeforeInstall(savedState);
        }

        protected override void OnAfterInstall(IDictionary savedState)
        {
            base.OnAfterInstall(savedState);
        }

        protected override void OnBeforeUninstall(IDictionary savedState)
        {
            base.OnBeforeUninstall(savedState);
        }

        protected override void OnAfterUninstall(IDictionary savedState)
        {
            base.OnAfterUninstall(savedState);
        }
    }
}
