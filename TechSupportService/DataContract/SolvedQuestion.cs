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
        public int ID { get; set; }

        [DataMember]
        public int WorkerID { get; set; }

        [DataMember]
        public int CustomerID { get; set; }

        [DataMember]
        public string WorkerName { get; set; }

        [DataMember]
        public string Question { get; set; }

        [DataMember]
        public string Answer { get; set; }

        [DataMember]
        public string Topic { get; set; }

        [DataMember]
        public string Category { get; set; }

        [DataMember]
        public string[] KeyWords { get; set; }

        [DataMember]
        public DateTime TimeAsked { get; set; }

        [DataMember]
        public DateTime TimeAnswered { get; set; }

        public static explicit operator SolvedQuestion(DbAndRepository.SolvedQuestion q)
        {
            return new SolvedQuestion() { ID=q.ID,WorkerID = q.WorkerID,CustomerID = q.UserID,WorkerName = q.Worker.FullName,Question = q.Question,Answer = q.Answer,Topic = q.Topic,Category = q.Category,KeyWords = q.KeyWords.Split(','),TimeAnswered = q.Timeanswered,TimeAsked = q.Timeasked };
        }

        public static SolvedQuestion DbToSolvedQuestion(DbAndRepository.SolvedQuestion q)
        {
            return new SolvedQuestion() { ID=q.ID,WorkerID = q.WorkerID,CustomerID = q.UserID,WorkerName = q.Worker.FullName,Question = q.Question,Answer = q.Answer,Topic = q.Topic,Category = q.Category,KeyWords = q.KeyWords.Split(','),TimeAnswered = q.Timeanswered,TimeAsked = q.Timeasked };
        }
        public static DbAndRepository.SolvedQuestion SolvedQuestionToDB(SolvedQuestion q)
        {
            return new DbAndRepository.SolvedQuestion()
            {
                Answer = q.Answer,
                Category = q.Category,
                KeyWords = string.Join(",", q.KeyWords),
                Question = q.Question,
                UserID = q.CustomerID,
                WorkerID = q.WorkerID,
                Timeanswered = q.TimeAnswered,
                Timeasked = q.TimeAsked,
                Topic = q.Topic
            };
        }

    }
}