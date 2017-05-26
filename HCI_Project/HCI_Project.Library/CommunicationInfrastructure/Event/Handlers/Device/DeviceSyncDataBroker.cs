using HCI_Project.Protocol.Communication.EventCodes;
using HCI_Project.Protocol.Communication.SyncDataCodes;
using System.Collections.Generic;

namespace HCI_Project.Library.CommunicationInfrastructure.Event.Handlers.Device
{
    public class DeviceSyncDataBroker : SyncDataResolver<Library.Device, DeviceEventCode, DeviceSyncDataCode>
    {
        internal DeviceSyncDataBroker(Library.Device subject) : base(subject)
        {

        }

        internal override void SendSyncData(DeviceSyncDataCode syncCode, Dictionary<byte, object> parameters)
        {
            subject.EventManager.SendSyncDataEvent(syncCode, parameters);
        }
    }
}
