using HCI_Project.Protocol;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ConnectPlayerController : MonoBehaviour
{
    [SerializeField]
    private InputField nicknameInputField;
    [SerializeField]
    private GameObject cannotConnectToNetworkPanel;


    void Start ()
    {
        cannotConnectToNetworkPanel.SetActive(!Global.PhotonService.ServerConnected);
        Global.PhotonService.OnConnectStatusChanged += PhotonService_OnConnectStatusChanged;
        Global.Device.OnPlayerChanged += Device_OnPlayerChanged;
	}

    private void PhotonService_OnConnectStatusChanged(bool connected)
    {
        cannotConnectToNetworkPanel.SetActive(!connected);
    }

    private void OnDestroy()
    {
        Global.PhotonService.OnConnectStatusChanged -= PhotonService_OnConnectStatusChanged;
        Global.Device.OnPlayerChanged -= Device_OnPlayerChanged;
    }

    private void Device_OnPlayerChanged(HCI_Project.Library.Device device)
    {
        switch(Global.DeviceCode)
        {
            case DeviceCode.Head:
                SceneManager.LoadScene("Main_Head");
                break;
            case DeviceCode.HandTake:
                SceneManager.LoadScene("Main_HandTake");
                break;
        }
    }
    public void ConnectHeadDevice()
    {
        if(nicknameInputField.text != null)
        {
            Global.DeviceCode = DeviceCode.Head;
            Global.Device.RequestManager.ConnectPlayer(nicknameInputField.text, DeviceCode.Head);
        }
    }
    public void ConnectHandTakeDevice()
    {
        if (nicknameInputField.text != null)
        {
            Global.DeviceCode = DeviceCode.HandTake;
            Global.Device.RequestManager.ConnectPlayer(nicknameInputField.text, DeviceCode.HandTake);
        }
    }
}
