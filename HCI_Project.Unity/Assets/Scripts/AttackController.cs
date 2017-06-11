using UnityEngine;
using UnitySampleAssets.CrossPlatformInput;

public class AttackController : MonoBehaviour
{
    float reloadTimer = 0;
    int shootableMask;
    float effectsDisplayTime = 0.2f;

    Light gunLight;
    Light faceLight;

    AudioSource gunAudio;

    ParticleSystem gunParticles;

    LineRenderer gunLine;

    Ray shootRay = new Ray();

    void Awake()
    {
        shootableMask = LayerMask.GetMask("Shootable");

        gunParticles = GetComponent<ParticleSystem>();
        gunLine = GetComponent<LineRenderer>();
        gunLight = GetComponent<Light>();
        faceLight = GetComponentInChildren<Light>();
        gunAudio = GetComponent<AudioSource>();
    }
    void Start()
    {
        Global.Avatar.OnAttack += Fire;
    }
    void Update()
    {
        // Add the time since Update was last called to the timer.
        reloadTimer -= Time.deltaTime;

#if MOBILE_INPUT
        if ((CrossPlatformInputManager.GetAxisRaw("Mouse X") != 0 || CrossPlatformInputManager.GetAxisRaw("Mouse Y") != 0) && timer >= timeBetweenBullets)
        {
            Global.Avatar.Attack();
        }        
#else
        if (Input.GetButton("Fire1") && reloadTimer <= 0 && Time.timeScale != 0)
        {
            Global.Avatar.Attack();
        }
#endif
        if ((Global.Avatar.ReloadTimeSpan - reloadTimer) >= Global.Avatar.ReloadTimeSpan * effectsDisplayTime)
        {
            DisableEffects();
        }
    }

    public void Fire(Avatar avatar)
    {
        reloadTimer = avatar.ReloadTimeSpan;
        gunLight.enabled = true;
        faceLight.enabled = true;

        gunParticles.Stop();
        gunParticles.Play();
        gunAudio.Play();

        gunLine.enabled = true;
        gunLine.SetPosition(0, transform.position);

        shootRay.origin = transform.position;
        shootRay.direction = transform.forward;

        RaycastHit shootHit;
        if (Physics.Raycast(shootRay, out shootHit, avatar.AttackRange, shootableMask))
        {
            CompleteProject.EnemyHealth enemyHealth = shootHit.collider.GetComponent<CompleteProject.EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(avatar.AttackDamage, shootHit.point);
            }
            gunLine.SetPosition(1, shootHit.point);
        }
        else
        {
            gunLine.SetPosition(1, shootRay.origin + shootRay.direction * avatar.AttackRange);
        }
    }

    public void DisableEffects()
    {
        gunLine.enabled = false;
        faceLight.enabled = false;
        gunLight.enabled = false;
    }
}
