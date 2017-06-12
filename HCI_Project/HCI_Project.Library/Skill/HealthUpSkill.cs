using HCI_Project.Protocol;

namespace HCI_Project.Library.Skill
{
    public class HealthUpSkill : Skill
    {
        public int ExtraHealth { get; private set; }

        public override string Description
        {
            get
            {
                return $"生命上限增加{ExtraHealth}";
            }
        }

        public override SkillCode SkillCode
        {
            get
            {
                return SkillCode.HealthUp;
            }
        }

        public HealthUpSkill(int skillID, int level, string skillName, int extraHealth) : base(skillID, level, skillName)
        {
            ExtraHealth = extraHealth;
        }

        public override void Learn(Avatar avatar)
        {
            Skill originalSkill;
            avatar.UpgradeSkill(this, out originalSkill);
            avatar.MaxHP += ExtraHealth;
            avatar.HP += ExtraHealth;
        }
    }
}
