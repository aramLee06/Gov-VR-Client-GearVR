using UnityEngine;
using System.Collections;

public class BigBadBoss : MonoBehaviour
{
    public float missileduration = 10.0f; //미사일 소환 주기
    private float nextFire;
    public Transform spotcontainer;
    public Transform[] spotpoint;
    public GameObject bbMissile;
    private bool isDead;
    private Transform targetTr;

    // Use this for initialization
    void Start()
    {
        Debug.Log("시작");
        if (spotcontainer != null)
            spotpoint = spotcontainer.GetComponentsInChildren<Transform>();
        isDead = false;
        targetTr = GameObject.FindWithTag("Player").GetComponent<Transform>();
    }

    public void battlemod()
    {
        StartCoroutine(MakeMissile());
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
