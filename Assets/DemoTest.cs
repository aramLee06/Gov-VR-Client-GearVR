using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class DemoTest : MonoBehaviour
{
    public GameObject test;
    void OnGUI()
    {
        if (GUI.Button(new Rect(20, 40, 80, 20), "루트 A"))
        {
            Demo.routenum = 1;
            SceneManager.LoadScene("Campaign_Mountain_Demo", LoadSceneMode.Single);
        }
        if (GUI.Button(new Rect(20, 70, 80, 20), "루트 B"))
        {
            Demo.routenum = 2;
            SceneManager.LoadScene("Campaign_Mountain_Demo", LoadSceneMode.Single);
        }
        if (GUI.Button(new Rect(20, 100, 80, 20), "루트 C"))
        {
            Demo.routenum = 3;
            SceneManager.LoadScene("Campaign_Mountain_Demo", LoadSceneMode.Single);
        }
    }

    // Use this for initialization
    void Start()
    {

    }
 
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            test = GameObject.Find("aim").GetComponent<TrackingManager>().trackedItem;
            if (test.tag == "A")
            {
                Demo.routenum = 1;
                SceneManager.LoadScene("Campaign_Mountain_Demo", LoadSceneMode.Single);
            }
            if (test.tag == "B")
            {
                Demo.routenum = 2;
                SceneManager.LoadScene("Campaign_Mountain_Demo", LoadSceneMode.Single);
            }
            if (test.tag == "C")
            {
                Demo.routenum = 3;
                SceneManager.LoadScene("Campaign_Mountain_Demo", LoadSceneMode.Single);
            }
        }
    }
}
