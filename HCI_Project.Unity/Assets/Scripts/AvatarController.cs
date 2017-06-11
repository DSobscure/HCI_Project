using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarController : MonoBehaviour
{
    Animator anim;
    CompleteProject.PlayerMovement playerMovement;
    AttackController attackController;

    // Use this for initialization
    void Awake ()
    {
        Avatar avatar = new Avatar();
        Global.Avatar = avatar;

        avatar.OnHP_Changed += Avatar_OnHP_Changed;
        avatar.OnLevelChanged += Avatar_OnLevelChanged;

        anim = GetComponent<Animator>();
        playerMovement = GetComponent<CompleteProject.PlayerMovement>();
        attackController = GetComponentInChildren<AttackController>();

        StartCoroutine(Restore());
    }

    private void Avatar_OnLevelChanged(Avatar avatar)
    {
        avatar.AttackDamage += 5;
    }

    private void Avatar_OnHP_Changed(Avatar avatar)
    {
        if(avatar.HP <= 0)
        {
            attackController.DisableEffects();
            anim.SetTrigger("Die");
            playerMovement.enabled = false;
            attackController.enabled = false;
        }
    }

    IEnumerator Restore()
    {
        while(true)
        {
            yield return new WaitForSeconds(0.1f);
            Global.Avatar.MP += 5;
        }
    }
}
