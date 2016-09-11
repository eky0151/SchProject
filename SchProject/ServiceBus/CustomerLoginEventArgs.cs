using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchProject.TechSupportSecure;

namespace SchProject.ServiceBus
{
    public class CustomerLoginEventArgs
    {
        public CustomerData Customer { get; set; }
    }
}
