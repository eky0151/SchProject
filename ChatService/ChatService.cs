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

        public IChatCallback CurrentCallBack
        {
            get
            {
                return OperationContext.Current.GetCallbackChannel<IChatCallback>();
            }
        }

        public bool Connect(Client client)
        {
            if (clients.ContainsKey(client))
                return false;
            clients.Add()
        }

        public void Disconnect(Client client)
        {
            throw new NotImplementedException();
        }
    }
}
