using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Web;
using TechSupportService.DataContract;

namespace TechSupportService
{
    [ServiceContract]
    public interface ITechSupportBotService
    {
        [OperationContract]
        [WebInvoke(Method = "GET",
           RequestFormat = WebMessageFormat.Json,
           ResponseFormat = WebMessageFormat.Json,
           UriTemplate = "availablehelpdesk")]
        int GetAvailableHelpDesk();

        [OperationContract]
        [WebInvoke(Method = "GET", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json,
           UriTemplate = "availabletechnician")]
        int GetAvailableTechnician();

        [OperationContract]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.Wrapped)]
        TechnicianData ResgisterNewTechWork(string location, CustomerData customer);

        [OperationContract]
        [WebInvoke(UriTemplate = "findsimilar", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        List<SolvedQuestion> FindSimilar(string question);


    }
}