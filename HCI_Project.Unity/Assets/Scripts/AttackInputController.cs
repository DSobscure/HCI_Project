using UnityEngine.SceneManagement;
using UnityEngine;
using UnitySampleAssets.CrossPlatformInput;

public class AttackInputController : MonoBehaviour
{
    float reloadTimer = 0;

    void Awake()
    {
        if (Global.Player != null)
            Global.Player.EventManager.OnRemoteOperation += EventManager_OnRemoteOperation;
    }
    private void Start()
    {
        Input.gyro.enabled = true;
    }

    public void FireInput()
    {
        Global.Player.RequestManager.RemoteOperation(Global.DeviceCode, (byte)RemoteOperationCode.Fire, new System.Collections.Generic.Dictionary<byte, object>());
    }
    private void EventManager_OnRemoteOperation(HCI_Project.Protocol.DeviceCode deviceCode, byte operationCode, System.Collections.Generic.Dictionary<byte, object> parameters)
    {
        switch ((RemoteOperationCode)operationCode)
        {
            case RemoteOperationCode.GameOver:
                SceneManager.LoadScene("Main_HandTake");
                break;
        }
    }
}
