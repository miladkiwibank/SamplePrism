using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SimplePrism.Presentation.Services.Common
{
    public static class EventServiceExtensions
    {
        private static void Publish<TEventsubject>(this TEventsubject eventArgs, string eventTopic, Action expectedAction)
        {
            var topicEvent = EventServiceFactory.EventService.GetEvent<GenericEvent<TEventsubject>>();
            topicEvent.Publish(new EventParameters<TEventsubject>(eventTopic, eventArgs, expectedAction));
        }

        public static void PublishEvent<TEventsubject>(this TEventsubject eventArgs, string eventTopic)
        {
            PublishEvent(eventArgs, eventTopic, false);
        }

        public static void PublishEvent<TEventsubject>(this TEventsubject eventArgs, string eventTopic, bool wait)
        {
            if (Application.Current == null) return;
            if (wait)
                Application.Current.Dispatcher.Invoke(new PublishEventDelegate<TEventsubject>(Publish), eventArgs, null);
            else
                Application.Current.Dispatcher.BeginInvoke(new PublishEventDelegate<TEventsubject>(Publish), eventArgs, eventTopic, null);
        }

        //public static void PublishIdEvent(int id, string eventTopic)
        //{
        //    ExtensionMethods.PublishIdEvent(id, eventTopic, null);
        //}

        //public static void PublishIdEvent(int id, string eventTopic, Action expectedAction)
        //{
        //    Application.Current.Dispatcher.BeginInvoke(new PublishEventDelegate<int>(InternalPublishIdEvent), id, eventTopic, expectedAction);
        //}

        //private static void InternalPublishIdEvent(int id, string eventTopic, Action expectedAction)
        //{
        //    var topicEvent = EventServiceFactory.EventService.GetEvent<GenericIdEvent>();
        //    topicEvent.Publish(new EventParameters<int>(eventTopic, id, expectedAction));
        //}
    }
}
