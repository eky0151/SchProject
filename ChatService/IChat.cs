using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace ChatService
{

    [ServiceContract(SessionMode = SessionMode.Required, CallbackContract = typeof(IChatCallback))]
    public interface IChat
    {
        [OperationContract(IsInitiating = true, IsOneWay = true)]
        bool Connect(Client client);

        [OperationContract(IsTerminating  = true, IsOneWay = true)]
        void Disconnect(Client client);


    }

   
}
