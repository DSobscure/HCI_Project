using HCI_Project.Library;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HCI_Project.Server
{
    public class DeviceFactory
    {
        public static DeviceFactory Instance { get; private set; }

        public static void Initial()
        {
            Instance = new DeviceFactory();
        }

        private Dictionary<Guid, ServerDevice> connectedDevices = new Dictionary<Guid, ServerDevice>();
        public IEnumerable<ServerDevice> Devices { get { return connectedDevices.Values.ToArray(); } }

        public bool ContainsDeviceGuid(Guid guid)
        {
            return connectedDevices.ContainsKey(guid);
        }
        public bool AddDevice(ServerDevice device)
        {
            if (ContainsDeviceGuid(device.Guid))
            {
                LogService.InfoFormat($"Device: {device} Duplicated");
                return false;
            }
            else
            {
                connectedDevices.Add(device.Guid, device);
                LogService.InfoFormat($"Device: {device} Connect");
                return true;
            }
        }
        public void RemoveDevice(ServerDevice device)
        {
            if (ContainsDeviceGuid(device.Guid))
            {
                connectedDevices.Remove(device.Guid);
                LogService.InfoFormat($"Device: {device} Disconnect");
                device.RemovePlayer();
            }
        }
    }
}
