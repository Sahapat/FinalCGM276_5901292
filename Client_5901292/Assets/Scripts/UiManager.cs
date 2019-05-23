using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    [Header("MainSection")]
    [SerializeField] Text mainSection_PlayerName = null;
    [SerializeField] Button createBtn = null;
    [SerializeField] Button joinBtn = null;

    [Header("LobbyListSection")]
    [SerializeField] UILobby[] uiLobbys = null;
    [Header("LobbySection")]
    [SerializeField] GameObject player1Obj = null;
    [SerializeField] Text player1Txt = null;
    [SerializeField] GameObject player2Obj = null;
    [SerializeField] Text player2Txt = null;

    [Header("Section Object")]
    [SerializeField] GameObject mainSectionObj = null;
    [SerializeField] GameObject lobbyListSectionObj = null;
    [SerializeField] GameObject lobbySectionObj = null;
    [SerializeField] GameObject gameSectionObj = null;

    void Update()
    {
        if (mainSectionObj.activeSelf)
        {
            InMainSection();
        }

        if(lobbySectionObj.activeSelf)
        {
            InLobbySection();
        }
        if(gameSectionObj.activeSelf)
        {
            InGameSection();
        }
    }
    public void UpdateLobbyListData(LobbyDataJson[] lobbyList)
    {
        for (int i = 0; i < uiLobbys.Length; i++)
        {
            uiLobbys[i].SetLobbyUiData(lobbyList[i]);
        }
    }
    public void OpenLobbySection()
    {
        lobbySectionObj.SetActive(true);
    }
    public void CloseLobbySection()
    {
        lobbySectionObj.SetActive(false);
    }
    public void OpenLobbyListSection()
    {
        lobbyListSectionObj.SetActive(true);
    }
    public void CloseLobbyListSection()
    {
        lobbyListSectionObj.SetActive(false);
    }
    public void OpenMainSection()
    {
        mainSectionObj.SetActive(true);
    }
    public void CloseMainSection()
    {
        mainSectionObj.SetActive(false);
    }
    public void EnableMainButton()
    {
        createBtn.enabled = true;
        createBtn.enabled = true;
    }
    void InMainSection()
    {
        mainSection_PlayerName.text = MainMenu.inputString;
    }
    void InLobbySection()
    {

    }
    void InGameSection()
    {

    }
}
