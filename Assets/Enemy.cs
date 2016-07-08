using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

    private Transform targetTr;
    private Transform goalTr;
    private Transform thisTr;
    private NavMeshAgent nma;
    public GameObject turret;

    // 상태 정보: 유휴, 추적, 공격, 죽음
    public enum State { idle=0, trace, attack, die };
    // 추적 사정거리.
    public float traceDist = 10.0f;
    State state;
    // 공격 사정거리.
    public float attackDist = 10.0f;
    // 죽음 여부.
    private bool isDie = false;

    void Start () {
        //right = Random.Range (0, 2) == 1;
        goalTr = GameObject.Find("RouteC_Goal").GetComponent<Transform>();
        targetTr = GameObject.Find("blue_tank_01_Test").GetComponent<Transform>();
        // 타겟을 추적할 게임오브젝트
        thisTr = GetComponent<Transform>();
        // 지정된 오브젝트에 NavMeshAgent 를 nma 변수에 할당
        nma = GetComponent<NavMeshAgent>();


        // 일정한 간격으로 상태를 체크
        // StartCoroutine(this.CheckState());

        // 상태에 따라 동작하는 루틴을 실행하는 코루틴 함수 실행.
        // StartCoroutine(this.Action());
    }
    void Attack()
    {
       // Vector3 difference = targetTr.position - transform.position;
        //float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        //turret.transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ);
    }

    IEnumerator CheckState()
    {
        while (!isDie)
        {
            yield return new WaitForSeconds(0.2f);
            // 플레이어 사이의 거리 측정.
            float dist = Vector3.Distance(targetTr.position, thisTr.position);

            if (dist <= attackDist) // 공격거리 범위 이내로 들어왔는지 확인.
                state = State.attack;
            //else if (dist <= traceDist) // 상태를 추적으로 설정.
                //state = State.trace;
            else // 상태를 휴면 모드로 설정.
                state = State.idle;
        }
    }

    void moving()
    {

        //transform.forward = Vector3.RotateTowards(transform.forward, goalTr.position - transform.position, 0.5f * Time.deltaTime, 0.0f);

        //transform.position = Vector3.MoveTowards(transform.position, goalTr.position, 0.5f * Time.deltaTime);
    }

    IEnumerator Action()
    {
        while (!isDie)
        {
            switch (state)
            {
                case State.idle: // 휴면 상태.
                    //nma.Stop();
                    moving();
                    break;

                case State.trace: // 추적 상태.
                                  // 추적 대상의 위치를 넘겨줌.
                    //nma.destination = targetTr.position;
                    // 추적을 재시작.
                    //nma.Resume();
                    break;

                case State.attack: // 공격 상태.
                                   //nma.Stop();

                    Attack();
                    break;
            }
            yield return null;
        }
    }

    void Update () {

        //nma.destination = targetTr.position;
        Vector3 dir = targetTr.transform.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(new Vector3(dir.x, dir.y, dir.z));
        Debug.Log(rotation);
        turret.transform.rotation = rotation;
    }
}
