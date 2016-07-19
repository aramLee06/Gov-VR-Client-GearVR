using UnityEngine;
using System.Collections;

public class BossMissileCtrl : MonoBehaviour
{

    public GameObject expEffect;
    public float speed = 1000.0f;
    public float fireRate = 2.5f; //미사일 발사 딜레이
    private float nextFire;
    private Transform targetTr;

    // Use this for initialization
    void Start()
    {
        targetTr = GameObject.FindWithTag("Player").GetComponent<Transform>();
        this.transform.LookAt(targetTr);
        StartCoroutine(FireMissile());
    }

    IEnumerator FireMissile()
    {

        yield return new WaitForSeconds(fireRate); //0.2초간 실행을 보류한다.

        GetComponent<Rigidbody>().AddForce(transform.forward * speed);
    }


    void Update()
    {
        if (Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
        }
    }


    void OnCollisionEnter(Collision coll)
    {
        Instantiate(expEffect, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }
}
