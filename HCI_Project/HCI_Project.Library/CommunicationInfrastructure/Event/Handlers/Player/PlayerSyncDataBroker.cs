using HCI_Project.Library.CommunicationInfrastructure.Event.Handlers.Player.Sync;
using HCI_Project.Protocol.Communication.EventCodes;
using HCI_Project.Protocol.Communication.SyncDataCodes;
using HCI_Project.Protocol.Communication.SyncDataParameters.Player;
using System.Collections.Generic;

namespace HCI_Project.Library.CommunicationInfrastructure.Event.Handlers.Player
{
    public class PlayerSyncDataBroker : SyncDataResolver<Library.Player, PlayerEventCode, PlayerSyncDataCode>
    {
        internal PlayerSyncDataBroker(Library.Player subject) : base(subject)
        {
            syncTable.Add(PlayerSyncDataCode.HeadDeviceConnected, new SyncHeadDeviceConnectedHandler(subject));
            syncTable.Add(PlayerSyncDataCode.HandDeviceConnected, new SyncHandDeviceConnectedHandler(subject));
        }

        internal override void SendSyncData(PlayerSyncDataCode syncCode, Dictionary<byte, object> parameters)
        {
            subject.EventManager.SendSyncDataEvent(syncCode, parameters);
        }

        public void SyncHeadDeviceConnected(Library.Player player)
        {
            var parameters = new Dictionary<byte, object>
            {
                { (byte)SyncHeadDeviceConnectedParameterCode.Connected, player.HeadDeviceConnected },
            };
            SendSyncData(PlayerSyncDataCode.HeadDeviceConnected, parameters);
        }
        public void SyncHandDeviceConnected(Library.Player player)
        {
            var parameters = new Dictionary<byte, object>
            {
                { (byte)SyncHandDeviceConnectedParameterCode.Connected, player.HandDeviceConnected },
            };
            SendSyncData(PlayerSyncDataCode.HandDeviceConnected, parameters);
        }
    }
}
