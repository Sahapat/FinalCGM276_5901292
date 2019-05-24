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