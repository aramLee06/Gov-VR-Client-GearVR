using UnityEngine;
using System.Collections;

public class UIMgr : MonoBehaviour
{

    TrackingManager trk_Manager;

    public GameObject replay;
    public GameObject exit;

    Renderer replay_Rend;
    Renderer exit_Rend;

    // Use this for initialization
    void Start()
    {
        trk_Manager = GameObject.Find("aim").GetComponent<TrackingManager>();

        replay_Rend = replay.GetComponent<Renderer>();
        exit_Rend = exit.GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (trk_Manager.trackedItem.name != null)
            CheckBGM(trk_Manager.trackedItem.name);
    }

    void CheckBGM(string bName)
    {
        switch (bName)
        {
            case "REPLAY":
                replay_Rend.material.mainTexture = Resources.Load("Replay_on") as Texture;
                exit_Rend.material.mainTexture = Resources.Load("Exit_off") as Texture;
                break;
            case "EXIT":
                replay_Rend.material.mainTexture = Resources.Load("Replay_off") as Texture;
                exit_Rend.material.mainTexture = Resources.Load("Exit_on") as Texture;
                break;
        }
    }
}