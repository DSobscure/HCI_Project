using HCI_Project.Library.CommunicationInfrastructure.Event.Handlers;
using HCI_Project.Library.CommunicationInfrastructure.Event.Handlers.Player;
using HCI_Project.Protocol;
using HCI_Project.Protocol.Communication.EventCodes;
using HCI_Project.Protocol.Communication.EventParameters.Player;
using HCI_Project.Protocol.Communication.SyncDataCodes;
using HCI_Project.Protocol.Communication.SyncDataParameters;
using System.Collections.Generic;

namespace HCI_Project.Library.CommunicationInfrastructure.Event.Managers
{
    public class PlayerEventManager
    {
        private readonly Player player;
        private readonly Dictionary<PlayerEventCode, EventHandler<Player, PlayerEventCode>> eventTable = new Dictionary<PlayerEventCode, EventHandler<Player, PlayerEventCode>>();
        public PlayerSyncDataBroker SyncDataBroker { get; private set; }

        public delegate void RemoteOperationEventHandler(DeviceCode deviceCode, byte operationCode, Dictionary<byte, object> parameters);
        public event RemoteOperationEventHandler OnRemoteOperation;

        internal PlayerEventManager(Player player)
        {
            this.player = player;
            SyncDataBroker = new PlayerSyncDataBroker(player);

            eventTable.Add(PlayerEventCode.SyncData, SyncDataBroker);
            eventTable.Add(PlayerEventCode.RemoteOperation, new RemoteOperationHandler(player));
        }

        internal bool Operate(PlayerEventCode eventCode, Dictionary<byte, object> parameters, out string errorMessage)
        {
            if (eventTable.ContainsKey(eventCode))
            {
                if (eventTable[eventCode].Handle(eventCode, parameters, out errorMessage))
                {
                    return true;
                }
                else
                {
                    errorMessage = $"Player Event Error: {eventCode} from Player: {player}\nErrorMessage: {errorMessage}";
                    return false;
                }
            }
            else
            {
                errorMessage = $"Unknow Player Event:{eventCode} from Player: {player}";
                return false;
            }
        }

        internal void SendEvent(PlayerEventCode eventCode, Dictionary<byte, object> parameters)
        {
            foreach(Device device in player.Devices)
            {
                device.EventManager.SendPlayerEvent(player, eventCode, parameters);
            }
        }
        internal void SendSyncDataEvent(PlayerSyncDataCode syncCode, Dictionary<byte, object> parameters)
        {
            Dictionary<byte, object> syncDataParameters = new Dictionary<byte, object>
            {
                { (byte)SyncDataEventParameterCode.SyncDataCode, (byte)syncCode },
                { (byte)SyncDataEventParameterCode.Parameters, parameters }
            };
            SendEvent(PlayerEventCode.SyncData, syncDataParameters);
        }

        public void RemoteOperation(DeviceCode deviceCode, byte operationCode, Dictionary<byte, object> operationParameters)
        {
            Dictionary<byte, object> parameters = new Dictionary<byte, object>
            {
                { (byte)RemoteOperationParameterCode.DeviceCode, (byte)deviceCode },
                { (byte)RemoteOperationParameterCode.OperationCode, operationCode },
                { (byte)RemoteOperationParameterCode.Parameters, operationParameters }
            };
            SendEvent(PlayerEventCode.RemoteOperation, parameters);
        }
        internal void RemoteOperationEvent(DeviceCode deviceCode, byte operationCode, Dictionary<byte, object> operationParameters)
        {
            OnRemoteOperation?.Invoke(deviceCode, operationCode, operationParameters);
        }
    }
}
