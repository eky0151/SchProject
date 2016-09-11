using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchProject.TechSupportSecure;

namespace SchProject.ServiceBus
{
    public class StatusChangedEventArgs:EventArgs
    {
        public string Username { get; set; }
        public Status Status { get; set; }
    }
}
