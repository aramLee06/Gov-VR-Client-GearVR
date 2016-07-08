using UnityEngine;
using System.Collections;

public class WayPoints : MonoBehaviour
{

    public Transform[] wayPointList;
    public Transform waypointContainer;
    public GameObject tanks;
	public int currentRoute;


    //public Transform[] wayPointList = new Transform[5];

    public int currentWayPoint = 0;
    Transform targetWayPoint;

    private float speed = 0.5f;

    void OnGUI()
    {
        if (GUI.Button(new Rect(20, 40, 80, 20), "루트 A"))
        {
            waypointContainer = GameObject.Find("RouteA (1)").GetComponent<Transform>();
            GetWaypoints();
            SpawnTank();
        }
        if (GUI.Button(new Rect(20, 70, 80, 20), "루트 B"))
        {
            waypointContainer = GameObject.Find("RouteB (1)").GetComponent<Transform>();
            GetWaypoints();
            SpawnTank();
        }
        if (GUI.Button(new Rect(20, 100, 80, 20), "루트 C"))
        {
            waypointContainer = GameObject.Find("RouteC (1)").GetComponent<Transform>();
            GetWaypoints();
            SpawnTank();
        }
    }
    void SpawnTank()
    {
        currentWayPoint = 1;
        Vector3 v = new Vector3();
        v.x = wayPointList[currentWayPoint].position.x;
        v.z = wayPointList[currentWayPoint].position.z;

        targetWayPoint = wayPointList[currentWayPoint];
        GameObject tank = GameObject.Find("blue_tank_01_Test");
        tank.transform.position = wayPointList[1].position;
        tank.transform.rotation = wayPointList[currentWayPoint].rotation;
        //GameObject tank = (GameObject)Instantiate(tanks, wayPointList[1].position, waypointContainer.rotation);
    }

    void Start()
    {
        //GetWaypoints();
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
            if (currentWayPoint==0)
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

		if (targetWayPoint.position.x - 0.3f <= transform.position.x && transform.position.x <= targetWayPoint.position.x + 0.3f || targetWayPoint.position.z - 0.3f <= transform.position.z&& transform.position.z+ 0.3f <= targetWayPoint.position.z + 0.3f)
			//(targetWayPoint.position == transform.position)
		{
			currentWayPoint++;
			targetWayPoint = wayPointList[currentWayPoint];
		}
    }
}