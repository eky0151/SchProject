using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;
using TechSupportService.DataContract;

namespace TechSupportService
{
    public static class AzureServiceBus
    {
        private static object _sync = new object();

        private static TopicClient _topicClient;
        private static MessagingFactory _factory;
        private static NamespaceManager _namespaceMgr;
        private static TopicClient _technicianMessage;
        private static readonly string _topicPath = "Notifications";
        private static readonly string _technicianChatPath = "TechnicianChat";

        static AzureServiceBus()
        {
            CreateManagerAndFactory();
            InitTechnicianMessages();
        }

        private static void CreateManagerAndFactory()
        {
            string connectionString =
                ConfigurationManager.AppSettings["Microsoft.ServiceBus.ConnectionString"];

            _namespaceMgr = NamespaceManager.CreateFromConnectionString(connectionString);
            _factory = MessagingFactory.CreateFromConnectionString(connectionString);
            if (!_namespaceMgr.TopicExists(_topicPath))
            {
                _namespaceMgr.CreateTopic(_topicPath);
            }
            _topicClient = _factory.CreateTopicClient(_topicPath);
        }
        private static async void InitTechnicianMessages()
        {
            _technicianMessage = await Task.Factory.StartNew(() =>
            {
                if (!_namespaceMgr.TopicExists(_technicianChatPath))
                {
                    _namespaceMgr.CreateTopic(_technicianChatPath);
                }
                return _factory.CreateTopicClient(_technicianChatPath);
            });
        }

        public static void SendStatusNotification(string username, Status status)
        {
            BrokeredMessage msg = new BrokeredMessage(new StatusChanged() { NewStatus = status, Username = username });
            msg.ContentType = "Status";
            SendMessage(msg);
        }

        public static void SendWorkerLoginData(string fullname)
        {
            BrokeredMessage msg = new BrokeredMessage(fullname);
            msg.ContentType = "Login";
            SendMessage(msg);

        }

        public static void SendAppBugNotification(string message)
        {
            BrokeredMessage msg = new BrokeredMessage(message);
            msg.ContentType = "Bug";
            SendMessage(msg);
        }

        public static void SendCustomerLoginNotification(CustomerData data)
        {
            BrokeredMessage msg = new BrokeredMessage(data);
            msg.ContentType = "CustomerLogin";
            SendMessage(msg);
        }

        public static async Task SendMessageToTechnician(string username,string sender, string message)
        {
            if (!_namespaceMgr.SubscriptionExists(_technicianChatPath, username))
            {
                var desc = new SubscriptionDescription(_technicianChatPath, username) { AutoDeleteOnIdle = TimeSpan.FromDays(3), DefaultMessageTimeToLive = TimeSpan.FromDays(3),MaxDeliveryCount = 100};
                _namespaceMgr.CreateSubscription(desc, new SqlFilter($" Username = '{username}' "));
            }
            await SendMessage(username, sender, message);
        }
        public static async Task SendMessage(string username,string sender, string message)
        {
            await Task.Factory.StartNew(() =>
            {
                BrokeredMessage brokeredMessage = new BrokeredMessage(message);
                brokeredMessage.Properties.Add("Username", username);
                brokeredMessage.Properties.Add("Sender", sender);
                _technicianMessage.Send(brokeredMessage);
            });
        }
        
        private static void SendMessage(BrokeredMessage msg)
        {
            lock (_sync)
            {
                try
                {
                    _topicClient.Send(msg);
                }
                catch (Exception)
                {
                    //log error   
                    throw;
                }
            }
        }

        public static List<Message> GetMessages(string username)
        {
            if (_namespaceMgr.SubscriptionExists(_technicianChatPath, username))
            {
                SubscriptionClient subClient = _factory.CreateSubscriptionClient(_technicianChatPath, username);
                var cucc = subClient.ReceiveBatch(100);
                var brokeredMessages = cucc as BrokeredMessage[] ?? cucc.ToArray();
                foreach (BrokeredMessage brokeredMessage in brokeredMessages)
                {
                    brokeredMessage.Complete();
                }
                return brokeredMessages.ToList().ConvertAll(Message.BrokerdMessageToMessage);
            }
            return new List<Message>();
        }
    }
}