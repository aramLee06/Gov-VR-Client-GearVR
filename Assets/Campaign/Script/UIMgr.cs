using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIMgr : MonoBehaviour
{

    TrackingManager trk_Manager;

    public GameObject UIcanvas;
    public GameObject firststar;
    public GameObject secondstar;
    public GameObject thirdstar;
    public GameObject replay;
    public GameObject exit;
    public GameObject complete;
    public GameObject test;

    Image firststar_Rend;
    Image secondstar_Rend;
    Image thirdstar_Rend;
    Image replay_Rend;
    Image exit_Rend;
    Image complete_Rend;
    
    // Use this for initialization
    void Start()
    {
        //trk_Manager = GameObject.Find("aim").GetComponent<TrackingManager>();
        UIcanvas.SetActive(false);
        //replay_Rend = replay.GetComponent<Renderer>();
        //exit_Rend = exit.GetComponent<Renderer
        firststar_Rend = firststar.GetComponent<Image>();
        secondstar_Rend = secondstar.GetComponent<Image>();
        thirdstar_Rend = thirdstar.GetComponent<Image>();
        complete_Rend = complete.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (trk_Manager.trackedItem.name != null)
          //  CheckBGM(trk_Manager.trackedItem.name);
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

    public void setFirstStar()
    {
        firststar_Rend.sprite = Resources.Load<Sprite>("Star_Yell_on");
        //firststart_Rend.sprite = Resources.Load("Exit_on") as Sprite;
    }

    public void setSecondStar()
    {
        secondstar_Rend.sprite = Resources.Load<Sprite>("Star_Yell_on");
    }

    public void setThirdStar()
    {
        thirdstar_Rend.sprite = Resources.Load<Sprite>("Star_Yell_on");
    }

    public void setComplete()
    {
        complete_Rend.sprite = Resources.Load<Sprite>("Mission_Clear");
    }

    public void UISHOW()
    {
        UIcanvas.SetActive(true);
    }
}