using System;

namespace Vsoff.WC.Core.Common.Workers
{
    public interface IWorker : IDisposable
    {
        void Start(TimeSpan interval, bool startImmediately = true);
        void Stop();
    }
}
