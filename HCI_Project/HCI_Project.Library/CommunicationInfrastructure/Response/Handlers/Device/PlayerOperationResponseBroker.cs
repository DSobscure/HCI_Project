using HCI_Project.Protocol;
using HCI_Project.Protocol.Communication.OperationCodes;
using HCI_Project.Protocol.Communication.ResponseParameters.Device;
using System.Collections.Generic;

namespace HCI_Project.Library.CommunicationInfrastructure.Response.Handlers.Device
{
    class PlayerOperationResponseBroker : ResponseHandler<Library.Device, DeviceOperationCode>
    {
        internal PlayerOperationResponseBroker(Library.Device subject) : base(subject, 5)
        {
        }

        internal override bool Handle(DeviceOperationCode operationCode, ReturnCode returnCode, string operationMessage, Dictionary<byte, object> parameters, out string errorMessage)
        {
            if (base.Handle(operationCode, returnCode, operationMessage, parameters, out errorMessage))
            {
                string nickname = (string)parameters[(byte)PlayerResponseParameterCode.Nickname];
                PlayerOperationCode resolvedOperationCode = (PlayerOperationCode)parameters[(byte)PlayerResponseParameterCode.OperationCode];
                ReturnCode resolvedReturnCode = (ReturnCode)parameters[(byte)PlayerResponseParameterCode.ReturnCode];
                string resolvedOperationMessage = (string)parameters[(byte)PlayerResponseParameterCode.OperationMessage];
                Dictionary<byte, object> resolvedParameters = (Dictionary<byte, object>)parameters[(byte)PlayerResponseParameterCode.Parameters];

                if (subject.Player.Nickname == nickname)
                {
                    return subject.Player.ResponseManager.Operate(resolvedOperationCode, resolvedReturnCode, resolvedOperationMessage, resolvedParameters, out errorMessage);
                }
                else
                {
                    errorMessage = $"PlayerOperationResponse Error Player: {nickname} Not in Device: {subject}";
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
