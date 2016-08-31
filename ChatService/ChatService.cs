namespace ChatService
{
    using System;
    using System.Collections.Generic;
    using System.ServiceModel;

    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class ChatService : IChat
    {
        //string name, IChatCallback for Clients
        private Dictionary<string, IChatCallback> clients = new Dictionary<string, IChatCallback>();

        //from the wcf host you can get the clients name or sessionId
        //public List<string> LoggedInClients
        //{
        //    get { return new List<string>(clients.Keys); }
        //}
       
        private static object syncObj = new object();

        //public Dictionary<string, EventHandler<ChatEventArgs>> Workers { get; private set; } =
        //    new Dictionary<string, EventHandler<ChatEventArgs>>();

        private IChatCallback currentCallback
        {
            get { return OperationContext.Current.GetCallbackChannel<IChatCallback>(); }
        }

        //public EventHandler<ChatEventArgs> WorkerMessageHandler;


        /// <summary>
        ///  From the asp.net client the logged client join the conversation
        /// </summary>
        /// <param name="clientName">the clients name OR SessionID</param>
        /// <returns></returns>
        public void Connect(string client)
        {
            if (clients.ContainsKey(client))
                return;

            lock(syncObj)
            {
                clients.Add(client, currentCallback);
                currentCallback.ClientConnectCallback(client); //eg: user Bill joined on 2016.02.30
            }
        }

        public void Disconnect(string client)
        {
            if (!clients.ContainsKey(client))
                return;

            lock (syncObj)
            {
                clients.Remove(client);
            }
        }

        #region WPFHOST
        //public void SendFile(byte[] content, string description, string receiverName)
        //{
        //    if(clients.ContainsKey(receiverName)) //from host to service reference
        //    {
        //        IChatCallback callback = clients[receiverName];
        //        callback.ReceiveFileMessageeCallback(content, "File");
        //    }
        //    else if(Workers.ContainsKey(receiverName))
        //    {
        //        EventHandler<ChatEventArgs> hostGet = Workers[receiverName];
        //        if (hostGet != null)
        //            hostGet(this, new ChatEventArgs { Content = content });
        //    }
        //}

        //public void SendMessage(string message, string receiverName)
        //{
        //    if(clients.ContainsKey(receiverName))
        //    {
        //        IChatCallback callback = clients[receiverName];
        //        callback.ReceiveMessageCallback(message, receiverName);
        //    }
        //    else if(Workers.ContainsKey(receiverName))
        //    {
        //        EventHandler<ChatEventArgs> hostGet = Workers[receiverName];
        //        if (hostGet != null)
        //            hostGet(this, new ChatEventArgs { Content = message });
        //    }
        //}
        #endregion

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
    }
}
