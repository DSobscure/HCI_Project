using HCI_Project.Protocol.Communication.OperationCodes;
using HCI_Project.Protocol.Communication.RequestParameters.Device;
using System.Collections.Generic;

namespace HCI_Project.Library.CommunicationInfrastructure.Request.Handlers.Device
{
    class PlayerRequestBroker : DeviceRequestHandler
    {
        internal PlayerRequestBroker(Library.Device subject) : base(subject, 3)
        {

        }

        internal override bool Handle(DeviceOperationCode operationCode, Dictionary<byte, object> parameters, out string errorMessage)
        {
            if (base.Handle(operationCode, parameters, out errorMessage))
            {
                string nickname = (string)parameters[(byte)PlayerRequestParameterCode.Nickname];
                PlayerOperationCode resolvedOperationCode = (PlayerOperationCode)parameters[(byte)PlayerRequestParameterCode.OperationCode];
                Dictionary<byte, object> resolvedParameters = (Dictionary<byte, object>)parameters[(byte)PlayerRequestParameterCode.Parameters];
                if (subject.Player.Nickname == nickname)
                {
                    return subject.Player.RequestManager.Operate(resolvedOperationCode, resolvedParameters, out errorMessage);
                }
                else
                {
                    errorMessage = $"PlayerOperation Error Player: {nickname} Not in Device: {subject}";
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}
