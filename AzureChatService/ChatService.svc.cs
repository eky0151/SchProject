namespace AzureChatService
{
    using System;
    using System.Collections.Generic;
    using System.ServiceModel;

    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class ChatService : IChat
    {
        //string name, IChatCallback for Clients
        private Dictionary<string, IChatCallback> clients = new Dictionary<string, IChatCallback>();

        //if the workers list is empty, the asp client add their messages here
        private Dictionary<string, List<string>> messages = new Dictionary<string, List<string>>();

        //if the workers list is empty, the asp client add their files here
        private Dictionary<string, List<byte[]>> files = new Dictionary<string, List<byte[]>>();

        private static object syncObj = new object();

        //if a wpf client is connected we add to the list
        private List<string> workers = new List<string>();

        private IChatCallback currentCallback
        {
            get { return OperationContext.Current.GetCallbackChannel<IChatCallback>(); }
        }


        public void Connect(string client)
        {
            lock (syncObj)
            {
                if (clients.ContainsKey(client))
                    return;

                clients.Add(client, currentCallback);
                currentCallback.ClientConnectCallback(client); //eg: user Bill joined on 2016.02.30
            }
        }

        public void Disconnect(string client)
        {
            
            lock (syncObj)
            {
                if (!clients.ContainsKey(client))
                    return;

                clients.Remove(client);
            }
        }

        public void SendFile(byte[] content, string description, string receiverName)
        {
            if (clients.ContainsKey(receiverName))
            {
                IChatCallback callback = clients[receiverName];
                callback.ReceiveFileMessageeCallback(content, "File");
            }
        }

        public void SendMessage(string message, string receiverName)
        {
          

            if (clients.ContainsKey(receiverName))
            {
                IChatCallback callback = clients[receiverName];
                callback.ReceiveMessageCallback(message, receiverName);
            }
        }

        public bool IsAnyWorker()
        {
            return workers.Count == 0 ? false : true;
        }

        public void AddWorker(string name)
        {
            workers.Add(name);
        }

        public void RemoveWorker(string name)
        {
            workers.Remove(name);
        }
    }
}
