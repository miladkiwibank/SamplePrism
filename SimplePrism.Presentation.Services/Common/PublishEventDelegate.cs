using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplePrism.Presentation.Services.Common
{
    public delegate void PublishEventDelegate<in TEventSubject>(TEventSubject eventArgs, string eventTopic, Action expectedAction);
}
