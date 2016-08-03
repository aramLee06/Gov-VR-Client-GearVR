using UnityEngine;
using UnityEngine.UI;

public class bl_PlayerLobby : MonoBehaviour {

    public PhotonPlayer m_Player;
    public Text PlayerNameText = null;
    public Text ReadyText = null;
    public GameObject isMaster = null;
    [SerializeField]private GameObject OnMaster;
    [HideInInspector]
    public string PlayerName = "";
    [HideInInspector]
    public bool Ready = false;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="n"></param>
    /// <param name="b"></param>
    public void GetInfo(string n, bool b = false)
    {
        if (PhotonNetwork.room == null)
        {
            Debug.Log("Not room for player list");
            Destroy(this.gameObject);
        }

        PlayerName = n;
        PlayerNameText.text = PlayerName;
        ReadyText.text = (b) ? "Ready" : "Not Ready";

        bool m = (PhotonNetwork.playerList.GetMasterClient().name == n) ? true : false;
        bool Imm = (PhotonNetwork.isMasterClient && n != PhotonNetwork.masterClient.name);
        isMaster.SetActive(m);
        OnMaster.SetActive(Imm);
    }

    public void Kick()
    {
        if (PhotonNetwork.isMasterClient)
        {
            Debug.Log("Only Masterclient can kick players");
            return;
        }
        PhotonNetwork.CloseConnection(m_Player);
        Destroy(gameObject);
    }
}