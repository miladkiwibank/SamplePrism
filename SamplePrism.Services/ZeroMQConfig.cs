using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetMQ;
using NetMQ.Sockets;

namespace SimplePrism.Services
{
    public static class ZeroMQConfig
    {
        //private static ConcurrentDictionary<(string, ushort), INetMQSocket> m_sockets
        //    = new ConcurrentDictionary<(string, ushort), INetMQSocket>();

        public static RequestSocket CreateClientSocket(string ip, ushort port)
        {
            //if (m_sockets.ContainsKey((ip, port)))
            //    return m_sockets[(ip, port)];
            return new RequestSocket($"tcp://{ip}:{port}");
        }

        public static ResponseSocket CreateServerSocket(ushort port)
        {
            //if (m_sockets.ContainsKey(("*", port)))
            //    return m_sockets[("*", port)];
            return new ResponseSocket($"tcp://*:{port}");
        }

        public static PublisherSocket CreatePublisherSocket(ushort port)
        {
            var publisher = new PublisherSocket();
            publisher.Bind($"tcp://*:{port}");

            return publisher;
        }

        public static SubscriberSocket CreateSubscriber(string ip, ushort port, string topic)
        {
            var subscriber = new SubscriberSocket($"tcp://{ip}:{port}");
            subscriber.Subscribe(topic);
            return subscriber;
        }

        //        var subscriber = ZeroMQConfig.CreateSubscriber("127.0.0.1", 8801, "Test");
        //        ThreadPool.QueueUserWorkItem(callBack =>
        //            {
        //                while (true)
        //                {
        //                    var msg = subscriber.ReceiveFrameString();
        //        Console.WriteLine(msg);
        //                }
        //});
    }
}
