using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SchProject.ViewModel
{
    public class ChatViewModel : ViewModelBase, Chatservice.IChatCallback
    {
        public ICommand SendMessageCommand
        {
            get
            {
                return SendMessageCommand ?? new RelayCommand<string>(SendMessage);
            }
        }

        private string message;
        public string Message
        {
            get { return message; }
            set { Set( ref message, value); }
        }

        public ObservableCollection<object> Messages { get; private set; }


        public ChatViewModel()
        {
            Messages = new ObservableCollection<object>();
        }


        private void SendMessage(string message)
        {

        }

        public void ClientConnectCallback(string name)
        {
            throw new NotImplementedException();
        }

        public void ReceiveFileMessageeCallback(byte[] fileMessage, string description)
        {
            throw new NotImplementedException();
        }

        public void ReceiveMessageCallback(string message, string receiver)
        {
            throw new NotImplementedException();
        }
    }
}
