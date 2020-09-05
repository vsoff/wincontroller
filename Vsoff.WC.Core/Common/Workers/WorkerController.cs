using System;

namespace Vsoff.WC.Core.Common.Workers
{
    public class DefaultWorkerController : IWorkerController
    {
        public IWorker CreateWorker(Action action)
        {
            IWorker worker = new DefaultWorker(action);
            return worker;
        }

        public IWorker StartWorker(Action action, TimeSpan interval, bool startImmediately = true)
        {
            IWorker worker = new DefaultWorker(action);
            worker.Start(interval, startImmediately);
            return worker;
        }
    }
}
