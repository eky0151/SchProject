namespace ChatService
{
    using System.ServiceModel;

    [ServiceContract(SessionMode = SessionMode.Required, CallbackContract = typeof(IChatCallback))]
    public interface IChat
    {
        [OperationContract(IsInitiating = true/*, IsOneWay = true*/)]
        bool Connect(Client client);

        [OperationContract(IsTerminating  = true /*,IsOneWay = true*/)]
        void Disconnect(Client client);

        [OperationContract(/*IsOneWay = true*/)]
        void SendFile(FileMessage fileMessage, Client receiver);

        [OperationContract(/*IsOneWay = true*/)]
        void SendMessage(Message message, Client receiver);

        [OperationContract(/*IsOneWay = true*/)]
        void IsWriting(Client client);
    }
}
