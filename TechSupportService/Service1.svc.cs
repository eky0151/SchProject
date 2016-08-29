﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
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

        public TechSupportService1()
        {
           TechSupportDatabaseEntities1 d=new TechSupportDatabaseEntities1();
           Auth=new LoginDataRepository(d);
        }
        public LoginResult Login(string username, string password)
        {
            string name = "";
            bool a= Auth.Authenticate(username, password,out name);
            return new LoginResult() {Role = "",Valid = a};
        }

    }
}