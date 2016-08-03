using UnityEngine;

public class bl_Passenger : bl_PhotonHelper {

    [Separator("References")]
    public GameObject CarCamera = null;
    public GameObject TextUI = null;
    public Transform ExitPoint;

    [HideInInspector]
    public bool Into = false;
    [HideInInspector]
    public bool InCar = false;
    [HideInInspector]
    public bool InUse = false;
    [HideInInspector]
    public string UseBy = "";
    private PhotonView view;
    private GameObject Player;
    private bl_GameController GController = null;

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
                Player = GController.m_Player;
            }
            return;
        }

        if (Input.GetKeyDown(KeyCode.E) && Into && !InCar && !InUse)
        {
            OnEnter();
        }
        else if (Input.GetKeyDown(KeyCode.E) && InCar && InUse && inMyControl)
        {
            OnExit();
        }

        if (InCar && InUse && inMyControl)
        {
            Player.transform.root.position = this.transform.root.position;
            Player.transform.root.rotation = this.transform.root.rotation;
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
        CarCamera.SetActive(true);
        InCar = true;
        InUse = true;
        UseBy = LocalName;
        Player.GetComponent<bl_PlayerCar>().OnEnterLocal();
        TextUI.SetActive(false);
        FindPlayerView(bl_GameController.m_ViewID).RPC("NetworkCarEvent", PhotonTargets.OthersBuffered, true,3);
        view.RPC("PassengerEvent", PhotonTargets.OthersBuffered, true, LocalName);
    }
    /// <summary>
    /// 
    /// </summary>
    public void OnExit()
    {

        CarCamera.SetActive(false);

        Player.transform.position = ExitPoint.position;
        Player.transform.rotation = ExitPoint.rotation;
        InUse = false;
        InCar = false;
        UseBy = "";
        Player.GetComponent<bl_PlayerCar>().OnExitLocal();
        FindPlayerView(bl_GameController.m_ViewID).RPC("NetworkCarEvent", PhotonTargets.OthersBuffered, false,3);
        view.RPC("PassengerEvent", PhotonTargets.OthersBuffered, false, string.Empty);
        
    }

    public bool inMyControl
    {
        get
        {
            bool b = false;
            if (UseBy == LocalName)
            {
                b = true;
            }
            return b;
        }
    }
}