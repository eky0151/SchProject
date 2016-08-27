using System;
using System.Runtime.Serialization;

namespace ChatService
{
    [DataContract]
    public class Message
    {
        [DataMember]
        public string Sender { get; set; }

        [DataMember]
        public string Content { get; set; }

        [DataMember]
        public DateTime TimeSended { get; set; }
    }
}
