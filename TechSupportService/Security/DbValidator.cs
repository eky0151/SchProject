using System;
using System.Collections.Generic;
using System.IdentityModel.Selectors;
using System.Linq;
using System.Web;
using DbAndRepository;
using DbAndRepository.IRepositories;
using DbAndRepository.Repostirories;

namespace TechSupportService.Security
{
    public class DbValidator: UserNamePasswordValidator
    {
        private ILoginDataRepository _repo;

        public DbValidator()
        {
            _repo=new LoginDataRepository(new TechSupportDatabaseEntities());
        }
        public override void Validate(string userName, string password)
        {
            if (!_repo.Authenticate(userName, password))
            {
                throw new ArgumentException("Bad username or password");
            }
        }
    }
}