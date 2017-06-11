using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HCI_Project.Protocol;

namespace HCI_Project.Library.Skill
{
    public class MissileSizeUpSkill : Skill
    {
        public float MissileRadius { get; private set; }
        public int ManaConsumption { get; private set; }

        public override string Description
        {
            get
            {
                return $"魔法彈半徑提升到{MissileRadius}，魔力消耗升為{ManaConsumption}";
            }
        }

        public override SkillCode SkillCode
        {
            get
            {
                return SkillCode.MissileSizeUp;
            }
        }

        public MissileSizeUpSkill(int skillID, int level, string skillName, float missileRadius, int manaConsumption) : base(skillID, level, skillName)
        {
            MissileRadius = missileRadius;
            ManaConsumption = manaConsumption;
        }

        public override void Learn(Avatar avatar)
        {
            throw new NotImplementedException();
        }
    }
}
