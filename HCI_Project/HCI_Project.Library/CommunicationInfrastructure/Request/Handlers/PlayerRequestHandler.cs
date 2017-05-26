using HCI_Project.Protocol;
using HCI_Project.Protocol.Communication.OperationCodes;
using System.Collections.Generic;

namespace HCI_Project.Library.CommunicationInfrastructure.Request.Handlers
{
    public class PlayerRequestHandler : RequestHandler<Library.Player, PlayerOperationCode>
    {
        internal PlayerRequestHandler(Library.Player subject, int correctParameterCount) : base(subject, correctParameterCount)
        {
        }

        internal override void SendResponse(PlayerOperationCode operationCode, ReturnCode returnCode, string operationMessage, Dictionary<byte, object> parameters)
        {
            subject.ResponseManager.SendResponse(operationCode, returnCode, operationMessage, parameters);
        }
    }
}
