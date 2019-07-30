using System;

namespace Vsoff.WC.Client.Notifiers
{
    public class ConsoleNotifier : INotifier
    {
        public ConsoleNotifier()
        {
        }

        public void Notify(NotifyMessage msg)
        {
            if (msg.Photo != null && msg.Photo.Length > 0)
                Console.WriteLine($"[{DateTime.Now}] >> [screenshot.jpeg]");
            else
                Console.WriteLine($"[{DateTime.Now}]: {msg.Text}");
        }
    }
}
