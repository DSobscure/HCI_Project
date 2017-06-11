using HCI_Project.Library.CommunicationInfrastructure.Event.Managers;
using HCI_Project.Library.CommunicationInfrastructure.Request.Managers;
using HCI_Project.Library.CommunicationInfrastructure.Response.Managers;
using HCI_Project.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HCI_Project.Library
{
    public class Player
    {
        public string Nickname { get; private set; }
        private Dictionary<Device, DeviceCode> deviceDictionary = new Dictionary<Device, DeviceCode>();
        public IEnumerable<Device> Devices { get { return deviceDictionary.Keys.ToArray(); } }
        public int ConnectedDeviceCount { get { return deviceDictionary.Count; } }

        private bool headDeviceConnected;
        public bool HeadDeviceConnected
        {
            get { return headDeviceConnected; }
            set
            {
                headDeviceConnected = value;
                OnHeadDeviceConnectedChanged?.Invoke(this);
            }
        }

        private bool handTakeDeviceConnected;
        public bool HandTakeDeviceConnected
        {
            get { return handTakeDeviceConnected; }
            set
            {
                handTakeDeviceConnected = value;
                OnHandTakeDeviceConnectedChanged?.Invoke(this);
            }
        }



        public PlayerEventManager EventManager { get; private set; }
        public PlayerRequestManager RequestManager { get; private set; }
        public PlayerResponseManager ResponseManager { get; private set; }

        public event Action<Player, Device> OnDeviceRemoved;
        public event Action<Player> OnHeadDeviceConnectedChanged;
        public event Action<Player> OnHandTakeDeviceConnectedChanged;

        public Player(string nickname)
        {
            Nickname = nickname;

            EventManager = new PlayerEventManager(this);
            RequestManager = new PlayerRequestManager(this);
            ResponseManager = new PlayerResponseManager(this);
        }
        public override string ToString()
        {
            return $"Player({Nickname})";
        }
        public void AddDevice(Device device, DeviceCode deviceCode)
        {
            switch(deviceCode)
            {
                case DeviceCode.Head:
                    HeadDeviceConnected = true;
                    break;
                case DeviceCode.HandTake:
                    HandTakeDeviceConnected = true;
                    break;
            }
            deviceDictionary.Add(device, deviceCode);
        }
        public bool RemoveDevice(Device device)
        {
            if(deviceDictionary.ContainsKey(device))
            {
                DeviceCode deviceCode = deviceDictionary[device];
                deviceDictionary.Remove(device);
                switch (deviceCode)
                {
                    case DeviceCode.Head:
                        HeadDeviceConnected = deviceDictionary.Values.Any(x => x == DeviceCode.Head);
                        break;
                    case DeviceCode.HandTake:
                        HandTakeDeviceConnected = deviceDictionary.Values.Any(x => x == DeviceCode.HandTake);
                        break;
                }
                OnDeviceRemoved?.Invoke(this, device);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
