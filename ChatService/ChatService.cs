namespace ChatService
{
    using System;
    using System.Collections.Generic;
    using System.ServiceModel;

    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class ChatService : IChat
    {
        private Dictionary<Client, IChatCallback> clients = new Dictionary<Client, IChatCallback>();

        private static object syncObj = new object();

        private IChatCallback currentCallback = OperationContext.Current.GetCallbackChannel<IChatCallback>();

        public bool Connect(Client client)
        {
            if (clients.ContainsKey(client))
                return false;

            lock(syncObj)
            {
                clients.Add(client, currentCallback);
                currentCallback.ClientConnectCallback(client.Name);
            }

            return true;
        }

        public void Disconnect(Client client)
        {
            if (!clients.ContainsKey(client))
                return;

            lock (syncObj)
            {
                clients.Remove(client);
                currentCallback.ClientLeaveCallback(client.Name);
            }
        }

        public void SendFile(FileMessage fileMessage, Client receiver)
        {
            foreach (var i in clients.Keys)
            {
                if(i.Name == receiver.Name)
                {
                    IChatCallback callback = clients[i];
                    callback.ReceiveFileMessageeCallback(fileMessage, receiver);
                }
            }
        }

        public void SendMessage(Message message, Client receiver)
        {
            foreach (var i in clients.Keys)
            {
                if(i.Name == receiver.Name)
                {
                    IChatCallback callback = clients[i];
                    callback.ReceiveMessageCallback(message, receiver);
                }
            }
        }

        public void IsWriting(Client client)
        {
            throw new NotImplementedException();
        }
    }
}
