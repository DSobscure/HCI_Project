using HCI_Project.Library;
using UnityEngine;

public static class Global
{
    public static PhotonService PhotonService { get; set; }
    public static Device Device { get; set; }
    public static Player Player { get; set; }

    static Global()
    {
        LogService.InitialService(
            infoMethod: Debug.Log,
            infoFormatMethod: Debug.LogFormat,
            warningMethod: Debug.LogWarning,
            warningFormatMethod: Debug.LogWarningFormat,
            errorMethod: Debug.LogError,
            errorFormatMethod: Debug.LogErrorFormat);

        Device = new Device(null, null);
        Device.OnPlayerChanged += Device_OnPlayerChanged;

        PhotonService = new PhotonService();
    }

    private static void Device_OnPlayerChanged(Device device)
    {
        Player = device.Player;
    }
}
