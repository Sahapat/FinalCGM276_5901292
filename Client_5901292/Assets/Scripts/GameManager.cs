using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GameCore
{
    public static PlayerDataJson clientPlayerData = null;
    public static PlayerDataJson otherPlayerData = null;
    public static GameManager gamemanager = null;
}
public class GameManager : MonoBehaviour
{
    [SerializeField]Character[] m_clients = null;

    private int myClientIndex = 0;
    private bool isSetUp = false;

    void FixedUpdate()
    {
        if(isSetUp)
        {
            if(Input.GetMouseButtonDown(0))
            {
                m_clients[myClientIndex].DoShoot(myClientIndex);
            }
        }
    }
    public void SetUp(int clientIndex,string player1Name,string player2Name)
    {
        this.myClientIndex = clientIndex;
        isSetUp = true;
    }
}
