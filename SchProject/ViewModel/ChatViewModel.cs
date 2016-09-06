namespace SchProject.ViewModel
{
    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.CommandWpf;
    using GalaSoft.MvvmLight.Messaging;
    using System;
    using System.Collections.ObjectModel;
    using System.IO;
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows.Media.Imaging;
    using System.ServiceModel;
    using System.Collections.Generic;

    public class ChatViewModel : ViewModelBase
    {
        private string message;
        public string Message
        {
            get { return message; }
            set { Set(ref message, value); }
        }


        private string fullName;
        public string FullName
        {
            get { return fullName; }
            set { Set(ref fullName, value); }
        }

        private string aspClientName;
        Dictionary<string, string[]> questionsByOneUser;

        public ICommand SendMessageCommand
        {
            get
            {
                return new RelayCommand<string>(SendMessage);
            }
        }

        public ICommand LoginWorkerCommand
        {
            get
            {
                return new RelayCommand(LoginWorker, () => fullName != string.Empty );
            }
        }

        public ObservableCollection<object> Messages { get; private set; } = new ObservableCollection<object>();

        private Chatservice.ChatClient client;

        public ChatViewModel()
        {
            //remove to ServiceLocator
            InstanceContext ctx = new InstanceContext(new ChatCallback());
            client  = new Chatservice.ChatClient(ctx);

            fullName = Global.FullName;

            Messenger.Default.Register(this, (SendClientConnect s) => Messages.Add(string.Format(
                "{0} is connected at {1}", s.Name, DateTime.Now.ToShortTimeString())));

            Messenger.Default.Register(this, (SendReceiveMessage s) => Messages.Add(s.Message));
        }

        //when de window loaded we connect to the chatservice
        private async void LoginWorker()
        {
            await client.ConnectAsync(fullName);
            await client.AddWorkerAsync(fullName);
        }

        private void SendMessage(string message)
        {
            
        }

        //get the first message from the chatservice 
        private async void GetFirstMessage()
        {
            Messages.Clear();
            questionsByOneUser = await client.GetQuestionsAsync();
            if (questionsByOneUser == null)
                Messages.Add("No question");
            else
            {
                string[] key = new string[1];
                aspClientName = key[0];
                questionsByOneUser.Keys.CopyTo(key, 0);
                foreach (var i in questionsByOneUser[key[0]])
                {
                    Messages.Add(aspClientName + i);
                }
            }
        }
    }


    public class ChatCallback : Chatservice.IChatCallback
    {
        public void ClientConnectCallback(string name)
        {
            Messenger.Default.Send<SendClientConnect>(new SendClientConnect
            {
                Name = name
            });
        }

        public void ReceiveFileMessageeCallback(byte[] fileMessage, string description)
        {
            Messenger.Default.Send<SendFileMessage>(new SendFileMessage
            {
                Content = fileMessage,
                Description = description
            });
        }

        public void ReceiveMessageCallback(string message, string receiver)
        {
            Messenger.Default.Send<SendReceiveMessage>(new SendReceiveMessage
            {
                Message = message
            });
        }
    }

    public class SendClientConnect
    {
        public string Name { get; set; }
    }

    public class SendReceiveMessage
    {
        public string Message { get; set; }
    }

    public class SendFileMessage
    {
        public byte[] Content { get; set; }
        public string Description { get; set; }
    }
}
