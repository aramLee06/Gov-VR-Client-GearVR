////////////////////////////////////////////////////////////////////////////////
//////////////////// bl_PlayerSync.cs///////////////////////////////////////////
////////////////////use this for the sincronizer pocision , rotation, states,/// 
///////////////////etc ...   via photon/////////////////////////////////////////
////////////////////////////////Briner Games////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////
using UnityEngine;

[RequireComponent(typeof(PhotonView))]
public class bl_PlayerSync : bl_PhotonHelper
{
    /// <summary>
    /// the player's team is not ours
    /// </summary>
    [HideInInspector]
    public string RemoteTeam;
    /// <summary>
    /// the current state of the current weapon
    /// </summary>
    public string WeaponState;
    /// <summary>
    /// the object to which the player looked
    /// </summary>
    public Transform HeatTarget;
    /// <summary>
    /// smooth interpolation amount
    /// </summary>
    public float SmoothingDelay = 8f;


    [SerializeField]
    PhotonTransformViewPositionModel m_PositionModel = new PhotonTransformViewPositionModel();

    [SerializeField]
    PhotonTransformViewRotationModel m_RotationModel = new PhotonTransformViewRotationModel();

    [SerializeField]
    PhotonTransformViewScaleModel m_ScaleModel = new PhotonTransformViewScaleModel();

    PhotonTransformViewPositionControl m_PositionControl;
    PhotonTransformViewRotationControl m_RotationControl;
    PhotonTransformViewScaleControl m_ScaleControl;

    bool m_ReceivedNetworkUpdate = false;
    [Space(5)]
   //Script Needed
    [Header("Necessary script")]
    public bl_PlayerAnimator m_PlayerAnimation;

//private
    private bl_PlayerMovement Controller;
    private GameObject CurrenGun;

#pragma warning disable 0414
    [SerializeField]
    bool ObservedComponentsFoldoutOpen = true;
#pragma warning disable 0414


     void Awake()
    {
        if (!PhotonNetwork.connected)
            Destroy(this);

        if (!this.isMine)
        {
            if (HeatTarget.gameObject.activeSelf == false)
            {
                HeatTarget.gameObject.SetActive(true);
            }
        }

        m_PositionControl = new PhotonTransformViewPositionControl(m_PositionModel);
        m_RotationControl = new PhotonTransformViewRotationControl(m_RotationModel);
        m_ScaleControl = new PhotonTransformViewScaleControl(m_ScaleModel);
        Controller = this.GetComponent<bl_PlayerMovement>();
    }
    /// <summary>
    /// serialization method of photon
    /// </summary>
    /// <param name="stream"></param>
    /// <param name="info"></param>
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {

        m_PositionControl.OnPhotonSerializeView(transform.position, stream, info);
        m_RotationControl.OnPhotonSerializeView(transform.rotation, stream, info);
        m_ScaleControl.OnPhotonSerializeView(transform.localScale, stream, info);
        if (isMine == false && m_PositionModel.DrawErrorGizmo == true)
        {
            DoDrawEstimatedPositionError();
        }
        if (stream.isWriting)
        {
            //We own this player: send the others our data
            stream.SendNext(gameObject.name);
            stream.SendNext(HeatTarget.position);
            stream.SendNext(HeatTarget.rotation);
            stream.SendNext(Controller.m_PlayerState);
            stream.SendNext(Controller.grounded);
            stream.SendNext(Controller.vel);
        }
        else
        {
            //Network player, receive data
            RemotePlayerName = (string)stream.ReceiveNext();
            HeadPos = (Vector3)stream.ReceiveNext();
            HeadRot = (Quaternion)stream.ReceiveNext();
            m_state = (PlayerState)stream.ReceiveNext();
            m_grounded = (bool)stream.ReceiveNext();
            NetVel = (Vector3)stream.ReceiveNext();
            //
            m_ReceivedNetworkUpdate = true;
        }
    }

    private Vector3 HeadPos = Vector3.zero;// Head Look to
    private Quaternion HeadRot = Quaternion.identity;
    private PlayerState m_state;
    private bool m_grounded;
    private string RemotePlayerName = string.Empty;
    private int CurNetGun;
    private Vector3 NetVel;

    /// <summary>
    /// 
    /// </summary>
    void Update()
    {
        ///if the player is not ours, then
        if (photonView == null || isMine == true || isConnected == false)
        {
            return;
        }

        UpdatePosition();
        UpdateRotation();
        UpdateScale();

        //Get information from other client
        this.HeatTarget.position = Vector3.Lerp(this.HeatTarget.position, HeadPos, Time.deltaTime * this.SmoothingDelay);
        this.HeatTarget.rotation = HeadRot;
        m_PlayerAnimation.m_PlayerState = m_state;//send the state of player local for remote animation*/
        m_PlayerAnimation.grounded = m_grounded;
        m_PlayerAnimation.velocity = NetVel;

        if (this.gameObject.name != RemotePlayerName)
        {
            gameObject.name = RemotePlayerName;
        }

    }

    /// <summary>
    /// public method to send the RPC shot synchronization
    /// </summary>
    /// <param name="m_type"></param>
    /// <param name="t_spread"></param>
    public void IsFire(string m_type,float t_spread)
    {
        photonView.RPC("FireSync", PhotonTargets.Others, new object[] {m_type,t_spread});
    }
    
    void UpdatePosition()
    {
        if (m_PositionModel.SynchronizeEnabled == false || m_ReceivedNetworkUpdate == false)
        {
            return;
        }

        transform.position = m_PositionControl.UpdatePosition(transform.position);
    }

    void UpdateRotation()
    {
        if (m_RotationModel.SynchronizeEnabled == false || m_ReceivedNetworkUpdate == false)
        {
            return;
        }

        transform.rotation = m_RotationControl.GetRotation(transform.rotation);
    }

    void UpdateScale()
    {
        if (m_ScaleModel.SynchronizeEnabled == false || m_ReceivedNetworkUpdate == false)
        {
            return;
        }

        transform.localScale = m_ScaleControl.GetScale(transform.localScale);
    }
    void DoDrawEstimatedPositionError()
    {
        Vector3 targetPosition = m_PositionControl.GetNetworkPosition();

        Debug.DrawLine(targetPosition, transform.position, Color.red, 2f);
        Debug.DrawLine(transform.position, transform.position + Vector3.up, Color.green, 2f);
        Debug.DrawLine(targetPosition, targetPosition + Vector3.up, Color.red, 2f);
    }
    /// <summary>
    /// These values are synchronized to the remote objects if the interpolation mode
    /// or the extrapolation mode SynchronizeValues is used. Your movement script should pass on
    /// the current speed (in units/second) and turning speed (in angles/second) so the remote
    /// object can use them to predict the objects movement.
    /// </summary>
    /// <param name="speed">The current movement vector of the object in units/second.</param>
    /// <param name="turnSpeed">The current turn speed of the object in angles/second.</param>
    public void SetSynchronizedValues(Vector3 speed, float turnSpeed)
    {
        m_PositionControl.SetSynchronizedValues(speed, turnSpeed);
    }

}