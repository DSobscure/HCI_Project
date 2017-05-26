using HCI_Project.Protocol;
using HCI_Project.Protocol.Communication.FetchDataCodes;
using HCI_Project.Protocol.Communication.OperationCodes;
using System.Collections.Generic;

namespace HCI_Project.Library.CommunicationInfrastructure.Request.Handlers.Player
{
    public class PlayerFetchDataRequestBroker : FetchDataRequestBroker<Library.Player, PlayerOperationCode, PlayerFetchDataCode>
    {
        internal PlayerFetchDataRequestBroker(Library.Player subject) : base(subject)
        {

        }

        internal override void SendResponse(PlayerOperationCode operationCode, ReturnCode returnCode, string operationMessage, Dictionary<byte, object> parameter)
        {
            subject.ResponseManager.SendResponse(operationCode, returnCode, operationMessage, parameter);
        }

        internal void SendOperation(PlayerFetchDataCode fetchCode, Dictionary<byte, object> parameters)
        {
            subject.RequestManager.SendFetchDataOperation(fetchCode, parameters);
        }
    }
}
