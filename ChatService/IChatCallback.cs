namespace ChatService
{
    using System.ServiceModel;

    public interface IChatCallback
    {
        [OperationContract(IsOneWay = true)]
        void ReceiveMessageCallback(Message message, Client receiver);

        [OperationContract(IsOneWay = true)]
        void ReceiveFileMessageeCallback(FileMessage fileMessage, Client receiver);

        [OperationContract(IsOneWay = true)]
        void ClientConnectCallback(string name);

        [OperationContract(IsOneWay = true)]
        void ClientLeaveCallback(string name);

        [OperationContract(IsOneWay = true)]
        void IsWritingCallback(Client client);
    }
}
