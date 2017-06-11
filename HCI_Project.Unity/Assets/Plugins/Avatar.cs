using System;

public class Avatar
{
    private int level;
    public int Level
    {
        get { return level; }
        set
        {
            level = value;
            if(OnLevelChanged != null)
                OnLevelChanged(this);
        }
    }

    private int maxEXP;
    public int MaxEXP
    {
        get { return maxEXP; }
        set
        {
            maxEXP = value;
            if (OnMaxEXP_Changed != null)
                OnMaxEXP_Changed(this);
        }
    }

    private int exp;
    public int EXP
    {
        get { return exp; }
        set
        {
            while(value >= MaxEXP)
            {
                value -= MaxEXP;
                Level++;
            }
            exp = value;
            if (OnEXP_Changed != null)
                OnEXP_Changed(this);
        }
    }

    private int maxHP;
    public int MaxHP
    {
        get { return maxHP; }
        set
        {
            maxHP = value;
            if (OnMaxHP_Changed != null)
                OnMaxHP_Changed(this);
        }
    }

    private int hp;
    public int HP
    {
        get { return hp; }
        set
        {
            hp = Math.Max(Math.Min(value, MaxHP), 0);
            if (OnHP_Changed != null)
                OnHP_Changed(this);
        }
    }

    private int maxMP;
    public int MaxMP
    {
        get { return maxMP; }
        set
        {
            maxMP = value;
            if (OnMaxMP_Changed != null)
                OnMaxMP_Changed(this);
        }
    }

    private int mp;
    public int MP
    {
        get { return mp; }
        set
        {
            mp = Math.Max(Math.Min(value, MaxMP), 0);
            if (OnMP_Changed != null)
                OnMP_Changed(this);
        }
    }

    public int AttackDamage { get; set; }
    public int AttackManaConsumption { get; set; }
    public float AttackRange { get; set; }
    public float ReloadTimeSpan { get; set; }

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
        AttackDamage = 25;
        AttackManaConsumption = 10;
        ReloadTimeSpan = 0.15f;
    }

    public void Attack()
    {
        if(MP >= AttackManaConsumption)
        {
            MP -= AttackManaConsumption;
            if(OnAttack != null)
            {
                OnAttack(this);
            }
        }
    }
}
