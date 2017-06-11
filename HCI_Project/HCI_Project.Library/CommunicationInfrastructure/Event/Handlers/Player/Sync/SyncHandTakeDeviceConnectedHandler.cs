using HCI_Project.Protocol.Communication.SyncDataCodes;
using HCI_Project.Protocol.Communication.SyncDataParameters.Player;
using System.Collections.Generic;

namespace HCI_Project.Library.CommunicationInfrastructure.Event.Handlers.Player.Sync
{
    class SyncHandTakeDeviceConnectedHandler : SyncDataHandler<Library.Player, PlayerSyncDataCode>
    {
        public SyncHandTakeDeviceConnectedHandler(Library.Player subject) : base(subject, 1)
        {
        }

        public override bool Handle(PlayerSyncDataCode syncCode, Dictionary<byte, object> parameters, out string errorMessage)
        {
            if(base.Handle(syncCode, parameters, out errorMessage))
            {
                bool connected = (bool)parameters[(byte)SyncHandTakeDeviceConnectedParameterCode.Connected];
                subject.HandTakeDeviceConnected = connected;
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
