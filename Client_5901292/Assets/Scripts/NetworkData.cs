using System;
using UnityEngine;

[Serializable]
public class PlayerDataJson
{
    public string name;
    public PlayerDataJson(string name)
    {
        this.name = name;
    }
    public static PlayerDataJson CreateFromJson(string data)
    {
        return JsonUtility.FromJson<PlayerDataJson>(data);
    }
}
[Serializable]
public class LobbyListDataJson
{
    public string[] hostNames;
    public int[] lobbyCap;

    public LobbyListDataJson(string[] hostNames,int[] lobbyCap)
    {
        this.hostNames = hostNames;
        this.lobbyCap = lobbyCap;
    }
    public static LobbyListDataJson CreateFromJson(string data)
    {
        return JsonUtility.FromJson<LobbyListDataJson>(data);
    }
}
[Serializable]
public class LobbyDataJson
{
    public string hostName;
    public int indexLobby;
    public bool isReady;
    
    public LobbyDataJson(string hostName,int indexLobby,bool isReady)
    {
        this.hostName = hostName;
        this.indexLobby = indexLobby;
        this.isReady = isReady;
    }
    public static LobbyDataJson CreateFromJson(string data)
    {
        return JsonUtility.FromJson<LobbyDataJson>(data);
    }
}

[Serializable]
public class ReadyCheckJson
{
    public bool isHost;
    public int isReady;
    public static ReadyCheckJson CreateFromJson(string data)
    {
        return JsonUtility.FromJson<ReadyCheckJson>(data);
    }
}

[Serializable]
public class FiringJson
{
    public float rotationZ;
    public bool isHost;
    public int lobbyIndex;
    public FiringJson(float rotationZ,bool isHost,int lobbyIndex)
    {
        this.rotationZ = rotationZ;
        this.isHost = isHost;
        this.lobbyIndex = lobbyIndex;
    }
    public static FiringJson CreateFromJson(string data)
    {
        return JsonUtility.FromJson<FiringJson>(data);
    }
}

[Serializable]
public class WiningCheckJson
{
    public string name;
    public int lobbyIndex;
    public WiningCheckJson(string name,int lobbyIndex)
    {
        this.name = name;
        this.lobbyIndex =lobbyIndex;
    }
    public static WiningCheckJson CreateFromJson(string data)
    {
        return JsonUtility.FromJson<WiningCheckJson>(data);
    }
}