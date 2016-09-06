using System;
using System.Collections.Generic;
using System.Diagnostics.PerformanceData;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchProject.TechSupportSecure1;

namespace SchProject
{
    public class UserData
    {
        public string FullName{get; private set; }
        public Role Role { get; private set; }

        public void SetData(LoginResult login)
        {
            FullName = login.FullName;
            Role = login.Role;
        }
    }
}
