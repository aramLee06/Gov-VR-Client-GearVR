using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class bl_MovePlataform : bl_PhotonHelper, IPunObservable
{

    [Separator("Settings")]
    public List<PositionInfo> Positions = new List<PositionInfo>();
    public bool PingPong = false;

    [Separator("Multiplayer")]
    public bool Sync = true;


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

    private int CurrentPosition = 0;
    private Transform m_Transform;
    private bool isForward = true;
    private Transform CurrentLocalPlayer;

    /// <summary>
    /// 
    /// </summary>
    void Awake()
    {
        if (Sync)
        {
            if (!view.ObservedComponents.Contains(this))
            {
                view.ObservedComponents.Add(this);
            }
            m_PositionControl = new PhotonTransformViewPositionControl(m_PositionModel);
            m_RotationControl = new PhotonTransformViewRotationControl(m_RotationModel);
            m_ScaleControl = new PhotonTransformViewScaleControl(m_ScaleModel);
        }
    }

    void OnEnable()
    {
        bl_EventHandler.PhotonInstantiateEvent += this.OnPhotonInstantiateEvent;
    }

    void OnDisable()
    {
        bl_EventHandler.PhotonInstantiateEvent -= this.OnPhotonInstantiateEvent;
    }

    /// <summary>
    /// 
    /// </summary>
    void Start()
    {
        m_Transform = transform;
        //add the current position as first position in the list
        PositionInfo[] all = Positions.ToArray();
        List<PositionInfo> newall = new List<PositionInfo>();
        PositionInfo npi = new PositionInfo();
        npi.Position = m_Transform.position;
        npi.Speed = all[0].Speed;
        npi.DelayToNext = all[0].DelayToNext;

        newall.Add(npi);
        newall.AddRange(all);
        Positions = newall;

        if (Sync)
        {
            if (!isMine)
                return;
        }

        StartCoroutine(Process());
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="c"></param>
    void OnTriggerEnter(Collider c)
    {

        if(c.transform.root.GetComponent<bl_PlayerMovement>() != null)
        {
            if (c.transform.root.GetComponent<bl_PlayerMovement>().IsLocalPlayer)
            {
                 c.transform.parent = transform;
                CurrentLocalPlayer = c.transform;
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="c"></param>
    void OnTriggerExit(Collider c)
    {

        if (c.transform == CurrentLocalPlayer)
        {
            CurrentLocalPlayer.parent = null;
        }
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

    /// <summary>
    /// 
    /// </summary>
    /// <param name="stream"></param>
    /// <param name="info"></param>
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (!Sync)
            return;

        m_PositionControl.OnPhotonSerializeView(transform.position, stream, info);
        m_RotationControl.OnPhotonSerializeView(transform.rotation, stream, info);
        m_ScaleControl.OnPhotonSerializeView(transform.localScale, stream, info);

        if (view.isMine == false && m_PositionModel.DrawErrorGizmo == true)
        {
            DoDrawEstimatedPositionError();
        }

        if (stream.isReading == true)
        {
            m_ReceivedNetworkUpdate = true;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    void DoDrawEstimatedPositionError()
    {
        Vector3 targetPosition = m_PositionControl.GetNetworkPosition();

        Debug.DrawLine(targetPosition, transform.position, Color.red, 2f);
        Debug.DrawLine(transform.position, transform.position + Vector3.up, Color.green, 2f);
        Debug.DrawLine(targetPosition, targetPosition + Vector3.up, Color.red, 2f);
    }

    /// <summary>
    /// 
    /// </summary>
    void Update()
    {
        if (!Sync)
            return;

        if (view == null || view.isMine == true || PhotonNetwork.connected == false)
        {
            return;
        }

        UpdatePosition();
        UpdateRotation();
        UpdateScale();
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

    //when MasterClient instantiate set it as owner of this view
    private void OnPhotonInstantiateEvent(PhotonMessageInfo info)
    {
        if (!Sync)
            return;

        if (info.sender.isMasterClient)
        {
            info.photonView.RequestOwnership();
        }
    }

    public override void OnMasterClientSwitched(PhotonPlayer newMasterClient)
    {
        if (!Sync)
            return;

        base.OnMasterClientSwitched(newMasterClient);
        if (newMasterClient == PhotonNetwork.player)
        {
            view.RequestOwnership();
            StartCoroutine(Process(true));
        }
    }

    /// <summary>
    /// 
    /// </summary>
    void OnDrawGizmosSelected()
    {
        Gizmos.color = bl_ColorHelper.HexToColor("30fbd6");
        for (int i = 0; i < Positions.Count; i++)
        {
            if (i >= 1)
            {
                Gizmos.DrawLine(Positions[i - 1].Position, Positions[i].Position);
            }
            else { Gizmos.DrawLine(transform.position, Positions[i].Position); }
        }
        Gizmos.DrawWireCube(transform.position, new Vector3(1, 1, 1));
        if(Positions.Count > 0)
        Gizmos.DrawWireCube(Positions[Positions.Count - 1].Position, new Vector3(1, 1, 1));
    }


    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    IEnumerator Process(bool force = false)
    {
        while (true)
        {
            if (!force)
            {
                if (Sync) { if (!isMine) { yield break; } }
            }

            while (Vector3.Distance(m_Transform.localPosition, GetCurrentPosition.Position) > 0.02f)
            {
                m_Transform.localPosition = Vector3.MoveTowards(m_Transform.localPosition, GetCurrentPosition.Position, Time.deltaTime * GetCurrentPosition.Speed);
                yield return new WaitForEndOfFrame();
            }
            yield return new WaitForSeconds(GetCurrentPosition.DelayToNext);
            GoToNext();
        }
    }

    /// <summary>
    /// Select the next position
    /// </summary>
    void GoToNext()
    {
        if (PingPong)
        {
            if (isForward)
            {
                if (CurrentPosition < Positions.Count - 1)
                {
                    CurrentPosition = (CurrentPosition + 1) % Positions.Count;
                }
                else
                {
                    isForward = !isForward;
                    CurrentPosition = Positions.Count - 1;
                }
            }
            else
            {
                if(CurrentPosition > 0)
                {
                    CurrentPosition = (CurrentPosition - 1) % Positions.Count;
                }
                else
                {
                    isForward = !isForward;
                    CurrentPosition = 1;
                }
            }
        }
        else
        {
            CurrentPosition = (CurrentPosition + 1) % Positions.Count;
        }      
    }

    public PositionInfo GetCurrentPosition
    {
        get
        {
            return Positions[CurrentPosition];
        }
    }

    private PhotonView _view = null;
    private PhotonView view
    {
        get
        {
            if (_view == null)
            {
                if (this.GetComponent<PhotonView>() == null)
                {
                    _view = this.gameObject.AddComponent<PhotonView>();
                }
                else { _view = this.GetComponent<PhotonView>(); }
            }
            return _view;
        }
    }


    [System.Serializable]
    public class PositionInfo
    {
        public Vector3 Position;
        [Range(1,100)]public float Speed;
        [Range(0,20)]public float DelayToNext;
    }

}