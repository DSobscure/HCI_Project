using System.Collections;
using HCI_Project.Library.Skill;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AvatarController : MonoBehaviour
{
    [SerializeField]
    private SkillSelectPanel skillSelectPanel;

    Animator anim;
    CompleteProject.PlayerMovement playerMovement;
    AttackController attackController;

    // Use this for initialization
    void Awake ()
    {
        HCI_Project.Library.Avatar avatar = new HCI_Project.Library.Avatar();
        Global.Avatar = avatar;

        avatar.OnHP_Changed += Avatar_OnHP_Changed;
        avatar.OnLevelChanged += Avatar_OnLevelChanged;

        anim = GetComponent<Animator>();
        playerMovement = GetComponent<CompleteProject.PlayerMovement>();
        attackController = GetComponentInChildren<AttackController>();

        StartCoroutine(Restore());
    }

    private void Avatar_OnLevelChanged(HCI_Project.Library.Avatar avatar)
    {
        skillSelectPanel.Show(SkillTable.RandomTakeUpgradableSkills(avatar, 3));
        Time.timeScale = 0;
    }

    private void Avatar_OnHP_Changed(HCI_Project.Library.Avatar avatar)
    {
        if(avatar.HP <= 0)
        {
            anim.SetTrigger("Die");
            playerMovement.enabled = false;
            attackController.enabled = false;
            StartCoroutine(Restart());
        }
    }

    IEnumerator Restore()
    {
        while(true)
        {
            yield return new WaitForSeconds(0.1f);
            Global.Avatar.MP += Global.Avatar.ManaRecovery * 0.1f;
        }
    }
    IEnumerator Restart()
    {
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene("Main_Head");
    }
}
