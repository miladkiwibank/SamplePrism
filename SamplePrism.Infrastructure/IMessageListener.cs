using System;

namespace SamplePrism.Infrastructure
{
    public interface IMessageListener
    {
        string Key { get; }
        void ProcessMessage(string message);
    }
}
