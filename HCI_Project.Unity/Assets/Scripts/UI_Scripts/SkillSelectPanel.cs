using HCI_Project.Library.Skill;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class SkillSelectPanel : MonoBehaviour
{
    [SerializeField]
    private Button skillButtonPrefab;

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
                targetSkill.Learn(Global.Avatar);
                Close();
            });
        }
        gameObject.SetActive(true);
    }

    public void Close()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1;
    }
}
