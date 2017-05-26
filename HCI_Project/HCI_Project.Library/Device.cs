using HCI_Project.Library.CommunicationInfrastructure;
using HCI_Project.Library.CommunicationInfrastructure.Event.Managers;
using HCI_Project.Library.CommunicationInfrastructure.Request.Managers;
using HCI_Project.Library.CommunicationInfrastructure.Response.Managers;

namespace HCI_Project.Library
{
    public class Device
    {
        public Player Player { get; private set; }

        public CommunicationInterface CommunicationInterface { get; private set; }
        public RequestInterface RequestInterface { get; private set; }
        public DeviceEventManager EventManager { get; private set; }
        public DeviceRequestManager RequestManager { get; private set; }
        public DeviceResponseManager ResponseManager { get; private set; }

        public Device(CommunicationInterface communicationInterface, RequestInterface requestInterface)
        {
            CommunicationInterface = communicationInterface;
            RequestInterface = requestInterface;
            EventManager = new DeviceEventManager(this);
            RequestManager = new DeviceRequestManager(this);
            ResponseManager = new DeviceResponseManager(this);
        }
        public void RemovePlayer()
        {
            Player.RemoveDevice(this);
            Player = null;
        }
    }
}
