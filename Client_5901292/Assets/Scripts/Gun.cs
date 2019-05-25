using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField]int damagePerHit = 0;
    [SerializeField] GameObject bulletObj = null;
    [SerializeField] Transform shootPosition = null;
    [SerializeField] ParticleSystem GunParticle = null;
    [SerializeField] Transform[] direction = null;
    [SerializeField] AudioSource audisource = null;
    private WeaponRotater weaponRotater = null;

    private bool isGunEnable = false;

    void Awake()
    {
        weaponRotater = GetComponent<WeaponRotater>();
        GunParticle.Stop();
    }
    public void EnableGun()
    {
        weaponRotater.StartRotate();
        isGunEnable = true;
    }
    public void DisableGun()
    {
        weaponRotater.StopRotate();
        isGunEnable = false;
    }
    public void SetFlip()
    {
        weaponRotater.SetFlip();
    }
    public void Shoot(float z)
    {
        if(!isGunEnable)return;
        weaponRotater.SetRotation(z);
        var bullet = Instantiate(bulletObj,shootPosition.position,shootPosition.rotation);
        if(transform.root.gameObject.name == "Player1")
        {
            bullet.layer = 9;
        }
        else
        {
            bullet.layer = 13;
        }
        bullet.GetComponent<Bullet>().SetDirection((direction[1].position-direction[0].position).normalized);
        GunParticle.Play();
        DisableGun();
        GameCore.gamemanager.numShoot+=1;
        audisource.Play();
    }
    public void Shoot(LayerMask mask)
    {
        if(!isGunEnable)return;
        var bullet = Instantiate(bulletObj,shootPosition.position,shootPosition.rotation);
        if(transform.root.gameObject.name == "Player1")
        {
            bullet.layer = 9;
        }
        else
        {
            bullet.layer = 13;
        }
        bullet.AddComponent(typeof(CollideChecker));
        bullet.GetComponent<CollideChecker>().SetUp(damagePerHit,mask);
        bullet.GetComponent<Bullet>().SetDirection((direction[1].position-direction[0].position).normalized);
        GunParticle.Play();
        DisableGun();
        GameCore.gamemanager.numShoot+=1;
        GameCore.networkManager.Shoot(this.transform.rotation.z);
        audisource.Play();
    }
}
