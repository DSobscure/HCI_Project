using HCI_Project.Library.CommunicationInfrastructure.Response.Handlers;
using HCI_Project.Library.CommunicationInfrastructure.Response.Handlers.Device;
using HCI_Project.Protocol;
using HCI_Project.Protocol.Communication.OperationCodes;
using HCI_Project.Protocol.Communication.ResponseParameters.Device;
using System.Collections.Generic;

namespace HCI_Project.Library.CommunicationInfrastructure.Response.Managers
{
    public class DeviceResponseManager
    {
        private readonly Device device;
        protected readonly Dictionary<DeviceOperationCode, ResponseHandler<Device, DeviceOperationCode>> operationTable = new Dictionary<DeviceOperationCode, ResponseHandler<Device, DeviceOperationCode>>();

        internal DeviceResponseManager(Device device)
        {
            this.device = device;

            operationTable.Add(DeviceOperationCode.FetchData, new FetchDataResponseBroker(device));
            operationTable.Add(DeviceOperationCode.PlayerRequest, new PlayerOperationResponseBroker(device));
        }
        public bool Operate(DeviceOperationCode operationCode, ReturnCode returnCode, string debugMessage, Dictionary<byte, object> parameters, out string errorMessage)
        {
            if (operationTable.ContainsKey(operationCode))
            {
                if (operationTable[operationCode].Handle(operationCode, returnCode, debugMessage, parameters, out errorMessage))
                {
                    return true;
                }
                else
                {
                    errorMessage = $"DeviceResponse Error: {operationCode} from Device: {device}\nErrorMessage: {errorMessage}";
                    return false;
                }
            }
            else
            {
                errorMessage = $"Unknow DeviceResponse:{operationCode} from Device: {device}";
                return false;
            }
        }
        internal void SendResponse(DeviceOperationCode operationCode, ReturnCode errorCode, string operationMessage, Dictionary<byte, object> parameters)
        {
            device.CommunicationInterface.SendResponse(operationCode, errorCode, operationMessage, parameters);
        }
        internal void SendPlayerResponse(Player player, PlayerOperationCode operationCode, ReturnCode returnCode, string operationMessage, Dictionary<byte, object> parameters)
        {
            Dictionary<byte, object> responseData = new Dictionary<byte, object>
            {
                { (byte)PlayerResponseParameterCode.PlayerID, player.PlayerID },
                { (byte)PlayerResponseParameterCode.OperationCode, (byte)operationCode },
                { (byte)PlayerResponseParameterCode.ReturnCode, (short)returnCode },
                { (byte)PlayerResponseParameterCode.OperationMessage, operationMessage },
                { (byte)PlayerResponseParameterCode.Parameters, parameters }
            };
            SendResponse(DeviceOperationCode.PlayerRequest, ReturnCode.Successful, null, responseData);
        }
    }
}
