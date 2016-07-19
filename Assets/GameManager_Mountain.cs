using UnityEngine;
using System.Collections;

[System.Serializable]
public class MountainRouteObject
{
    public GameObject RouteA, RouteB, RouteC;
}

[System.Serializable]
public class DroneObject
{
    public GameObject BlueTank1, BlueTank2, RedTank1, RedTank2;
}

[System.Serializable]
public class MountainSpawnObject
{
    public GameObject SpawnPointA, SpawnPointB, SpawnPointC;
}

public class GameManager_Mountain : MonoBehaviour
{
    public MountainRouteObject routeobject;
    public DroneObject tankobject;
    public MountainSpawnObject spawnobject;
    public enum Route { None = 0, A, B, C };
    public enum Tank { None = 0, B1, B2, R1 };
    public Transform[] wayPointList; //루트 좌표
    GameObject selectedroute;
    public GameObject selecteddrone;
    Route route;
    Tank tank;

    // Use this for initialization
    void Start()
    {

    }

    void OnGUI()
    {
        if (route == Route.None)
        {
            if (GUI.Button(new Rect(20, 40, 80, 20), "루트 A"))
            {
                route = Route.A;
                selectedroute = routeobject.RouteA;
                //CreateRoute(selectedroute);
                SetRoute();
                Spawntank();
                //Instantiate(spawnobject.SpawnPointA, spawnobject.SpawnPointA.transform.position, spawnobject.SpawnPointA.transform.rotation);

            }
            if (GUI.Button(new Rect(20, 70, 80, 20), "루트 B"))
            {
                route = Route.B;
                selectedroute = routeobject.RouteB;
                SetRoute();
                Spawntank();
            }
            if (GUI.Button(new Rect(20, 100, 80, 20), "루트 C"))
            {
                route = Route.C;
                selectedroute = routeobject.RouteC;
                SetRoute();
                Spawntank();
            }
        }
    }

    void CreateRoute(GameObject A)
    {
        Instantiate(A, A.transform.position, A.transform.rotation);
    }

    void SetRoute()
    {
        wayPointList = selectedroute.GetComponentsInChildren<Transform>();
    }

    void Spawntank()
    {
        Vector3 dir = wayPointList[1].transform.position - selectedroute.transform.position;
        Quaternion rotation = Quaternion.LookRotation(new Vector3(dir.x, dir.y, dir.z));
        //GameObject spawnDrone = Instantiate(selecteddrone, selectedroute.transform.position, rotation) as GameObject;
        selecteddrone.transform.position = wayPointList[1].position;
        selecteddrone.GetComponent<Mountain_Drone_Move>().wayPointList = wayPointList;
        selecteddrone.GetComponent<Mountain_Drone_Move>().targetWayPoint = wayPointList[2];
        selecteddrone.transform.rotation = rotation;
        //GameObject.Find("CameraContainer").GetComponent<CameraChase>().Unit = spawnDrone.transform;
    }
}
