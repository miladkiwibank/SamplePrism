using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplePrism.Presentation.Services.Common
{
    public class EventParameters
    {
        public EventParameters(string topic, Action action = null)
        {
            Topic = topic;
            ExpectedAction = action;
        }

        public string Topic { get; private set; }

        public Action ExpectedAction { get; private set; }
    }

    public class EventParameters<TValue> : EventParameters
    {
        public EventParameters(string topic)
            : this(topic, default(TValue))
        {
        }

        public EventParameters(string topic, TValue value, Action action = null)
            : base(topic, action)
        {
            Value = value;
        }

        public TValue Value { get; private set; }
    }
}
