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
        // GetWaypoints();
        StartCoroutine(CreateEnemy());
        //wayPointList = GameObject.Find("SpawnPointA").GetComponentsInChildren<Transform>();
    }

    void GetWaypoints()
    {

        wayPointList = wp.wayPointList;

    }

    void Update()
    {
        // wp = targetObj.GetComponent<WayPoints>();
        // GetWaypoints();
    }
    IEnumerator CreateEnemy()
    {
        
        yield return new WaitForSeconds(5f);

        //if (wp.currentWayPoint > 1)
        // {
        //  if (wp.currentWayPoint != currentSpawnPoint)
        // {
        //Debug.Log("타겟의웨이포인트"+wp.currentWayPoint);
        //Debug.Log("현재스폰된포인트"+currentSpawnPoint);
        //A = GameObject.Find("A_RouteEnemySpawnPoint 1").transform;
        Debug.Log("테스트");
        //this.transform.position = A.transform.position;
        //currentSpawnPoint = wp.currentWayPoint;
        GameObject temp1 = Instantiate(enemy, EnemywayPointsList1.transform.position, EnemywayPointsList1.transform.rotation) as GameObject;
        temp1.GetComponent<EnemyTank>().wayPointList = EnemywayPointsList1.GetComponentsInChildren<Transform>();

        GameObject tempDrone1 = Instantiate(enemydrone, EnemywayPointsList1.transform.position, EnemywayPointsList1.transform.rotation) as GameObject;
        tempDrone1.GetComponent<EnemyDrone>().wayPointList = EnemywayPointsList1.GetComponentsInChildren<Transform>();

        GameObject temp2 = Instantiate(enemy, EnemywayPointsList2.transform.position, EnemywayPointsList2.transform.rotation) as GameObject;
        temp2.GetComponent<EnemyTank>().wayPointList = EnemywayPointsList2.GetComponentsInChildren<Transform>();

        GameObject temp3 = Instantiate(enemy, EnemywayPointsList3.transform.position, EnemywayPointsList3.transform.rotation) as GameObject;
        temp3.GetComponent<EnemyTank>().wayPointList = EnemywayPointsList3.GetComponentsInChildren<Transform>();
        //Debug.Log(wayPointList[wp.currentWayPoint + 2].transform.position);
        //Instantiate(enemy, wayPointList[wp.currentWayPoint+2].transform.position, Quaternion.identity);

        //}
        //}
    }
}

