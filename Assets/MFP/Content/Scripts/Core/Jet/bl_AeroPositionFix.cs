using UnityEngine;

public class bl_AeroPositionFix : bl_PhotonHelper {

    private Vector3 OriginPosition;
    private Quaternion OriginRotation;

    // Use this for initialization
    void Start()
    {
        if (PhotonNetwork.isMasterClient)
        {
            OriginPosition = transform.position;
            OriginRotation = transform.rotation;
            photonView.RPC("GetOriginTransform", PhotonTargets.OthersBuffered, transform.position, transform.rotation);
        }
    }
	

    [PunRPC]
    void GetOriginTransform(Vector3 p,Quaternion r)
    {
        OriginPosition = p;
        OriginRotation = r;
    }

    /// <summary>
    /// 
    /// </summary>
    public void ResetPosition()
    {
        photonView.RPC("GetOriginTransform", PhotonTargets.OthersBuffered, OriginPosition, OriginRotation);
    }


}