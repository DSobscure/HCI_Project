using HCI_Project.Protocol;
using HCI_Project.Protocol.Communication.FetchDataCodes;
using HCI_Project.Protocol.Communication.FetchDataResponseParameters;
using HCI_Project.Protocol.Communication.OperationCodes;
using System.Collections.Generic;

namespace HCI_Project.Library.CommunicationInfrastructure.Request.Handlers.Device
{
    abstract class DeviceFetchDataRequestHandler : FetchDataRequestHandler<Library.Device, DeviceFetchDataCode>
    {
        protected DeviceFetchDataRequestHandler(Library.Device subject, int correctParameterCount) : base(subject, correctParameterCount)
        {
        }

        public override void SendResponse(DeviceFetchDataCode fetchCode, ReturnCode returnCode, string debugMessage, Dictionary<byte, object> parameters)
        {
            Dictionary<byte, object> eventData = new Dictionary<byte, object>
            {
                { (byte)FetchDataResponseParameterCode.FetchCode, (byte)fetchCode },
                { (byte)FetchDataResponseParameterCode.ReturnCode, (short)returnCode },
                { (byte)FetchDataResponseParameterCode.OperationMessage, null },
                { (byte)FetchDataResponseParameterCode.Parameters, parameters }
            };
            subject.ResponseManager.SendResponse(DeviceOperationCode.FetchData, ReturnCode.Successful, "", eventData);
        }
    }
}
