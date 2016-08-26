using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ChatService
{
    [DataContract]
    public class Message
    {
        [DataMember]
        public string Sender { get; set; }

        public string Content { get; set; }

        public DateTime TimeSended { get; set; }
    }
}
