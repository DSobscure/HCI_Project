using System;
using HCI_Project.Library;
using HCI_Project.Library.CommunicationInfrastructure;
using HCI_Project.Protocol;

namespace HCI_Project.Server
{
    class ServerRequestInterface : RequestInterface
    {
        public bool ConnectPlayer(string nickname, out Player player, out ReturnCode returnCode, out string errorMessage)
        {
            if(PlayerFactory.Instance.FindPlayer(nickname, out player))
            {
                returnCode = ReturnCode.Successful;
                errorMessage = "";
                return true;
            }
            else if(PlayerFactory.Instance.CreatePlayer(nickname, out player, out returnCode, out errorMessage))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
