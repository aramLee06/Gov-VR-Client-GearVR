/////////////////////////////////////////////////////////////////////////////////
///////////////////////////////bl_RoundTime.cs///////////////////////////////////
///////////////Use this to manage time in rooms//////////////////////////////////
/////////////////////////////////////////////////////////////////////////////////
//////////////////////////////Lovatto Studio/////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////////
using UnityEngine;
using System.Collections;
using Hashtable = ExitGames.Client.Photon.Hashtable; //Replace default Hashtables with Photon hashtables
using UnityEngine.UI;

public class bl_RoundTime : MonoBehaviour {

    /// <summary>
    /// mode of the round room
    /// </summary>
    public RoundStyle m_RoundStyle;
    /// <summary>
    /// expected duration in round (automatically obtained)
    /// </summary>
    public int RoundDuration;
    [HideInInspector]
    public float CurrentTime;
    [System.Serializable]
    public class UI_
    {
        public Text TimeText;
        public GameObject TimeUI;
        [Space(5)]
        public Text FinalText;
        public GameObject FinalUI;
    }
    public UI_ UI;
    //private
    private const string StartTimeKey = "CurrentTimeRoom";       // the name of our "start time" custom property.
    private float m_Reference;
    private int m_countdown = 10;
    public static bool isFinish = false;

    /// <summary>
    /// 
    /// </summary>
    void Awake()
    {
        //When you start from room scene, return to lobby for connect to server first.
        if (!PhotonNetwork.connected || PhotonNetwork.room == null)
        {
            Application.LoadLevel(0);
            return;
        }
        isFinish = false;
        if ((string)PhotonNetwork.room.customProperties[PropiertiesKeys.RoomRoundKey] == "1")
        {
            m_RoundStyle = RoundStyle.Rounds;
        }
        else
        {
            m_RoundStyle = RoundStyle.OneMacht;
        }
        GetTime();
    }

    void OnDisable()
    {
        isFinish = false;
    }
    /// <summary>
    /// get the current time and verify if it is correct
    /// </summary>
    void GetTime()
    {
        RoundDuration = (int)PhotonNetwork.room.customProperties[PropiertiesKeys.TimeRoomKey];
        if (PhotonNetwork.isMasterClient)
        {
            m_Reference = (float)PhotonNetwork.time;

            Hashtable startTimeProp = new Hashtable();  // only use ExitGames.Client.Photon.Hashtable for Photon
            startTimeProp.Add(StartTimeKey, m_Reference);
            PhotonNetwork.room.SetCustomProperties(startTimeProp);
        }
        else
        {
            if (PhotonNetwork.room.customProperties.ContainsKey(StartTimeKey))
            {
                m_Reference = (float)PhotonNetwork.room.customProperties[StartTimeKey];
                if (!UI.TimeUI.activeSelf)
                {
                    UI.TimeUI.SetActive(true);
                }
            }
        }
    }
    /// <summary>
    /// 
    /// </summary>
    void FixedUpdate()
    {
        //Calculate the time server
        float t_time = RoundDuration - ((float)PhotonNetwork.time - m_Reference);
        if (t_time > 0)
        {
            CurrentTime = t_time;
        }
        else if (t_time <= 0.001 && GetTimeServed == true)//Round Finished
        {
            CurrentTime = 0;
            
            bl_EventHandler.OnRoundEndEvent();
            if (!isFinish)
            {
                isFinish = true;
                InvokeRepeating("countdown", 1, 1);
            }
        }
        else//even if I do not photonnetwork.time then obtained to regain time
        {
            Refresh();
        }
        TimeLogic();

    }
    /// <summary>
    /// 
    /// </summary>
    void TimeLogic()
    {
        //Convert in time format
        int normalSecons = 60;
        float remainingTime = Mathf.CeilToInt(CurrentTime);
        int m_Seconds = Mathf.FloorToInt(remainingTime % normalSecons);
        int m_Minutes = Mathf.FloorToInt((remainingTime / normalSecons) % normalSecons);
        string t_time = bl_CoopUtils.GetTimeFormat(m_Minutes, m_Seconds);

        //Update UI 
        if (UI.TimeText != null)
        {
            UI.TimeText.text = "<size=9>Remaining</size> \n" + t_time;
        }
        if (isFinish)
        {
            if (!UI.FinalUI.activeSelf)
            {
                UI.FinalUI.SetActive(true);
            }
            if (m_RoundStyle == RoundStyle.OneMacht)
            {
                UI.FinalText.text = "Round finished, return to lobby in \n " + m_countdown;
            }
            else if (m_RoundStyle == RoundStyle.Rounds)
            {
                UI.FinalText.text = "Round finished, restart in \n " + m_countdown;
            }
        }
    }

    /// <summary>
    /// with this fixed the problem of the time lag in the Photon
    /// </summary>
    void Refresh()
    {
        //Only masterClient can send the time.
        if (PhotonNetwork.isMasterClient)
        {
            m_Reference = (float)PhotonNetwork.time;

            Hashtable startTimeProp = new Hashtable();  // only use ExitGames.Client.Photon.Hashtable for Photon
            startTimeProp.Add(StartTimeKey, m_Reference);
            PhotonNetwork.room.SetCustomProperties(startTimeProp);
        }
        else//When is a normal player (Client)
        {
            //Receive the time reference from the server (masterClient side)
            if (PhotonNetwork.room.customProperties.ContainsKey(StartTimeKey))
            {
                m_Reference = (float)PhotonNetwork.room.customProperties[StartTimeKey];
                if (!UI.TimeUI.activeSelf)
                {
                    UI.TimeUI.SetActive(true);
                }
            }
        }
    }
    /// <summary>
    /// When round is finished, start this countdown for return to lobby
    /// You can change the second of countdown in the private variable "m_counddown".
    /// </summary>
    bool callFade = false;
    void countdown()
    {
        m_countdown--;

        //Do fade effect when is finished countdown.
        if (m_countdown == 1 && !callFade)
        {
            StartCoroutine(this.GetComponent<bl_GameController>().FadeOut(2));
        }

        if (m_countdown <= 0)
        {
            FinishGame();
            CancelInvoke("countdown");
            m_countdown = 10;
        }
    }
    /// <summary>
    /// When countdown is rached, this is call for determine if return
    /// to lobby or restart scene.
    /// </summary>
    void FinishGame()
    {
        bl_CoopUtils.LockCursor(false);
        if (m_RoundStyle == RoundStyle.OneMacht)
        {
            if (PhotonNetwork.connected)
            {
                PhotonNetwork.LeaveRoom();
            }
            else
            {
                Application.LoadLevel(0);
            }
        }
        if (m_RoundStyle == RoundStyle.Rounds)
        {
            GetTime();
        }
    }
    /// <summary>
    /// Determines whether the time is long enough to receive the server response
    /// </summary>
    bool GetTimeServed
    {
        get
        {
            bool m_bool = false ;
            if (Time.timeSinceLevelLoad > 7)
            {
                m_bool = true;
            }
            return m_bool;
        }
    }

}