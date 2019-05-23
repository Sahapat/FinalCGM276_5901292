using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public static string inputString = string.Empty;
    [SerializeField]InputField m_inputField = null;

    public void SendNameToNewScene()
    {
        inputString = m_inputField.text;
        SceneManager.LoadScene("Game");
    }
}
