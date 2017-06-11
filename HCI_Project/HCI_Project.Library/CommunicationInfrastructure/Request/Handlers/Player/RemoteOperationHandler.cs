using HCI_Project.Protocol;
using HCI_Project.Protocol.Communication.OperationCodes;
using HCI_Project.Protocol.Communication.RequestParameters.Player;
using System.Collections.Generic;

namespace HCI_Project.Library.CommunicationInfrastructure.Request.Handlers.Player
{
    class RemoteOperationHandler : PlayerRequestHandler
    {
        public RemoteOperationHandler(Library.Player subject) : base(subject, 3)
        {
        }

        internal override bool Handle(PlayerOperationCode operationCode, Dictionary<byte, object> parameters, out string errorMessage)
        {
            if(base.Handle(operationCode, parameters, out errorMessage))
            {
                DeviceCode deviceCode = (DeviceCode)parameters[(byte)RemoteOperationParameterCode.DeviceCode];
                byte remoteOperationCode = (byte)parameters[(byte)RemoteOperationParameterCode.OperationCode];
                Dictionary<byte, object> remoteOperationParameters = (Dictionary<byte, object>)parameters[(byte)RemoteOperationParameterCode.Parameters];
                subject.EventManager.RemoteOperation(deviceCode, remoteOperationCode, remoteOperationParameters);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
