using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class bl_GameController : bl_PhotonHelper {

    public static bool isPlaying = false;
    public static int m_ViewID = -1;
    [HideInInspector] public GameObject m_Player { get; set; }

    /// <summary>
    /// Player to instantiate, this need stay in "Resources" folder.
    /// </summary>
    public GameObject PlayerPrefab = null;
    /// <summary>
    /// Scene To return when left of room or disconnect
    /// </summary>
    public string ReturnScene;
    /// <summary>
    /// All SpawnPoint where player can appear in scene / map.
    /// </summary>
    private static List<bl_SpawnPoint> SpawnPoint = new List<bl_SpawnPoint>();
    public SpawnType m_SpawnType = SpawnType.Random;
    [Space(7)]
    public GameObject RoomCamera = null;
    public GameObject StartWindow = null;
    public Image BlackBg = null;

    /// <summary>
    /// 
    /// </summary>
    void Awake()
    {
        //When start in this scene, but is not connect, return to lobby to connect
        if (!isConnected || PhotonNetwork.room == null)
        {
            Application.LoadLevel(0);
            return;
        }
        StartCoroutine(FadeIn(1.5f));  
        //Avaible to receive messages from cloud.
        PhotonNetwork.isMessageQueueRunning = true;
        //Infor to Lobby information, that this scene is playing
        if (PhotonNetwork.isMasterClient)
        {
            PhotonNetwork.room.SendRoomState(true);
        }
        if (StartWindow != null)
        {
            StartWindow.SetActive(true);
        }
    }
    /// <summary>
    /// 
    /// </summary>
    public void SpawnPlayer()
    {
        if (PlayerPrefab == null)
        {
            Debug.Log("Player Prefabs I was not assigned yet!");
            return;
        }
        //Sure of hace just only player on room
        if (m_Player != null)
        {
            NetworkDestroy(m_Player);
        }

        Vector3 p = Vector3.zero;
        Quaternion r = Quaternion.identity;
        //Get Position and rotation from a spawnPoint
        GetSpawnPoint(out p, out r);

        m_Player = PhotonNetwork.Instantiate(PlayerPrefab.name,p,r, 0);
        m_ViewID = m_Player.GetViewID();
        if (RoomCamera != null)
        {
            RoomCamera.SetActive(false);
        }
        bl_CoopUtils.LockCursor(true);
        isPlaying = true;

        //Send a new log information
        string logText = LocalName + " joined to game";
        bl_LogInfo inf = new bl_LogInfo(logText, Color.green);
        bl_EventHandler.OnLogMsnEvent(inf);
    }
    /// <summary>
    /// 
    /// </summary>
    void OnDisable()
    {
        isPlaying = false;
    }
    /// <summary>
    /// Get the spawnPoint
    /// </summary>
    private int currentSpawnPoint = 0;
    private void GetSpawnPoint(out Vector3 position, out Quaternion rotation)
    {
            if (SpawnPoint.Count <= 0)
            {
                Debug.LogWarning("Doesn´t have spawnpoint in scene");
                position = Vector3.zero;
                rotation = Quaternion.identity;
            }

            if (m_SpawnType == SpawnType.Random)
            {

                int random = Random.Range(0, SpawnPoint.Count);
                Vector3 s = Random.insideUnitSphere * SpawnPoint[random].SpawnRadius;
                Vector3 pos = SpawnPoint[random].transform.position + new Vector3(s.x, 0, s.z);

                position = pos;
                rotation = SpawnPoint[random].transform.rotation;
            }
            else if (m_SpawnType == SpawnType.RoundRobin)
            {
                if (currentSpawnPoint >= SpawnPoint.Count) { currentSpawnPoint = 0; }
                Vector3 s = Random.insideUnitSphere * SpawnPoint[currentSpawnPoint].SpawnRadius;
                Vector3 v = SpawnPoint[currentSpawnPoint].transform.position + new Vector3(s.x, 0, s.z);
                currentSpawnPoint++;

                position = v;
                rotation = SpawnPoint[currentSpawnPoint].transform.rotation;
            }
            else
            {
                position = Vector3.zero;
                rotation = Quaternion.identity;
            }

    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="point"></param>
    public static void RegisterSpawnPoint(bl_SpawnPoint point)
    {
        SpawnPoint.Add(point);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="point"></param>
    public static void UnRegisterSpawnPoint(bl_SpawnPoint point)
    {
        SpawnPoint.Remove(point);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="t"></param>
    /// <returns></returns>
    public IEnumerator FadeIn(float t)
    {
        if (BlackBg == null)
            yield return null;

        BlackBg.gameObject.SetActive(true);
        Color c = BlackBg.color;
        while (t > 0.0f)
        {
            t -= Time.deltaTime;
            c.a = t;
            BlackBg.color = c;
            yield return null;
        }
        BlackBg.gameObject.SetActive(false);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="t"></param>
    /// <returns></returns>
    public IEnumerator FadeOut(float t)
    {
        if (BlackBg == null)
            yield return null;
        BlackBg.gameObject.SetActive(true);
        Color c = BlackBg.color;
        while (c.a < t)
        {
            c.a += Time.deltaTime;
            BlackBg.color = c;
            yield return null;
        }
    }
    /// <summary>
    /// 
    /// </summary>
    public override void OnLeftRoom()
    {
        base.OnLeftRoom();
        if(m_Player != null)
        {  
            PhotonNetwork.Destroy(m_Player);
        }
        Application.LoadLevel(ReturnScene);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="otherPlayer"></param>
    public override void OnPhotonPlayerDisconnected(PhotonPlayer otherPlayer)
    {
        base.OnPhotonPlayerDisconnected(otherPlayer);

        //Send a new log information
        string logText = otherPlayer.name + " Left of room";
        //Send this as local because this function is already call in other players in room.
        bl_LogInfo inf = new bl_LogInfo(logText, Color.red,true);
        bl_EventHandler.OnLogMsnEvent(inf);
        //Debug.Log(otherPlayer.name + " Left of room");
        if (PhotonNetwork.isMasterClient)
        {
            PhotonNetwork.DestroyPlayerObjects(otherPlayer);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="newMasterClient"></param>
    public override void OnMasterClientSwitched(PhotonPlayer newMasterClient)
    {
        base.OnMasterClientSwitched(newMasterClient);
        //Send a new log information
        string logText = "We have a new Master Client: " + newMasterClient.name;
        //Send this as local because this function is already call in other players in room.
        bl_LogInfo inf = new bl_LogInfo(logText, Color.yellow, true);
        bl_EventHandler.OnLogMsnEvent(inf);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="info"></param>
    public override void OnPhotonInstantiate(PhotonMessageInfo info)
    {
        bl_EventHandler.OnPhotonInstantiate(info);
    }

    [System.Serializable]
    public enum SpawnType
    {
        Random,
        RoundRobin,
    }
}