using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LobbyDataJson
{
    public string hostName;
    public int playerInLobby;
}
public class UILobby : MonoBehaviour
{
    [SerializeField]Text hostNameTxt = null;
    [SerializeField]Text playerInLobbyTxt = null;
    [SerializeField]Image LobbyBackground = null;
    public void SetLobbyUiData(LobbyDataJson lobbyData)
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
