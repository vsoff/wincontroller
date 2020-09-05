using System;

namespace Vsoff.WC.Core.Common.Workers
{
    public interface IWorkerController
    {
        IWorker CreateWorker(Action action);
        IWorker StartWorker(Action action, TimeSpan interval, bool startImmediately = true);
    }
}
