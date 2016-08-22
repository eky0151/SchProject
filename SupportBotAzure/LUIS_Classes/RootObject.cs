using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SupportBot.LUIS_Classes
{
    public class RootObject
    {
        public string query { get; set; }
        public Intent[] intents { get; set; }
        public Entity[] entities { get; set; }
    }
}