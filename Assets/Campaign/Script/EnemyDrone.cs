using UnityEngine;
using System.Collections;

public class EnemyDrone : MonoBehaviour
{

    private Transform targetTr;
    public GameObject shot; //미사일
    public Transform spotcontainer;
    public Transform[] spotpoint;
    public Transform[] wayPointList;
    public int myRouteNum;
    public int currentWayPoint; //현재 위치
    Transform targetWayPoint; //다음 위치
    private WayPoints wp;

    public GameObject expEffect;
    // 상태 정보: 탐색, 접촉, 공격, 죽음
    public enum State { idle = 0, contact, attack, die };
    State state;
    // 공격 사정거리.
    float contactDist = 10.0f;
    float attackDist = 11.0f;
    // 죽음 여부.
    private bool isDie = false;

    void Start()
    {
        if (spotcontainer != null)
            spotpoint = spotcontainer.GetComponentsInChildren<Transform>();
        targetTr = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        state = State.idle;
        targetWayPoint = wayPointList[currentWayPoint];
        StartCoroutine(this.CheckState());
        StartCoroutine(this.Action());
    }

    void Aim()
    {
        transform.LookAt(targetTr);
    }


    void Attack()
    {
        for (int bbm = 1; bbm < spotpoint.Length; bbm++)
        {
            spotpoint[bbm].LookAt(targetTr);
            Instantiate(shot, spotpoint[bbm].transform.position, spotpoint[bbm].transform.rotation);
        }
    }

    void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.tag == "Missle")
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
            if (dist <= contactDist)
                state = State.contact;
            else if (state == State.contact && dist >= attackDist)
                state = State.attack;
            //Debug.Log(state);
            //Debug.Log(dist);
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
