using HCI_Project.Library.CommunicationInfrastructure.Event.Handlers;
using HCI_Project.Library.CommunicationInfrastructure.Event.Handlers.Device;
using HCI_Project.Protocol.Communication.EventCodes;
using HCI_Project.Protocol.Communication.EventParameters.Device;
using HCI_Project.Protocol.Communication.SyncDataCodes;
using HCI_Project.Protocol.Communication.SyncDataParameters;
using System.Collections.Generic;

namespace HCI_Project.Library.CommunicationInfrastructure.Event.Managers
{
    public class DeviceEventManager
    {
        private readonly Device device;
        private readonly Dictionary<DeviceEventCode, EventHandler<Device, DeviceEventCode>> eventTable = new Dictionary<DeviceEventCode, EventHandler<Device, DeviceEventCode>>();
        public DeviceSyncDataBroker SyncDataBroker { get; private set; }

        internal DeviceEventManager(Device device)
        {
            this.device = device;
            SyncDataBroker = new DeviceSyncDataBroker(device);

            eventTable.Add(DeviceEventCode.SyncData, SyncDataBroker);
            eventTable.Add(DeviceEventCode.PlayerEvent, new PlayerEventBroker(device));
        }
        public bool Operate(DeviceEventCode eventCode, Dictionary<byte, object> parameters, out string errorMessage)
        {
            if (eventTable.ContainsKey(eventCode))
            {
                if (eventTable[eventCode].Handle(eventCode, parameters, out errorMessage))
                {
                    return true;
                }
                else
                {
                    errorMessage = $"DeviceEvent Error: {eventCode} from Device: {device}\nErrorMessage: {errorMessage}";
                    return false;
                }
            }
            else
            {
                errorMessage = $"Unknow DeviceEvent:{eventCode} from Device: {device}";
                return false;
            }
        }
        internal void SendEvent(DeviceEventCode eventCode, Dictionary<byte, object> parameters)
        {
            device.CommunicationInterface.SendEvent(eventCode, parameters);
        }
        internal void SendSyncDataEvent(DeviceSyncDataCode syncCode, Dictionary<byte, object> parameters)
        {
            Dictionary<byte, object> syncDataParameters = new Dictionary<byte, object>
            {
                { (byte)SyncDataEventParameterCode.SyncDataCode, (byte)syncCode },
                { (byte)SyncDataEventParameterCode.Parameters, parameters }
            };
            SendEvent(DeviceEventCode.SyncData, syncDataParameters);
        }
        internal void SendPlayerEvent(Player player, PlayerEventCode eventCode, Dictionary<byte, object> parameters)
        {
            Dictionary<byte, object> eventData = new Dictionary<byte, object>
            {
                { (byte)PlayerEventParameterCode.PlayerID, player.PlayerID },
                { (byte)PlayerEventParameterCode.EventCode, (byte)eventCode },
                { (byte)PlayerEventParameterCode.Parameters, parameters }
            };
            SendEvent(DeviceEventCode.PlayerEvent, eventData);
        }
    }
}
