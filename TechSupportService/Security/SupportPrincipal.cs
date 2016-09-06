using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading;
using System.Web;
using DbAndRepository;
using DbAndRepository.IRepositories;
using DbAndRepository.Repostirories;

namespace TechSupportService.Security
{
    public class SupportPrincipal : IPrincipal
    {
        private IIdentity _identity;
        private ILoginDataRepository _repo;
        private string[] _roles;

        public SupportPrincipal(IIdentity identity)
        {
            _identity = identity;
            _repo = new LoginDataRepository(new TechSupportDatabaseEntities());
        }
        public IIdentity Identity
        {
            get { return _identity; }
        }
        public string[] Roles
        {
            get
            {
                GetRoles();
                return _roles;
            }
        }
        public static SupportPrincipal Current
        {
            get
            {
                return Thread.CurrentPrincipal as SupportPrincipal;
            }
        }

        protected virtual void GetRoles()
        {
            string baseRole = _repo.Get(x => x.Username == _identity.Name).FirstOrDefault()?.Urole;
            _roles[0] = baseRole;
        }

        public bool IsInRole(string role)
        {
            GetRoles();
            return _roles.Contains(role);
        }


    }
}