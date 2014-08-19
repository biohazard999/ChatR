using System.Collections.Generic;
using System.Threading;

namespace ChatR.TestUtils
{
    public class AutoResetList
    {
        private readonly Dictionary<string, AutoResetEvent> _items = new Dictionary<string, AutoResetEvent>();

        public AutoResetList(IEnumerable<string> items)
        {
            foreach (var item in items)
            {
                _items[item] = new AutoResetEvent(false);
            }
        }

        public void Set(string key)
        {
            _items[key].Set();
        }

        public void WaitOne(int timeout)
        {
            foreach (var item in _items)
                item.Value.WaitOne(timeout);
        }

        public void WaitOne()
        {
            foreach (var item in _items)
                item.Value.WaitOne();
        }
    }
}