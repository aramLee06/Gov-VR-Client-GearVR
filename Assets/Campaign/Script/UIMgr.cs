using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIMgr : MonoBehaviour
{

    TrackingManager trk_Manager;

    public GameObject UIcanvas;
    public GameObject pause_menu;
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

    bool isPause;

    // Use this for initialization
    void Start()
    {
        //trk_Manager = GameObject.Find("aim").GetComponent<TrackingManager>();
        //UIcanvas.SetActive(false);
        //spause_menu.SetActive(false);
        //replay_Rend = replay.GetComponent<Renderer>();
        //exit_Rend = exit.GetComponent<Renderer
        firststar_Rend = firststar.GetComponent<Image>();
        secondstar_Rend = secondstar.GetComponent<Image>();
        thirdstar_Rend = thirdstar.GetComponent<Image>();
        complete_Rend = complete.GetComponent<Image>();
        isPause = false;
    }

    // Update is called once per frame
    void Update()
    {
        //if (trk_Manager.trackedItem.name != null)
        //  CheckBGM(trk_Manager.trackedItem.name);     
        if (Input.GetKeyDown("escape"))
        {
            //pause_menu.SetActive(true);
            Transform camera = GameObject.Find("CenterEyeAnchor").transform;
            Instantiate(pause_menu, camera.transform.position, camera.transform.rotation);
            Time.timeScale = 0;
            //SceneManager.LoadScene("Campaign_Mars", LoadSceneMode.Single);
            /* if (isPause)
             {
                 Debug.Log("pause");
                 Time.timeScale = 0;
                 isPause = false;
             }
             else
             {
                 Time.timeScale = 1;
                 isPause = true;
             }*/
        }
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

        Time.timeScale = 0;
        UIcanvas.SetActive(true);
    }

    public void onReplayButton()
    {
        SceneManager.LoadScene("Campaign_Mars", LoadSceneMode.Single);
    }
    
    public void onExitButton()
    {
        SceneManager.LoadScene("Demo 1", LoadSceneMode.Single);
    }

    public void onContinueButton()
    {
        Time.timeScale = 1;
    }
}