﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SocketIO;

public class NetworkManager : MonoBehaviour
{
    private SocketIOComponent m_socketIoComponent = null;

    void Awake()
    {
        m_socketIoComponent = GetComponent<SocketIOComponent>();
    }

    void Start()
    {
        m_socketIoComponent.On("login require",OnLoginRequire);
        m_socketIoComponent.On("connected",OnConnected);
        m_socketIoComponent.On("otherPlayerConnected",OnOtherPlayerConnected);
        m_socketIoComponent.On("game start",OnGameStart);
    }
    void OnConnected(SocketIOEvent socketIOEvent)
    {
        var data = socketIOEvent.data.ToString();
        GameCore.clientPlayerData = PlayerDataJson.CreateFromJson(data);
    }
    void OnOtherPlayerConnected(SocketIOEvent socketIOEvent)
    {
        var data = socketIOEvent.data.ToString();
        GameCore.otherPlayerData = PlayerDataJson.CreateFromJson(data);
    }
    void OnGameStart(SocketIOEvent socketIOEvent)
    {
        GameCore.gamemanager.SetUp();
    }
    void OnLoginRequire(SocketIOEvent socketIOEvent)
    {
        string data = JsonUtility.ToJson(new PlayerDataJson(MainMenu.inputString,0));
        m_socketIoComponent.Emit("login",new JSONObject(data));
    }
}
