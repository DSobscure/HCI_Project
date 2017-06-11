using HCI_Project.Protocol;
using HCI_Project.Protocol.Communication.EventCodes;
using HCI_Project.Protocol.Communication.EventParameters.Player;
using System.Collections.Generic;

namespace HCI_Project.Library.CommunicationInfrastructure.Event.Handlers.Player
{
    class RemoteOperationHandler : EventHandler<Library.Player, PlayerEventCode>
    {
        public RemoteOperationHandler(Library.Player subject) : base(subject, 3)
        {
        }

        internal override bool Handle(PlayerEventCode eventCode, Dictionary<byte, object> parameters, out string errorMessage)
        {
            if (base.Handle(eventCode, parameters, out errorMessage))
            {
                DeviceCode deviceCode = (DeviceCode)parameters[(byte)RemoteOperationParameterCode.DeviceCode];
                byte remoteOperationCode = (byte)parameters[(byte)RemoteOperationParameterCode.OperationCode];
                Dictionary<byte, object> remoteOperationParameters = (Dictionary<byte, object>)parameters[(byte)RemoteOperationParameterCode.Parameters];
                subject.EventManager.RemoteOperationEvent(deviceCode, remoteOperationCode, remoteOperationParameters);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
