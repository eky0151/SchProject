using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SupportBot
{

    public class Rootobject
    {
        public Findsimilarresult[] FindSimilarResult { get; set; }
    }

    public class Findsimilarresult
    {
        public string Answer { get; set; }
        public string Category { get; set; }
        public int CustomerID { get; set; }
        public int ID { get; set; }
        public string[] KeyWords { get; set; }
        public string Question { get; set; }
        public DateTime TimeAnswered { get; set; }
        public DateTime TimeAsked { get; set; }
        public string Topic { get; set; }
        public int WorkerID { get; set; }
        public string WorkerName { get; set; }
    }

}