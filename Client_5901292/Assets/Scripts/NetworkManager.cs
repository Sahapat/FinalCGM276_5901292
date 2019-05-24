using System.Collections;
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
        m_socketIoComponent.On("joinable",OnJoinAble);
        m_socketIoComponent.On("not joinable",OnNotJoinAble);
    }
    public void CreateHost()
    {
        m_socketIoComponent.Emit("hosting");
    }
    public void GetLobbyList()
    {
        m_socketIoComponent.Emit("lobby list");
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
        print("on update lobby list");
        var lobbyListData = LobbyListDataJson.CreateFromJson(socketIOEvent.data.ToString());

        LobbyData[] lobbyData = new LobbyData[lobbyListData.lobbyCap.Length];

        for(int i = 0;i<lobbyData.Length;i++)
        {
            lobbyData[i]= new LobbyData(lobbyListData.hostNames[i],lobbyListData.lobbyCap[i]);
        }

        GameCore.uiManager.UpdateLobbyListData(lobbyData);
    }
    void OnJoinAble(SocketIOEvent socketIOEvent)
    {
        GameCore.uiManager.CloseLobbyListSection();
        GameCore.uiManager.OpenLobbySection();
    }
    void OnNotJoinAble(SocketIOEvent socketIOEvent)
    {
        print("can't join");
    }
}
