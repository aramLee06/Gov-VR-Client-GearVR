using UnityEngine;
using System.Collections;

public class BigBadBoss : MonoBehaviour
{
    public float missileduration = 1.0f; //미사일 소환 주기
    private float nextFire;
    public Transform spotcontainer;
    public Transform[] spotpoint;
    public GameObject bbMissile;
    private bool isDead;

    // Use this for initialization
    void Start()
    {
        if (spotcontainer != null)
            spotpoint = spotcontainer.GetComponentsInChildren<Transform>();
        StartCoroutine(MakeMissile());
        isDead = false;
}

    IEnumerator MakeMissile()
    {
        while (!isDead)
        {
            Debug.Log(spotpoint.Length);
            for (int bbm = 1; bbm < spotpoint.Length; bbm++)
            {
                Instantiate(bbMissile, spotpoint[bbm].transform.position, spotpoint[bbm].transform.rotation);
            }
            yield return new WaitForSeconds(missileduration); //0.2초간 실행을 보류한다.
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > nextFire)
        {
            nextFire = Time.time + missileduration;

        }
    }
}
