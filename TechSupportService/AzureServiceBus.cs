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
        private static readonly string _topicPath = "Notifications";
        private static readonly string _technicianChatPath = "TechnicianChat";
        private static NamespaceManager NamespaceMgr;
        private static object _sync = new object();
        private static MessagingFactory Factory;
        private static TopicClient _topicClient;
        private static TopicClient _technicianMessage;

        static AzureServiceBus()
        {
            CreateManagerAndFactory();
            InitTechnicianMessages();
        }

        private static void CreateManagerAndFactory()
        {
            string connectionString =
                ConfigurationManager.AppSettings["Microsoft.ServiceBus.ConnectionString"];

            NamespaceMgr = NamespaceManager.CreateFromConnectionString(connectionString);
            Factory = MessagingFactory.CreateFromConnectionString(connectionString);
            if (!NamespaceMgr.TopicExists(_topicPath))
            {
                NamespaceMgr.CreateTopic(_topicPath);
            }
            _topicClient = Factory.CreateTopicClient(_topicPath);
        }
        private static async void InitTechnicianMessages()
        {
            _technicianMessage = await Task.Factory.StartNew(() =>
            {
                if (!NamespaceMgr.TopicExists(_technicianChatPath))
                {
                    NamespaceMgr.CreateTopic(_technicianChatPath);
                }
                return Factory.CreateTopicClient(_technicianChatPath);
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

        public static async Task SendMessageToTechnician(string username, string message)
        {
            await Task.Factory.StartNew(() =>
            {
                BrokeredMessage brokeredMessage = new BrokeredMessage(message);
                brokeredMessage.Properties.Add("Username", username);
                _technicianMessage.Send(brokeredMessage);
            });
        }
        public static async Task SendMessage(string username, string message)
        {
            await Task.Factory.StartNew(() =>
            {
                BrokeredMessage brokeredMessage = new BrokeredMessage(message);
                brokeredMessage.Properties.Add("Username", username);
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
    }
}