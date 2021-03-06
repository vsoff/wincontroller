﻿using System;
using System.Collections.Generic;
using Vsoff.WC.Core.Common;
using Vsoff.WC.Core.Common.Workers;
using Vsoff.WC.Core.Notifiers;

namespace Vsoff.WC.Common.Messengers
{
    public interface IMessenger
    {
        void Send(string text);
        void Send(NotifyMessage message);
    }

    public class Messenger : IMessenger
    {
        private const int MaxMessagesHandlePerIteration = 5;
        private static readonly TimeSpan MessagesHandleInterval = TimeSpan.FromSeconds(3);

        private readonly IWorker _worker;
        private readonly INotifier _notifier;

        private readonly Queue<NotifyMessage> _messagesQueue;

        public Messenger(INotifier notifier,
            IWorkerController workerController)
        {
            _notifier = notifier ?? throw new ArgumentNullException(nameof(notifier));
            _worker = workerController?.StartWorker(HandleMessages, MessagesHandleInterval, false)
                      ?? throw new ArgumentNullException(nameof(workerController));

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

        public void Send(NotifyMessage message) => _messagesQueue.Enqueue(message);

        public void Send(string text) => _messagesQueue.Enqueue(new NotifyMessage {Text = text});
    }
}