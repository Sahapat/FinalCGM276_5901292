using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GameCore
{
    public static PlayerDataJson clientPlayerData = null;
    public static PlayerDataJson otherPlayerData = null;
    public static GameManager gamemanager = null;
    public static NetworkManager networkManager = null;
    public static UIManager uiManager = null;
}
public class GameManager : MonoBehaviour
{
    [SerializeField] Character[] m_clients = null;
    [Header("GameStart Show")]
    [SerializeField] Animator m_animator = null;
    [SerializeField] UnityEngine.UI.Text gamestart_show = null;
    [Header("StateChage")]
    [SerializeField] float changeStateDuration = 1f;
    [SerializeField] float CameraStateYChange = 0.5f;
    [SerializeField] Vector3 leftToRightChange = Vector3.zero;
    [SerializeField] Vector3 rightToLeftChange = Vector3.zero;
    [SerializeField] float jumpPower = 2f;
    [Header("Game end ref")]
    [SerializeField] UnityEngine.UI.Text winnerTxt = null;

    [SerializeField]AudioSource m_audiosource = null;
    [SerializeField]AudioSource other = null;
    [SerializeField]AudioSource other2 = null;
    [SerializeField] AudioClip[] clip = null;
    private bool isSetUp = false;
    private bool controlable = false;
    public int round = 0;
    public int numShoot = 0;

    void Awake()
    {
        GameCore.gamemanager = this.GetComponent<GameManager>();
        GameCore.uiManager = FindObjectOfType<UIManager>();
        GameCore.networkManager = FindObjectOfType<NetworkManager>();
    }
    void Start()
    {
        m_audiosource.clip = clip[0];
        m_audiosource.loop = true;
        m_audiosource.Play();
    }
    void FixedUpdate()
    {
        if (isSetUp)
        {
            if ((Input.GetMouseButtonDown(0) && controlable))
            {
                if (GameCore.networkManager.isHost)
                {
                    m_clients[0].DoShoot();
                }
                else
                {
                    m_clients[1].DoShoot();
                }
            }
            else if (Input.touches.Length > 0)
            {
                if (Input.touches[0].phase == TouchPhase.Began && controlable)
                {
                    if (GameCore.networkManager.isHost)
                    {
                        m_clients[0].DoShoot();
                    }
                    else
                    {
                        m_clients[1].DoShoot();
                    }
                }
            }
            if (numShoot >= 2)
            {
                numShoot = 0;
                controlable = false;
                Invoke("ChangeState", 1f);
            }
        }
    }
    public void PlayBulletHit()
    {
        other2.Play();
    }
    public void GameEnd(string name, bool isHost)
    {
        if (isHost)
        {
            Destroy(m_clients[0].gameObject);
        }
        else
        {
            Destroy(m_clients[1].gameObject);
        }

        GameCore.uiManager.CloseGameSection();
        GameCore.uiManager.CloseLobbyListSection();
        GameCore.uiManager.CloseLobbySection();
        GameCore.uiManager.CloseMainSection();

        GameCore.uiManager.OpenGameEnd();
        winnerTxt.text = name;
        isSetUp = false;
        m_audiosource.loop = false;
        m_audiosource.PlayOneShot(clip[2]);
    }
    public void LoadScene(string name)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(name);
    }
    public void SetTakedamage(int index, int health)
    {
        m_clients[index].SetHealth(health);
    }
    public void SetUp()
    {
        m_animator.gameObject.SetActive(true);
        m_animator.SetTrigger("Show");
        if (GameCore.networkManager.isHost)
        {
            m_clients[0].SetUp(MainMenu.inputString);
            m_clients[1].SetUp(GameCore.networkManager.otherPlayerName);


        }
        else
        {
            m_clients[1].SetUp(MainMenu.inputString);
            m_clients[0].SetUp(GameCore.networkManager.otherPlayerName);
        }
        m_clients[0].UpdateFacing();
        m_clients[1].UpdateFacing();
        isSetUp = true;
        m_clients[0].GunFlip();
        m_clients[1].GunFlip();
        m_audiosource.clip = clip[1];
        m_audiosource.loop = true;
        m_audiosource.Play();
    }
    public void GameStart()
    {
        gamestart_show.text = "Game Start";
        Destroy(m_animator.gameObject, 0.5f);
        controlable = true;
        foreach (var i in m_clients)
        {
            i.SetEnableGun();
        }
    }
    public void OthetShoot(bool isHost, float rotationZ)
    {
        if (isHost)
        {
            m_clients[0].DoShoot(rotationZ);
        }
        else
        {
            m_clients[1].DoShoot(rotationZ);
        }
    }
    void ChangeState()
    {
        try
        {
            Camera.main.transform.DOMoveY(Camera.main.transform.position.y + CameraStateYChange, changeStateDuration, false);
            if (m_clients[0].transform.lossyScale.x > 0)
            {
                var endValueToLeft = new Vector3(rightToLeftChange.x, m_clients[0].transform.position.y + rightToLeftChange.y, m_clients[0].transform.position.z);
                m_clients[0].transform.DOJump(endValueToLeft, jumpPower, 1, changeStateDuration);
                m_clients[0].facingLeft = true;

                var endValueToRight = new Vector3(leftToRightChange.x, m_clients[1].transform.position.y + leftToRightChange.y, m_clients[1].transform.position.z);
                m_clients[1].transform.DOJump(endValueToRight, jumpPower, 1, changeStateDuration);
                m_clients[1].facingLeft = false;

                Invoke("FinishStateChange", changeStateDuration + 0.05f);
            }
            else
            {
                var endValueToLeft = new Vector3(rightToLeftChange.x, m_clients[1].transform.position.y + rightToLeftChange.y, m_clients[1].transform.position.z);
                m_clients[1].transform.DOJump(endValueToLeft, jumpPower, 1, changeStateDuration);
                m_clients[1].facingLeft = true;

                var endValueToRight = new Vector3(leftToRightChange.x, m_clients[0].transform.position.y + leftToRightChange.y, m_clients[0].transform.position.z);
                m_clients[0].transform.DOJump(endValueToRight, jumpPower, 1, changeStateDuration);
                m_clients[0].facingLeft = false;

                Invoke("FinishStateChange", changeStateDuration);
            }
            other.PlayOneShot(clip[3]);
            round++;
        }
        catch (System.NullReferenceException)
        {

        }
    }
    void FinishStateChange()
    {
        foreach (var i in m_clients)
        {
            i.UpdateFacing();
            i.SetEnableGun();
            i.GunFlip();
        }
        controlable = true;
    }
}
