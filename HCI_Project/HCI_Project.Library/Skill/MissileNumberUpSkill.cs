using HCI_Project.Protocol;

namespace HCI_Project.Library.Skill
{
    public class MissileNumberUpSkill : Skill
    {
        public int MissileNumber { get; private set; }
        public int ManaConsumption { get; private set; }

        public override string Description
        {
            get
            {
                return $"魔法彈數量提升到{MissileNumber}，魔力消耗升為{ManaConsumption}";
            }
        }

        public override SkillCode SkillCode
        {
            get
            {
                return SkillCode.MissileNumberUp;
            }
        }

        public MissileNumberUpSkill(int skillID, int level, string skillName, int missileNumber, int manaConsumption) : base(skillID, level, skillName)
        {
            MissileNumber = missileNumber;
            ManaConsumption = manaConsumption;
        }

        public override void Learn(Avatar avatar)
        {
            Skill originalSkill;
            avatar.UpgradeSkill(this, out originalSkill);
            if (originalSkill != null)
            {
                avatar.ManaConsumption -= (originalSkill as MissileNumberUpSkill).ManaConsumption;
            }
            avatar.ManaConsumption += ManaConsumption;
            avatar.MissileNumber = MissileNumber;
        }
    }
}
