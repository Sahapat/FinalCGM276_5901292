using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyPathColiderChecker : MonoBehaviour
{
    [SerializeField]float multiplyDamage = 1f;
    private Character m_player = null;

    void Awake()
    {
        m_player = GetComponentInParent<Character>();
    }

    public void TakeHit(int Damage)
    {
        var applyDamage = Damage * multiplyDamage;
        m_player.TakeDamage((int)applyDamage);
    }
}
