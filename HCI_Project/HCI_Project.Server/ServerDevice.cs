using HCI_Project.Library;
using HCI_Project.Library.CommunicationInfrastructure;
using System;

namespace HCI_Project.Server
{
    public class ServerDevice : Device
    {
        public Guid Guid { get; protected set; }

        public ServerDevice(CommunicationInterface communicationInterface) : base(communicationInterface, new ServerRequestInterface())
        {
            Guid = Guid.NewGuid();
            while (DeviceFactory.Instance.ContainsDeviceGuid(Guid))
            {
                Guid = Guid.NewGuid();
            }
        }
        public override string ToString()
        {
            return $"Device{Guid}";
        }
    }
}
