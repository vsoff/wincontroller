﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Vsoff.WC.Core.Common.Workers
{
    public interface IWorker : IDisposable
    {
        void Start(TimeSpan interval, bool startImmediately = true);
        void Stop();
    }
}
