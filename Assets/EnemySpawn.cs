using UnityEngine;
using System.Collections;

public class EnemySpawn : MonoBehaviour
{

    private static EnemySpawn instance;

    public GameObject enemy;
    public GameObject enemydrone;
    private WayPoints wp;
    public Transform[] wayPointList;
    private int currentSpawnPoint;
    public GameObject targetObj;
    public Transform waypointContainer;
    public Transform EnemywayPointsList1;
    public Transform EnemywayPointsList2;
    public Transform EnemywayPointsList3;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        //InvokeRepeating ("CreateEnemy", 1f, 3f * (Random.Range (7, 10) / 10f));
        GetWaypoints();
        //wayPointList = GameObject.Find("SpawnPointA").GetComponentsInChildren<Transform>();
    }

    void GetWaypoints()
    {

        wayPointList = wp.wayPointList;

    }

    void Update()
    {
        wp = targetObj.GetComponent<WayPoints>();
        CreateEnemy();
        GetWaypoints();
    }

    void CreateEnemy()
    {
        if (wp.currentWayPoint > 1)
        {
            if (wp.currentWayPoint != currentSpawnPoint)
            {
                //Debug.Log("타겟의웨이포인트"+wp.currentWayPoint);
                //Debug.Log("현재스폰된포인트"+currentSpawnPoint);
                //A = GameObject.Find("A_RouteEnemySpawnPoint 1").transform;
                //Debug.Log(A.transform.position);
                //this.transform.position = A.transform.position;
                currentSpawnPoint = wp.currentWayPoint;
                GameObject temp1 = Instantiate(enemy, EnemywayPointsList1.transform.position, EnemywayPointsList1.transform.rotation) as GameObject;
                temp1.GetComponent<Enemy>().myRouteNum = 1;
                GameObject tempDrone1 = Instantiate(enemydrone, EnemywayPointsList1.transform.position, EnemywayPointsList1.transform.rotation) as GameObject;
                tempDrone1.GetComponent<EnemyDrone>().myRouteNum = 1;
                GameObject temp2 = Instantiate(enemy, EnemywayPointsList2.transform.position, EnemywayPointsList2.transform.rotation) as GameObject;
                temp2.GetComponent<Enemy>().myRouteNum = 2;
                GameObject temp3 = Instantiate(enemy, EnemywayPointsList3.transform.position, EnemywayPointsList3.transform.rotation) as GameObject;
                temp3.GetComponent<Enemy>().myRouteNum = 3;
                //Debug.Log(wayPointList[wp.currentWayPoint + 2].transform.position);
                //Instantiate(enemy, wayPointList[wp.currentWayPoint+2].transform.position, Quaternion.identity);

            }
        }
    }

    public static void Cancel()
    {
        instance.CancelInvoke();
    }
}

