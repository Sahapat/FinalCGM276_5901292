using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UiManager : MonoBehaviour
{
    [SerializeField]GameObject lobbyMenu = null;

    public void CloseLobby()
    {
        lobbyMenu.SetActive(false);
    }
}
