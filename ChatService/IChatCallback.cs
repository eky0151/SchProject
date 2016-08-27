using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace ChatService
{
    public interface IChatCallback
    {
        [OperationContract(IsOneWay = true)]
        void ReceiveMessage(Message message);

        [OperationContract(IsOneWay = true]
        void ReceiveFileMessage(FileMessage fileMessage);

        [OperationContract(IsOneWay = true)]
        void ClientConnect(string name);

        [OperationContract(IsOneWay = true)]
        void ClientLeave(string name);
    }
}
