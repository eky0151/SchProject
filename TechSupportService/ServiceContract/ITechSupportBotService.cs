using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Web;
using TechSupportService.DataContract;

namespace TechSupportService
{
    [ServiceContract]
    public interface ITechSupportBotService
    {
        [OperationContract]
        int GetAvailableHelpDesk();

        [OperationContract]
        int GetAvailableTechnician();

        [OperationContract]
        TechnicianData ResgisterNewTechWork(string location,CustomerData customer);

        [OperationContract]
        List<SolvedQuestion> FindSimilar(string question);


    }
}