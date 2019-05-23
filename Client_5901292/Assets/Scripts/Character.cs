using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Character : MonoBehaviour
{
    [SerializeField] int clientId = 0;
    [SerializeField] int maxHealth = 100;
    [SerializeField] Gun gun = null;
    [SerializeField] LayerMask targetLayer = 0;
    private Health characterHealth = null;

    void Awake()
    {
        characterHealth = new Health(maxHealth);
        characterHealth.OnHPChanged += UpdateHP;
    }
    public void DoShoot(int id)
    {
        if (this.clientId == id)
        {
            gun.Shoot(targetLayer);
        }
        else
        {
            gun.Shoot();
        }
    }
    public void DoMoveAndJump(Vector3 position)
    {
    }
    public void SetPositionAndRotation(Vector3 position,float rotationZ)
    {

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
