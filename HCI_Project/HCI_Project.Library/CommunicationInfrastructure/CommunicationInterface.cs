using HCI_Project.Protocol;
using HCI_Project.Protocol.Communication.EventCodes;
using HCI_Project.Protocol.Communication.OperationCodes;
using System.Collections.Generic;

namespace HCI_Project.Library.CommunicationInfrastructure
{
    public interface CommunicationInterface
    {
        Device Device { get; set; }
        void SendEvent(DeviceEventCode eventCode, Dictionary<byte, object> parameters);
        void SendRequest(DeviceOperationCode operationCode, Dictionary<byte, object> parameters);
        void SendResponse(DeviceOperationCode operationCode, ReturnCode returnCode, string debugMessage, Dictionary<byte, object> parameters);
    }
}
