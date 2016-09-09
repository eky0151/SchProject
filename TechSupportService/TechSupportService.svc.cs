using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using DbAndRepository;
using DbAndRepository.IRepositories;
using DbAndRepository.Repostirories;

namespace TechSupportService
{
    public class TechSupportService1:ITechSupportService1
    {
        private IRegUserRepository aspUsers;
        private ILoginDataRepository _auth;
        private ISolvedQuestionsRepository _solvedQuestions;

        public TechSupportService1()
        {
            TechSupportDatabaseEntities db = new TechSupportDatabaseEntities();
            aspUsers = new RegUserRepository(db);
            _auth = new LoginDataRepository(db);
            _solvedQuestions = new SolvedQuestionsRepository(db);
        }
        public bool UserLogin(string username, string password)
        {
            return aspUsers.Autenthicate(username, password);
        }

        public string GetProfilePicture(string username)
        {
            bool res = _auth.CheckUsername(username);
            string picture = null;
            if (res)
            {
                picture = _auth.GetPicture(username);
            }
            return picture;
        }
        public string GetUserProfilePicture(string username)
        {
            return "";
        }
        public void RegisterNewUser(string fullName, string email, string userName, string password)
        {
            aspUsers.Insert(new RegUser
            {
                Fullname = fullName,
                Email = email,
                Username = userName,
                Password = password,
                Points = 1,
                Regtime = DateTime.Now,
            });
        }

    }
}
