using HCI_Project.Library.Skill;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class SkillSelectPanel : MonoBehaviour
{
    [SerializeField]
    private Button skillButtonPrefab;

    private void Awake()
    {
        Close();
        if (Global.Player != null)
            Global.Player.EventManager.OnRemoteOperation += EventManager_OnRemoteOperation;
    }

    public void Show(IEnumerable<Skill> skills)
    {
        foreach(Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        foreach(Skill skill in skills)
        {
            StringBuilder skillInfoBuilder = new StringBuilder();
            Button skillButton = Instantiate(skillButtonPrefab, transform);

            skillInfoBuilder.AppendLine(skill.SkillName);
            skillInfoBuilder.AppendLine(string.Format("LV. {0}", skill.Level));
            skillInfoBuilder.AppendLine();
            skillInfoBuilder.AppendLine(skill.Description);
            skillButton.GetComponentInChildren<Text>().text = skillInfoBuilder.ToString();
            Skill targetSkill = skill;
            skillButton.onClick.AddListener(() => 
            {
                Global.Player.RequestManager.RemoteOperation(Global.DeviceCode, (byte)RemoteOperationCode.UpgradeSkill, new System.Collections.Generic.Dictionary<byte, object>
                {
                    { 0, targetSkill.SkillID }
                });
                Close();
            });
        }
        gameObject.SetActive(true);
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }

    private void EventManager_OnRemoteOperation(HCI_Project.Protocol.DeviceCode deviceCode, byte operationCode, System.Collections.Generic.Dictionary<byte, object> parameters)
    {       
        switch ((RemoteOperationCode)operationCode)
        {
            case RemoteOperationCode.ShowUpgradeSkillPanel:
                List<Skill> skills = new List<Skill>
                {
                    SkillTable.GetSkill((int)parameters[0]),
                    SkillTable.GetSkill((int)parameters[1]),
                    SkillTable.GetSkill((int)parameters[2])
                };
                Show(skills);
                break;
            case RemoteOperationCode.UpgradeSkill:
                Close();
                break;
        }
    }
}
