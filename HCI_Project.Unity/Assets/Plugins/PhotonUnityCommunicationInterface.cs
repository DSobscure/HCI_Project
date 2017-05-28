using HCI_Project.Library;
using HCI_Project.Library.CommunicationInfrastructure;
using HCI_Project.Protocol;
using HCI_Project.Protocol.Communication.EventCodes;
using HCI_Project.Protocol.Communication.OperationCodes;
using System;
using System.Collections.Generic;

public class PhotonUnityCommunicationInterface : CommunicationInterface
{
    public Device Device
    {
        get
        {
            return Global.Device;
        }

        set
        {
            Global.Device = value;
        }
    }

    public void SendEvent(DeviceEventCode eventCode, Dictionary<byte, object> parameters)
    {
        throw new NotImplementedException();
    }

    public void SendRequest(DeviceOperationCode operationCode, Dictionary<byte, object> parameters)
    {
        Global.PhotonService.SendOperation(operationCode, parameters);
    }

    public void SendResponse(DeviceOperationCode operationCode, ReturnCode returnCode, string debugMessage, Dictionary<byte, object> parameters)
    {
        throw new NotImplementedException();
    }
}
