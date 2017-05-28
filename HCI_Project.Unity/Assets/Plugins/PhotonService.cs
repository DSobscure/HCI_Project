using ExitGames.Client.Photon;
using HCI_Project.Library;
using HCI_Project.Protocol;
using HCI_Project.Protocol.Communication.EventCodes;
using HCI_Project.Protocol.Communication.OperationCodes;
using System;
using System.Collections.Generic;

public class PhotonService : IPhotonPeerListener
{
    private PhotonPeer peer;

    public event Action<bool> OnConnectStatusChanged;

    private bool serverConnected;
    public bool ServerConnected
    {
        get { return serverConnected; }
        private set
        {
            serverConnected = value;
            if (OnConnectStatusChanged != null)
            {
                OnConnectStatusChanged(serverConnected);
            }
        }
    }

    public PhotonService()
    {
        peer = new PhotonPeer(this, ConnectionProtocol.Tcp);
    }

    public void DebugReturn(DebugLevel level, string message)
    {
        switch(level)
        {
            case DebugLevel.INFO:
                LogService.InfoFormat("{0}:{1}", level, message);
                break;
            case DebugLevel.WARNING:
                LogService.WarningFormat("{0}:{1}", level, message);
                break;
            case DebugLevel.ERROR:
                LogService.ErrorFormat("{0}:{1}", level, message);
                break;
            default:
                LogService.InfoFormat("{0}:{1}", level, message);
                break;
        }
    }

    public void OnEvent(EventData eventData)
    {
        string errorMessage;
        if (!Global.Device.EventManager.Operate((DeviceEventCode)eventData.Code, eventData.Parameters, out errorMessage))
        {
            LogService.WarningFormat("Event Fail, ErrorMessage: {0}", errorMessage);
        }
    }

    public void OnOperationResponse(OperationResponse operationResponse)
    {
        string errorMessage;
        if (!Global.Device.ResponseManager.Operate((DeviceOperationCode)operationResponse.OperationCode, (ReturnCode)operationResponse.ReturnCode, operationResponse.DebugMessage, operationResponse.Parameters, out errorMessage))
        {
            LogService.WarningFormat("OperationResponse Fail, ErrorMessage: {0}", errorMessage);
        }
    }

    public void OnStatusChanged(StatusCode statusCode)
    {
        switch (statusCode)
        {
            case StatusCode.Connect:
                peer.EstablishEncryption();
                break;
            case StatusCode.Disconnect:
                ServerConnected = false;
                break;
            case StatusCode.EncryptionEstablished:
                ServerConnected = true;
                break;
        }
    }

    public void Connect(string serverName, string serverAddress, int port)
    {
        if (peer.Connect(serverAddress + ":" + port.ToString(), serverName))
        {
            DebugReturn(DebugLevel.INFO, peer.PeerState.ToString());
        }
        else
        {
            DebugReturn(DebugLevel.ERROR, "Connect Fail");
            ServerConnected = false;
        }
    }

    public void Service()
    {
        peer.Service();
    }

    public void Disconnect()
    {
        peer.Disconnect();
    }

    public void SendOperation(DeviceOperationCode operationCode, Dictionary<byte, object> parameters)
    {
        peer.OpCustom((byte)operationCode, parameters, true, 0, true);
    }
}
