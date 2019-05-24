﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SocketIO;

public class NetworkManager : MonoBehaviour
{
    [SerializeField] SocketIOComponent m_socketIoComponent = null;

    void Start()
    {
        m_socketIoComponent.On("connect",OnConnect);
        m_socketIoComponent.On("connected",OnConnected);
        m_socketIoComponent.On("hostable",OnHostable);
        m_socketIoComponent.On("not hostable",OnNotHostable);
        m_socketIoComponent.On("update lobby list",OnUpdateLobbyList);
    }
    public void CreateHost()
    {
        m_socketIoComponent.Emit("hosting");
    }
    public void GetLobbyList()
    {
        m_socketIoComponent.Emit("lobby list");
    }
    void OnConnect(SocketIOEvent socketIOEvent)
    {
        string data = JsonUtility.ToJson(new PlayerDataJson(MainMenu.inputString));
        m_socketIoComponent.Emit("login",new JSONObject(data));
    }
    void OnConnected(SocketIOEvent socketIOEvent)
    {
        print("Connected");
        GameCore.uiManager.EnableMainButton();
    }
    void OnHostable(SocketIOEvent socketIOEvent)
    {
        print("can host");
    }
    void OnNotHostable(SocketIOEvent socketIOEvent)
    {
        GameCore.uiManager.CloseLobbySection();
        GameCore.uiManager.OpenMainSection();
        print("can't host");
    }
    void OnUpdateLobbyList(SocketIOEvent socketIOEvent)
    {
        int[] temp = JsonUtility.FromJson<int[]>(socketIOEvent.data.ToString());
        foreach(var i in temp)
        {
            print(i);
        }
    }
}
