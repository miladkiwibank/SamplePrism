using SamplePrism.Presentation.Services.Common;

namespace SamplePrism.Presentation.Common
{
    public static class MessageProcessor
    {
        public static void ProcessMessage(string message)
        {
            new Message(message).PublishEvent(EventTopicNames.MessageReceivedEvent);
        }
    }
}
