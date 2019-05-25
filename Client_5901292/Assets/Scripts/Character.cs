using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Character : MonoBehaviour
{
    [SerializeField] int clientId = 0;
    public bool facingLeft = false;
    [Header("Health Component")]
    [SerializeField] int maxHealth = 100;
    [SerializeField] GameObject healthBarObj = null;
    [SerializeField] GameObject healthBarMask = null;
    [SerializeField] GameObject textObj = null;
    [Header("Gun Component")]
    [SerializeField] Gun gun = null;
    [SerializeField] LayerMask targetLayer = 0;
    private Health characterHealth = null;

    public void DoShoot(float z)
    {
        gun.Shoot(z);
    }
    public void DoShoot()
    {
        gun.Shoot(targetLayer);
    }
    public void SetEnableGun()
    {
        gun.EnableGun();
    }
    public void GunFlip()
    {
        if(facingLeft)
        {
            gun.SetFlip(-1);
        }
        else
        {
            gun.SetFlip(1);
        }
    }
    public void TakeDamage(int Damage)
    {
        characterHealth.HP -= Damage;
        GameCore.networkManager.sendTakeDamage(characterHealth.HP);
        if(characterHealth.HP <= 0)
        {
            GameCore.networkManager.sendWinner();
        }
    }
    public void SetHealth(int health)
    {
        characterHealth.HP = health;
    }
    public void UpdateHP(int current)
    {
        var percentage = ((current * 100) / characterHealth.maxHealth) * 0.01f;
        healthBarMask.transform.localScale = new Vector3(percentage, healthBarMask.transform.localScale.y, healthBarMask.transform.localScale.z);
    }
    public void SetUp(string name)
    {
        textObj.GetComponent<TextMesh>().text = name;
        characterHealth = new Health(maxHealth);
        characterHealth.OnHPChanged += UpdateHP;
        textObj.GetComponent<Renderer>().sortingLayerName = "UI";
        UpdateHP(characterHealth.HP);
        UpdateFacing();
    }
    public void UpdateFacing()
    {
        transform.rotation = Quaternion.identity;
        if (facingLeft)
        {
            transform.localScale = new Vector3(-1,1,1);
            textObj.transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
            textObj.transform.localScale = new Vector3(1, 1, 1);
        }
    }
}
