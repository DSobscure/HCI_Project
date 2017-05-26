using HCI_Project.Library.CommunicationInfrastructure.Event.Managers;
using HCI_Project.Library.CommunicationInfrastructure.Request.Managers;
using HCI_Project.Library.CommunicationInfrastructure.Response.Managers;
using System.Collections.Generic;

namespace HCI_Project.Library
{
    public class Player
    {
        public int PlayerID { get; private set; }
        public string Nickname { get; private set; }
        private List<Device> devices = new List<Device>();
        public IEnumerable<Device> Devices { get { return devices.ToArray(); } }
        public int ConnectedDeviceCount { get { return devices.Count; } }

        public PlayerEventManager EventManager { get; private set; }
        public PlayerRequestManager RequestManager { get; private set; }
        public PlayerResponseManager ResponseManager { get; private set; }

        public Player(int playerID, string nickname)
        {
            PlayerID = playerID;
            Nickname = nickname;

            EventManager = new PlayerEventManager(this);
            RequestManager = new PlayerRequestManager(this);
            ResponseManager = new PlayerResponseManager(this);
        }
        public override string ToString()
        {
            return $"Player{PlayerID}({Nickname})";
        }
        public void AddDevice(Device device)
        {
            devices.Add(device);
        }
        public bool RemoveDevice(Device device)
        {
            return devices.Remove(device);
        }
    }
}
