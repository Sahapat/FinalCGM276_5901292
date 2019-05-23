using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SocketIO;

public class NetworkManager : MonoBehaviour
{
    [SerializeField] SocketIOComponent m_socketIoComponent = null;

    void Start()
    {
        m_socketIoComponent.On("login require", OnLoginRequire);
        m_socketIoComponent.On("connected", OnConnected);
        m_socketIoComponent.On("otherPlayerConnected", OnOtherPlayerConnected);
        m_socketIoComponent.On("game start", OnGameStart);
    }
    void OnConnected(SocketIOEvent socketIOEvent)
    {
        var data = socketIOEvent.data.ToString();
        GameCore.clientPlayerData = PlayerDataJson.CreateFromJson(data);
        print("Connect success");
    }
    void OnOtherPlayerConnected(SocketIOEvent socketIOEvent)
    {
        var data = socketIOEvent.data.ToString();
        GameCore.otherPlayerData = PlayerDataJson.CreateFromJson(data);
        print("player: "+GameCore.otherPlayerData.name+" has connected");
    }
    void OnGameStart(SocketIOEvent socketIOEvent)
    {
        GameCore.gamemanager.SetUp();
        print("Game start");
    }
    void OnLoginRequire(SocketIOEvent socketIOEvent)
    {
        string data = JsonUtility.ToJson(new PlayerDataJson(MainMenu.inputString, 0));
        m_socketIoComponent.Emit("login", new JSONObject(data));
    }
}
