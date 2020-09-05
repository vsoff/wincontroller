using System.ServiceProcess;
using WinController.ServiceApp;

namespace WinController.Service
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        static void Main()
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new WinControllerService()
            };
            ServiceBase.Run(ServicesToRun);
        }
    }
}
