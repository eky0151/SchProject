using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;
using SchProject.Resources;
using SchProject.ServiceBus;
using SchProject.TechSupportSecure;
using TechSupportService.DataContract;


namespace SchProject
{
    public class AzureServiceBus
    {
        private readonly string _notificationsPath = "Notifications";
        private readonly string _technicianChatPath = "TechnicianChat";
        private readonly string _customerChatPath = "Messages";
        private readonly string _messagesSubName= "Messages";
        private NamespaceManager NamespaceMgr;
        private MessagingFactory Factory;
        public SubscriptionClient CustomerClient;
        private SubscriptionClient _subscriptionClient;
        private SubscriptionClient _technicianMessage;
        private SubscriptionClient _customerMessageWireTap;
        public event EventHandler<LoginEventArgs> LoginHandler;
        public event EventHandler<StatusChangedEventArgs> StatusHandler;
        public event EventHandler<BugEventArgs> BugHandler;
        public event EventHandler<CustomerLoginEventArgs> CustomerLoginHandler;
        public event EventHandler<MessageEventArgs> MessageHandler;
        public event EventHandler<NewCustomerMessageEventArgs> CustomerMessage;
        private string _subName;


        public AzureServiceBus()
        {
            CreateManagerAndFactory();
        }

        public async void MessagesInit(string username)
        {
            _technicianMessage = await Task.Factory.StartNew(() =>
            {
                if (!NamespaceMgr.SubscriptionExists(_technicianChatPath, username))
                {
                    var desc = new SubscriptionDescription(_technicianChatPath, username)
                    {
                        AutoDeleteOnIdle = TimeSpan.FromDays(3),
                        DefaultMessageTimeToLive = TimeSpan.FromDays(3),
                        MaxDeliveryCount = 100
                    };
                    NamespaceMgr.CreateSubscription(desc, new SqlFilter($" Username = '{username}' "));
                }
                return Factory.CreateSubscriptionClient(_technicianChatPath, username);
            });
            OnMessageOptions options = new OnMessageOptions() { MaxConcurrentCalls = 1, AutoComplete = false };
            _technicianMessage.OnMessage(RecieveMessage, options);
            MessageHandler += SimpleIoc.Default.GetInstance<Notifications>().ShowMessageAsync;
        }

        private void RecieveMessage(BrokeredMessage message)
        {
            EventHandler<MessageEventArgs> temp = MessageHandler;
            if (temp != null)
            {
                temp.Invoke(this, new MessageEventArgs(message.Properties["Username"].ToString(), message.GetBody<string>()));
            }
            message.Complete();
        }

        private void CreateManagerAndFactory()
        {
            _subName= Guid.NewGuid().ToString();
            
            string connectionString =
                ConfigurationManager.AppSettings["Microsoft.Azure.NotificationHubs.ConnectionString"];

            NamespaceMgr = NamespaceManager.CreateFromConnectionString(connectionString);
            Factory = MessagingFactory.CreateFromConnectionString(connectionString);
            try
            {
                CreateSubs(_subName);
                _subscriptionClient = SubscriptionClient.CreateFromConnectionString(connectionString, _notificationsPath, _subName);
                OnMessageOptions options = new OnMessageOptions() { MaxConcurrentCalls = 1, AutoComplete = false };
                _subscriptionClient.OnMessage(ProcessMessage, options);
                CustomerClient = SubscriptionClient.CreateFromConnectionString(connectionString, _customerChatPath, _messagesSubName);
                _customerMessageWireTap = SubscriptionClient.CreateFromConnectionString(connectionString, _customerChatPath, _subName);
                _customerMessageWireTap.OnMessage(Customermessageprocess);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void Customermessageprocess(BrokeredMessage message)
        {
            EventHandler<NewCustomerMessageEventArgs> temp = CustomerMessage;
            temp?.Invoke(this, new NewCustomerMessageEventArgs()
            {
                ID = message.Properties["Group"].ToString(),
                Message = message.GetBody<string>()
            });
        }

        private void CreateSubs(string subscriptName)
        {
            var description = new SubscriptionDescription(_notificationsPath, subscriptName) { AutoDeleteOnIdle = TimeSpan.FromMinutes(5) };
            var customerDescription = new SubscriptionDescription(_customerChatPath, _messagesSubName) { AutoDeleteOnIdle = TimeSpan.FromDays(7), DefaultMessageTimeToLive = TimeSpan.FromDays(7) };
            var wiretapDescription = new SubscriptionDescription(_customerChatPath, subscriptName) { AutoDeleteOnIdle = TimeSpan.FromMinutes(5) };
            NamespaceMgr.CreateSubscription(description);
            NamespaceMgr.CreateSubscription(wiretapDescription);
            if (!NamespaceMgr.SubscriptionExists(_customerChatPath, _messagesSubName))
            {
                NamespaceMgr.CreateSubscription(customerDescription);
            }
        }

        private void ProcessMessage(BrokeredMessage message)
        {
            message.Complete();
            if (message.ContentType == "Login")
            {
                EventHandler<LoginEventArgs> temp = LoginHandler;
                temp?.Invoke(this, new LoginEventArgs() { FullName = message.GetBody<string>() });
            }

            else if (message.ContentType == "Status")
            {
                EventHandler<StatusChangedEventArgs> temp = StatusHandler;
                if (temp != null)
                {
                    var msg = message.GetBody<StatusChanged>();
                    temp.Invoke(this, new StatusChangedEventArgs() { Username = msg.Username, Status = msg.NewStatus });
                }

            }
            else if (message.ContentType == "CustomerLogin")
            {
                EventHandler<CustomerLoginEventArgs> temp = CustomerLoginHandler;
                if (temp != null)
                {
                    var msg = message.GetBody<CustomerData>();
                    temp.Invoke(this, new CustomerLoginEventArgs() { Customer = msg });
                }
            }

            else if (message.ContentType == "Bug")
            {
                EventHandler<BugEventArgs> temp = BugHandler;
                if (temp != null)
                {
                    var msg = message.GetBody<string>();
                    temp.Invoke(this, new BugEventArgs() { Message = msg });
                }
            }  
        }

        public void DeleteSubs()
        {
            _subscriptionClient.Close();
            _customerMessageWireTap.Close();
            _technicianMessage.Close();
            CustomerClient.Close();
            NamespaceMgr.DeleteSubscription(_notificationsPath, _subName);
            NamespaceMgr.DeleteSubscription(_customerChatPath, _subName);
        }
        public int GetMessagesCount()
        {
            var data = NamespaceMgr.GetSubscription(_customerChatPath, _messagesSubName).MessageCount;
            return (int)data;
        }
    }
}
