namespace ChatService
{
    using System.ServiceModel;

    public interface IChatCallback
    {
        //[OperationContract(IsOneWay = true)]
        //void ReceiveMessageCallback(Message message, Client receiver); //csak egyszerű paraméterek: bool, string, stb

        //[OperationContract(IsOneWay = true)]
        //void ReceiveFileMessageeCallback(FileMessage fileMessage, Client receiver);

        //[OperationContract(IsOneWay = true)]
        //void ReceiveMessageCallback(Message message, string receiver); 

        //[OperationContract(IsOneWay = true)]
        //void ReceiveFileMessageeCallback(FileMessage fileMessage, string receiver);

        [OperationContract(IsOneWay = true)]
        void ReceiveMessageCallback(string message, string receiver);

        [OperationContract(IsOneWay = true)]
        void ReceiveFileMessageeCallback(byte[] fileMessage, string receiver);

        [OperationContract(IsOneWay = true)]
        void ClientConnectCallback(string name);

        //[OperationContract(IsOneWay = true)]
        //void ClientLeaveCallback(string name);

        //[OperationContract(IsOneWay = true)]
        //void IsWritingCallback(Client client);
    }
}
