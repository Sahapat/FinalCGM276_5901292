﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SocketIO;

public class NetworkManager : MonoBehaviour
{
    public bool isHost = false;
    public int lobbyDataindex = -1;
    public string otherPlayerName = string.Empty;

    [SerializeField] SocketIOComponent m_socketIoComponent = null;

    bool isReady = false;
    void Start()
    {
        m_socketIoComponent.On("connect", OnConnect);
        m_socketIoComponent.On("connected", OnConnected);
        m_socketIoComponent.On("hostable", OnHostable);
        m_socketIoComponent.On("not hostable", OnNotHostable);
        m_socketIoComponent.On("update lobby list", OnUpdateLobbyList);
        m_socketIoComponent.On("joinable", OnJoinAble);
        m_socketIoComponent.On("not joinable", OnNotJoinAble);
        m_socketIoComponent.On("sync lobby", OnSyncLobby);
        m_socketIoComponent.On("sync ready press", OnSyncReadyPress);
        m_socketIoComponent.On("countdown", OnCountDown);
        m_socketIoComponent.On("game start", OnGameStart);
        m_socketIoComponent.On("other firing",OnOtherFiring);
        m_socketIoComponent.On("other take damage",OnOtherTakeDamage);
        m_socketIoComponent.On("game end",OnGameEnd);
    }
    public void CreateHost()
    {
        m_socketIoComponent.Emit("hosting");
    }
    public void GetLobbyList()
    {
        m_socketIoComponent.Emit("lobby list");
    }
    public void Ready()
    {
        isReady = !isReady;
        var updateLobbydata = new LobbyDataJson(MainMenu.inputString, 0, isReady);
        var reqUpdateLobbydata = JsonUtility.ToJson(updateLobbydata);
        m_socketIoComponent.Emit("ready press", new JSONObject(reqUpdateLobbydata));
        GameCore.uiManager.UpdateLobbyData(updateLobbydata, isHost);
    }
    public void sendTakeDamage(int playerHealth)
    {
        var sendTakeDamageJson = new HitJson(playerHealth,isHost,(byte)lobbyDataindex);
        var reqTake = JsonUtility.ToJson(sendTakeDamageJson);

        m_socketIoComponent.Emit("take damage",new JSONObject(reqTake));
    }
    public void sendWinner()
    {
        var sendWinner = new WiningCheckJson(otherPlayerName,lobbyDataindex,!isHost);
        var reqWinner = JsonUtility.ToJson(sendWinner);
        m_socketIoComponent.Emit("sent winner",new JSONObject(reqWinner));
    }
    public void JoinLobby(int index)
    {
        m_socketIoComponent.Emit("join lobby", new JSONObject(index));
    }
    public void Shoot(float rotationZ)
    {
        var shootData = new FiringJson(rotationZ,isHost,lobbyDataindex);
        var reqShoot = JsonUtility.ToJson(shootData);
        m_socketIoComponent.Emit("fire",new JSONObject(reqShoot));
    }
    void OnConnect(SocketIOEvent socketIOEvent)
    {
        string data = JsonUtility.ToJson(new PlayerDataJson(MainMenu.inputString));
        m_socketIoComponent.Emit("login", new JSONObject(data));
    }
    void OnConnected(SocketIOEvent socketIOEvent)
    {
        print("Connected");
        GameCore.uiManager.EnableMainButton();
    }
    void OnHostable(SocketIOEvent socketIOEvent)
    {
        var lobbyData = LobbyDataJson.CreateFromJson(socketIOEvent.data.ToString());
        this.lobbyDataindex = lobbyData.indexLobby;
        this.isHost = true;
        GameCore.uiManager.UpdateLobbyData(lobbyData, true);
    }
    void OnNotHostable(SocketIOEvent socketIOEvent)
    {
        GameCore.uiManager.CloseLobbySection();
        GameCore.uiManager.OpenMainSection();
        print("can't host");
    }
    void OnUpdateLobbyList(SocketIOEvent socketIOEvent)
    {
        print("on update lobby list");
        var lobbyListData = LobbyListDataJson.CreateFromJson(socketIOEvent.data.ToString());

        LobbyData[] lobbyData = new LobbyData[lobbyListData.lobbyCap.Length];

        for (int i = 0; i < lobbyData.Length; i++)
        {
            lobbyData[i] = new LobbyData(lobbyListData.hostNames[i], lobbyListData.lobbyCap[i]);
        }

        GameCore.uiManager.UpdateLobbyListData(lobbyData);
    }
    void OnSyncLobby(SocketIOEvent socketIOEvent)
    {
        var lobbyData = LobbyDataJson.CreateFromJson(socketIOEvent.data.ToString());
        GameCore.uiManager.UpdateLobbyData(lobbyData, false);
    }
    void OnSyncReadyPress(SocketIOEvent socketIOEvent)
    {
        var readyCheck = ReadyCheckJson.CreateFromJson(socketIOEvent.data.ToString());
        GameCore.uiManager.UpdateLobbyData(readyCheck.isHost, readyCheck.isReady);
    }
    void OnJoinAble(SocketIOEvent socketIOEvent)
    {
        var lobbyData = LobbyDataJson.CreateFromJson(socketIOEvent.data.ToString());
        this.isHost = false;
        this.lobbyDataindex = lobbyData.indexLobby;
        GameCore.uiManager.CloseLobbyListSection();
        GameCore.uiManager.OpenLobbySection();
        GameCore.uiManager.UpdateLobbyData(lobbyData, true);
        m_socketIoComponent.Emit("update lobby", new JSONObject(this.lobbyDataindex));
    }
    void OnCountDown(SocketIOEvent socketIOEvent)
    {
        var resName = LobbyDataJson.CreateFromJson(socketIOEvent.data.ToString());
        otherPlayerName = resName.hostName;
        GameCore.uiManager.ToGameScene();
        GameCore.gamemanager.SetUp();
    }
    void OnGameStart(SocketIOEvent socketIOEvent)
    {
        GameCore.gamemanager.GameStart();
    }
    void OnNotJoinAble(SocketIOEvent socketIOEvent)
    {
        print("can't join");
    }
    void OnOtherFiring(SocketIOEvent socketIOEvent)
    {
        var shootData = FiringJson.CreateFromJson(socketIOEvent.data.ToString());

        GameCore.gamemanager.OthetShoot(shootData.isHost,shootData.rotationZ);
    }
    void OnOtherTakeDamage(SocketIOEvent socketIOEvent)
    {
        var hitJson = HitJson.CreateFromJson(socketIOEvent.data.ToString());

        if(hitJson.isHost)
        {
            GameCore.gamemanager.SetTakedamage(1,hitJson.currentHealth);
        }
        else
        {
            GameCore.gamemanager.SetTakedamage(0,hitJson.currentHealth);
        }
    }
    void OnGameEnd(SocketIOEvent socketIOEvent)
    {
        var winnerRes = WiningCheckJson.CreateFromJson(socketIOEvent.data.ToString());
        GameCore.gamemanager.GameEnd(winnerRes.name,winnerRes.isHost);
    }
}
