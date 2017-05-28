using HCI_Project.Library;
using HCI_Project.Protocol;
using UnityEngine;

public static class Global
{
    public static PhotonService PhotonService { get; set; }
    public static Device Device { get; set; }
    public static Player Player { get; set; }
    public static DeviceCode DeviceCode { get; set; }

    static Global()
    {
        LogService.InitialService(
            infoMethod: Debug.Log,
            infoFormatMethod: Debug.LogFormat,
            warningMethod: Debug.LogWarning,
            warningFormatMethod: Debug.LogWarningFormat,
            errorMethod: Debug.LogError,
            errorFormatMethod: Debug.LogErrorFormat);

        Device = new Device(new PhotonUnityCommunicationInterface(), null);
        Device.OnPlayerChanged += Device_OnPlayerChanged;

        PhotonService = new PhotonService();
    }

    private static void Device_OnPlayerChanged(Device device)
    {
        Player = device.Player;
    }
}
