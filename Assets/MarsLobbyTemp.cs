using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MarsLobbyTemp : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnRouteA()
    {
        Demo.routenum = 1;
        SceneManager.LoadScene("Campaign_Mars", LoadSceneMode.Single);
    }
    public void OnRouteB()
    {
        Demo.routenum = 2;
        SceneManager.LoadScene("Campaign_Mars", LoadSceneMode.Single);
    }
    public void OnRouteC()
    {
        Demo.routenum = 3;
        SceneManager.LoadScene("Campaign_Mars", LoadSceneMode.Single);
    }
}
