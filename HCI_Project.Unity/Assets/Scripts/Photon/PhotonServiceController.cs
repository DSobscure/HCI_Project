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
            GUI.Label(new Rect(50, 50, 200, 20), string.Format("頭戴: {0}, 手持: {1}", Global.Player.HeadDeviceConnected, Global.Player.HandDeviceConnected));
        }
        else
        {
            GUI.Label(new Rect(50, 50, 200, 20), "未連接");
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
