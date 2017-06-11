using System;

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
        public int Damage { get; set; }
        public float MissileSpeed { get; set; }
        public float ReloadTimeSpan { get; set; }
        public int MissileNumber { get; set; }
        public float　MissleRadius { get; set; }

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
            AttackRange = 100;
            Level = 1;
            MaxEXP = 100;
            MaxHP = 100;
            HP = 100;
            MaxMP = 50;
            MP = 50;
            Damage = 25;
            ManaConsumption = 10;
            ReloadTimeSpan = 0.15f;
        }

        public void Attack()
        {
            if (MP >= ManaConsumption)
            {
                MP -= ManaConsumption;
                OnAttack?.Invoke(this);
            }
        }
    }
}
