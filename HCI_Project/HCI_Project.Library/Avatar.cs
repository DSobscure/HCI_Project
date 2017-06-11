using System;
using System.Collections.Generic;
using System.Linq;

namespace HCI_Project.Library
{
    public class Avatar
    {
        private int level;
        public int Level
        {
            get { return level; }
            set
            {
                level = value;
                OnLevelChanged?.Invoke(this);
            }
        }

        private int maxEXP;
        public int MaxEXP
        {
            get { return maxEXP; }
            set
            {
                maxEXP = value;
                OnMaxEXP_Changed?.Invoke(this);
            }
        }

        private int exp;
        public int EXP
        {
            get { return exp; }
            set
            {
                while (value >= MaxEXP)
                {
                    value -= MaxEXP;
                    Level++;
                }
                exp = value;
                OnEXP_Changed?.Invoke(this);
            }
        }

        private int maxHP;
        public int MaxHP
        {
            get { return maxHP; }
            set
            {
                maxHP = value;
                OnMaxHP_Changed?.Invoke(this);
            }
        }

        private float hp;
        public float HP
        {
            get { return hp; }
            set
            {
                hp = Math.Max(Math.Min(value, MaxHP), 0);
                OnHP_Changed?.Invoke(this);
            }
        }

        private int maxMP;
        public int MaxMP
        {
            get { return maxMP; }
            set
            {
                maxMP = value;
                OnMaxMP_Changed?.Invoke(this);
            }
        }

        private float mp;
        public float MP
        {
            get { return mp; }
            set
            {
                mp = Math.Max(Math.Min(value, MaxMP), 0);
                OnMP_Changed?.Invoke(this);
            }
        }

        public float ManaRecovery { get; set; }       
        public int ManaConsumption { get; set; }
        public float ManaConsumptionDiscountRatio { get; set; }
        public int Damage { get; set; }
        public float MissileSpeed { get; set; }
        public float ReloadTimeSpan { get; set; }
        public int MissileNumber { get; set; }
        public float　MissleRadius { get; set; }

        private List<Skill.Skill> skills = new List<Skill.Skill>();
        public IEnumerable<Skill.Skill> Skills { get { return skills; } }

        public event Action<Avatar> OnLevelChanged;
        public event Action<Avatar> OnMaxEXP_Changed;
        public event Action<Avatar> OnEXP_Changed;
        public event Action<Avatar> OnMaxHP_Changed;
        public event Action<Avatar> OnHP_Changed;
        public event Action<Avatar> OnMaxMP_Changed;
        public event Action<Avatar> OnMP_Changed;

        public event Action<Avatar> OnAttack;

        public Avatar()
        {
            Level = 1;
            MaxEXP = EXP_Table.EXP(1);
            MaxHP = 50;
            HP = 50;
            MaxMP = 100;
            MP = 100;
            ManaRecovery = 20;
            ManaConsumption = 10;
            Damage = 10;
            MissileSpeed = 1;
            ReloadTimeSpan = 0.1f;
            MissileNumber = 1;
            MissleRadius = 0.1f;
        }

        public void Attack()
        {
            if (MP >= ManaConsumption * ManaConsumptionDiscountRatio)
            {
                MP -= ManaConsumption * ManaConsumptionDiscountRatio;
                OnAttack?.Invoke(this);
            }
        }

        public void UpgradeSkill(Skill.Skill skill, out Skill.Skill originalSkill)
        {
            if(Skills.Any(x => x.SkillCode == skill.SkillCode))
            {
                originalSkill = Skills.First(x => x.SkillCode == skill.SkillCode);
                skills.Remove(originalSkill);
            }
            else
            {
                originalSkill = null;
            }
            skills.Add(skill);
        }
    }
}
