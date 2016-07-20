using UnityEngine;
using System.Collections;

public class BossMissileCtrl : MonoBehaviour
{

    public GameObject expEffect;
    public float speed = 1000.0f;
    private Transform targetTr;
    private float myTimer = 6.0f;
    public Transform mypoint;


    void Awake()
    {
        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Boss_Missile"), LayerMask.NameToLayer("Boss_Missile"), true);
    }
    void Start()
    {
        targetTr = GameObject.FindWithTag("Player").GetComponent<Transform>();
        //자기 위치로 가야해 어떻게?
        Debug.Log(mypoint.position);
        StartCoroutine(FireMissile());
    }

    IEnumerator FireMissile()
    {
        yield return new WaitForSeconds(myTimer);

        Debug.Log("발사");
        GetComponent<Rigidbody>().AddForce(transform.forward * speed);
    }


    void Update()
    {
        this.transform.LookAt(targetTr);
        transform.Rotate(new Vector3(0, 0, 1) * 60 * Time.deltaTime);
        transform.position = Vector3.MoveTowards(transform.position, mypoint.position, 1f * Time.deltaTime);
    }


    void OnCollisionEnter(Collision coll)
    {
        Instantiate(expEffect, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }
}
