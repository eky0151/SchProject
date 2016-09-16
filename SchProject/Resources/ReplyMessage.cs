using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchProject.Resources
{
     public class ReplyMessage
    {
        public string SendTo { get; }
        public string Reply { get; }

        public ReplyMessage(string sendTo, string reply)
        {
            SendTo = sendTo;
            Reply = reply;
        }
    }
}
