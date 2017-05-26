using HCI_Project.Protocol.Communication.FetchDataCodes;
using HCI_Project.Protocol.Communication.OperationCodes;

namespace HCI_Project.Library.CommunicationInfrastructure.Response.Handlers.Device
{
    class FetchDataResponseBroker : FetchDataResponseResolver<Library.Device, DeviceOperationCode, DeviceFetchDataCode>
    {
        internal FetchDataResponseBroker(Library.Device subject) : base(subject)
        {
        }
    }
}
