using System;
using System.Collections.Generic;
using System.Text;

namespace Vsoff.WC.Core.Common.Workers
{
    public interface IWorkerController
    {
        IWorker CreateWorker(Action action);
        IWorker StartWorker(Action action, TimeSpan interval, bool startImmediately = true);
    }
}
