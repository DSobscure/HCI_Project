using HCI_Project.Protocol;

namespace HCI_Project.Library.Skill
{
    public class ManaConsumptionDownSkill : Skill
    {
        public float DiscountRatio { get; private set; }

        public override string Description
        {
            get
            {
                return $"魔力消耗 -{(int)((1 - DiscountRatio) * 100)}%";
            }
        }

        public override SkillCode SkillCode
        {
            get
            {
                return SkillCode.ManaConsumptionDown;
            }
        }

        public ManaConsumptionDownSkill(int skillID, int level, string skillName, float discountRatio) : base(skillID, level, skillName)
        {
            DiscountRatio = discountRatio;
        }

        public override void Learn(Avatar avatar)
        {
            Skill originalSkill;
            avatar.UpgradeSkill(this, out originalSkill);
            avatar.ManaConsumptionDiscountRatio = DiscountRatio;
        }
    }
}
