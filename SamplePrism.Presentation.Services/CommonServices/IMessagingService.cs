using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SamplePrism.Infrastructure;

namespace SamplePrism.Services
{
    public interface IMessagingService
    {
        void RegisterMessageListener(IMessageListener listener);
        void SendMessage(string command, string value);
        void StartMessagingClient();
        bool CanStartMessagingClient();
        bool IsConnected { get; }
    }
}
