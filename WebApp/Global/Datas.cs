using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Web;

namespace WebApp.Global
{
    public class Datas
    {
        static Wcf.Chat uc = new Wcf.Chat();
        static InstanceContext ic = new InstanceContext(uc);
        public static ChatService.ChatClient chatClient = new ChatService.ChatClient(ic);
    }
}