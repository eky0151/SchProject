using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ChatService
{
    [DataContract]
    public class Client
    {
        [DataMember]
        public string IP { get; set; }

        [DataMember]
        public string Name { get; set; }



    }
}
