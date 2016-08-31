namespace ChatService
{
    using System.ServiceModel;

    public interface IChatCallback
    {
        [OperationContract(IsOneWay = true)]
        void ReceiveMessageCallback(string message, string receiver);

        [OperationContract(IsOneWay = true)]
        void ReceiveFileMessageeCallback(byte[] fileMessage, string description);

        [OperationContract(IsOneWay = true)]
        void ClientConnectCallback(string name);

    }
}
