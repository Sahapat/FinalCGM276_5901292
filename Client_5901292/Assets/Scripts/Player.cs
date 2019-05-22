using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]Gun gun = null;

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            gun.Shoot();
        }
    }
    public void DoShoot()
    {
        gun.Shoot();
    }
}
