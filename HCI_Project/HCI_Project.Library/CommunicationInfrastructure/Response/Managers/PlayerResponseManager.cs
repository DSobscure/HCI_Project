using HCI_Project.Library.CommunicationInfrastructure.Response.Handlers;
using HCI_Project.Library.CommunicationInfrastructure.Response.Handlers.Player;
using HCI_Project.Protocol;
using HCI_Project.Protocol.Communication.OperationCodes;
using System;
using System.Collections.Generic;

namespace HCI_Project.Library.CommunicationInfrastructure.Response.Managers
{
    public class PlayerResponseManager
    {
        private readonly Player player;
        protected readonly Dictionary<PlayerOperationCode, ResponseHandler<Player, PlayerOperationCode>> operationTable = new Dictionary<PlayerOperationCode, ResponseHandler<Player, PlayerOperationCode>>();

        internal PlayerResponseManager(Player player)
        {
            this.player = player;

            operationTable.Add(PlayerOperationCode.FetchData, new FetchDataResponseBroker(player));
        }
        public bool Operate(PlayerOperationCode operationCode, ReturnCode returnCode, string operationMessage, Dictionary<byte, object> parameters, out string errorMessage)
        {
            if (operationTable.ContainsKey(operationCode))
            {
                if (operationTable[operationCode].Handle(operationCode, returnCode, operationMessage, parameters, out errorMessage))
                {
                    return true;
                }
                else
                {
                    errorMessage = $"PlayerResponse Error: {operationCode} from Player: {player.Nickname}\nErrorMessage: {errorMessage}";
                    return false;
                }
            }
            else
            {
                errorMessage = $"Unknow PlayerResponse:{operationCode} from Player: {player.Nickname}";
                return false;
            }
        }
        internal void SendResponse(PlayerOperationCode operationCode, ReturnCode errorCode, string operationMessage, Dictionary<byte, object> parameters)
        {
            foreach (Device device in player.Devices)
            {
                device.ResponseManager.SendPlayerResponse(player, operationCode, errorCode, operationMessage, parameters);
            }
        }
    }
}
