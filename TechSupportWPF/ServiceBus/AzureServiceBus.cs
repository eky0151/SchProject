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
        private readonly string _messagesSubName = "Messages";
        private readonly string _customerChatPath = "Messages";
        private readonly string _notificationsPath = "Notifications";
        private readonly string _technicianChatPath = "TechnicianChat";

        private string _subName;
        private MessagingFactory _factory;
        private NamespaceManager _namespaceMgr;
        public SubscriptionClient CustomerClient;
        private SubscriptionClient _technicianMessage;
        private SubscriptionClient _subscriptionClient;
        private SubscriptionClient _customerMessageWireTap;
        public event EventHandler<BugEventArgs> BugHandler;
        public event EventHandler<LoginEventArgs> LoginHandler;
        public event EventHandler<MessageEventArgs> MessageHandler;
        public event EventHandler<StatusChangedEventArgs> StatusHandler;
        public event EventHandler<CustomerLoginEventArgs> CustomerLoginHandler;
        public event EventHandler<NewCustomerMessageEventArgs> CustomerMessage;



        public AzureServiceBus()
        {
            CreateManagerAndFactory();
        }

        private void CreateManagerAndFactory()
        {
            _subName = Guid.NewGuid().ToString();

            string connectionString = ConfigurationManager.AppSettings["Microsoft.Azure.NotificationHubs.ConnectionString"];
            _factory = MessagingFactory.CreateFromConnectionString(connectionString);
            _namespaceMgr = NamespaceManager.CreateFromConnectionString(connectionString);

            OnMessageOptions options = new OnMessageOptions() { MaxConcurrentCalls = 1, AutoComplete = false };

            CreateSubs(_subName);
            _subscriptionClient = SubscriptionClient.CreateFromConnectionString(connectionString, _notificationsPath, _subName);
            CustomerClient = SubscriptionClient.CreateFromConnectionString(connectionString, _customerChatPath, _messagesSubName);
            _customerMessageWireTap = SubscriptionClient.CreateFromConnectionString(connectionString, _customerChatPath, _subName);
            _subscriptionClient.OnMessage(ProcessMessage, options);
            _customerMessageWireTap.OnMessage(CustomerMessagesTapProcess);
        }

        private void CreateSubs(string subscriptName)
        {
            var description = new SubscriptionDescription(_notificationsPath, subscriptName)
            {
                AutoDeleteOnIdle = TimeSpan.FromMinutes(5)
            };
            var customerDescription = new SubscriptionDescription(_customerChatPath, _messagesSubName)
            {
                AutoDeleteOnIdle = TimeSpan.FromDays(7),
                DefaultMessageTimeToLive = TimeSpan.FromDays(7)
            };
            var wiretapDescription = new SubscriptionDescription(_customerChatPath, subscriptName)
            {
                AutoDeleteOnIdle = TimeSpan.FromMinutes(5)
            };

            _namespaceMgr.CreateSubscription(description);
            _namespaceMgr.CreateSubscription(wiretapDescription);

            if (!_namespaceMgr.SubscriptionExists(_customerChatPath, _messagesSubName))
            {
                _namespaceMgr.CreateSubscription(customerDescription);
            }
        }

        private void CustomerMessagesTapProcess(BrokeredMessage message)
        {
            EventHandler<NewCustomerMessageEventArgs> temp = CustomerMessage;
            temp?.Invoke(this, new NewCustomerMessageEventArgs()
            {
                ID = message.Properties["Group"].ToString(),
                Message = message.GetBody<string>()
            });
            CompleteMessagesSafe(message);
        }

        private void ProcessMessage(BrokeredMessage message)
        {
            switch (message.ContentType)
            {
                case "Login":
                    {
                        EventHandler<LoginEventArgs> temp = LoginHandler;
                        temp?.Invoke(this, new LoginEventArgs() { FullName = message.GetBody<string>() });
                    }
                    break;
                case "Status":
                    {
                        EventHandler<StatusChangedEventArgs> temp = StatusHandler;
                        if (temp != null)
                        {
                            var msg = message.GetBody<StatusChanged>();
                            temp.Invoke(this, new StatusChangedEventArgs() { Username = msg.Username, Status = msg.NewStatus });
                        }

                    }
                    break;
                case "CustomerLogin":
                    {
                        EventHandler<CustomerLoginEventArgs> temp = CustomerLoginHandler;
                        if (temp != null)
                        {
                            var msg = message.GetBody<CustomerData>();
                            temp.Invoke(this, new CustomerLoginEventArgs() { Customer = msg });
                        }
                    }
                    break;
                case "Bug":
                    {
                        EventHandler<BugEventArgs> temp = BugHandler;
                        if (temp != null)
                        {
                            var msg = message.GetBody<string>();
                            temp.Invoke(this, new BugEventArgs() { Message = msg });
                        }
                    }
                    break;
            }
            CompleteMessagesSafe(message);
        }

        public void DeleteSubs()
        {

            CustomerClient.Close();
            _technicianMessage?.Close();
            _subscriptionClient.Close();
            _customerMessageWireTap.Close();
            _namespaceMgr.DeleteSubscription(_customerChatPath, _subName);
            _namespaceMgr.DeleteSubscription(_notificationsPath, _subName);

        }

        public int GetMessagesCount()
        {
            return (int)_namespaceMgr.GetSubscription(_customerChatPath, _messagesSubName).MessageCount;
        }

        public async void MessagesInit(string username)
        {
            _technicianMessage = await Task.Factory.StartNew(() =>
            {
                if (!_namespaceMgr.SubscriptionExists(_technicianChatPath, username))
                {
                    var desc = new SubscriptionDescription(_technicianChatPath, username)
                    {
                        AutoDeleteOnIdle = TimeSpan.FromDays(3),
                        DefaultMessageTimeToLive = TimeSpan.FromDays(3),
                        MaxDeliveryCount = 100
                    };
                    _namespaceMgr.CreateSubscription(desc, new SqlFilter($" Username = '{username}' "));
                }
                return _factory.CreateSubscriptionClient(_technicianChatPath, username);
            });
            OnMessageOptions options = new OnMessageOptions() { MaxConcurrentCalls = 1, AutoComplete = false };
            _technicianMessage.OnMessage(RecieveMessage, options);
            MessageHandler += SimpleIoc.Default.GetInstance<Notifications>().ShowMessageAsync;
        }

        private void RecieveMessage(BrokeredMessage message)
        {
            EventHandler<MessageEventArgs> temp = MessageHandler;
            temp?.Invoke(this,
                new MessageEventArgs(message.Properties["Sender"].ToString(), message.GetBody<string>()));
            CompleteMessagesSafe(message);
        }

        private void CompleteMessagesSafe(BrokeredMessage msg)
        {
            try
            {
                msg.Complete();
            }
            catch (OperationCanceledException)
            {
                //LOG
                Console.WriteLine(Properties.Resources.AzureServiceBus_CompleteMessagesSafe_Failed_to_mark_message_as_completed);
            }
        }
    }
}
