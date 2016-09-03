namespace AzureChatService
{
    using System.ServiceModel;

    [ServiceContract(SessionMode = SessionMode.Required, CallbackContract = typeof(IChatCallback))]
    public interface IChat
    {
        [OperationContract(IsInitiating = true, IsOneWay = true)]
        void Connect(string client);

        [OperationContract(IsTerminating = true, IsOneWay = true)]
        void Disconnect(string client);

        [OperationContract(IsOneWay = true)]
        void SendFile(byte[] content, string description, string receiverName);

        [OperationContract(IsOneWay = true)]
        void SendMessage(string message, string receiverName);

        [OperationContract]
        bool IsAnyWorker();

        [OperationContract]
        void AddWorker(string name);

        [OperationContract]
        void RemoveWorker(string name);
    }
}
