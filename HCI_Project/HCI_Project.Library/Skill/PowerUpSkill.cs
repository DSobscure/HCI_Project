using HCI_Project.Protocol;

namespace HCI_Project.Library.Skill
{
    public class PowerUpSkill : Skill
    {
        public int Damage { get; private set; }
        public int ManaConsumption { get; private set; }

        public override string Description
        {
            get
            {
                return $"魔法彈傷害提升到{Damage}，魔力消耗升為{ManaConsumption}";
            }
        }

        public override SkillCode SkillCode
        {
            get
            {
                return SkillCode.PowerUp;
            }
        }

        public PowerUpSkill(int skillID, int level, string skillName, int damage, int manaConsumption) : base(skillID, level, skillName)
        {
            Damage = damage;
            ManaConsumption = manaConsumption;
        }

        public override void Learn(Avatar avatar)
        {
            Skill originalSkill;
            avatar.UpgradeSkill(this, out originalSkill);
            if (originalSkill != null)
            {
                avatar.ManaConsumption -= (originalSkill as PowerUpSkill).ManaConsumption;
            }
            avatar.ManaConsumption += ManaConsumption;
            avatar.Damage = Damage;
        }
    }
}
