using UnityEngine;
using UnityEngine.UI;

public class bl_PlayerList : MonoBehaviour {

    public Text PlayerName;
    public Text Ping;
    public GameObject KickButton = null;

    private PhotonPlayer cachePlayer = null;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="PName"></param>
    /// <param name="ping"></param>
    public void GetInfo(string PName, string ping,PhotonPlayer player)
    {
        PlayerName.text = PName;
        Ping.text = ping;
        //Appear kick button only to the master client.
        bool _active = (PhotonNetwork.isMasterClient && player != PhotonNetwork.player ) ? true : false;
        KickButton.SetActive(_active);
        cachePlayer = player;
    }
    /// <summary>
    /// 
    /// </summary>
    public void KickPlayer()
    {
        if (!PhotonNetwork.isMasterClient)
            return;

        if (cachePlayer != null)
        {
            PhotonNetwork.CloseConnection(cachePlayer);
        }
        else
        {
            Debug.LogWarning("This Player doesnt exit!");
        }
    }
}