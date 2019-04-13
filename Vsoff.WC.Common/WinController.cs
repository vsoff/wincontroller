using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Vsoff.WC.Common.Messengers;
using Vsoff.WC.Common.Modules.Commands;
using Vsoff.WC.Core.Common.Workers;
using Vsoff.WC.Core.Notifiers;

namespace Vsoff.WC.Common
{
    public interface IWinController
    {
        void Start();
        void Stop();
    }

    public class WinController : IWinController
    {
        private readonly TimeSpan _statusReportInterval = TimeSpan.FromSeconds(30);

        private readonly IMessenger _messenger;
        private readonly IReceiver _reciever;

        public WinController(
            IWorkerController workerController,
            IMessenger messenger,
            IReceiver reciever)
        {
            _messenger = messenger ?? throw new ArgumentNullException(nameof(messenger));
            _reciever = reciever ?? throw new ArgumentNullException(nameof(reciever));
        }

        public void Start()
        {
            _messenger.Send($"Application started at {DateTime.Now}");
            _reciever.Start();
        }

        public void Stop()
        {
            _reciever.Stop();
            _messenger.Send($"Application stopped at {DateTime.Now}");
        }
    }
}
