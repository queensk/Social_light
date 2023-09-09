using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Social_light_Message_Bus.MessageBus
{
    public interface IMessageBus
    {
        Task PublishMessage(object message, string queue_topic_name);
    }
}