using HCI_Project.Protocol.Communication.SyncDataCodes;
using HCI_Project.Protocol.Communication.SyncDataParameters.Player;
using System.Collections.Generic;

namespace HCI_Project.Library.CommunicationInfrastructure.Event.Handlers.Player.Sync
{
    class SyncHandDeviceConnectedHandler : SyncDataHandler<Library.Player, PlayerSyncDataCode>
    {
        public SyncHandDeviceConnectedHandler(Library.Player subject) : base(subject, 1)
        {
        }

        public override bool Handle(PlayerSyncDataCode syncCode, Dictionary<byte, object> parameters, out string errorMessage)
        {
            if(base.Handle(syncCode, parameters, out errorMessage))
            {
                bool connected = (bool)parameters[(byte)SyncHandDeviceConnectedParameterCode.Connected];
                subject.HandDeviceConnected = connected;
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
