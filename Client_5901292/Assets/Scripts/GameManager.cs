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
    [SerializeField]Character[] m_clients = null;
    [Header("GameStart Show")]
    [SerializeField]Animator m_animator = null;
    [SerializeField]UnityEngine.UI.Text gamestart_show = null;
    [Header("StateChage")]
    [SerializeField]float changeStateDuration = 1f;
    [SerializeField]float CameraStateYChange = 0.5f;
    [SerializeField]Vector3 leftToRightChange = Vector3.zero;
    [SerializeField]Vector3 rightToLeftChange = Vector3.zero;
    [SerializeField]float jumpPower = 2f;

    private bool isSetUp = false;
    private bool controlable = false;
    

    void Awake()
    {
        GameCore.gamemanager = this.GetComponent<GameManager>();
        GameCore.uiManager = FindObjectOfType<UIManager>();
        GameCore.networkManager = FindObjectOfType<NetworkManager>();
    }
    void Start()
    {
        SetUp();
    }
    void FixedUpdate()
    {
        if(isSetUp)
        {
        }
        if(Input.GetKeyDown(KeyCode.Space))
        {
            ChangeState();
        }
    }
    public void SetUp()
    {
        m_animator.gameObject.SetActive(true);
        m_animator.SetTrigger("Show");
    }
    public void GameStart()
    {
        gamestart_show.text = "Game Start";
        Destroy(m_animator.gameObject,0.5f);
    }
    void ChangeState()
    {
        Camera.main.transform.DOMoveY(Camera.main.transform.position.y + CameraStateYChange,changeStateDuration,false);
        if(m_clients[0].transform.lossyScale.x > 0)
        {
            var endValueToLeft = new Vector3(rightToLeftChange.x,m_clients[0].transform.position.y+rightToLeftChange.y,m_clients[0].transform.position.z);
            m_clients[0].transform.DOJump(endValueToLeft,jumpPower,1,changeStateDuration);

            var endValueToRight = new Vector3(leftToRightChange.x,m_clients[1].transform.position.y+leftToRightChange.y,m_clients[1].transform.position.z);
            m_clients[1].transform.DOJump(endValueToRight,jumpPower,1,changeStateDuration);
            m_clients[1].facingLeft = false;

            Invoke("FinishStateChange",changeStateDuration+0.05f);
        }
        else
        {
            var endValueToLeft = new Vector3(rightToLeftChange.x,m_clients[1].transform.position.y+rightToLeftChange.y,m_clients[1].transform.position.z);
            m_clients[1].transform.DOJump(endValueToLeft,jumpPower,1,changeStateDuration);
            m_clients[1].facingLeft = true;

            var endValueToRight = new Vector3(leftToRightChange.x,m_clients[0].transform.position.y+leftToRightChange.y,m_clients[0].transform.position.z);
            m_clients[0].transform.DOJump(endValueToRight,jumpPower,1,changeStateDuration);
            m_clients[0].facingLeft = false;

            Invoke("FinishStateChange",changeStateDuration);
        }
    }
    void FinishStateChange()
    {
        foreach(var i in m_clients)
        {
            i.UpdateFacing();
        }
    }
}
