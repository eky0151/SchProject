using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchProject.ServiceBus
{
    public class MessageEventArgs:EventArgs
    {
        public string Sender { get; }
        public string Message { get; }

        public MessageEventArgs(string sender, string message)
        {
            Sender = sender;
            Message = message;
        }
    }
}
