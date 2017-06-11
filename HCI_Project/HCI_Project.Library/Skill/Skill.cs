using HCI_Project.Protocol;

namespace HCI_Project.Library.Skill
{
    public abstract class Skill
    {
        public int SkillID { get; private set; }
        public int Level { get; private set; }
        public string SkillName { get; private set; }
        public abstract SkillCode SkillCode { get; }
        public abstract string Description { get; }
        public abstract void Learn(Avatar avatar);

        protected Skill(int skillID, int level, string skillName)
        {
            SkillID = skillID;
            Level = level;
            SkillName = skillName;
        }
    }
}
