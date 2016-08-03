using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class bl_JetManager : bl_PhotonHelper {
    
    public List<MonoBehaviour> JetScripts = new List<MonoBehaviour>();
    [Separator("References")]
    public GameObject CarCamera = null;
    public Transform ExitPoint;
    
    [Separator("UI References")]
    public GameObject TextUI;
    public Text CarSpeedText = null;
    public Text DriverText = null;
    [HideInInspector]
    public bool Into = false;
    [HideInInspector]
    public bool InCar = false;

    private Animator m_Animator;
    private bool LocalInCar = false;
    private AeroplaneController JetController;
    private PhotonView view;
    private bl_GameController GController = null;
    private GameObject Player;

    /// <summary>
    /// 
    /// </summary>
    void Awake()
    {
        view = PhotonView.Get(this);
        JetController = GetComponent<AeroplaneController>();
        GController = FindObjectOfType<bl_GameController>();
        if (!view.ObservedComponents.Contains(this))
        {
            view.ObservedComponents.Add(this);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    private void Start()
    {
        m_Animator = GetComponent<Animator>();
        if (PhotonNetwork.isMasterClient && view.owner == null)
        {
            view.RequestOwnership();
            Debug.Log("Assign Master as owner");
        }
    }

    /// <summary>
    /// 
    /// </summary>
    void Update()
    {
        if (Player == null)
        {
            if (GController != null && GController.m_Player != null)
            {
                //Get Local Player, when this is instance.
                Player = GController.m_Player;
            }
            return;
        }

        Inputs();
        PositionControl();
        Speedometer();
    }

    /// <summary>
    /// 
    /// </summary>
    void Speedometer()
    {
        //Speedometer
        if (inMyControl && CarSpeedText != null)
        {
            if (InCar)
            {
                if (!CarSpeedText.gameObject.activeSelf)
                {
                    CarSpeedText.gameObject.SetActive(true);
                }
                CarSpeedText.text = JetController.ForwardSpeed.ToString("000") + "\n KMT/h";
            }
            else
            {
                if (CarSpeedText.gameObject.activeSelf)
                {
                    CarSpeedText.gameObject.SetActive(false);
                }
            }
        }
        if (DriverText != null)
        {

            if (InCar && inMyControl)
            {
                string d = (view.owner == null || !InCar) ? "None" : view.owner.name;
                DriverText.text = "<color=white>Driver:</color> " + d;
                if (!DriverText.gameObject.activeSelf) { DriverText.gameObject.SetActive(true); }
            }
            else
            {
                if (DriverText.gameObject.activeSelf) { DriverText.gameObject.SetActive(false); }
            }
        }
    }
    /// <summary>
    /// 
    /// </summary>
    void PositionControl()
    {
        //When car is not controlled by us, send information from server
        if (!view.isMine)
        {
            JetController.Move(roll, pitch, m_Yaw, m_Throttle, m_AirBrakes);
        }
        else//When car is our the update information
        {
            if (InCar)
            {
                roll = JetController.RollInput;
                pitch = JetController.PitchInput;
                m_AirBrakes = JetController.AirBrakes;
                m_Yaw = JetController.YawInput;
                m_Throttle = JetController.ThrottleInput;
            }
        }

        //Sure to player follow the car position, for avoid teletranportation when exit.
        if (inMyControl && InCar && LocalInCar)
        {
            Player.transform.root.position = this.transform.root.position;
            Player.transform.root.rotation = this.transform.root.rotation;
        }
        else if (inMyControl && !InCar)
        {
            JetController.Move(0, 0, 0, 0, false);
            m_Animator.SetInteger("GearState", 1);
        }
    }
    /// <summary>
    /// 
    /// </summary>
    void Inputs()
    {
        if (Input.GetKeyDown(KeyCode.E) && Into && !InCar)
        {
            OnEnter();
        }
        else if (Input.GetKeyDown(KeyCode.E) && InCar && inMyControl)
        {
            OnExit();
        }
    }


    float roll;
    float pitch;
    bool m_AirBrakes;
    float m_Yaw;
    float m_Throttle;
    /// <summary>
    /// 
    /// </summary>
    /// <param name="stream"></param>
    /// <param name="info"></param>
    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            // We own this player: send the others our data
            stream.SendNext(roll);
            stream.SendNext(pitch);
            stream.SendNext(m_AirBrakes);
            stream.SendNext(m_Yaw);
            stream.SendNext(m_Throttle);

        }
        else
        {
            // Network player, receive data
            this.roll = (float)stream.ReceiveNext();
            this.pitch = (float)stream.ReceiveNext();
            this.m_AirBrakes = (bool)stream.ReceiveNext();
            this.m_Yaw = (float)stream.ReceiveNext();
            this.m_Throttle = (float)stream.ReceiveNext();


        }
    }

    /// <summary>
    /// 
    /// </summary>
    public void OnDetectorEnter()
    {
        Into = true;
        TextUI.SetActive(true);
    }
    
    /// <summary>
    /// 
    /// </summary>
    public void OnDetectorExit()
    {
        Into = false;
        TextUI.SetActive(false);
    }

    /// <summary>
    /// 
    /// </summary>
    public void OnEnter()
    {
        if (view.ownerId != PhotonNetwork.player.ID)
        {
            view.RequestOwnership();
        }
        foreach (MonoBehaviour m in JetScripts)
        {
            m.enabled = true;
        }
        CarCamera.SetActive(true);
        Player.GetComponent<bl_PlayerCar>().OnEnterLocal();
        InCar = true;
        LocalInCar = true;
        TextUI.SetActive(false);
        FindPlayerView(bl_GameController.m_ViewID).RPC("NetworkCarEvent", PhotonTargets.OthersBuffered, true,1);
        //avoid to other player can enter when we are inside of car.
        view.RPC("JetEvent", PhotonTargets.OthersBuffered, true);
        JetController.Reset();
    }
    /// <summary>
    /// 
    /// </summary>
    public void OnExit()
    {
        foreach (MonoBehaviour m in JetScripts)
        {
            m.enabled = false;
        }
        CarCamera.SetActive(false);

        Player.transform.position = ExitPoint.position;
        Player.transform.rotation = ExitPoint.rotation;

        Player.GetComponent<bl_PlayerCar>().OnExitLocal();
        FindPlayerView(bl_GameController.m_ViewID).RPC("NetworkCarEvent", PhotonTargets.OthersBuffered, false,1);
        InCar = false;
        Into = false;
        LocalInCar = false;
        view.RPC("JetEvent", PhotonTargets.OthersBuffered, false);
        JetController.Immobilize();
    }

    /// <summary>
    /// 
    /// </summary>
    private bool inMyControl
    {
        get
        {
            bool b = false;
            if (view.ownerId == PhotonNetwork.player.ID)
            {
                b = true;
            }
            return b;
        }
    }
    /// <summary>
    /// Verify when a player left if this not is in car.
    /// if, yes, then reset domain of car.
    /// </summary>
    /// <param name="otherPlayer"></param>
    public override void OnPhotonPlayerDisconnected(PhotonPlayer otherPlayer)
    {
        if (view == null)
            return;
        if (view.owner == null || view.owner.name == "Scene")
        {
            InCar = false;
            view.RPC("JetEvent", PhotonTargets.OthersBuffered, false);
            return;
        }
        //is the player in car.
        if (view.owner.name == otherPlayer.name)
        {
            InCar = false;
            view.RPC("JetEvent", PhotonTargets.OthersBuffered, false);
        }
    }

    //when MasterClient instantiate set it as owner of this view
    public override void OnPhotonInstantiate(PhotonMessageInfo info)
    {
        if (info.sender.isMasterClient && view.owner == null)
        {
            view.RequestOwnership();
            Debug.Log("Assign Master as owner");
        }
    }
}