using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LobbyData
{
    public string hostName;
    public int playerInLobby;

    public LobbyData(string hostName,int playerInLobby)
    {
        this.hostName = hostName;
        this.playerInLobby =playerInLobby;
    }
}
public class UILobby : MonoBehaviour
{
    [SerializeField]Text hostNameTxt = null;
    [SerializeField]Text playerInLobbyTxt = null;
    [SerializeField]Image LobbyBackground = null;
    public void SetLobbyUiData(LobbyData lobbyData)
    {
        if(lobbyData.playerInLobby == 0)
        {
            LobbyBackground.enabled = false;
            hostNameTxt.enabled = false;
            playerInLobbyTxt.enabled = false;
        }
        else
        {
            LobbyBackground.enabled = true;
            hostNameTxt.enabled = true;
            playerInLobbyTxt.enabled = true;
            hostNameTxt.text = lobbyData.hostName;
            playerInLobbyTxt.text = $"{lobbyData.playerInLobby}/2";
        }
    }
}
