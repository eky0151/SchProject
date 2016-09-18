using Microsoft.AspNet.SignalR;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Hosting;
using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;
using Owin;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;

namespace WPFServer
{
    /// <summary>
    /// WPF host for a SignalR server. The host can stop and start the SignalR
    /// server, report errors when trying to start the server on a URI where a
    /// server is already being hosted, and monitor when clients connect and disconnect. 
    /// The hub used in this server is a simple echo service, and has the same 
    /// functionality as the other hubs in the SignalR Getting Started tutorials.
    /// For simplicity, MVVM will not be used for this sample.
    /// </summary>
    public partial class MainWindow : Window
    {
        public IDisposable SignalR { get; set; }
        const string ServerURI = "http://localhost:8080";

        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Calls the StartServer method with Task.Run to not
        /// block the UI thread. 
        /// </summary>
        private void ButtonStart_Click(object sender, RoutedEventArgs e)
        {
            WriteToConsole("Starting server...");
            ButtonStart.IsEnabled = false;            
            Task.Run(() => StartServer());
        }

        /// <summary>
        /// Stops the server and closes the form. Restart functionality omitted
        /// for clarity.
        /// </summary>
        private void ButtonStop_Click(object sender, RoutedEventArgs e)
        {
            SignalR.Dispose();
            Close();
        }

        /// <summary>
        /// Starts the server and checks for error thrown when another server is already 
        /// running. This method is called asynchronously from Button_Start.
        /// </summary>
        private void StartServer()
        {
            try
            {
                SignalR = WebApp.Start(ServerURI);
            }
            catch (TargetInvocationException)
            {
                WriteToConsole("A server is already running at " + ServerURI);
                this.Dispatcher.Invoke(() => ButtonStart.IsEnabled = true);
                return;
            }
            this.Dispatcher.Invoke(() => ButtonStop.IsEnabled = true);
            WriteToConsole("Server started at " + ServerURI);
        }
        ///This method adds a line to the RichTextBoxConsole control, using Dispatcher.Invoke if used
        /// from a SignalR hub thread rather than the UI thread.
        public void WriteToConsole(String message)
        {
            if (!(RichTextBoxConsole.CheckAccess()))
            {
                this.Dispatcher.Invoke(() =>
                    WriteToConsole(message)
                );
                return;
            }
            RichTextBoxConsole.AppendText(message + "\r");
        }
    }
    /// <summary>
    /// Used by OWIN's startup process. 
    /// </summary>
    class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseCors(CorsOptions.AllowAll);
            app.MapSignalR();
        }
    }
    /// <summary>
    /// Echoes messages sent using the Send message by calling the
    /// addMessage method on the client. Also reports to the console
    /// when clients connect and disconnect.
    /// </summary>
    public class MyHub : Hub
    {
        public static string ConnectionString = ConfigurationManager.AppSettings["Microsoft.Azure.NotificationHubs.ConnectionString"];
        private NamespaceManager m_NamespaceManager;
        private static MessagingFactory Factory;
        public static Dictionary<string, bool> UserGroups = new Dictionary<string, bool>();
        private static TopicClient TClient;
        private static string messagesTopicPath = "Messages";

        public MyHub()
        {
            m_NamespaceManager = NamespaceManager.CreateFromConnectionString(ConnectionString);
            Factory = MessagingFactory.CreateFromConnectionString(ConnectionString);
            if (!m_NamespaceManager.TopicExists(messagesTopicPath))
            {
                m_NamespaceManager.CreateTopic(messagesTopicPath);
            }
            TClient = Factory.CreateTopicClient(messagesTopicPath);
        }

        public static void IntoQueue(string roomId, string message)
        {
            BrokeredMessage msg = new BrokeredMessage(message);
            msg.Properties.Add("Group", roomId);
            TClient.Send(msg);
        }

        public void Send(string username, MyMessage message)
        {
            // Call the addMessage method on all clients                       
            if (message.Group != null)
            {
                if (UserGroups[message.Group] == false)
                {
                    IntoQueue(message.Group, message.Msg);
                    UserGroups[message.Group] = true;
                }
                Clients.Group(message.Group).addMessage(username, " Group Message: " + message.Msg);                
            }
            else
            {
                //Clients.All.addMessage(username, message.Msg);
            }
        }

        
        public void Join(string groupName)
        {
            if (!UserGroups.ContainsKey(groupName))
            {
                UserGroups[groupName] = false;
            }           
            Groups.Add(Context.ConnectionId, groupName);
        }

        public override Task OnConnected()
        {
            //Use Application.Current.Dispatcher to access UI thread from outside the MainWindow class
            Application.Current.Dispatcher.Invoke(() => 
                ((MainWindow)Application.Current.MainWindow).WriteToConsole("Client connected: " + Context.ConnectionId));

            return base.OnConnected();
        }
        public override Task OnDisconnected()
        {
            //Use Application.Current.Dispatcher to access UI thread from outside the MainWindow class
            Application.Current.Dispatcher.Invoke(() => 
                ((MainWindow)Application.Current.MainWindow).WriteToConsole("Client disconnected: " + Context.ConnectionId));

            return base.OnDisconnected();
        }



    }

    public class MyMessage
    {
        public string Msg { get; set; }
        public string Group { get; set; }
    }
}
