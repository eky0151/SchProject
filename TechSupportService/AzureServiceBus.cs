using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;
using TechSupportService.DataContract;

namespace TechSupportService
{
    public static class AzureServiceBus
    {
        private static readonly string _topicPath = "Notifications";
        private static NamespaceManager NamespaceMgr;
        private static object _sync = new object();
        private static MessagingFactory Factory;
        private static TopicClient _topicClient;

        static AzureServiceBus()
        {
            CreateManagerAndFactory();
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