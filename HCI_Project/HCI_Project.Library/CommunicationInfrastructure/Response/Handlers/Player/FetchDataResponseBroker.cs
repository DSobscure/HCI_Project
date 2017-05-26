using HCI_Project.Protocol.Communication.FetchDataCodes;
using HCI_Project.Protocol.Communication.OperationCodes;

namespace HCI_Project.Library.CommunicationInfrastructure.Response.Handlers.Player
{
    class FetchDataResponseBroker : FetchDataResponseResolver<Library.Player, PlayerOperationCode, PlayerFetchDataCode>
    {
        internal FetchDataResponseBroker(Library.Player subject) : base(subject)
        {

        }
    }
}
