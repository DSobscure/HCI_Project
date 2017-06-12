using HCI_Project.Protocol;

namespace HCI_Project.Library.Skill
{
    public class ManaRecoveryUpSkill : Skill
    {
        public float RecoveryRatio { get; private set; }

        public override string Description
        {
            get
            {
                return $"魔力回復 +{(int)(RecoveryRatio - 1) * 100}%";
            }
        }

        public override SkillCode SkillCode
        {
            get
            {
                return SkillCode.ManaRecoveryUp;
            }
        }

        public ManaRecoveryUpSkill(int skillID, int level, string skillName, float recoveryRatio) : base(skillID, level, skillName)
        {
            RecoveryRatio = recoveryRatio;
        }

        public override void Learn(Avatar avatar)
        {
            Skill originalSkill;
            avatar.UpgradeSkill(this, out originalSkill);
            if (originalSkill != null)
            {
                avatar.ManaRecovery /= (originalSkill as ManaRecoveryUpSkill).RecoveryRatio;
            }
            avatar.ManaRecovery *= RecoveryRatio;
        }
    }
}
