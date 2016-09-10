using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using TechSupportService.DataContract;

namespace TechSupportService.ServiceContract
{ 
    public interface ISecureServiceCallback
    {
        [OperationContract(IsOneWay = true)]
        void HandleStatusChanged(string username, Status newStatus);

        [OperationContract(IsOneWay = true)]
        void HandleWorkerLogin(string fullname);

        [OperationContract(IsOneWay = true)]
        void HandleNewAppBugreport(string message);

        [OperationContract(IsOneWay = true)]
        void HandleNewSolvedQuestion(string fullname);
    }
}
