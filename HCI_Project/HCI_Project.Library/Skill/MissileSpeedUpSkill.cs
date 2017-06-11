using HCI_Project.Protocol;

namespace HCI_Project.Library.Skill
{
    public class MissileSpeedUpSkill : Skill
    {
        public float MissileSpeed { get; private set; }
        public int ManaConsumption { get; private set; }

        public override string Description
        {
            get
            {
                return $"魔法彈速度提升到{MissileSpeed}，魔力消耗升為{ManaConsumption}";
            }
        }

        public override SkillCode SkillCode
        {
            get
            {
                return SkillCode.MissileSpeedUp;
            }
        }

        public MissileSpeedUpSkill(int skillID, int level, string skillName, float missileSpeed, int manaConsumption) : base(skillID, level, skillName)
        {
            MissileSpeed = missileSpeed;
            ManaConsumption = manaConsumption;
        }

        public override void Learn(Avatar avatar)
        {
            Skill originalSkill;
            avatar.UpgradeSkill(this, out originalSkill);
            if (originalSkill != null)
            {
                avatar.ManaConsumption -= (originalSkill as MissileSpeedUpSkill).ManaConsumption;
            }
            avatar.ManaConsumption += ManaConsumption;
            avatar.MissileSpeed = MissileSpeed;
        }
    }
}
