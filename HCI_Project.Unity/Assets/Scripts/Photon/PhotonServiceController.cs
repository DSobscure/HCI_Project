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
