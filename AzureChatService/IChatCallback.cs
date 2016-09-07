namespace AzureChatService
{
    using System.ServiceModel;

    public interface IChatCallback
    {
        [OperationContract(IsOneWay = true)]
        void ReceiveMessageCallback(string message, string sender);

        [OperationContract(IsOneWay = true)]
        void ReceiveFileMessageeCallback(byte[] fileMessage, string description, string sender);

        [OperationContract(IsOneWay = true)]
        void ClientConnectCallback(string name);

    }
}
