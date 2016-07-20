using UnityEngine;
using System.Collections;

public class EnemyTank : MonoBehaviour
{

    private Transform targetTr;
    public GameObject shot; //미사일
    public GameObject SpawnPoint; //미사일발사위치
    public GameObject turret;
    public Transform[] wayPointList;
    public Transform targetWayPoint; //다음 위치
    public int myRouteNum;
    public int currentWayPoint; //현재 위치
    private WayPoints wp;
    private float speed = 1f; //이동 속도

    // 상태 정보: 탐색, 접촉, 공격, 죽음
    public enum State { idle = 0, contact, attack, die };
    State state;
    // 공격 사정거리.
    public float contactDist = 2.0f;
    public float attackDist = 3.0f;
    // 죽음 여부.
    private bool isDie = false;

    void Start()
    {
        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Enemy"), LayerMask.NameToLayer("Enemy"), true);
        targetTr = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        state = State.idle;
        //GetWaypoints();
        targetWayPoint = wayPointList[currentWayPoint];
        StartCoroutine(this.CheckState());
        StartCoroutine(this.Action());
    }

    public void GetWaypoints()
    {
        Debug.Log(myRouteNum);
        //targetWayPoint = wayPointList[2];
       /* if (myRouteNum != 0)
        {
            if (myRouteNum == 1)
                wayPointList = GameObject.Find("EnemyRouteA").GetComponentsInChildren<Transform>();
            if (myRouteNum == 2)
                wayPointList = GameObject.Find("EnemyRouteA (2)").GetComponentsInChildren<Transform>();
            if (myRouteNum == 3)
                wayPointList = GameObject.Find("EnemyRouteA (3)").GetComponentsInChildren<Transform>();
        }*/
    }

    void Aim()
    {
        turret.transform.LookAt(targetTr);
    }


    void Attack()
    {
        //Debug.Log("발사");
        Instantiate(shot, SpawnPoint.transform.position, SpawnPoint.transform.rotation);
        state = State.idle;
        StopCoroutine(CheckState());
    }

    void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.tag == "MISSILE")
        {
            Die();
        }
    }

    IEnumerator CheckState()
    {
        while (!isDie)
        {
            yield return new WaitForSeconds(0.2f);
            Debug.Log(state);
            float dist = Vector3.Distance(targetTr.position, transform.position);
            if (dist <= contactDist) 
                state = State.contact;
            else if (state == State.contact && dist >= attackDist) 
                state = State.attack;
            Debug.Log(dist);
        }
    }

    IEnumerator Action()
    {
        while (!isDie)
        {
            switch (state)
            {
                case State.idle: // 휴면 상태.
                    walk();
                    break;

                case State.contact: //만남
                    walk();
                    Aim();
                    break;

                case State.attack: // 공격 상태.
                    walk();
                    Aim();
                    Attack();
                    break;
            }
            yield return null;
        }
    }

    void Die()
    {
        StopAllCoroutines();
        isDie = true;
        state = State.die;
        Destroy(gameObject);
    }

    void walk()
    {
        //Debug.Log(targetWayPoint.position);
        //Vector3 dir = targetWayPoint.position - transform.position;
        Vector3 dirXZ = new Vector3(targetWayPoint.position.x, 0f, targetWayPoint.position.z);
        transform.forward = Vector3.RotateTowards(transform.forward, dirXZ - transform.position, speed * Time.deltaTime, 0.0f);
        transform.position = Vector3.MoveTowards(transform.position, dirXZ, speed * Time.deltaTime);
        //Debug.Log(dir);
        if (targetWayPoint.position.x - 0.3f <= transform.position.x && transform.position.x <= targetWayPoint.position.x + 0.3f && targetWayPoint.position.z - 0.3f <= transform.position.z && transform.position.z + 0.3f <= targetWayPoint.position.z + 0.3f)
        {
            currentWayPoint++;
            targetWayPoint = wayPointList[currentWayPoint];
        }
    }
}
