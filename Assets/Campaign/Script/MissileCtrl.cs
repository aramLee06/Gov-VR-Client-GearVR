using UnityEngine;
using System.Collections;

public class MissileCtrl : MonoBehaviour
{
    public GameObject expEffect;
    public float speed;

    // Use this for initialization
    void Start () {
        GetComponent<Rigidbody>().AddForce(transform.forward * speed);
    }


    void OnCollisionEnter(Collision coll)
    {
        Instantiate(expEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
