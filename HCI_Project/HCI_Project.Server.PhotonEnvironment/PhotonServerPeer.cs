using HCI_Project.Library;
using HCI_Project.Protocol.Communication.OperationCodes;
using Photon.SocketServer;
using PhotonHostRuntimeInterfaces;
using System;
using System.Collections.Generic;

namespace HCI_Project.Server.PhotonEnvironment
{
    public class PhotonServerPeer : ClientPeer
    {
        public ServerDevice Device { get; private set; }
        public Guid Guid { get { return Device.Guid; } }

        public PhotonServerPeer(InitRequest initRequest) : base(initRequest)
        {
            Device = new ServerDevice(new PhotonServerCommunicationInterface(this));
            DeviceFactory.Instance.AddDevice(Device);
        }

        protected override void OnDisconnect(DisconnectReason reasonCode, string reasonDetail)
        {
            DeviceFactory.Instance.RemoveDevice(Device);
        }

        protected override void OnOperationRequest(OperationRequest operationRequest, SendParameters sendParameters)
        {
            DeviceOperationCode operationCode = (DeviceOperationCode)operationRequest.OperationCode;
            Dictionary<byte, object> parameters = operationRequest.Parameters;

            string errorMessage;
            if (!Device.RequestManager.Operate(operationCode, parameters, out errorMessage))
            {
                LogService.Error($"Request Fail, Guid: {Guid}\nErrorMessage: {errorMessage}");
            }
        }
    }
}
