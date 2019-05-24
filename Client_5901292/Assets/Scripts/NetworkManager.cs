using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SocketIO;

public class NetworkManager : MonoBehaviour
{
    public bool isHost = false;
    public int lobbyDataindex = -1;

    [SerializeField] SocketIOComponent m_socketIoComponent = null;

    bool isReady = false;
    void Start()
    {
        m_socketIoComponent.On("connect",OnConnect);
        m_socketIoComponent.On("connected",OnConnected);
        m_socketIoComponent.On("hostable",OnHostable);
        m_socketIoComponent.On("not hostable",OnNotHostable);
        m_socketIoComponent.On("update lobby list",OnUpdateLobbyList);
        m_socketIoComponent.On("joinable",OnJoinAble);
        m_socketIoComponent.On("not joinable",OnNotJoinAble);
        m_socketIoComponent.On("sync lobby",OnSyncLobby);
        m_socketIoComponent.On("sync ready press",OnSyncReadyPress);
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
        var updateLobbydata = new LobbyDataJson(MainMenu.inputString,0,isReady);
        var reqUpdateLobbydata = JsonUtility.ToJson(updateLobbydata);
        m_socketIoComponent.Emit("ready press",new JSONObject(reqUpdateLobbydata));
        GameCore.uiManager.UpdateLobbyData(updateLobbydata,isHost);
    }
    public void JoinLobby(int index)
    {
        m_socketIoComponent.Emit("join lobby",new JSONObject(index));
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
        var lobbyData = LobbyDataJson.CreateFromJson(socketIOEvent.data.ToString());
        this.lobbyDataindex = lobbyData.indexLobby;
        this.isHost = true;
        GameCore.uiManager.UpdateLobbyData(lobbyData,true);
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

        for(int i = 0;i<lobbyData.Length;i++)
        {
            lobbyData[i]= new LobbyData(lobbyListData.hostNames[i],lobbyListData.lobbyCap[i]);
        }

        GameCore.uiManager.UpdateLobbyListData(lobbyData);
    }
    void OnSyncLobby(SocketIOEvent socketIOEvent)
    {
        var lobbyData = LobbyDataJson.CreateFromJson(socketIOEvent.data.ToString());
        GameCore.uiManager.UpdateLobbyData(lobbyData,false);
        print("sync");
    }
    void OnSyncReadyPress(SocketIOEvent socketIOEvent)
    {
        var lobbyData = LobbyDataJson.CreateFromJson(socketIOEvent.data.ToString());
        GameCore.uiManager.UpdateLobbyData(lobbyData,!this.isHost);
    }
    void OnJoinAble(SocketIOEvent socketIOEvent)
    {
        var lobbyData = LobbyDataJson.CreateFromJson(socketIOEvent.data.ToString());
        this.isHost = false;
        this.lobbyDataindex = lobbyData.indexLobby;
        GameCore.uiManager.CloseLobbyListSection();
        GameCore.uiManager.OpenLobbySection();
        GameCore.uiManager.UpdateLobbyData(lobbyData,true);
        m_socketIoComponent.Emit("update lobby",new JSONObject(this.lobbyDataindex));
    }
    void OnNotJoinAble(SocketIOEvent socketIOEvent)
    {
        print("can't join");
    }
}
