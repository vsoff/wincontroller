using System.ComponentModel;
using System.ServiceProcess;

namespace WinController.Service
{
    [RunInstaller(true)]
    public partial class WinControllerInstaller : System.Configuration.Install.Installer
    {
        public WinControllerInstaller()
        {
            InitializeComponent();
            var processInstaller = new ServiceProcessInstaller {Account = ServiceAccount.LocalSystem};
            var serviceInstaller = new ServiceInstaller
            {
                StartType = ServiceStartMode.Automatic,
                ServiceName = "WinControllerService"
            };
            Installers.Add(processInstaller);
            Installers.Add(serviceInstaller);
        }
    }
}