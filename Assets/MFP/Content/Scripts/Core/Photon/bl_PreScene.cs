using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using Hashtable = ExitGames.Client.Photon.Hashtable; //Replace default Hashtables with Photon hashtables
using ExitGames.Client.Photon;

public class bl_PreScene : bl_PhotonHelper{

    public Text RoomNameText = null;
    public GameObject StartButton = null;
    public GameObject ReadyButton = null;
    public GameObject EnterButton = null;
    public List<bl_PlayerLobby> PlayersInRoom = new List<bl_PlayerLobby>();
    //
    [Separator("Player List")]
    public Transform PlayerListPanel;
    public GameObject PlayerPrefabs;
    public List<bl_PlayerLobby> Players = new List<bl_PlayerLobby>();

    [Separator("Chat")]
    public Text ChatText = null;
    public static Text CacheChat = null;


    public static bool isReady = false;
    private bool isAlredyLoaded = false;

    /// <summary>
    /// 
    /// </summary>
    void OnEnable()
    {
        if (!isConnected)
        {
            Debug.Log("Not coonected to room, please try again");
            this.enabled = false;
            return;
        }
        //Register in photon events calls
        PhotonNetwork.OnEventCall += this.OnEventCustom;
      
    }
    /// <summary>
    /// 
    /// </summary>
    void OnDisable()
    {
        //unregister from photon event calls
        PhotonNetwork.OnEventCall -= this.OnEventCustom;
        CancelInvoke("InvokeList");
    }
    /// <summary>
    /// When is master player you have the control of room
    /// only master client can start the macht
    /// </summary>
    void MasterLogic()
    {
        StartButton.SetActive(true);
        ReadyButton.SetActive(false);
        EnterButton.SetActive(false);
        isReady = true;
    }
    /// <summary>
    /// When you are a normal client
    /// you only can wait for start the macht
    /// </summary>
    void NormalPlayerLogic()
    {
        StartButton.SetActive(false);
        ReadyButton.SetActive(true);
        EnterButton.SetActive(false);

    }
    /// <summary>
    /// 
    /// </summary>
    void FixedUpdate()
    {
        if (!PhotonNetwork.connected)
            return;

        //verify is level loaded
        if (PhotonNetwork.room.GetRoomState() && !isAlredyLoaded)
        {
            PhotonNetwork.isMessageQueueRunning = false;
            isAlredyLoaded = true;
            LevelLoaded(0);
        }
    }
    /// <summary>
    /// when master client start macht and you are not ready yet
    /// then, you can load the map where you want.
    /// </summary>
    public void LevelLoaded(int state)
    {
        if (state == 0)
        {
            StartButton.SetActive(false);
            ReadyButton.SetActive(false);
            EnterButton.SetActive(true);
        }
        else
        {

            PhotonNetwork.isMessageQueueRunning = false;
            CancelInvoke("InvokeList");
            Application.LoadLevel(PhotonNetwork.room.RoomScene());
        }
    }
    /// <summary>
    /// Update the player list with all photon player in room
    /// </summary>
    void InvokeList()
    {
        for (int i = 0; i < PhotonNetwork.playerList.Length; i++)
        {
            UpdatPlayerList(PhotonNetwork.playerList[i].name, PhotonNetwork.playerList[i].GetReady());
        }
    }

    /// <summary>
    /// Load and sync level
    /// Only masterclient can call this function
    /// </summary>
    public void LoadSyncLevel()
    {
        //Start game only when all are ready
        if (PhotonNetwork.playerList.AllPlayersReady())
        {
            Hashtable e = new Hashtable();
            e.Add("Level", PhotonNetwork.room.RoomScene());
            PhotonNetwork.RaiseEvent(EventID.LoadSyncLevel, e, true, new RaiseEventOptions() { Receivers = ReceiverGroup.All });
        }
        else
        {
            Debug.Log("Some players are not ready, wait for hes");
        }
    }
    /// <summary>
    /// change the state and sync for other players
    /// </summary>
    public void Ready()
    {
        isReady = !isReady;
        PhotonNetwork.player.SendReady(isReady);
        Hashtable e = new Hashtable();
        e.Add("PName",LocalName);
        e.Add("Ready",isReady);
        PhotonNetwork.RaiseEvent(EventID.PlayerJoinPre, e, true, new RaiseEventOptions() { Receivers = ReceiverGroup.All});    
    }
    /// <summary>
    /// Update the playerlist
    /// </summary>
    public void UpdatPlayerList(string n,bool ready,bool remove = false)
    {
        if (!remove)
        {
            bool find = false;
            for (int i = 0; i < Players.Count; i++)
            {
                if (Players[i].PlayerName == n)
                {
                    find = true;//then, only update
                    Players[i].GetInfo(n, ready);
                }
            }
            //If a new player
            if (!find)//if player doest exist.
            {
                GameObject p = Instantiate(PlayerPrefabs) as GameObject;
                bl_PlayerLobby pl = p.GetComponent<bl_PlayerLobby>();
                pl.GetInfo(n, ready);
                Players.Add(pl);
                p.transform.SetParent(PlayerListPanel, false);
            }
        }
        else
        {
            for (int i = 0; i < Players.Count; i++)
            {
                if (Players[i].PlayerName == n)
                {
                    Destroy(Players[i].gameObject);
                    Players.RemoveAt(i);
                }
            }
        }
    }
    /// <summary>
    /// 
    /// </summary>
    private List<PhotonPlayer> PlayersAvailables
    {
        get
        {
            List<PhotonPlayer> list = new List<PhotonPlayer>();
            foreach (PhotonPlayer p in PhotonNetwork.playerList)
            {
                list.Add(p);
            }
            return list;
        }
    }
    // <summary>
    /// Receive events from server
    /// </summary>
    /// <param name="eventCode"></param>
    /// <param name="content"></param>
    /// <param name="senderID"></param>
    public void OnEventCustom(byte eventCode, object content, int senderID)
    {
        Hashtable hash = new Hashtable();
        hash = (Hashtable)content;
        switch (eventCode)
        {
            case EventID.PlayerJoinPre:
                string mName = (string)hash["PName"];
                bool ready = (bool)hash["Ready"];

                UpdatPlayerList(mName, ready);
                break;
        }
    }
    /// <summary>
    /// Simple Chat
    /// </summary>
    /// <param name="msn"></param>
    public static void SendChat(string sender, string msn)
    {
        CacheChat.text += "\n ["+ bl_CoopUtils.CoopColorStr(sender) + "] " + msn;
    }
    /// <summary>
    /// Add text sync
    /// </summary>
    /// <param name="i"></param>
    public void NewChatMsn(InputField i)
    {
        string t = i.text;
        photonView.RPC("AddChat", PhotonTargets.All, t);
        i.text = string.Empty;
    }

    [PunRPC]
    void AddChat(string text,PhotonMessageInfo p)
    {
        ChatText.text += "\n (" + bl_CoopUtils.CoopColorStr(p.sender.name) + "): " + text;
    }
    // We have two options here: we either joined(by title, list or random) or created a room.
    public override void OnJoinedRoom()
    {
        if (PhotonNetwork.isMasterClient)
        {
            PhotonNetwork.room.SendRoomState(false);
        }
        bool b = false;
        if (PhotonNetwork.room.customProperties.ContainsKey(PropiertiesKeys.RoomState))
        {
            b = PhotonNetwork.room.GetRoomState();
        }

        if (b)//if scene alredy loaded
        {
            //load scene
            LevelLoaded(1);
        }
        else//if waiting
        {
            if (RoomNameText != null)
            {
                RoomNameText.text = PhotonNetwork.room.name;
            }
            //Determine the logic 
            if (IsMaster)
            {
                MasterLogic();
            }
            else
            {
                NormalPlayerLogic();
            }
            CacheChat = ChatText;
            InvokeRepeating("InvokeList", 1, 3);
        }
    }

    public void ReturnToLobby() { PhotonNetwork.LeaveRoom(); }
    /// <summary>
    /// 
    /// </summary>
    public override void OnLeftRoom()
    {
        base.OnLeftRoom();
        this.GetComponent<bl_LobbyUI>().ChangeWindow(6);
        CancelInvoke("InvokeList");
        for (int i = 0; i < Players.Count; i++)
        {
            Destroy(Players[i].gameObject);
        }
        Players.Clear();
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="info"></param>
    public override void OnPhotonInstantiate(PhotonMessageInfo info)
    {
        base.OnPhotonInstantiate(info);
        Debug.Log("This Player: " + info.sender.name + " is Instantiate");
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="otherPlayer"></param>
    public override void OnPhotonPlayerDisconnected(PhotonPlayer otherPlayer)
    {
        base.OnPhotonPlayerDisconnected(otherPlayer);
        Debug.Log(string.Format("Player: {0} is left of room.", otherPlayer.name));
        UpdatPlayerList(otherPlayer.name, false, true);
    }
}