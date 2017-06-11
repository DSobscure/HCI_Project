using UnityEngine;
using UnityEngine.UI;
using HCI_Project.Library;

public class AvatarUI : MonoBehaviour
{
    public Scrollbar hpScrollbar;
    public Scrollbar mpScrollbar;
    public Text levelText;
    public RectTransform expFill;
    public Image damageImage;
    public float flashSpeed = 2f;
    public Color flashColor = new Color(1f, 0f, 0f, 0.25f);
    private bool damaged = false;

    private void Start()
    {
        Avatar avatar = Global.Avatar;
        avatar.OnHP_Changed += UpdateHP;
        avatar.OnMaxHP_Changed += UpdateHP;
        avatar.OnMP_Changed += UpdateMP;
        avatar.OnMaxMP_Changed += UpdateMP;
        avatar.OnLevelChanged += UpdateLevel;
        avatar.OnEXP_Changed += UpdateEXP;
        avatar.OnMaxEXP_Changed += UpdateEXP;

        UpdateHP(avatar);
        UpdateMP(avatar);
        UpdateLevel(avatar);
        UpdateEXP(avatar);
    }
    private void OnDestroy()
    {
        Avatar avatar = Global.Avatar;
        avatar.OnHP_Changed -= UpdateHP;
        avatar.OnMaxHP_Changed -= UpdateHP;
        avatar.OnMP_Changed -= UpdateMP;
        avatar.OnMaxMP_Changed -= UpdateMP;
        avatar.OnLevelChanged -= UpdateLevel;
        avatar.OnEXP_Changed -= UpdateEXP;
        avatar.OnMaxEXP_Changed -= UpdateEXP;
    }

    void Update()
    {
        if (damaged)
        {
            damageImage.color = flashColor;
        }
        else
        {
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }
        damaged = false;
    }

    private void UpdateHP(HCI_Project.Library.Avatar avatar)
    {
        if (avatar.HP / (float)avatar.MaxHP < hpScrollbar.size)
            damaged = true;
        hpScrollbar.size = avatar.HP / (float)avatar.MaxHP;
    }
    private void UpdateMP(HCI_Project.Library.Avatar avatar)
    {
        mpScrollbar.size = avatar.MP / (float)avatar.MaxMP;
    }
    private void UpdateLevel(HCI_Project.Library.Avatar avatar)
    {
        levelText.text = avatar.Level.ToString();
    }
    private void UpdateEXP(HCI_Project.Library.Avatar avatar)
    {
        expFill.localPosition = new Vector3(0, -expFill.sizeDelta.y * (1 - avatar.EXP / (float)avatar.MaxEXP), 0);
    }
}
