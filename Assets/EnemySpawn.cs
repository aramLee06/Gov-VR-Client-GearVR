using UnityEngine;
using System.Collections;

public class EnemySpawn : MonoBehaviour {

	private static EnemySpawn instance;

	public GameObject enemy;
    private WayPoints wp;
    public Transform[] wayPointList;
    private int currentSpawnPoint;
    public GameObject targetObj;
    public Transform waypointContainer;

    void Awake() {
		instance = this;
	}

	void Start () {
        //InvokeRepeating ("CreateEnemy", 1f, 3f * (Random.Range (7, 10) / 10f));
        GetWaypoints();
        //wayPointList = GameObject.Find("SpawnPointA").GetComponentsInChildren<Transform>();
    }

    void GetWaypoints()
    {

        wayPointList = wp.wayPointList;
           
    }

    void Update () {
        CreateEnemy();
        GetWaypoints();
    }

	void CreateEnemy() {
        wp = targetObj.GetComponent<WayPoints>();
        if (wp.currentWayPoint != currentSpawnPoint)
        {
            //Debug.Log("타겟의웨이포인트"+wp.currentWayPoint);
            //Debug.Log("현재스폰된포인트"+currentSpawnPoint);
            //A = GameObject.Find("A_RouteEnemySpawnPoint 1").transform;
            //Debug.Log(A.transform.position);
            //this.transform.position = A.transform.position;
            currentSpawnPoint = wp.currentWayPoint;
            //Debug.Log(wayPointList[wp.currentWayPoint + 2].transform.position);
            Instantiate(enemy, wayPointList[wp.currentWayPoint+2].transform.position, Quaternion.identity);
        }
	}

	public static void Cancel() {
		instance.CancelInvoke();
	}
}

