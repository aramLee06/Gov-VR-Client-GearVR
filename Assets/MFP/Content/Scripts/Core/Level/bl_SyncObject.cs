using UnityEngine;

public class bl_SyncObject : bl_PhotonHelper {

    [Range(1,20)]
    public float LerpMovement = 15.0f;
    public bool TakeMaster = true;

    private Vector3 originPos = Vector3.zero;
    private Quaternion originRot = Quaternion.identity;

    private bool ReceiveData = false;
    /// <summary>
    /// 
    /// </summary>
    void Start()
    {
        view.observed = this;
        view.synchronization = ViewSynchronization.UnreliableOnChange;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="stream"></param>
    /// <param name="info"></param>
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            //We own this player: send the others our data
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
        }
        else
        {
            //Network player, receive data
            originPos = (Vector3)stream.ReceiveNext();
            originRot = (Quaternion)stream.ReceiveNext();

            ReceiveData = true;
        }
    }
    /// <summary>
    /// 
    /// </summary>
    void FixedUpdate()
    {
        if (view == null || view.isMine == true || PhotonNetwork.connected == false || !ReceiveData)
        {
            return;
        }

        m_Transform.position = Vector3.Lerp(m_Transform.position, originPos, Time.deltaTime * LerpMovement);
        m_Transform.rotation = Quaternion.Lerp(m_Transform.rotation, originRot, Time.deltaTime * LerpMovement);
    }

    //when MasterClient instantiate set it as owner of this view
    public override void OnPhotonInstantiate(PhotonMessageInfo info)
    {
        if (info.sender.isMasterClient)
        {
            view.RequestOwnership();
        }
    }

    public override void OnMasterClientSwitched(PhotonPlayer newMasterClient)
    {
        base.OnMasterClientSwitched(newMasterClient);
        if (newMasterClient == PhotonNetwork.player)
        {
            view.RequestOwnership();
        }
    }

    private Transform _Transform = null;
    private Transform m_Transform
    {
        get
        {
            if (_Transform == null)
            {
                _Transform = this.GetComponent<Transform>();
            }
            return _Transform;
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
}