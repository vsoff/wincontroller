using System;
using Vsoff.WC.Core.Common;
using Vsoff.WC.Core.Notifiers;

namespace Vsoff.WC.Common.Notifiers
{
    public class ConsoleNotifier : INotifier
    {
        public void Notify(NotifyMessage msg)
        {
            if (msg.Photo != null && msg.Photo.Length > 0)
                Console.WriteLine($"[{DateTime.Now}] >> [screenshot.jpeg]");
            else
                Console.WriteLine($"[{DateTime.Now}]: {msg.Text}");
        }
    }
}
