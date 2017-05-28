using HCI_Project.Protocol;
using HCI_Project.Protocol.Communication.OperationCodes;
using HCI_Project.Protocol.Communication.ResponseParameters.Device;
using System.Collections.Generic;

namespace HCI_Project.Library.CommunicationInfrastructure.Response.Handlers.Device
{
    class ConnectPlayerResponseHandler : ResponseHandler<Library.Device, DeviceOperationCode>
    {
        public ConnectPlayerResponseHandler(Library.Device subject) : base(subject, 3)
        {
        }

        internal override bool CheckError(DeviceOperationCode operationCode, Dictionary<byte, object> parameters, ReturnCode returnCode, string operationMessage, out string errorMessage)
        {
            if(base.CheckError(operationCode, parameters, returnCode, operationMessage, out errorMessage))
            {
                return true;
            }
            else
            {
                switch(returnCode)
                {
                    case ReturnCode.Duplicate:
                        LogService.Warning($"ConnectPlayer Fail OperationMessahe: {operationMessage}");
                        break;
                    default:
                        break;
                }
                return false;
            }
        }
        internal override bool Handle(DeviceOperationCode operationCode, ReturnCode returnCode, string operationMessage, Dictionary<byte, object> parameters, out string errorMessage)
        {
            if(base.Handle(operationCode, returnCode, operationMessage, parameters, out errorMessage))
            {
                string nickname = (string)parameters[(byte)ConnectPlayerResponseParameterCode.Nickname];
                bool headDeviceConnected = (bool)parameters[(byte)ConnectPlayerResponseParameterCode.HeadDeviceConnected];
                bool handDeviceConnected = (bool)parameters[(byte)ConnectPlayerResponseParameterCode.HandDeviceConnected];

                Library.Player player = new Library.Player(nickname);
                subject.AddPlayer(player, DeviceCode.DontCare);
                player.HeadDeviceConnected = headDeviceConnected;
                player.HandDeviceConnected = handDeviceConnected;
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
