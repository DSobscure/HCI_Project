using UnityEngine;

public class PhotonServiceController : MonoBehaviour
{
    void Awake()
    {
        Global.PhotonService.OnConnectStatusChanged += Instance_OnConnectStatusChanged;
    }
    void Start()
    {
        DontDestroyOnLoad(this);
        Global.PhotonService.Connect("HCI_Project.DevelopmentServer", "140.113.123.134", 25000);
    }
    void Update()
    {
        Global.PhotonService.Service();
    }
    private void OnGUI()
    {
        if(Global.Player != null)
        {
            GUI.Label(new Rect(50, 50, 300, 20), string.Format("Head Device: {0}, HandTake Device: {1}", Global.Player.HeadDeviceConnected, Global.Player.HandTakeDeviceConnected));
        }
        else
        {
            GUI.Label(new Rect(50, 50, 200, 20), "No Connected Device");
        }
    }
    void OnDestroy()
    {
        Global.PhotonService.Disconnect();
        Global.PhotonService.OnConnectStatusChanged -= Instance_OnConnectStatusChanged;
    }

    private void Instance_OnConnectStatusChanged(bool connected)
    {
        if (connected)
        {
            Debug.Log("Connected");
        }
        else
        {
            Debug.Log("Disconnected");
        }
    }
}
