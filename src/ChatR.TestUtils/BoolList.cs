using System.Collections.Generic;
using System.Linq;
using FluentAssertions;

namespace ChatR.TestUtils
{
    public class BoolList
    {
        private readonly bool _initialValue;
        private readonly Dictionary<string, bool> _items = new Dictionary<string, bool>();

        public BoolList(bool initialValue)
        {
            _initialValue = initialValue;
        }

        public bool this[string key]
        {
            get { return _items[key]; }
            set { _items[key] = value; }
        }

        public bool ResultValue
        {
            get
            {
                if (_items.Count <= 0)
                    return _initialValue;

                return _items.Values.All(m => m);
            }
        }

        public void Assert(bool value)
        {
            var message = string.Empty;
            foreach (var item in _items)
            {
                if (item.Value != value)
                    message = string.Format("\r\n\tItem '{0}' reported '{1}'.", item.Key, item.Value);
            }

            ResultValue.Should().Be(value, message);
        }
    }
}