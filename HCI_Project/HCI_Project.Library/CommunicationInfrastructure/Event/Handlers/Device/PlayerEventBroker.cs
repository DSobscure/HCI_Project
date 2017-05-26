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
                int playerID = (int)parameters[(byte)PlayerEventParameterCode.PlayerID];
                PlayerEventCode resolvedEventCode = (PlayerEventCode)parameters[(byte)PlayerEventParameterCode.EventCode];
                Dictionary<byte, object> resolvedParameters = (Dictionary<byte, object>)parameters[(byte)PlayerEventParameterCode.Parameters];

                if (subject.Player.PlayerID == playerID)
                {
                    return subject.Player.EventManager.Operate(resolvedEventCode, resolvedParameters, out errorMessage);
                }
                else
                {
                    errorMessage = $"PlayerEvent Error Player ID: {playerID} Not in Device: {subject}";
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
