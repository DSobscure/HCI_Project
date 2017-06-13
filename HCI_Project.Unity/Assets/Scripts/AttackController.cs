using UnityEngine;
using UnitySampleAssets.CrossPlatformInput;
using DigitalRuby.PyroParticles;

public class AttackController : MonoBehaviour
{
    float reloadTimer = 0;
    [SerializeField]
    private FireProjectileScript fireballPrefab;
    public Camera m_camera;

    void Start()
    {
        Global.Player.EventManager.OnRemoteOperation += EventManager_OnRemoteOperation;
        Global.Avatar.OnAttack += Fire;
    }

    private void EventManager_OnRemoteOperation(HCI_Project.Protocol.DeviceCode deviceCode, byte operationCode, System.Collections.Generic.Dictionary<byte, object> parameters)
    {
        if(deviceCode == HCI_Project.Protocol.DeviceCode.HandTake)
        {
            if((RemoteOperationCode)operationCode == RemoteOperationCode.Fire)
            {
                if(reloadTimer <= 0 && Time.timeScale != 0)
                    Global.Avatar.Attack();
            }
        }
    }

    void Update()
    {
        transform.rotation = m_camera.transform.rotation;
        // Add the time since Update was last called to the timer.
        reloadTimer -= Time.deltaTime;
    }

    public void Fire(HCI_Project.Library.Avatar avatar)
    {
        reloadTimer = avatar.ReloadTimeSpan;

        for (int i = 0; i < avatar.MissileNumber; i++)
        {
            FireProjectileScript fireball = Instantiate(fireballPrefab, transform);
            fireball.transform.Rotate(0, avatar.MissileNumber / 2 * -5 + 10 * i, 0);
            fireball.ProjectileColliderSpeed = avatar.MissileSpeed / 2;
            ParticleSystem fireParticleSystem = fireball.transform.Find("FireboltParticle").GetComponent<ParticleSystem>();
            fireParticleSystem.startSpeed = avatar.MissileSpeed / 2;
            fireParticleSystem.startSize = avatar.MissleRadius * 5;
            fireball.transform.Find("FireboltCollider").GetComponent<SphereCollider>().radius = avatar.MissleRadius * 2;
        }
    }
}
