using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    [Header("MainSection")]
    [SerializeField] Text mainSection_PlayerName = null;
    [SerializeField] GameObject createBtn = null;
    [SerializeField] GameObject joinBtn = null;

    [Header("LobbyListSection")]
    [SerializeField] UILobby[] uiLobbys = null;
    [Header("LobbySection")]
    [SerializeField] GameObject player1Obj = null;
    [SerializeField] Text player1Txt = null;
    [SerializeField] Image ic_correctImg1 = null;
    [SerializeField] GameObject player2Obj = null;
    [SerializeField] Text player2Txt = null;
    [SerializeField] Image ic_correctImg2 = null;
    [SerializeField] Text countdownTxt = null;
    [SerializeField] GameObject readyBtnObj = null;
    [SerializeField] GameObject backBtnObj = null;

    [Header("Section Object")]
    [SerializeField] GameObject mainSectionObj = null;
    [SerializeField] GameObject lobbyListSectionObj = null;
    [SerializeField] GameObject lobbySectionObj = null;
    [SerializeField] GameObject gameSectionObj = null;
    [SerializeField]GameObject gameEndSectionObj = null;

    void Update()
    {
        if (mainSectionObj.activeSelf)
        {
            InMainSection();
        }
    }
    public void ToGameScene()
    {
        readyBtnObj.SetActive(false);
        backBtnObj.SetActive(false);
        StartCoroutine(CountDownToGameScene());
    }
    public void UpdateLobbyListData(LobbyData[] lobbyList)
    {
        for (int i = 0; i < uiLobbys.Length; i++)
        {
            uiLobbys[i].SetLobbyUiData(lobbyList[i]);
        }
    }
    public void UpdateLobbyData(LobbyDataJson lobbydata, bool isHost)
    {
        if (isHost)
        {
            player1Obj.SetActive(true);
            player1Txt.text = lobbydata.hostName;
            ic_correctImg1.enabled = lobbydata.isReady;
        }
        else
        {
            player2Obj.SetActive(true);
            player2Txt.text = lobbydata.hostName;
            ic_correctImg2.enabled = lobbydata.isReady;
        }
    }
    public void UpdateLobbyData(bool isHost, int isReady)
    {
        if (isHost)
        {
            ic_correctImg1.enabled = (isReady == 1);
        }
        else
        {
            ic_correctImg2.enabled = (isReady == 1);
        }
    }
    public void OpenGameEnd()
    {
        gameEndSectionObj.SetActive(true);
    }
    public void CloseGameEnd()
    {
        gameEndSectionObj.SetActive(false);
    }
    public void OpenGameSection()
    {
        gameSectionObj.SetActive(true);
    }
    public void CloseGameSection()
    {
        gameSectionObj.SetActive(false);
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
        createBtn.SetActive(true);
        joinBtn.SetActive(true);
    }
    void InMainSection()
    {
        mainSection_PlayerName.text = MainMenu.inputString;
    }
    IEnumerator CountDownToGameScene()
    {
        countdownTxt.text = 3.ToString();
        yield return new WaitForSeconds(1);
        countdownTxt.text = 2.ToString();
        yield return new WaitForSeconds(1);
        countdownTxt.text = 1.ToString();
        yield return new WaitForSeconds(1);

        CloseLobbyListSection();
        CloseLobbySection();
        CloseMainSection();

        OpenGameSection();

        GameCore.gamemanager.SetUp();
    }
}
