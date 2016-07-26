using UnityEngine;
using System.Collections;

public class BigBadBoss : MonoBehaviour
{
    float missileduration = 10.0f; //미사일 소환 주기
    public Transform spotcontainer;
    public Transform[] spotpoint;
    public GameObject bbMissile;
    private bool isDead;
    private Transform targetTr;
    private int hp;
    public GameObject expEffect;

    // Use this for initialization
    void Start()
    {
        Debug.Log("시작");
        if (spotcontainer != null)
            spotpoint = spotcontainer.GetComponentsInChildren<Transform>();
        isDead = false;
        targetTr = GameObject.FindWithTag("Player").GetComponent<Transform>();
        hp = 20;
        //StartCoroutine(MakeMissile());
    }

    public void battlemod()
    {
        //Debug.Log("보스전");
        StartCoroutine(MakeMissile());
    }

    void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.tag == "Missile")
        {
            Debug.Log("보스 맞춤");
            hp = hp - 1;
            if (hp <= 0)
            {
                Instantiate(expEffect, transform.position, Quaternion.identity);
                Destroy(this.gameObject);
                GameObject.Find("UIManager").SendMessage("setComplete");
                GameObject.Find("UIManager").SendMessage("UISHOW");
            }
        }
    }

    IEnumerator MakeMissile()
    {
        while (!isDead)
        {
            yield return new WaitForSeconds(missileduration);
            for (int bbm = 1; bbm < spotpoint.Length; bbm++)
            {
                GameObject spawnMissile = Instantiate(bbMissile, spotcontainer.transform.position, spotcontainer.transform.rotation) as GameObject;
                spawnMissile.GetComponent<BossMissileCtrl>().mypoint = spotpoint[bbm].transform;
            }
            Debug.Log("생성");
        }
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.LookAt(targetTr);
    }
}
