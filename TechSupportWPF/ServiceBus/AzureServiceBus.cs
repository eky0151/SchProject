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
        private NamespaceManager NamespaceMgr;
        private MessagingFactory Factory;
        private SubscriptionClient _subscriptionClient;
        private SubscriptionClient _technicianMessage;
        public event EventHandler<LoginEventArgs> LoginHandler;
        public event EventHandler<StatusChangedEventArgs> StatusHandler;
        public event EventHandler<BugEventArgs> BugHandler;
        public event EventHandler<CustomerLoginEventArgs> CustomerLoginHandler;
        public event EventHandler<MessageEventArgs> MessageHandler;


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
                temp.Invoke(this, new MessageEventArgs(message.Properties["Sender"].ToString(), message.GetBody<string>()));
            }
            message.Complete();
        }

        private void CreateManagerAndFactory()
        {
            string subscriptName = Guid.NewGuid().ToString();
            string connectionString =
                ConfigurationManager.AppSettings["Microsoft.Azure.NotificationHubs.ConnectionString"];

            NamespaceMgr = NamespaceManager.CreateFromConnectionString(connectionString);
            Factory = MessagingFactory.CreateFromConnectionString(connectionString);
            try
            {
                var description = new SubscriptionDescription(_notificationsPath, subscriptName) { AutoDeleteOnIdle = TimeSpan.FromMinutes(5) };
                NamespaceMgr.CreateSubscription(description);
                _subscriptionClient = SubscriptionClient.CreateFromConnectionString(connectionString, _notificationsPath, subscriptName);
                OnMessageOptions options = new OnMessageOptions() { MaxConcurrentCalls = 1, AutoComplete = false };
                _subscriptionClient.OnMessageAsync(message => ProcessMessage(message), options);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private async Task ProcessMessage(BrokeredMessage message)
        {
            if (message.ContentType == "Login")
            {
                EventHandler<LoginEventArgs> temp = LoginHandler;
                if (temp != null)
                {
                    temp.Invoke(this, new LoginEventArgs() { FullName = message.GetBody<string>() });
                }
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
            message.Complete();
        }
    }
}
