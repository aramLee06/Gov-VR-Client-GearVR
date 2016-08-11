using UnityEngine;
using System.Collections;

public class EnemySpawn : MonoBehaviour
{

    private static EnemySpawn instance;

    public GameObject enemy;
    public GameObject enemydrone;
    public GameObject boss;
    private WayPoints wp;
    Transform[] wayPointList;
    private int currentSpawnPoint;
    public GameObject targetObj;
    public Transform waypointContainer;
    public Transform bossspot;
    public Transform EnemywayPointsList1;
    public Transform EnemywayPointsList2;
    public Transform EnemywayPointsList3;

    public bool bossbattle = false;
    float spawntime = 30f;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        wayPointList = new Transform[4];
           //InvokeRepeating ("CreateEnemy", 1f, 3f * (Random.Range (7, 10) / 10f));
           // GetWaypoints();
        wayPointList[1] = EnemywayPointsList1;
        wayPointList[2] = EnemywayPointsList2;
        wayPointList[3] = EnemywayPointsList3;
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
        Instantiate(boss, bossspot.transform.position, bossspot.transform.rotation);

        while (!bossbattle)
        {

            yield return new WaitForSeconds(spawntime);
        
            //if (wp.currentWayPoint > 1)
            // {
            //  if (wp.currentWayPoint != currentSpawnPoint)
            // {
            //Debug.Log("타겟의웨이포인트"+wp.currentWayPoint);
            //Debug.Log("현재스폰된포인트"+currentSpawnPoint);
            //A = GameObject.Find("A_RouteEnemySpawnPoint 1").transform;
            //this.transform.position = A.transform.position;
            //currentSpawnPoint = wp.currentWayPoint;
           // GameObject temp1 = Instantiate(enemy, EnemywayPointsList1.transform.position, EnemywayPointsList1.transform.rotation) as GameObject;
           // temp1.GetComponent<EnemyTank>().wayPointList = EnemywayPointsList1.GetComponentsInChildren<Transform>();
           // GameObject.Find("GameManager").SendMessage("countingenemy");

           // GameObject tempDrone1 = Instantiate(enemydrone, EnemywayPointsList1.transform.position, EnemywayPointsList1.transform.rotation) as GameObject;
           // tempDrone1.GetComponent<EnemyDrone>().wayPointList = EnemywayPointsList1.GetComponentsInChildren<Transform>();
           
         //   GameObject temp2 = Instantiate(enemy, EnemywayPointsList2.transform.position, EnemywayPointsList2.transform.rotation) as GameObject;
         //   temp2.GetComponent<EnemyTank>().wayPointList = EnemywayPointsList2.GetComponentsInChildren<Transform>();
         //   GameObject.Find("GameManager").SendMessage("countingenemy");

          //  GameObject temp3 = Instantiate(enemy, EnemywayPointsList3.transform.position, EnemywayPointsList3.transform.rotation) as GameObject;
         //   temp3.GetComponent<EnemyTank>().wayPointList = EnemywayPointsList3.GetComponentsInChildren<Transform>();
         //   GameObject.Find("GameManager").SendMessage("countingenemy");
            //Debug.Log(wayPointList[wp.currentWayPoint + 2].transform.position);
            //Instantiate(enemy, wayPointList[wp.currentWayPoint+2].transform.position, Quaternion.identity);
            for(int i=1; i<=3; i++)
            {
                spawntank(wayPointList[i]);
            }

            for (int i = 1; i <= 3; i++)
            {
                spawndrone(wayPointList[i]);
            }
        }
        //}
        //}
    }
    void spawntank(Transform p)
    {
        GameObject spawnTank = Instantiate(enemy, p.transform.position, p.transform.rotation) as GameObject;
        spawnTank.GetComponent<EnemyTank>().wayPointList = p.GetComponentsInChildren<Transform>();
        Vector3 dir = spawnTank.GetComponent<EnemyTank>().wayPointList[1].transform.position - p.transform.position;
        Quaternion rotation = Quaternion.LookRotation(new Vector3(dir.x, dir.y, dir.z));
        spawnTank.transform.rotation = rotation;
    }

    void spawndrone(Transform p)
    {
        GameObject spawndrone = Instantiate(enemydrone, p.transform.position, p.transform.rotation) as GameObject;
        spawndrone.GetComponent<EnemyDrone>().wayPointList = p.GetComponentsInChildren<Transform>();
        Vector3 dir = spawndrone.GetComponent<EnemyDrone>().wayPointList[1].transform.position - p.transform.position;
        Quaternion rotation = Quaternion.LookRotation(new Vector3(dir.x, dir.y, dir.z));
        spawndrone.transform.rotation = rotation;
    }
}

