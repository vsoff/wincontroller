using System;
using System.Collections.Generic;
using Vsoff.WC.Common.Workers;
using Vsoff.WC.Domain.Main;

namespace Vsoff.WC.Server.Modules.Messengers
{
    public interface IMessenger
    {
        void Send(NotifyMessage message);
    }

    public class Messenger : IMessenger
    {
        private const int MaxMessagesHandlePerIteration = 5;
        private readonly TimeSpan _messagesHandleInterval = TimeSpan.FromSeconds(1);

        private readonly INotifier _notifier;

        private readonly Queue<NotifyMessage> _messagesQueue;
        private readonly IWorker _worker;

        public Messenger(INotifier notifier,
            IWorkerController workerController)
        {
            if (workerController == null) throw new ArgumentNullException(nameof(workerController));
            _notifier = notifier ?? throw new ArgumentNullException(nameof(notifier));

            _worker = workerController.StartWorker(HandleMessages, _messagesHandleInterval, false);
            _messagesQueue = new Queue<NotifyMessage>();
        }

        private void HandleMessages()
        {
            int i = 0;
            while (_messagesQueue.Count != 0 || i++ >= MaxMessagesHandlePerIteration)
            {
                var message = _messagesQueue.Dequeue();
                try
                {
                    _notifier.Notify(message);
                }
                catch (Exception ex)
                {
                    _messagesQueue.Enqueue(message);
                }
            }
        }

        public void Send(NotifyMessage message)
        {
            _messagesQueue.Enqueue(message);
        }
    }
}