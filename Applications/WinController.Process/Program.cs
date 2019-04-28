using Unity;
using Vsoff.WC.Common;

namespace WinController.Process
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
            winController.WaitExit();
        }
    }
}
