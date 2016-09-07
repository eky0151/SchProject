using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Wcf
{
    public class Chat : ChatService.IChatCallback
    {
        public void ClientConnectCallback(string name)
        {
            Models.UserModel.ChatText += String.Format("\n {0} is connected", name);
        }

        public void ReceiveFileMessageeCallback(byte[] fileMessage, string description, string sender)
        {
            throw new NotImplementedException();
        }

        public void ReceiveMessageCallback(string message, string receiver)
        {
            throw new NotImplementedException();
        }
    }
}