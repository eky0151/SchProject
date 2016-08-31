using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using DbAndRepository;
using DbAndRepository.IRepositories;
using DbAndRepository.Repostirories;

namespace TechSupportService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "TechSupportService1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select TechSupportService1.svc or TechSupportService1.svc.cs at the Solution Explorer and start debugging.
    public class TechSupportService1 : ITechSupportService1
    {
        private ILoginDataRepository Auth;
        private IRegUserRepository aspUsers;

        public TechSupportService1()
        {
            TechSupportDatabaseEntities d = new TechSupportDatabaseEntities();
            Auth = new LoginDataRepository(d);
            aspUsers = new RegUserRepository(d);
        }
        public LoginResult Login(string username, string password)
        {
            string name = string.Empty,
                   role = string.Empty;
            bool result = Auth.Authenticate(username, password, out name, out role);
            return new LoginResult()
            {
                Role = role,
                Valid = result,
                FullName = name
            };
        }

        //Register a new user from the aps.net registration form
        public void RegisterNewUser(string fullName, string email, string userName, string password)
        {
            aspUsers.Insert(new RegUser
            {
                //ID = aspUsers.GetAll().LastOrDefault().ID == default(int) ?
                //                                                    default(int) + 1 :
                //                                                    aspUsers.GetAll().LastOrDefault().ID + 1,
                Fullname = fullName,
                Email = email,
                Username = userName,
                Password = password,
                Points = 1,
                Regtime = DateTime.Now,
            });
        }

        public bool UserLogin(string username, string password)
        {
            return aspUsers.Autenthicate(username, password);
        }
    }
}
