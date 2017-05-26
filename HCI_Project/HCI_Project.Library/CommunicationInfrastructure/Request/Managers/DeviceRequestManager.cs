using HCI_Project.Library.CommunicationInfrastructure.Request.Handlers;
using HCI_Project.Library.CommunicationInfrastructure.Request.Handlers.Device;
using HCI_Project.Protocol.Communication.FetchDataCodes;
using HCI_Project.Protocol.Communication.FetchDataRequestParameters;
using HCI_Project.Protocol.Communication.OperationCodes;
using HCI_Project.Protocol.Communication.RequestParameters.Device;
using System.Collections.Generic;

namespace HCI_Project.Library.CommunicationInfrastructure.Request.Managers
{
    public class DeviceRequestManager
    {
        private readonly Device device;
        private readonly Dictionary<DeviceOperationCode, RequestHandler<Device, DeviceOperationCode>> requestTable = new Dictionary<DeviceOperationCode, RequestHandler<Device, DeviceOperationCode>>();
        public DeviceFetchDataRequestBroker FetchDataRequestBroker { get; private set; }

        internal DeviceRequestManager(Device device)
        {
            this.device = device;
            FetchDataRequestBroker = new DeviceFetchDataRequestBroker(device);

            requestTable.Add(DeviceOperationCode.FetchData, FetchDataRequestBroker);
            requestTable.Add(DeviceOperationCode.PlayerRequest, new PlayerRequestBroker(device));
        }
        public bool Operate(DeviceOperationCode operationCode, Dictionary<byte, object> parameters, out string errorMessage)
        {
            if (requestTable.ContainsKey(operationCode))
            {
                if (requestTable[operationCode].Handle(operationCode, parameters, out errorMessage))
                {
                    return true;
                }
                else
                {
                    errorMessage = $"DeviceOperation Error: {operationCode} from Device: {device}\nErrorMessage: {errorMessage}";
                    return false;
                }
            }
            else
            {
                errorMessage = $"Unknow DeviceOperation:{operationCode} from Device: {device}";
                return false;
            }
        }
        internal void SendOperation(DeviceOperationCode operationCode, Dictionary<byte, object> parameters)
        {
            device.CommunicationInterface.SendRequest(operationCode, parameters);
        }

        internal void SendFetchDataOperation(DeviceFetchDataCode fetchCode, Dictionary<byte, object> parameters)
        {
            Dictionary<byte, object> fetchDataParameters = new Dictionary<byte, object>
            {
                { (byte)FetchDataRequestParameterCode.FetchDataCode, (byte)fetchCode },
                { (byte)FetchDataRequestParameterCode.Parameters, parameters }
            };
            SendOperation(DeviceOperationCode.FetchData, fetchDataParameters);
        }
        internal void SendPlayerOperation(Player player, PlayerOperationCode operationCode, Dictionary<byte, object> parameters)
        {
            Dictionary<byte, object> operationParameters = new Dictionary<byte, object>
            {
                { (byte)PlayerRequestParameterCode.PlayerID, player.PlayerID },
                { (byte)PlayerRequestParameterCode.OperationCode, operationCode },
                { (byte)PlayerRequestParameterCode.Parameters, parameters }
            };
            SendOperation(DeviceOperationCode.PlayerRequest, operationParameters);
        }
    }
}
