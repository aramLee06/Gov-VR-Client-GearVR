using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class bl_LobbyUI : bl_PhotonHelper
{
    [Separator("Lobby Windows")]
    public GameObject PlayerNameWindow = null;
    public GameObject SearchWindow = null;
    public GameObject HostRoomWindow = null;
    public GameObject MenuWindow = null;
    public GameObject PreSceneWindow = null;
    public GameObject ConnectingWindow = null;
    public GameObject OptionsWindow = null;
    public GameObject SelectServerWindow = null;
    [Space(7)]
    public float UpdateListEach = 5f;
    public Transform RoomPanel = null;
    public GameObject RoomInfoPrefab;
    [Space(5)]
    [Separator("UI Reference")]
    public Text PhotonStatusText = null;
    public Text PhotonRegionText = null;
    public Image FadeImage = null;
    public bl_PreScene PreScene = null;
    public InputField RoomNameInput = null;
    public InputField PlayerNameImput = null;
    [Space(5)]   
    public Text TimeText;
    public Text MaxPlayersText;
    public Text RoomListText = null;
    public Text MapNameText = null;
    public Image MapPreviewImage = null;

    //Privates
    private List<GameObject> CacheRoomList = new List<GameObject>();
    private bool OptionMenuOpen = false;
    private bool isFadeEvent = false;

    void Start()
    {    
        ChangeWindow(6);
      
        //Start Update List Loop 
        InvokeRepeating("UpdateRoomList", 1, UpdateListEach);
        TimeText.text = (Lobby.RoomTime[Lobby.r_Time] / 60) + " <size=12>Min</size>";
        MaxPlayersText.text = "Players: " + Lobby.MaxPlayers[Lobby.m_MaxPlayer];
        MapPreviewImage.sprite = Lobby.SceneManager[Lobby.CurrentScene].PreviewImage;
        MapNameText.text = Lobby.SceneManager[Lobby.CurrentScene].MapName;
        if (RoomNameInput != null) { RoomNameInput.text = "Room (" + Random.Range(0, 9999) + ")"; }
    }

    void OnDisable()
    {
        StopAllCoroutines();
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    public void ChangeWindow(int id)
    {
        switch (id)
        {

            case 0 :
                PlayerNameWindow.SetActive(true);
                MenuWindow.SetActive(false);
                HostRoomWindow.SetActive(false);
                PreSceneWindow.SetActive(false);
                SearchWindow.SetActive(false);
                ConnectingWindow.SetActive(false);
                break;
            case 1:
                PlayerNameWindow.SetActive(false);
                MenuWindow.SetActive(true);
                HostRoomWindow.SetActive(false);
                PreSceneWindow.SetActive(false);
                SearchWindow.SetActive(true);
                ConnectingWindow.SetActive(false);
                break;
            case 2:// Host Room
                PlayerNameWindow.SetActive(false);
                MenuWindow.SetActive(true);
                HostRoomWindow.SetActive(true);
                PreSceneWindow.SetActive(false);
                SearchWindow.SetActive(false);
                if (OptionMenuOpen) { ChangeWindow(3); }
                break;
            case 3://Options
                if (!OptionsWindow.activeSelf) { OptionsWindow.SetActive(true); }
                OptionMenuOpen = !OptionMenuOpen;
                Animation a = OptionsWindow.GetComponent<Animation>();
                if (OptionMenuOpen)
                {
                    a["OptionsWindow"].speed = 1.0f;
                    a.CrossFade("OptionsWindow", 0.2f);
                }
                else
                {
                    if (a["OptionsWindow"].time == 0.0f)
                    {
                        a["OptionsWindow"].time = a["OptionsWindow"].length;
                    }
                    a["OptionsWindow"].speed = -1.0f;
                    a.CrossFade("OptionsWindow", 0.2f);
                }
                break;
            case 4://Menu Window
                PlayerNameWindow.SetActive(false);
                MenuWindow.SetActive(true);
                HostRoomWindow.SetActive(true);
                PreSceneWindow.SetActive(false);
                SearchWindow.SetActive(false);
                break;
            case 5://Create Room
                StartCoroutine(FadeIn(1));
                PlayerNameWindow.SetActive(false);
                MenuWindow.SetActive(false);
                HostRoomWindow.SetActive(false);
                PreSceneWindow.SetActive(true);
                SearchWindow.SetActive(false);
                PreScene.enabled = true;
                if (OptionMenuOpen) { ChangeWindow(3); }
                break;
            case 6://Connecting...
                StartCoroutine(FadeIn(1));
                PlayerNameWindow.SetActive(false);
                MenuWindow.SetActive(false);
                HostRoomWindow.SetActive(false);
                PreSceneWindow.SetActive(false);
                SearchWindow.SetActive(false);
                ConnectingWindow.SetActive(true);
                SelectServerWindow.SetActive(false);
                PreScene.enabled = false;
                if (OptionMenuOpen) { ChangeWindow(3); }
                break;
            case 7://Select server
                if(PhotonNetwork.connected){ PhotonNetwork.Disconnect();}

                PlayerNameWindow.SetActive(false);
                MenuWindow.SetActive(false);
                HostRoomWindow.SetActive(false);
                PreSceneWindow.SetActive(false);
                SearchWindow.SetActive(false);
                ConnectingWindow.SetActive(false);
                SelectServerWindow.SetActive(true);
                break;
        }
    }
    /// <summary>
    /// 
    /// </summary>
    public void UpdateRoomList()
    {
        if (!isConnected )
            return;
        RoomListText.canvasRenderer.SetAlpha(0);
        RoomListText.CrossFadeAlpha(1, 2, true);
        //Removed old list
        if (CacheRoomList.Count > 0)
        {
            foreach (GameObject g in CacheRoomList)
            {
                Destroy(g);
            }
            CacheRoomList.Clear();
        }
        //Update List
        RoomInfo[] ri = PhotonNetwork.GetRoomList();
        if (ri.Length > 0)
        {
            RoomListText.text = string.Empty;
            for (int i = 0; i < ri.Length; i++)
            {
                GameObject r = Instantiate(RoomInfoPrefab) as GameObject;
                CacheRoomList.Add(r);
                r.GetComponent<bl_RoomInfo>().GetInfo(ri[i]);
                r.transform.SetParent(RoomPanel, false);
                Debug.Log("Update");
            }
        }
        else
        {
            RoomListText.text = "There are no available rooms, create one to get started.";
        }
       
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="b"></param>
    public void ChangerTime(bool b)
    {
        if (b)
        {
            if (Lobby.r_Time < Lobby.RoomTime.Length)
            {
                Lobby.r_Time++;
                if (Lobby.r_Time > (Lobby.RoomTime.Length - 1))
                {
                    Lobby.r_Time = 0;

                }

            }
        }
        else
        {
            if (Lobby.r_Time < Lobby.RoomTime.Length)
            {
                Lobby.r_Time--;
                if (Lobby.r_Time < 0)
                {
                    Lobby.r_Time = Lobby.RoomTime.Length - 1;

                }
            }
        }
        TimeText.text = (Lobby.RoomTime[Lobby.r_Time] / 60) + " <size=12>Min</size>";
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="b"></param>
    public void ChangeMaxPlayer(bool b)
    {
        if (b)
        {
            if (Lobby.m_MaxPlayer < Lobby.MaxPlayers.Length)
            {
                Lobby.m_MaxPlayer++;
                if (Lobby.m_MaxPlayer > (Lobby.MaxPlayers.Length - 1))
                {
                    Lobby.m_MaxPlayer = 0;

                }

            }
        }
        else
        {
            if (Lobby.m_MaxPlayer < Lobby.MaxPlayers.Length)
            {
                Lobby.m_MaxPlayer--;
                if (Lobby.m_MaxPlayer < 0)
                {
                    Lobby.m_MaxPlayer = Lobby.MaxPlayers.Length - 1;

                }
            }
        }
        MaxPlayersText.text = "Players: "+ Lobby.MaxPlayers[Lobby.m_MaxPlayer] ;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="b"></param>
    public void ChangeMap(bool b)
    {
        if (b)
        {
            if (m_Lobby.CurrentScene < m_Lobby.SceneManager.Count)
            {
                m_Lobby.CurrentScene++;
                if (m_Lobby.CurrentScene > (m_Lobby.SceneManager.Count - 1))
                {
                    m_Lobby.CurrentScene = 0;
                }
            }
        }
        else
        {
            if (m_Lobby.CurrentScene < m_Lobby.SceneManager.Count)
            {
                m_Lobby.CurrentScene--;
                if (m_Lobby.CurrentScene < 0)
                {
                    m_Lobby.CurrentScene = Lobby.SceneManager.Count - 1;
                }
            }
        }
        MapPreviewImage.sprite = Lobby.SceneManager[Lobby.CurrentScene].PreviewImage;
        MapNameText.text = Lobby.SceneManager[Lobby.CurrentScene].MapName;
    }
    /// <summary>
    /// 
    /// </summary>
    void Update()
    {
        if (PhotonNetwork.connected)
        {
            if (PhotonStatusText != null)
            {
                PhotonStatusText.text = "Connection State: " + bl_CoopUtils.CoopColorStr(PhotonNetwork.connectionStateDetailed.ToString());
            }
            if (PhotonRegionText != null)
            {
                PhotonRegionText.text = "Connected to: " + bl_CoopUtils.CoopColorStr(bl_CoopUtils.RegionToString(this.GetComponent<bl_Lobby>().ServerHost));
            }
        }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="n"></param>
    public void UpdatePlayerName(InputField n)
    {
        if (System.String.IsNullOrEmpty(n.text))
        {
            Debug.Log("Player Name can not be empty!");
        }
        else
        {
            PhotonNetwork.player.name = n.text;
            ChangeWindow(1);
        }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="t"></param>
    /// <returns></returns>
    public IEnumerator FadeIn(float t)
    {
        if (FadeImage == null || isFadeEvent)
            yield return null;

        isFadeEvent = true;
        FadeImage.gameObject.SetActive(true);
        Color c = FadeImage.color;
        while (t > 0.0f)
        {
            t -= Time.deltaTime;
            c.a = t;
            FadeImage.color = c;
            yield return null;
        }
        FadeImage.gameObject.SetActive(false);
        isFadeEvent = false;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="t"></param>
    /// <returns></returns>
    public IEnumerator FadeOut(float t)
    {
        if (FadeImage == null || isFadeEvent)
            yield return null;

        isFadeEvent = true;
        FadeImage.gameObject.SetActive(true);
        Color c = FadeImage.color;
        while (c.a < t)
        {
            c.a += Time.deltaTime;
            FadeImage.color = c;
            yield return null;
        }
        isFadeEvent = false;
    }
    /// <summary>
    /// 
    /// </summary>
    public void Fade()
    {
        StartCoroutine(FadeOut(2));
    }

    private bl_Lobby m_Lobby;
    private bl_Lobby Lobby
    {
        get
        {
            if (m_Lobby == null)
            {
                m_Lobby = this.GetComponent<bl_Lobby>();
            }
            return m_Lobby;
        }
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("We joined the lobby.");
        if (PhotonNetwork.player.name == string.Empty || PhotonNetwork.player.name == null)
        {
            ChangeWindow(0);
        }
        else
        {
            ChangeWindow(1);
        }
    }
}