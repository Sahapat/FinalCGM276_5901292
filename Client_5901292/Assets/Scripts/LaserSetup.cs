using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class LaserSetup : MonoBehaviour
{
    [SerializeField]Transform startPos = null;
    [SerializeField]Transform endPos = null;
    public bool isCreateLine = false;
    private LineRenderer m_linerenderer = null;
    void Awake()
    {
        m_linerenderer = GetComponent<LineRenderer>();
    }
    void Update()
    {
        if(isCreateLine)
        {
            m_linerenderer.SetPosition(0,startPos.position);
            m_linerenderer.SetPosition(1,endPos.position);
        }
    }
}
