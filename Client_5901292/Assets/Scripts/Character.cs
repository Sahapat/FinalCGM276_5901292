using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] int maxHealth = 100;
    [SerializeField] Gun gun = null;
    [SerializeField] LayerMask targetLayer = 0;
    private Health characterHealth = null;

    void Awake()
    {
        characterHealth = new Health(maxHealth);
        characterHealth.OnHPChanged += UpdateHP;
    }
    void Update()
    {
    }
    public void DoShoot(bool isClient)
    {
        if (isClient)
        {
            gun.Shoot(targetLayer);
        }
        else
        {
            gun.Shoot();
        }
    }
    public void TakeDamage(int Damage)
    {
        characterHealth.HP += Damage;
    }
    public void UpdateHP(int current)
    {
        print(current);
    }
}
