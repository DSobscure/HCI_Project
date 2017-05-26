using HCI_Project.Library;
using HCI_Project.Library.CommunicationInfrastructure;
using HCI_Project.Protocol;
using HCI_Project.Protocol.Communication.EventCodes;
using HCI_Project.Protocol.Communication.OperationCodes;
using Photon.SocketServer;
using System.Collections.Generic;
using System;

namespace HCI_Project.Server.PhotonEnvironment
{
    class PhotonServerCommunicationInterface : CommunicationInterface
    {
        private PhotonServerPeer peer;

        public Device Device { get; set; }

        public PhotonServerCommunicationInterface(PhotonServerPeer peer)
        {
            this.peer = peer;
        }

        public void SendEvent(DeviceEventCode eventCode, Dictionary<byte, object> parameters)
        {
            EventData eventData = new EventData
            {
                Code = (byte)eventCode,
                Parameters = parameters
            };
            peer.SendEvent(eventData, new SendParameters());
        }

        public void SendRequest(DeviceOperationCode operationCode, Dictionary<byte, object> parameters)
        {
            LogService.Error($"PhotonServer SendRequest from : {peer.Device}");
        }

        public void SendResponse(DeviceOperationCode operationCode, ReturnCode returnCode, string debugMessage, Dictionary<byte, object> parameters)
        {
            OperationResponse response = new OperationResponse((byte)operationCode, parameters)
            {
                ReturnCode = (short)returnCode,
                DebugMessage = debugMessage
            };
            peer.SendOperationResponse(response, new SendParameters());
        }
    }
}
