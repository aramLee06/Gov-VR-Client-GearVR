using UnityEngine;
using System.Collections;

public class EnemyDrone : MonoBehaviour
{

    private Transform targetTr;
    public GameObject shot; //미사일
    public GameObject SpawnPoint; //미사일발사위치
    public Transform[] wayPointList;
    public int myRouteNum;
    public int currentWayPoint; //현재 위치
    Transform targetWayPoint; //다음 위치
    private WayPoints wp;

    public GameObject expEffect;
    // 상태 정보: 탐색, 접촉, 공격, 죽음
    public enum State { idle = 0, contact, attack, die };
    // 추적 사정거리.
    public float traceDist = 10.0f;
    State state;
    // 공격 사정거리.
    public float attackDist = 10.0f;
    // 죽음 여부.
    private bool isDie = false;

    void Start()
    {
        targetTr = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        state = State.idle;
        //GetWaypoints();
        targetWayPoint = wayPointList[currentWayPoint];
        StartCoroutine(this.CheckState());
        StartCoroutine(this.Action());
    }

    void GetWaypoints()
    {
        Debug.Log(myRouteNum);
        if (myRouteNum != 0)
        {
            if (myRouteNum == 1)
                wayPointList = GameObject.Find("EnemyRouteA").GetComponentsInChildren<Transform>();
            if (myRouteNum == 2)
                wayPointList = GameObject.Find("EnemyRouteA (2)").GetComponentsInChildren<Transform>();
            if (myRouteNum == 3)
                wayPointList = GameObject.Find("EnemyRouteA (3)").GetComponentsInChildren<Transform>();
            targetWayPoint = wayPointList[2];
        }
    }

    void Aim()
    {
        transform.LookAt(targetTr);
    }


    void Attack()
    {
        //Instantiate(shot, SpawnPoint.transform.position, SpawnPoint.transform.rotation);
    }

    void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.tag == "MISSILE")
        {
            Debug.Log("아야");
            Die();
        }
        if (coll.gameObject.tag == "Player")
        {
            Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Enemy"), LayerMask.NameToLayer("Player"), true);
        }
    }

    IEnumerator CheckState()
    {
        while (!isDie)
        {
            yield return new WaitForSeconds(0.2f);
            float dist = Vector3.Distance(targetTr.position, transform.position);
            if (dist <= attackDist) // 공격거리 범위 이내로 들어왔는지 확인.
                state = State.contact;
            else if (state == State.contact && dist > attackDist)
                state = State.attack;
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

                case State.contact:
                    walk();
                    Aim();
                    break;

                case State.attack: // 공격 상태.
                    walk();
                    Aim();
                    Attack();
                    state = State.idle;
                    break;
            }
            yield return null;
        }
    }

    void Die()
    {
        Instantiate(expEffect, transform.position, Quaternion.identity);
        StopAllCoroutines();
        isDie = true;
        state = State.die;
        Destroy(gameObject);
    }

    void walk()
    {
        //Debug.Log(targetWayPoint.position);
        //Vector3 dir = targetWayPoint.position - transform.position;
        Vector3 dirXZ = new Vector3(targetWayPoint.position.x, 3f, targetWayPoint.position.z);
        transform.forward = Vector3.RotateTowards(transform.forward, dirXZ - transform.position, 3f * Time.deltaTime, 0.0f);
        transform.position = Vector3.MoveTowards(transform.position, dirXZ, 3f * Time.deltaTime);
        //Debug.Log(dir);
        if (targetWayPoint.position.x - 0.3f <= transform.position.x && transform.position.x <= targetWayPoint.position.x + 0.3f && targetWayPoint.position.z - 0.3f <= transform.position.z && transform.position.z + 0.3f <= targetWayPoint.position.z + 0.3f)
        {
            currentWayPoint++;
            if (currentWayPoint >= wayPointList.Length)
            { 
                Destroy(this.gameObject);
                return;
            }
            targetWayPoint = wayPointList[currentWayPoint];
        }
    }
}
