using UnityEngine;
using System.Collections;


[System.Serializable]
public class RouteObject
{
    public GameObject RouteA, RouteB, RouteC;
}

[System.Serializable]
public class TankObject
{
    public GameObject BlueTank1, BlueTank2, RedTank1, RedTank2;
}

[System.Serializable]
public class SpawnObject
{
    public GameObject SpawnPointA, SpawnPointB, SpawnPointC;
}

public class GameMgr : MonoBehaviour
{
    public RouteObject routeobject;
    public TankObject tankobject;
    public SpawnObject spawnobject;
    public enum Route { None = 0, A, B, C };
    public enum Tank { None = 0, B1, B2, R1 };
    public int enemycount;
    public int enemykillcount;
    GameObject selectedroute;
    GameObject selectedtank;
    Route route;
    Tank tank;
    GameObject spawnTank;
    //CanvasGroup canvasGroup;
    GameObject canvas;

    public Transform[] wayPointList; //루트 좌표
    public Transform waypointContainer;
    //미사일 번호 추가

    // Use this for initialization
    void Start()
    { //여기서 루트랑 탱크 설정 if (Demo.routenum == 1)
      //if (GUI.Button(new Rect(20, 40, 80, 20), "루트 A"))
        if (Demo.routenum == 1)
        {
            route = Route.A;
            selectedroute = routeobject.RouteA;
            CreateRoute(selectedroute);
            SetRoute();
            Instantiate(spawnobject.SpawnPointA, spawnobject.SpawnPointA.transform.position, spawnobject.SpawnPointA.transform.rotation);
        }
        if (Demo.routenum == 2)
        {
            route = Route.B;
            selectedroute = routeobject.RouteB;
            CreateRoute(selectedroute);
            SetRoute();
            Instantiate(spawnobject.SpawnPointB, spawnobject.SpawnPointB.transform.position, spawnobject.SpawnPointB.transform.rotation);
        }
        if (Demo.routenum == 3)
        {
            route = Route.C;
            selectedroute = routeobject.RouteC;
            CreateRoute(selectedroute);
            SetRoute();
            Instantiate(spawnobject.SpawnPointC, transform.position, transform.rotation);
        }

        selectedtank = tankobject.BlueTank1;
        Spawntank();
        enemycount = 0;
        enemykillcount = 0;
        //canvasGroup = GetComponent<CanvasGroup>();
    }

    void OnGUI()
    {
        if (route == Route.None)
        {
            if (GUI.Button(new Rect(20, 40, 80, 20), "루트 A"))
            {
                route = Route.A;
                selectedroute = routeobject.RouteA;
                CreateRoute(selectedroute);
                SetRoute();
                Instantiate(spawnobject.SpawnPointA, spawnobject.SpawnPointA.transform.position, spawnobject.SpawnPointA.transform.rotation);
            }
            if (GUI.Button(new Rect(20, 70, 80, 20), "루트 B"))
            {
                route = Route.B;
                selectedroute = routeobject.RouteB;
                CreateRoute(selectedroute);
                SetRoute();
                Instantiate(spawnobject.SpawnPointB, spawnobject.SpawnPointB.transform.position, spawnobject.SpawnPointB.transform.rotation);
            }
            if (GUI.Button(new Rect(20, 100, 80, 20), "루트 C"))
            {
                route = Route.C;
                selectedroute = routeobject.RouteC;
                CreateRoute(selectedroute);
                SetRoute();
                Instantiate(spawnobject.SpawnPointC, transform.position, transform.rotation);
            }
        }
        else if (tank == Tank.None)
        {
            if (GUI.Button(new Rect(20, 40, 80, 20), "블루 탱크 1"))
            {
                tank = Tank.B1;
                selectedtank = tankobject.BlueTank1;
                Spawntank();
            }
            if (GUI.Button(new Rect(20, 70, 80, 20), "블루 탱크 2"))
            {
                tank = Tank.B2;
                selectedtank = tankobject.BlueTank2;
                Spawntank();
            }
            if (GUI.Button(new Rect(20, 100, 80, 20), "레드 탱크 1"))
            {
                tank = Tank.R1;
                selectedtank = tankobject.RedTank1;
                Spawntank();
            }

        }
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnTank.GetComponent<PlayerMove>().isdead)
        {
            //check
            GameObject.Find("UIManager").SendMessage("setFirstStar");
            //GameObject.FindWithTag("Boss").GetComponent<BigBadBoss>().battlemod();
        }
        //canvas.alpha=1; //플레이어가 죽으면 캔버스 보이기
        if (spawnTank.GetComponent<PlayerMove>().bossbattle)
            //GameObject.Find("UIManager").GetComponent<UIMgr>().setFirstStar();
            GameObject.Find("UIManager").SendMessage("setFirstStar");
    }

    public void countingenemy()
    {
        enemycount++;
    }

    public void countingkill()
    {
        enemykillcount++;
        secondstar();
    }

    void CreateRoute(GameObject A)
    {
        Instantiate(A, A.transform.position, A.transform.rotation);
    }

    void SetRoute()
    {
        if (route == Route.A)
        {
            wayPointList = routeobject.RouteA.GetComponentsInChildren<Transform>();
        }
        else if (route == Route.B)
        {
            wayPointList = routeobject.RouteB.GetComponentsInChildren<Transform>();
        }
        else if (route == Route.C)
        {
            wayPointList = routeobject.RouteC.GetComponentsInChildren<Transform>();
        }
    }

    void Spawntank()
    {
        Vector3 dir = wayPointList[1].transform.position - selectedroute.transform.position;
        Quaternion rotation = Quaternion.LookRotation(new Vector3(dir.x, dir.y, dir.z));
        spawnTank = Instantiate(selectedtank, selectedroute.transform.position, rotation) as GameObject;
        spawnTank.GetComponent<PlayerMove>().wayPointList = wayPointList;
        spawnTank.GetComponent<PlayerMove>().targetWayPoint = wayPointList[1];
        spawnTank.transform.rotation = rotation;
        GameObject.Find("CameraContainer").GetComponent<CameraChase>().Unit = spawnTank.transform;
    }

    void secondstar()
    {
        //죽인수 ÷ 적수 X 100
        int score = enemykillcount / enemycount * 100;
        if (score >= 95)
            GameObject.Find("UIManager").SendMessage("setThirdStar");
        if (score >= 85)
            GameObject.Find("UIManager").SendMessage("setSecondStar");
    }
}
