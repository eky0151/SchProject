using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbAndRepository
{
    public partial class Bank
    {
        public Worker SingleWorker { get { return Worker.SingleOrDefault(); } }
    }
}
