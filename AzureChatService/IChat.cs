﻿namespace AzureChatService
{
    using System.Collections.Generic;
    using System.ServiceModel;

    [ServiceContract(SessionMode = SessionMode.Required, CallbackContract = typeof(IChatCallback))]
    public interface IChat
    {
        [OperationContract(IsInitiating = true, IsOneWay = true)]
        void Connect(string client);

        [OperationContract(IsTerminating = true, IsOneWay = true)]
        void Disconnect(string client);

        [OperationContract(IsOneWay = true)]
        void SendFile(byte[] content, string sender, string description, string receiverName, ClientType clientType);

        [OperationContract(IsOneWay = true)]
        void SendMessage(string message, string sender, string receiverName, ClientType clientType);

        [OperationContract]
        bool IsAnyWorker();

        [OperationContract]
        void AddWorker(string name);

        [OperationContract]
        void RemoveWorker(string name);

        [OperationContract]
        Dictionary<string, List<string>> GetFirstUserQuestions();

        [OperationContract]
        bool CheckUserOnline(string clientName);

    }
}
