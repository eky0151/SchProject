using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace ChatService
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class ChatService : IChat
    {
        private Dictionary<Client, IChatCallback> clients = new Dictionary<Client, IChatCallback>();

        private static object sync = new object();

        private IChatCallback currentCallback;

        public bool Connect(Client client)
        {
            if (clients.ContainsKey(client))
                return false;
            lock(sync)
            {
                clients.Add(client, currentCallback);
                currentCallback.ClientConnect(client.Name);
            }
            return true;
        }

        public void Disconnect(Client client)
        {
            throw new NotImplementedException();
        }
    }
}
