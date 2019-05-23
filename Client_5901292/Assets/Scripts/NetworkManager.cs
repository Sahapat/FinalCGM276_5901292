using System.Collections;
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
        m_socketIoComponent.On("connected",OnConnected);
        m_socketIoComponent.On("otherPlayerConnected",OnOtherPlayerConnected);
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
}
