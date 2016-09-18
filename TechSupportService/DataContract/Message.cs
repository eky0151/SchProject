using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using Microsoft.ServiceBus.Messaging;

namespace TechSupportService.DataContract
{
    [DataContract]
    public class Message
    {
        [DataMember]
        public string Messag { get; set; }

        [DataMember]
        public string Sender { get; set; }

        public static Message BrokerdMessageToMessage(BrokeredMessage msg)
        {
            var sender = msg.Properties["Sender"];
            return new Message() { Sender = (string)sender, Messag = msg.GetBody<string>() };
        }
    }
}