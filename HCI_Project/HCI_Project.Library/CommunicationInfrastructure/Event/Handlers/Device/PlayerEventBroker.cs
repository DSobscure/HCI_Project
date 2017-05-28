using HCI_Project.Protocol.Communication.EventCodes;
using HCI_Project.Protocol.Communication.EventParameters.Device;
using System.Collections.Generic;

namespace HCI_Project.Library.CommunicationInfrastructure.Event.Handlers.Device
{
    class PlayerEventBroker : EventHandler<Library.Device, DeviceEventCode>
    {
        internal PlayerEventBroker(Library.Device subject) : base(subject, 3)
        {
        }
        internal override bool Handle(DeviceEventCode eventCode, Dictionary<byte, object> parameters, out string errorMessage)
        {
            if (base.Handle(eventCode, parameters, out errorMessage))
            {
                string nickname = (string)parameters[(byte)PlayerEventParameterCode.Nickname];
                PlayerEventCode resolvedEventCode = (PlayerEventCode)parameters[(byte)PlayerEventParameterCode.EventCode];
                Dictionary<byte, object> resolvedParameters = (Dictionary<byte, object>)parameters[(byte)PlayerEventParameterCode.Parameters];

                if (subject.Player.Nickname == nickname)
                {
                    return subject.Player.EventManager.Operate(resolvedEventCode, resolvedParameters, out errorMessage);
                }
                else
                {
                    errorMessage = $"PlayerEvent Error Player : {nickname} Not in Device: {subject}";
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}
