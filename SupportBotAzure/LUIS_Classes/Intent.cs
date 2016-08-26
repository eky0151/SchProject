using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SupportBot.LUIS_Classes
{
    public class Intent
    {
        public string intent { get; set; }
        public float score { get; set; }
    }
}