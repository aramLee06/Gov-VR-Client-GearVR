using UnityEngine;
using System.Collections;

public class EnemySpawn : MonoBehaviour {

	private static EnemySpawn instance;

	public GameObject enemy;
    private WayPoints wp;
    Transform[] A;
    private int currentcheck;

    void Awake() {
		instance = this;
	}

	void Start () {
        //InvokeRepeating ("CreateEnemy", 1f, 3f * (Random.Range (7, 10) / 10f));

        A = GameObject.Find("SpawnPointA").GetComponentsInChildren<Transform>();
        Debug.Log(A[0].transform.position);
        Debug.Log(A[1].transform.position);
        Debug.Log(A[2].transform.position);

    }

	void Update () {
        CreateEnemy();

    }

	void CreateEnemy() {
        wp = GameObject.Find("blue_tank_01_Test").GetComponent<WayPoints>();
        if (wp.currentWayPoint == 3 && currentcheck != 3)
        {
            //A = GameObject.Find("A_RouteEnemySpawnPoint 1").transform;

            //Debug.Log(A.transform.position);
            //this.transform.position = A.transform.position;
            currentcheck = 3;
            Instantiate(enemy, A[1].transform.position, Quaternion.identity);
        }
	}

	public static void Cancel() {
		instance.CancelInvoke();
	}
}

