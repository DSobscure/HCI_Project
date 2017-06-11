using HCI_Project.Protocol;

namespace HCI_Project.Library.Skill
{
    public class ManaUpSkill : Skill
    {
        public int ExtraMana { get; private set; }

        public override string Description
        {
            get
            {
                return $"魔力上限增加{ExtraMana}";
            }
        }

        public override SkillCode SkillCode
        {
            get
            {
                return SkillCode.ManaUp;
            }
        }

        public ManaUpSkill(int skillID, int level, string skillName, int extraMana) : base(skillID, level, skillName)
        {
            ExtraMana = extraMana;
        }

        public override void Learn(Avatar avatar)
        {
            Skill originalSkill;
            avatar.UpgradeSkill(this, out originalSkill);
            avatar.MaxMP += ExtraMana;
            avatar.MP += ExtraMana;
        }
    }
}
