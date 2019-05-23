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
        m_socketIoComponent.On("disconnect",OnDisconnect);
    }
    void OnConnect(SocketIOEvent socketIOEvent)
    {
        string data = JsonUtility.ToJson(new PlayerDataJson(MainMenu.inputString));
        m_socketIoComponent.Emit("login",new JSONObject(data));
    }
    void OnConnected(SocketIOEvent socketIOEvent)
    {
        /* GameCore.uiManager.EnableMainButton(); */
        print("Connected");
    }
    void OnDisconnect(SocketIOEvent socketIOEvent)
    {
        var data = JsonUtility.FromJson<string>(socketIOEvent.data.ToString());

        print($"user {data} has disconnect");
    }
}
