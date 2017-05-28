using HCI_Project.Protocol;
using HCI_Project.Protocol.Communication.OperationCodes;
using HCI_Project.Protocol.Communication.RequestParameters.Device;
using HCI_Project.Protocol.Communication.ResponseParameters.Device;
using System.Collections.Generic;

namespace HCI_Project.Library.CommunicationInfrastructure.Request.Handlers.Device
{
    class ConnectPlayerHandler : DeviceRequestHandler
    {
        public ConnectPlayerHandler(Library.Device subject) : base(subject, 2)
        {
        }

        internal override bool Handle(DeviceOperationCode operationCode, Dictionary<byte, object> parameters, out string errorMessage)
        {
            if(base.Handle(operationCode, parameters, out errorMessage))
            {
                
                string nickname = (string)parameters[(byte)ConnectPlayerParameterCode.Nickname];
                DeviceCode deviceCode = (DeviceCode)parameters[(byte)ConnectPlayerParameterCode.DeviceCode];
                Library.Player player;
                ReturnCode returnCode;
                if (subject.RequestInterface.ConnectPlayer(nickname, out player, out returnCode, out errorMessage))
                {
                    subject.AddPlayer(player, deviceCode);
                    Dictionary<byte, object> responseParameters = new Dictionary<byte, object>
                    {
                        { (byte)ConnectPlayerResponseParameterCode.Nickname, player.Nickname },
                        { (byte)ConnectPlayerResponseParameterCode.HeadDeviceConnected, player.HeadDeviceConnected },
                        { (byte)ConnectPlayerResponseParameterCode.HandDeviceConnected, player.HandDeviceConnected },
                    };
                    SendResponse(operationCode, returnCode, errorMessage, responseParameters);
                }
                else
                {
                    SendResponse(operationCode, returnCode, errorMessage, new Dictionary<byte, object>());
                }
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
