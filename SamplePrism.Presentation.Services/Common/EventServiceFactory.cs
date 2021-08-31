using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamplePrism.Presentation.Services.Common
{
    public static class EventServiceFactory
    {
        private static EventAggregator m_eventService;
        private static readonly object m_syncRoot = new object();

        public static EventAggregator EventService
        {
            get
            {
                lock (m_syncRoot)
                {
                    if (m_eventService == null)
                        m_eventService = new EventAggregator();
                    return m_eventService;
                }
            }
        }
    }
}
