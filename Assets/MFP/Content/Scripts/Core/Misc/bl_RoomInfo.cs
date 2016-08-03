using UnityEngine;
using UnityEngine.UI;

public class bl_RoomInfo : MonoBehaviour  {

    public Text RoomNameUI;
    public Text MapNameUI;
    public Text MaxPlayerUI;
    public Text TypeUI;
    public Text ButtonText;
    public Image StatusImg;
    [Space(7)]
    public Color AvailableColor;
    public Color FullColor;

    [HideInInspector]
    public RoomInfo m_Room;
    /// <summary>
    /// 
    /// </summary>
    /// <param name="rn"></param>
    /// <param name="mn"></param>
    /// <param name="p"></param>
    /// <param name="mType"></param>
    public void GetInfo(RoomInfo r)
    {
        m_Room = r;
        RoomNameUI.text = r.name;
        
        MapNameUI.text = r.RoomScene();
        MaxPlayerUI.text = r.playerCount + " / " + r.maxPlayers;

        bool b = r.GetRoomState();
        TypeUI.text = (b == true) ? "Playing" : "Waiting";

        if (r.playerCount >= r.maxPlayers)
        {
            ButtonText.text = "Full";
            StatusImg.color = FullColor;
        }
        else
        {
            ButtonText.text = "Join";
            StatusImg.color = AvailableColor;
        }
    }
    /// <summary>
    /// 
    /// </summary>
    public void EnterRoom()
    {
        if (m_Room.playerCount < m_Room.maxPlayers)
        {
            PhotonNetwork.JoinRoom(m_Room.name);
            bl_CoopUtils.GetLobbyUI.ChangeWindow(5);
        }
        else
        {
            Debug.Log("This Room is Full");
        }
    }
}