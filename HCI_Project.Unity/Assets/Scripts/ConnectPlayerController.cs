using HCI_Project.Protocol;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ConnectPlayerController : MonoBehaviour
{
    [SerializeField]
    private InputField nicknameInputField;


    void Start ()
    {
        Global.Device.OnPlayerChanged += Device_OnPlayerChanged;
	}
    private void OnDestroy()
    {
        Global.Device.OnPlayerChanged -= Device_OnPlayerChanged;
    }

    private void Device_OnPlayerChanged(HCI_Project.Library.Device device)
    {
        switch(Global.DeviceCode)
        {
            case DeviceCode.Head:
                SceneManager.LoadScene("Main_Head");
                break;
            case DeviceCode.Hand:
                SceneManager.LoadScene("Main_Hand");
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
    public void ConnectHandDevice()
    {
        if (nicknameInputField.text != null)
        {
            Global.DeviceCode = DeviceCode.Hand;
            Global.Device.RequestManager.ConnectPlayer(nicknameInputField.text, DeviceCode.Hand);
        }
    }
}
