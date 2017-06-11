using HCI_Project.Library;
using HCI_Project.Protocol;
using System.Collections.Generic;
using System.Linq;

namespace HCI_Project.Server
{
    public class PlayerFactory
    {
        public static PlayerFactory Instance { get; private set; }

        public static void Initial()
        {
            Instance = new PlayerFactory();
        }

        private Dictionary<string, Player> playerDictionary = new Dictionary<string, Player>();
        public IEnumerable<Player> Players { get { return playerDictionary.Values.ToArray(); } }

        public bool ContainsPlayer(string nickname)
        {
            return playerDictionary.ContainsKey(nickname);
        }
        public bool AddPlayer(Player player)
        {
            if (ContainsPlayer(player.Nickname))
            {
                LogService.InfoFormat($"Nickname: {player.Nickname} Duplicated");
                return false;
            }
            else
            {
                playerDictionary.Add(player.Nickname, player);
                AssemblyPlayerEvents(player);
                LogService.InfoFormat($"Player: {player} Play");
                return true;
            }
        }
        public void RemovePlayer(string nickname)
        {
            if (ContainsPlayer(nickname))
            {
                DisassemblyPlayerEvents(playerDictionary[nickname]);
                playerDictionary.Remove(nickname);
                LogService.InfoFormat($"Player: {nickname} Exit");
            }
        }
        public bool CreatePlayer(string nickname, out Player player, out ReturnCode returnCode, out string errorMessage)
        {
            if(ContainsPlayer(nickname))
            {
                returnCode = ReturnCode.Duplicate;
                errorMessage = "暱稱已被使用";
                player = null;
                return false;
            }
            else
            {
                returnCode = ReturnCode.Successful;
                errorMessage = "";
                player = new Player(nickname);
                AddPlayer(player);
                return true;
            }
        }
        public bool FindPlayer(string nickname, out Player player)
        {
            if (ContainsPlayer(nickname))
            {
                player = playerDictionary[nickname];
                return true;
            }
            else
            {
                player = null;
                return false;
            }
        }

        private void AssemblyPlayerEvents(Player player)
        {
            player.OnHeadDeviceConnectedChanged += player.EventManager.SyncDataBroker.SyncHeadDeviceConnected;
            player.OnHandTakeDeviceConnectedChanged += player.EventManager.SyncDataBroker.SyncHandTakeDeviceConnected;
            player.OnDeviceRemoved += Player_OnDeviceRemoved;
        }

        private void Player_OnDeviceRemoved(Player player, Device device)
        {
            if (player.ConnectedDeviceCount == 0)
            {
                RemovePlayer(player.Nickname);
            }
        }

        private void DisassemblyPlayerEvents(Player player)
        {
            player.OnHeadDeviceConnectedChanged -= player.EventManager.SyncDataBroker.SyncHeadDeviceConnected;
            player.OnHandTakeDeviceConnectedChanged -= player.EventManager.SyncDataBroker.SyncHandTakeDeviceConnected;
            player.OnDeviceRemoved -= Player_OnDeviceRemoved;
        }
    }
}
