using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GameCore
{
    public static PlayerDataJson clientPlayerData = null;
    public static PlayerDataJson otherPlayerData = null;
    public static GameManager gamemanager = null;
    public static NetworkManager networkManager = null;
    public static UIManager uiManager = null;
}
public class GameManager : MonoBehaviour
{
    [SerializeField]Character[] m_clients = null;

    private bool isSetUp = false;

    void Awake()
    {
        GameCore.gamemanager = this.GetComponent<GameManager>();
        GameCore.uiManager = FindObjectOfType<UIManager>();
        GameCore.networkManager = FindObjectOfType<NetworkManager>();
    }
    void FixedUpdate()
    {
        if(isSetUp)
        {
        }
    }
    public void SetUp()
    {
    }
}
