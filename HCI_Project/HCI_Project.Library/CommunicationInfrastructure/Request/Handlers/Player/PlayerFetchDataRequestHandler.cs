using HCI_Project.Protocol;
using HCI_Project.Protocol.Communication.FetchDataCodes;
using HCI_Project.Protocol.Communication.FetchDataResponseParameters;
using HCI_Project.Protocol.Communication.OperationCodes;
using System.Collections.Generic;

namespace HCI_Project.Library.CommunicationInfrastructure.Request.Handlers.Player
{
    abstract class PlayerFetchDataRequestHandler : FetchDataRequestHandler<Library.Player, PlayerFetchDataCode>
    {
        protected PlayerFetchDataRequestHandler(Library.Player subject, int correctParameterCount) : base(subject, correctParameterCount)
        {
        }

        public override void SendResponse(PlayerFetchDataCode fetchCode, ReturnCode returnCode, string debugMessage, Dictionary<byte, object> parameters)
        {
            Dictionary<byte, object> eventData = new Dictionary<byte, object>
            {
                { (byte)FetchDataResponseParameterCode.FetchCode, (byte)fetchCode },
                { (byte)FetchDataResponseParameterCode.ReturnCode, (short)returnCode },
                { (byte)FetchDataResponseParameterCode.OperationMessage, null },
                { (byte)FetchDataResponseParameterCode.Parameters, parameters }
            };
            subject.ResponseManager.SendResponse(PlayerOperationCode.FetchData, ReturnCode.Successful, "", eventData);
        }
    }
}
