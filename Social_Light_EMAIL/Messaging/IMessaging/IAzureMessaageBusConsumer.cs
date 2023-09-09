using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Social_Light_EMAIL.Messaging.IMessaging
{
    public interface IAzureMessageBusConsumer
    {
        Task Start();
        Task Stop();
    }
}