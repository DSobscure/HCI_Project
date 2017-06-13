using DigitalRuby.PyroParticles;
using UnityEngine;
using UnitySampleAssets.CrossPlatformInput;

public class AttackInputController : MonoBehaviour
{
    float reloadTimer = 0;

    public void FireInput()
    {
        Global.Player.RequestManager.RemoteOperation(Global.DeviceCode, (byte)RemoteOperationCode.Fire, new System.Collections.Generic.Dictionary<byte, object>());
    }
}
