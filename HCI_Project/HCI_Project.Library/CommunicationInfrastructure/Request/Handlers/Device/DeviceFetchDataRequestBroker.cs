using HCI_Project.Protocol;
using HCI_Project.Protocol.Communication.FetchDataCodes;
using HCI_Project.Protocol.Communication.OperationCodes;
using System.Collections.Generic;

namespace HCI_Project.Library.CommunicationInfrastructure.Request.Handlers.Device
{
    public class DeviceFetchDataRequestBroker : FetchDataRequestBroker<Library.Device, DeviceOperationCode, DeviceFetchDataCode>
    {
        internal DeviceFetchDataRequestBroker(Library.Device subject) : base(subject)
        {

        }

        internal override void SendResponse(DeviceOperationCode operationCode, ReturnCode returnCode, string operationMessage, Dictionary<byte, object> parameter)
        {
            subject.ResponseManager.SendResponse(operationCode, returnCode, operationMessage, parameter);
        }

        internal void SendOperation(DeviceFetchDataCode fetchCode, Dictionary<byte, object> parameters)
        {
            subject.RequestManager.SendFetchDataOperation(fetchCode, parameters);
        }
    }
}
