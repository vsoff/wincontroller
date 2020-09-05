using System;
using System.Threading;

namespace Vsoff.WC.Core.Common.Workers
{
    public class DefaultWorker : IWorker
    {
        private readonly Action _action;
        private readonly Timer _timer;

        private bool _isBusy;
        private readonly object _locker;

        public DefaultWorker(Action action)
        {
            _action = action ?? throw new ArgumentNullException(nameof(action));

            _locker = new object();
            TimerCallback tm = new TimerCallback(DoWork);
            _timer = new Timer(tm, null, Timeout.Infinite, Timeout.Infinite);
        }

        private void DoWork(object o)
        {
            lock (_locker)
            {
                if (_isBusy)
                    return;
                _isBusy = true;
            }

            _action.Invoke();

            _isBusy = false;
        }

        public void Start(TimeSpan interval, bool startImmediately = true)
        {
            _timer.Change(startImmediately ? TimeSpan.Zero : interval, interval);
        }

        public void Stop()
        {
            _timer.Change(Timeout.Infinite, Timeout.Infinite);
        }

        public void Dispose()
        {
            _timer.Change(Timeout.Infinite, Timeout.Infinite);
            _timer.Dispose();
        }
    }
}
