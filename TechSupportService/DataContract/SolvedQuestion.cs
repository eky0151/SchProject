using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace TechSupportService
{
    [DataContract]
    public class SolvedQuestion
    {
        [DataMember]
        public string WorkerName { get; set; }

        [DataMember]
        public string Question { get; set; }

        [DataMember]
        public string Answer { get; set; }

        [DataMember]
        public string[] Topic { get; set; }

        [DataMember]
        public string[] Category { get; set; }

        [DataMember]
        public string[] KeyWords { get; set; }

        [DataMember]
        public DateTime TimeAsked { get; set; }

        [DataMember]
        public DateTime TimeAnswered { get; set; }

        public static explicit operator SolvedQuestion(DbAndRepository.SolvedQuestion q)
        {
            SolvedQuestion question = new SolvedQuestion() { Answer = q.Answer, Question = q.Question, TimeAsked = q.Timeasked, TimeAnswered = q.Timeanswered, Category = q.Category.Split('$'), KeyWords = q.KeyWords.Split('$'), Topic = q.Topic.Split('$'), WorkerName = q.Worker.FullName };
            return question;
        }

    }
}