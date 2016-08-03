using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class bl_CarManager : bl_PhotonHelper{

    public List<MonoBehaviour> CarScripts = new List<MonoBehaviour>();
    [Separator("References")]
    public GameObject CarCamera = null;
    public Transform ExitPoint;
    public GameObject TextUI;
    public Text CarSpeedText = null;
    public Text DriverText = null;
    public Text PassengerText = null;
    [HideInInspector] public bool Into = false;
    [HideInInspector] public bool InCar = false;

    private bool LocalInCar = false;
    private bl_GameController GController = null;
    private CarController Car;
    private PhotonView view;
    private bl_Passenger Passenger = null;
    private GameObject Player;

    /// <summary>
    /// 
    /// </summary>
    void Awake()
    {
        view = PhotonView.Get(this);
        if (FindObjectOfType<bl_GameController>() != null)
        {
            GController = FindObjectOfType<bl_GameController>();
        }
        if (GetComponent<CarController>() != null)
        {
            Car = GetComponent<CarController>();
        }
        if (GetComponent<bl_Passenger>() != null)
        {
            Passenger = GetComponent<bl_Passenger>();
        }
        if (!view.ObservedComponents.Contains(this))
        {
            view.ObservedComponents.Add(this);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    void Start()
    {
        if (PhotonNetwork.isMasterClient && view.owner == null)
        {
            view.RequestOwnership();
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
                CarSpeedText.text = Car.CarVelocity.ToString("000") + "\n" + Car.m_SpeedType.ToString() + "/h"; 
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

            if (InCar && inMyControl || Passenger.InCar && Passenger.InUse)
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
        if (PassengerText != null)
        {

            if (Passenger.InCar && Passenger.InUse || InCar && inMyControl)
            {
                string t = (Passenger.UseBy == string.Empty) ? "None" : Passenger.UseBy;
                PassengerText.text = "<color=white>Passenger:</color> " + t;
                if (!PassengerText.gameObject.activeSelf) { PassengerText.gameObject.SetActive(true); }
            }
            else
            {
                if (PassengerText.gameObject.activeSelf) { PassengerText.gameObject.SetActive(false); }
            }
        }
    }
    /// <summary>
    /// 
    /// </summary>
    void PositionControl()
    {
        //When car is not controlled by us,move vehicle with information from server.
        if (!inMyControl)
        {
            Car.Move(h, v, brakeInput, handBrake, true);
        }
        else//when we have the control
        {
            //send local variable input for sync with remote players.
            if (LocalInCar && InCar)
            {
                h = Car.m_steerin;
                v = Car.m_accel;
                brakeInput = Car.m_brakeInput;
                handBrake = Car.m_handbrake;
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
            Car.Move(0, 0, 0, 0, false);
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

    private float h = 0.0f;
    private float v = 0.0f;
    [HideInInspector]public float brakeInput = 0.0f;
    private float handBrake;
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
            stream.SendNext(h);
            stream.SendNext(v);
            stream.SendNext(brakeInput);
            stream.SendNext(handBrake);
        }
        else
        {
            // Network player, receive data
            this.h = (float)stream.ReceiveNext();
            this.v = (float)stream.ReceiveNext();
            this.brakeInput = (float)stream.ReceiveNext();
            handBrake = (float)stream.ReceiveNext();
            
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public void OnEnterDetector()
    {
        Into = true;
        TextUI.SetActive(true);
    }

    /// <summary>
    /// 
    /// </summary>
    public void OnExitDetector()
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
        foreach (MonoBehaviour m in CarScripts)
        {
            m.enabled = true;
        }
        CarCamera.SetActive(true);
        Player.GetComponent<bl_PlayerCar>().OnEnterLocal();
        InCar = true;
        LocalInCar = true;
        TextUI.SetActive(false);
        FindPlayerView(bl_GameController.m_ViewID).RPC("NetworkCarEvent",PhotonTargets.OthersBuffered, true,0);
        //avoid to other player can enter when we are inside of car.
        view.RPC("CarEvent", PhotonTargets.OthersBuffered, true);
    }
    /// <summary>
    /// 
    /// </summary>
    public void OnExit()
    {
        foreach (MonoBehaviour m in CarScripts)
        {
            m.enabled = false;
        }
        CarCamera.SetActive(false);

        Player.transform.position = ExitPoint.position;
        Player.transform.rotation = ExitPoint.rotation;

        Player.GetComponent<bl_PlayerCar>().OnExitLocal();
        FindPlayerView(bl_GameController.m_ViewID).RPC("NetworkCarEvent", PhotonTargets.OthersBuffered, false,0);
        InCar = false;
        Into = false;
        LocalInCar = false;
        view.RPC("CarEvent", PhotonTargets.OthersBuffered, false);
    }

    /// <summary>
    /// 
    /// </summary>
    public bool inMyControl
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
        if(view.owner == null || view.owner.name == "Scene")
        {
            InCar = false;
            view.RPC("CarEvent", PhotonTargets.OthersBuffered, false);
            return;
        }
        //is the player in car.
        if (view.owner.name == otherPlayer.name)
        {
            InCar = false;
            view.RPC("CarEvent", PhotonTargets.OthersBuffered, false);
        }
    }

    public override void OnPhotonPlayerConnected(PhotonPlayer newPlayer)
    {
        if(PhotonNetwork.isMasterClient && view.owner == null)
        {
            view.RequestOwnership();
        }
    }
}