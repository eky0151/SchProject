namespace ChatService
{
    using System.Runtime.Serialization;

    [DataContract]
    public class Client
    {
        [DataMember]
        public string IP { get; set; }

        [DataMember]
        public string Name { get; set; }
    }
}
