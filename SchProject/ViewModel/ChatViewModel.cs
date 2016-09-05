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

    public class ChatViewModel : ViewModelBase, Chatservice.IChatCallback
    {
        //for the methods only
        //InstanceContext ctx = new InstanceContext();

        Chatservice.ChatClient client;  /*= new Chatservice.ChatClient(ctx);*/

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

        public ICommand SendMessageCommand
        {
            get
            {
                return new RelayCommand<string>(SendMessage);
                //return SendMessageCommand ?? new RelayCommand<string>(SendMessage);
                //stackoverflow
            }
        }

        public ICommand LoginWorkerCommand
        {
            get
            {
                return new RelayCommand(LoginWorker);
            }
        }

        public ObservableCollection<object> Messages { get; private set; }


        public ChatViewModel()
        {
            Messages = new ObservableCollection<object>();
            Messenger.Default.Register(this, (SendFullNameMessage s) => fullName = s.FullName);
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

        public void ClientConnectCallback(string name)
        {
            Messages.Add(string.Format("{0} is connected at {1}", name, DateTime.Now));
        }

        public void ReceiveFileMessageeCallback(byte[] fileMessage, string description)
        {
            MemoryStream ms = new MemoryStream(fileMessage);
            //Image image = Image.FromStream(ms);
        }

        public void ReceiveMessageCallback(string message, string receiver)
        {
            Messages.Add(message);
        }
    }
}
