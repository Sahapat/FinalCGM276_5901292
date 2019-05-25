using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health
{
    public delegate void _func(int a);
    public event _func OnHPChanged;
    
    private int _HP = 0;
    public int HP
    {
        get
        {
            return _HP;
        }
        set
        {
            _HP = value;
            _HP = Mathf.Clamp(_HP,0,maxHealth);
            OnHPChanged?.Invoke(_HP);
        }
    }

    public int maxHealth {get;private set;}
    public Health(int maxHealth)
    {
        this.maxHealth = maxHealth;
        HP = maxHealth;
    }
}
