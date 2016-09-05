using System;
using System.Collections.Generic;
using System.Data.Entity.Spatial;
using System.Linq;
using System.Runtime.Serialization;
using System.Security;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;
using DbAndRepository;
using DbAndRepository.IRepositories;
using DbAndRepository.Repostirories;
using TechSupportService.DataContract;

namespace TechSupportService
{
    public class TechSupportService1 : ITechSupportService1
    {
        private Dictionary<string, SecureString> _UserUUID;
        private readonly ILoginDataRepository _auth;
        private IRegUserRepository aspUsers;
        private readonly ILoginLogsRepository _logs;
        private IWorkerRepository _worker;
        private IBankRepository _bank;
        private ITechnicianRepository _technician;
        private ILoginLogsRepository _loginLogs;


        public TechSupportService1()
        {
            TechSupportDatabaseEntities db = new TechSupportDatabaseEntities();
            _auth = new LoginDataRepository(db);
            _logs = new LoginLogsRepository(db);
            aspUsers = new RegUserRepository(db);
            _worker = new WorkerRepository(db);
            _bank = new BankRepository(db);
            _technician=new TechninicanRepository(db);
            _loginLogs=new LoginLogsRepository(db);
            _UserUUID = new Dictionary<string, SecureString>();
        }
        public async Task<LoginResult> LoginAsync(string username, string password)
        {
            var authenticate = await Task.Factory.StartNew(() =>
            {
                string UUID = Guid.NewGuid().ToString("N");
                  string name = string.Empty,
                      role = string.Empty;
                  bool result = _auth.Authenticate(username, password, out name, out role);
                  return new LoginResult()
                  {
                      Role =(Role)Enum.Parse(typeof(Role),role),
                      Valid = result,
                      FullName = name
                  };
              });
            _logs.Insert(new LoginLogs() {Date = DateTime.Now,LoginName = username,Success = authenticate.Valid});
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

        public UsernameValidationResult UsernameValidation(string username)
        {
            bool res = _auth.CheckUsername(username);
            string picture = null;
            if (res)
            {
                picture = _auth.GetPicture(username);
            }
            return new UsernameValidationResult() { ProfilePicture = picture, Valid = res };
        }

        public void RegisterNewStaffMember(WorkerDataRegistrationData regData)
        {
            //_bank.Insert(new DbAndRepository.Bank() { Account = regData.BankAccount, Name = Enum.GetName(typeof(Bank), regData.Bank) });
            //var bank = _bank.Get(x => x.Account == regData.BankAccount).FirstOrDefault();

            DbAndRepository.Worker wk = new DbAndRepository.Worker()
            {
                Address = regData.Address,
                BankID = 8,
                Email = regData.Email,
                FullName = regData.FullName,
                Phone = regData.Phone,
                ProfilePicture = regData.ProfilePicture,
                Status = Enum.GetName(typeof(Status),regData.Status)
            };
            _worker.Insert(wk);
            
            var workerdata = _worker.Get(x => x.FullName.ToString() == regData.FullName.ToString() && x.BankID == 8).FirstOrDefault();
            _auth.Insert(new LoginData() { Password = regData.PassWD, Urole = Enum.GetName(typeof(Role), regData.Role), Username = regData.Username, WorkerID = workerdata.ID });
            if(regData.Technician)
                _technician.Insert(new Technician() {Available = "Break",WorkerID = workerdata.ID});
            
        }

        public List<CustomerLoginData> LastCustomerList()
        {
            throw new NotImplementedException();
        }

        public List<WorkerData> StaffList()
        {
            throw new NotImplementedException();
        }

        public void ChangeWorkerStatus(string username, Status status)
        {
            throw new NotImplementedException();
        }

        public void SendBugreport(string message, string sender, string file)
        {
            throw new NotImplementedException();
        }

        public void ChangeWorkerPassWD(string uuid, string username, string newPassWD)
        {
            throw new NotImplementedException();
        }

        public bool UserLogin(string username, string password)
        {
            return aspUsers.Autenthicate(username, password);
        }

    }
}
