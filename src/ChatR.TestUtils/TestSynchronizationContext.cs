using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ChatR.TestUtils
{
    public sealed class TestSynchronizationContext : SynchronizationContext
    {
        public override void Send(SendOrPostCallback d, object state)
        {
            d(state);
        }

        public override void Post(SendOrPostCallback d, object state)
        {
            d(state);
        }
    }
}
