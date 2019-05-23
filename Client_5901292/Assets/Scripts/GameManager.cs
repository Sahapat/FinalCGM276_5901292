using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]Character m_client = null;

    void FixedUpdate()
    {
        if(Input.GetMouseButtonDown(0))
        {
            m_client.DoShoot(true);
        }
    }
}
