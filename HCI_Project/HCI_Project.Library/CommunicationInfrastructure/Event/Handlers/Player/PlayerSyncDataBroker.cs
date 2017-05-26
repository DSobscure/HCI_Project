using HCI_Project.Protocol.Communication.EventCodes;
using HCI_Project.Protocol.Communication.SyncDataCodes;
using System.Collections.Generic;

namespace HCI_Project.Library.CommunicationInfrastructure.Event.Handlers.Player
{
    public class PlayerSyncDataBroker : SyncDataResolver<Library.Player, PlayerEventCode, PlayerSyncDataCode>
    {
        internal PlayerSyncDataBroker(Library.Player subject) : base(subject)
        {

        }

        internal override void SendSyncData(PlayerSyncDataCode syncCode, Dictionary<byte, object> parameters)
        {
            subject.EventManager.SendSyncDataEvent(syncCode, parameters);
        }
    }
}
