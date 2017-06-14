using HCI_Project.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HCI_Project.Library.Skill
{
    public static class SkillTable
    {
        private static Dictionary<int, Skill> skillDictionary = new Dictionary<int, Skill>();
        private static Dictionary<SkillCode, List<Skill>> skillTable = new Dictionary<SkillCode, List<Skill>>
        {
            { SkillCode.HealthUp, new List<Skill>
            {
                new HealthUpSkill(1, 1, "HealthUp 1", 25),
                new HealthUpSkill(2, 2, "HealthUp 2", 30),
                new HealthUpSkill(3, 3, "HealthUp 3", 35),
                new HealthUpSkill(4, 4, "HealthUp 4", 40),
                new HealthUpSkill(5, 5, "HealthUp 5", 50),
            } },
            { SkillCode.ManaUp, new List<Skill>
            {
                new ManaUpSkill(6, 1, "ManaUp 1", 50),
                new ManaUpSkill(7, 2, "ManaUp 2", 75),
                new ManaUpSkill(8, 3, "ManaUp 3", 100),
                new ManaUpSkill(9, 4, "ManaUp 4", 150),
                new ManaUpSkill(10, 5, "ManaUp 5", 200),
            } },
            { SkillCode.ManaRecoveryUp, new List<Skill>
            {
                new ManaRecoveryUpSkill(11, 1, "ManaSpring 1", 1.3f),
                new ManaRecoveryUpSkill(12, 2, "ManaSpring 2", 1.7f),
                new ManaRecoveryUpSkill(13, 3, "ManaSpring 3", 2.05f),
                new ManaRecoveryUpSkill(14, 4, "ManaSpring 4", 2.5f),
                new ManaRecoveryUpSkill(15, 5, "ManaSpring 5", 3f),
            } },
            { SkillCode.ManaConsumptionDown, new List<Skill>
            {
                new ManaConsumptionDownSkill(16, 1, "MagicSkill 1", 0.85f),
                new ManaConsumptionDownSkill(17, 2, "MagicSkill 2", 0.75f),
                new ManaConsumptionDownSkill(18, 3, "MagicSkill 3", 0.6f),
                new ManaConsumptionDownSkill(19, 4, "MagicSkill 4", 0.4f),
                new ManaConsumptionDownSkill(20, 5, "MagicSkill 5", 0.2f),
            } },
            { SkillCode.PowerUp, new List<Skill>
            {
                new PowerUpSkill(21, 1, "PowerUp 1", 20, 5),
                new PowerUpSkill(22, 2, "PowerUp 2", 30, 10),
                new PowerUpSkill(23, 3, "PowerUp 3", 40, 16),
                new PowerUpSkill(24, 4, "PowerUp 4", 50, 24),
                new PowerUpSkill(25, 5, "PowerUp 5", 75, 35),
            } },
            { SkillCode.MissileSpeedUp, new List<Skill>
            {
                new MissileSpeedUpSkill(26, 1, "FlashMissile 1", 8, 2),
                new MissileSpeedUpSkill(27, 2, "FlashMissile 2", 12, 6),
                new MissileSpeedUpSkill(28, 3, "FlashMissile 3", 18, 10),
                new MissileSpeedUpSkill(29, 4, "FlashMissile 4", 25, 14),
                new MissileSpeedUpSkill(30, 5, "FlashMissile 5", 40, 20),
            } },
            { SkillCode.MissileNumberUp, new List<Skill>
            {
                new MissileNumberUpSkill(31, 1, "MutiMissile 1", 2, 8),
                new MissileNumberUpSkill(32, 2, "MutiMissile 2", 3, 16),
                new MissileNumberUpSkill(33, 3, "MutiMissile 3", 4, 24),
                new MissileNumberUpSkill(34, 4, "MutiMissile 4", 5, 36),
                new MissileNumberUpSkill(35, 5, "MutiMissile 5", 6, 50),
            } },
            { SkillCode.MissileSizeUp, new List<Skill>
            {
                new MissileSizeUpSkill(36, 1, "Comet 1", 0.15f, 3),
                new MissileSizeUpSkill(37, 2, "Comet 2", 0.2f, 8),
                new MissileSizeUpSkill(38, 3, "Comet 3", 0.3f, 15),
                new MissileSizeUpSkill(39, 4, "Comet 4", 0.5f, 20),
                new MissileSizeUpSkill(40, 5, "Comet 5", 1f, 25),
            } },
        };

        static SkillTable()
        {
            foreach(var skills in skillTable.Values)
            {
                foreach(var skill in skills)
                {
                    skillDictionary.Add(skill.SkillID, skill);
                }
            }
        }

        public static IEnumerable<Skill> UpgradableSkills(Avatar avatar)
        {
            List<Skill> skills = new List<Skill>();
            foreach (var pair in skillTable)
            {
                if(avatar.Skills.Any(x => x.SkillCode == pair.Key && x.Level < 5))
                {
                    int level = avatar.Skills.First(x => x.SkillCode == pair.Key && x.Level < 5).Level;
                    skills.Add(pair.Value[level]);
                }
                else if(!avatar.Skills.Any(x => x.SkillCode == pair.Key))
                {
                    skills.Add(pair.Value[0]);
                }
            }
            return skills;
        }
        public static IEnumerable<Skill> RandomTakeUpgradableSkills(Avatar avatar, int count)
        {
            IEnumerable<Skill> skills = UpgradableSkills(avatar);
            Random random = new Random();
            return skills.OrderBy(x => random.NextDouble()).Take(count);
        }
        public static Skill GetSkill(int skillID)
        {
            return skillDictionary[skillID];
        }
    }
}
