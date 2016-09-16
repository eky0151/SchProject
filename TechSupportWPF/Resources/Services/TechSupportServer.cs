using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Security;
using System.Text;
using System.Threading.Tasks;
using SchProject.TechSupportSecure;

namespace SchProject
{
    public class TechSupportServer
    {
        public TechSupportServiceSecure1Client host { get; private set; }

        public TechSupportServer()
        {
            
        }

        public async Task<bool> Login(string username, string passWD)
        {
            bool success=false;
            await Task.Factory.StartNew(() =>
            {
                try
                {
                    host = new TechSupportServiceSecure1Client();
                    host.ClientCredentials.UserName.UserName = username;
                    host.ClientCredentials.UserName.Password = passWD;
                    host.Open();
                    success = true;
                }
                catch (SecurityAccessDeniedException e)
                {

                    success = false;
                }
                catch (TimeoutException exception)
                {
                    Console.WriteLine("Got {0}", exception.GetType());
                }
                catch (CommunicationException exception)
                {
                    Console.WriteLine("Got {0}", exception.GetType());
                }
            });
            return success;
        }
    }
}
