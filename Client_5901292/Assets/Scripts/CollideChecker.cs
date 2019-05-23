using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollideChecker : MonoBehaviour
{
    private int Damage = 0;
    private LayerMask mask = 0;
    private bool isSet = false;
    private BoxCollider2D m_boxcolider = null;
    void Awake()
    {
        m_boxcolider = GetComponent<BoxCollider2D>();
    }
    void FixedUpdate()
    {
        if(!isSet)return;

        var hitInfo = Physics2D.OverlapBox(m_boxcolider.transform.position,m_boxcolider.size,0f,mask);

        if(hitInfo)
        {
            var bodyPath = hitInfo.GetComponent<BodyPathColiderChecker>();
            bodyPath.TakeHit(Damage);
            isSet = false;
        }
    }
    public void SetUp(int damage,LayerMask target)
    {
        this.Damage = damage;
        this.mask = target;
        isSet = true;
    }
}
