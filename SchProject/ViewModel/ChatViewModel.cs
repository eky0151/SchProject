using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace SchProject.ViewModel
{
    public class ChatViewModel : ViewModelBase, Chatservice.IChatCallback
    {
        //for the methods only
        Chatservice.ChatClient client;

        public ICommand SendMessageCommand
        {
            get
            {
                return SendMessageCommand ?? new RelayCommand<string>(SendMessage);
            }
        }

        public ICommand LoginWorkerCommand
        {
            get
            {
                return LoginWorkerCommand ?? new RelayCommand(LoginWorker);
            }
        }

        private string message;
        public string Message
        {
            get { return message; }
            set { Set(ref message, value); }
        }

        public ObservableCollection<object> Messages { get; private set; }

        private string fullName;
        public string FullName
        {
            get { return fullName; }
            set { Set(ref fullName, value); }
        }


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
            throw new NotImplementedException();
        }

        public void ReceiveMessageCallback(string message, string receiver)
        {
            Messages.Add(message);
        }
    }
}
