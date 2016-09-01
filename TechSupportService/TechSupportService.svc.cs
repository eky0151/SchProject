using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;
using DbAndRepository;
using DbAndRepository.IRepositories;
using DbAndRepository.Repostirories;

namespace TechSupportService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "TechSupportService1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select TechSupportService1.svc or TechSupportService1.svc.cs at the Solution Explorer and start debugging.
    public class TechSupportService1 : ITechSupportService1
    {
        private readonly ILoginDataRepository _auth;
        private IRegUserRepository aspUsers;
        private readonly ILogsRepository _logs;

        public TechSupportService1()
        {
            TechSupportDatabaseEntities db = new TechSupportDatabaseEntities();
            _auth = new LoginDataRepository(db);
            _logs=new LogsRepository(db);
            aspUsers = new RegUserRepository(db);
        }
        public async Task<LoginResult> LoginAsync(string username, string password)
        {
            var authenticate = await Task.Factory.StartNew(() =>
              {
                  string name = string.Empty,
                      role = string.Empty;
                  bool result = _auth.Authenticate(username, password, out name, out role);
                  return new LoginResult()
                  {
                      Role = role,
                      Valid = result,
                      FullName = name
                  };
              });
            return authenticate;
        }

        //Register a new user from the aps.net registration form
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

        public bool UserLogin(string username, string password)
        {
            return aspUsers.Autenthicate(username, password);
        }

    }
}
