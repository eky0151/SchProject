using System.Runtime.Serialization;

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
