using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField]int damagePerHit = 0;
    [SerializeField] GameObject bulletObj = null;
    [SerializeField] Transform shootPosition = null;
    [SerializeField] ParticleSystem GunParticle = null;

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
    public void Shoot(float z)
    {
        if(!isGunEnable)return;
        weaponRotater.SetRotation(z);
        var bullet = Instantiate(bulletObj,shootPosition.position,shootPosition.rotation);
        GunParticle.Play();
        DisableGun();
    }
    public void Shoot(LayerMask mask)
    {
        var bullet = Instantiate(bulletObj,shootPosition.position,shootPosition.rotation);
        bullet.AddComponent(typeof(CollideChecker));
        bullet.GetComponent<CollideChecker>().SetUp(damagePerHit,mask);
        GunParticle.Play();
        DisableGun();
    }
}
