using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RemoteNewGame : MonoBehaviour
{
    void Awake()
    {
        if (Global.Player != null)
            Global.Player.EventManager.OnRemoteOperation += EventManager_OnRemoteOperation;
    }

    public void NewGame()
    {
        Global.Player.RequestManager.RemoteOperation(Global.DeviceCode, (byte)RemoteOperationCode.NewGame, new System.Collections.Generic.Dictionary<byte, object>());
    }
    private void EventManager_OnRemoteOperation(HCI_Project.Protocol.DeviceCode deviceCode, byte operationCode, System.Collections.Generic.Dictionary<byte, object> parameters)
    {
        switch ((RemoteOperationCode)operationCode)
        {
            case RemoteOperationCode.NewGame:
                if(Global.DeviceCode == HCI_Project.Protocol.DeviceCode.Head)
                    SceneManager.LoadScene("Game_Head");
                else if (Global.DeviceCode == HCI_Project.Protocol.DeviceCode.HandTake)
                    SceneManager.LoadScene("Game_HandTake");
                break;
        }
    }
}
