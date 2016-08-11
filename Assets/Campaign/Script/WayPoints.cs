using UnityEngine;
using System.Collections;

public class WayPoints : MonoBehaviour
{

    public Transform[] wayPointList;
    public Transform waypointContainer;
    public GameObject tank;
    public int currentRoute;

    //public Transform[] wayPointList = new Transform[5];

    public int currentWayPoint = 0;
    Transform targetWayPoint;

    private float speed = 5f;

    void SpawnTank()
    {
        currentWayPoint = 1;
        Vector3 v = new Vector3();
        v.x = wayPointList[currentWayPoint].position.x;
        v.z = wayPointList[currentWayPoint].position.z;

        targetWayPoint = wayPointList[currentWayPoint];
        //GameObject tank = GameObject.Find("blue_tank_01_Test");
        tank.transform.position = wayPointList[1].position;
        Vector3 dir = wayPointList[2].transform.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(new Vector3(dir.x, dir.y, dir.z));
        //Debug.Log(rotation);
        tank.transform.rotation = rotation;
        //GameObject tank = (GameObject)Instantiate(tanks, wayPointList[1].position, waypointContainer.rotation);
    }

    void Start()
    {
        //GetWaypoints();
        waypointContainer = GameObject.Find("EnemyRouteC_3").GetComponent<Transform>();
        GetWaypoints();
        SpawnTank();
    }

    void GetWaypoints()
    {
        if (waypointContainer != null)
        {
            wayPointList = waypointContainer.gameObject.GetComponentsInChildren<Transform>();
            //Debug.Log(wayPointList.Length);
        }
    }

    void Update()
    {
        if (currentWayPoint < this.wayPointList.Length)
        {
            //Debug.Log(currentWayPoint);
            if (currentWayPoint == 0)
            {
                currentWayPoint++;
            }
            if (targetWayPoint == null)
                targetWayPoint = wayPointList[currentWayPoint];


            walk();
        }
    }

    void walk()
    {
        //Debug.Log(currentWayPoint);
        //Debug.Log(targetWayPoint.position);

        transform.forward = Vector3.RotateTowards(transform.forward, targetWayPoint.position - transform.position, speed * Time.deltaTime, 0.0f);

        transform.position = Vector3.MoveTowards(transform.position, targetWayPoint.position, speed * Time.deltaTime);

        //if (targetWayPoint.position.x - 0.3f <= transform.position.x && transform.position.x <= targetWayPoint.position.x + 0.3f || targetWayPoint.position.z - 0.3f <= transform.position.z && transform.position.z + 0.3f <= targetWayPoint.position.z + 0.3f)
        //(targetWayPoint.position == transform.position)
        if (targetWayPoint.position.x - 0.3f <= transform.position.x && transform.position.x <= targetWayPoint.position.x + 0.3f && targetWayPoint.position.z - 0.3f <= transform.position.z && transform.position.z + 0.3f <= targetWayPoint.position.z + 0.3f)
        {
            Debug.Log("포인트 도착");
            currentWayPoint++;
            targetWayPoint = wayPointList[currentWayPoint];
        }
    }
}