using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Unity;
using Vsoff.WC.Common;
using Vsoff.WC.Common.Modules.System;

namespace WinController.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            IUnityContainer container = new UnityContainer();
            container.AddExtension(new Diagnostic());
            WinControllerModule.Register(container);

            var winController = container.Resolve<IWinController>();
            winController.Start();

            new ManualResetEvent(false).WaitOne();

            winController.Stop();
        }
    }
}
