using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchProject.ServiceBus
{
    public class BugEventArgs:EventArgs
    {
        public string Message { get; set; }
            
    }
}
