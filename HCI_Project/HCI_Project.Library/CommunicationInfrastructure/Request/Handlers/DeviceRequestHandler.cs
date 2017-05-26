using HCI_Project.Protocol;
using HCI_Project.Protocol.Communication.OperationCodes;
using System.Collections.Generic;

namespace HCI_Project.Library.CommunicationInfrastructure.Request.Handlers
{
    public class DeviceRequestHandler : RequestHandler<Library.Device, DeviceOperationCode>
    {
        internal DeviceRequestHandler(Library.Device subject, int correctParameterCount) : base(subject, correctParameterCount)
        {
        }

        internal override void SendResponse(DeviceOperationCode operationCode, ReturnCode returnCode, string operationMessage, Dictionary<byte, object> parameter)
        {
            subject.ResponseManager.SendResponse(operationCode, returnCode, operationMessage, parameter);
        }
    }
}
