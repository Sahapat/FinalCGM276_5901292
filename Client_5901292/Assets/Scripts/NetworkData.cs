﻿using System;
using UnityEngine;

[Serializable]
public class PlayerDataJson
{
    public string name;
    public int characterId;
    
    public PlayerDataJson(string name,int characterId)
    {
        this.name = name;
        this.characterId = characterId;
    }
    public static PlayerDataJson CreateFromJson(string data)
    {
        return JsonUtility.FromJson<PlayerDataJson>(data);
    }
}

[Serializable]
public class LobbyDataJson
{
    public string hostName;
    public string roomData;
    public int playerInLobby;

    public LobbyDataJson(string hostName,string roomData,int playerInLobby)
    {
        this.hostName= hostName;
        this.roomData = roomData;
        this.playerInLobby = playerInLobby;
    }
    public static LobbyDataJson CreateFromJson(string data)
    {
        return JsonUtility.FromJson<LobbyDataJson>(data);
    }
}