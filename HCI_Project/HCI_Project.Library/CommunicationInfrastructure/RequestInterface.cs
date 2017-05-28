using HCI_Project.Protocol;

namespace HCI_Project.Library.CommunicationInfrastructure
{
    public interface RequestInterface
    {
        bool ConnectPlayer(string nickname, out Player player, out ReturnCode returnCode, out string errorMessage);
    }
}
