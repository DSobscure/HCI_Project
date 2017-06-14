using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        if (Global.Player != null)
            Global.Player.EventManager.OnRemoteOperation += EventManager_OnRemoteOperation;

        avatar.OnHP_Changed += Avatar_OnHP_Changed;
        avatar.OnLevelChanged += Avatar_OnLevelChanged;

        anim = GetComponent<Animator>();
        playerMovement = GetComponent<CompleteProject.PlayerMovement>();
        attackController = GetComponentInChildren<AttackController>();

        StartCoroutine(Restore());
    }

    private void Avatar_OnLevelChanged(HCI_Project.Library.Avatar avatar)
    {
        List<Skill> skills = SkillTable.RandomTakeUpgradableSkills(avatar, 3).ToList();
        Time.timeScale = 0;
        Global.Player.RequestManager.RemoteOperation(Global.DeviceCode, (byte)RemoteOperationCode.ShowUpgradeSkillPanel, new System.Collections.Generic.Dictionary<byte, object>
        {
            { 0, skills[0].SkillID },
            { 1, skills[1].SkillID },
            { 2, skills[2].SkillID }
        });
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
        Global.Player.RequestManager.RemoteOperation(Global.DeviceCode, (byte)RemoteOperationCode.GameOver, new System.Collections.Generic.Dictionary<byte, object>());
    }

    private void EventManager_OnRemoteOperation(HCI_Project.Protocol.DeviceCode deviceCode, byte operationCode, System.Collections.Generic.Dictionary<byte, object> parameters)
    {
        switch ((RemoteOperationCode)operationCode)
        {
            case RemoteOperationCode.UpgradeSkill:
                int skillID = (int)parameters[0];
                SkillTable.GetSkill(skillID).Learn(Global.Avatar);
                Time.timeScale = 1;
                break;
            case RemoteOperationCode.GameOver:
                SceneManager.LoadScene("Main_Head");
                break;
        }
    }
}
