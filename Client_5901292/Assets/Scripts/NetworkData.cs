using System;
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