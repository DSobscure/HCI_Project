using HCI_Project.Library.CommunicationInfrastructure.Request.Handlers.Player;
using HCI_Project.Protocol;
using HCI_Project.Protocol.Communication.FetchDataCodes;
using HCI_Project.Protocol.Communication.FetchDataRequestParameters;
using HCI_Project.Protocol.Communication.OperationCodes;
using HCI_Project.Protocol.Communication.RequestParameters.Player;
using System.Collections.Generic;

namespace HCI_Project.Library.CommunicationInfrastructure.Request.Managers
{
    public class PlayerRequestManager
    {
        private readonly Player player;
        private readonly Dictionary<PlayerOperationCode, RequestHandler<Player, PlayerOperationCode>> operationTable = new Dictionary<PlayerOperationCode, RequestHandler<Player, PlayerOperationCode>>();
        public PlayerFetchDataRequestBroker FetchDataRequestBroker { get; private set; }

        internal PlayerRequestManager(Player player)
        {
            this.player = player;
            FetchDataRequestBroker = new PlayerFetchDataRequestBroker(player);

            operationTable.Add(PlayerOperationCode.FetchData, FetchDataRequestBroker);
            operationTable.Add(PlayerOperationCode.RemoteOperation, new RemoteOperationHandler(player));
        }
        internal bool Operate(PlayerOperationCode operationCode, Dictionary<byte, object> parameters, out string errorMessage)
        {
            if (operationTable.ContainsKey(operationCode))
            {
                if (operationTable[operationCode].Handle(operationCode, parameters, out errorMessage))
                {
                    return true;
                }
                else
                {
                    errorMessage = $"PlayerOperation Error: {operationCode} from Player: {player.Nickname}\nErrorMessahe: {errorMessage}";
                    return false;
                }
            }
            else
            {
                errorMessage = $"Unknow PlayerOperation:{operationCode} from Player: {player.Nickname}";
                return false;
            }
        }
        internal void SendOperation(PlayerOperationCode operationCode, Dictionary<byte, object> parameters)
        {
            foreach (Device device in player.Devices)
            {
                device.RequestManager.SendPlayerOperation(player, operationCode, parameters);
            }
        }

        internal void SendFetchDataOperation(PlayerFetchDataCode fetchCode, Dictionary<byte, object> parameters)
        {
            Dictionary<byte, object> fetchDataParameters = new Dictionary<byte, object>
            {
                { (byte)FetchDataRequestParameterCode.FetchDataCode, (byte)fetchCode },
                { (byte)FetchDataRequestParameterCode.Parameters, parameters }
            };
            SendOperation(PlayerOperationCode.FetchData, fetchDataParameters);
        }

        public void RemoteOperation(DeviceCode deviceCode, byte operationCode, Dictionary<byte, object> operationParameters)
        {
            Dictionary<byte, object> parameters = new Dictionary<byte, object>
            {
                { (byte)RemoteOperationParameterCode.DeviceCode, (byte)deviceCode },
                { (byte)RemoteOperationParameterCode.OperationCode, operationCode },
                { (byte)RemoteOperationParameterCode.Parameters, operationParameters }
            };
            SendOperation(PlayerOperationCode.RemoteOperation, parameters);
        }
    }
}
