namespace SchProject.ViewModel
{
    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.CommandWpf;
    using GalaSoft.MvvmLight.Messaging;
    using System;
    using System.Collections.ObjectModel;
    using System.Windows.Input;
    using System.ServiceModel;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Windows.Forms;
    using System.Threading;
    using System.Threading.Tasks;

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

        public ICommand GetCustomerCommand
        {
            get
            {
                return new RelayCommand(GetFirstMessage
                                       );
            }
        }

        public ICommand SendFileMessageCommand
        {
            get
            {
                return new RelayCommand(UploadFile
                                      );
            }
        }

        public ICommand SendMessageCommand
        {
            get
            {
                return new RelayCommand(SendMessage,
                                       () => message != string.Empty );
            }
        }

        //&&
        //client.State == CommunicationState.Opened

        public ICommand LoginWorkerCommand
        {
            get
            {
                return new RelayCommand(LoginWorker,
                                        () => fullName != string.Empty );
            }
        }

        //for SendClientConnect, SendReceiveMessage, 
        public ObservableCollection<object> Messages { get; private set; } = new ObservableCollection<object>();

        private Chatservice.ChatClient client;

        public ChatViewModel()
        {
            //remove to ServiceLocator
            InstanceContext ctx = new InstanceContext(new ChatCallback());
            client  = new Chatservice.ChatClient(ctx);
            fullName = (Global.FullName == string.Empty ? "Non authenticated user" : Global.FullName);
            Init();           
        }

        private void Init()
        {
            Messenger.Default.Register(this, (SendClientConnect s) => Messages.Add(s));
            Messenger.Default.Register(this, (SendReceiveMessage s) =>
            {
                SendReceiveMessage temp = s;
                temp.Sender = aspClientName;
                Messages.Add(temp);
            });
            Messenger.Default.Register(this, (SendReceiveFileMessage s) =>
            {
                SendReceiveFileMessage temp = s;
                temp.Sender = aspClientName;
                Messages.Add(temp);
            });
        }

        private async void UploadFile()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            ofd.DefaultExt = "*.png";
            ofd.Filter = "Image files (*.png;*.jpeg)|*.png;*.jpeg|All files (*.*)|*.*";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                string filename = ofd.FileName;
                Bitmap b = new Bitmap(filename);
                if(b != null)
                {
                    ImageConverter c = new ImageConverter();
                    await client.SendFileAsync((byte[])c.ConvertTo(b, typeof(byte[])), "Picture", aspClientName);
                }
               
            }
        }

        //when de window loaded we connect to the chatservice
        private async void LoginWorker()
        {
            await client.ConnectAsync(fullName);
            await client.AddWorkerAsync(fullName);
            GetFirstMessage();
        }

        private async void SendMessage()
        {
            await client.SendMessageAsync(message, aspClientName);
            bool flag = await client.CheckUserOnlineAsync(aspClientName);
            if(!flag)
            {
                Messages.Add(new SendReceiveMessage
                {
                    Content = "Client left",
                    Sender = aspClientName
                });

                //Thread.Sleep(500);
                //GetFirstMessage();
            }   
        }

        //get the first message from the chatservice 
        private async void GetFirstMessage()
        {
            Messages.Clear();
            questionsByOneUser = await client.GetFirstUserQuestionsAsync();
            if (questionsByOneUser == null)
                Messages.Add("No question");
            else
            {
                string[] key = new string[1];
                aspClientName = key[0];
                questionsByOneUser.Keys.CopyTo(key, 0);
                foreach (var i in questionsByOneUser[key[0]])
                {
                    Messages.Add(new SendReceiveMessage
                    {
                        Content = i,
                        Sender = aspClientName
                    });
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
                Name = name,
                ConnectTime = DateTime.Now
            });
        }

        public void ReceiveFileMessageeCallback(byte[] fileMessage, string description)
        {
            ImageConverter converter = new ImageConverter();
            Messenger.Default.Send<SendReceiveFileMessage>(new SendReceiveFileMessage
            {
                Content = new Bitmap((System.Drawing.Image)converter.ConvertFrom(fileMessage)),
                Description = description
            });
        }

        public void ReceiveMessageCallback(string message, string receiver)
        {
            Messenger.Default.Send<SendReceiveMessage>(new SendReceiveMessage
            {
                Content = message
            });
        }
    }

    public class SendClientConnect
    {
        public string Name { get; set; }

        public DateTime ConnectTime { get; set; }
    }

    public class SendReceiveMessage
    {
        public string Content { get; set; }

        public string Sender { get; set; }
    }

    public class SendReceiveFileMessage
    {
        public Bitmap Content { get; set; }
        public string Description { get; set; }

        public string Sender { get; set; }
    }
}
