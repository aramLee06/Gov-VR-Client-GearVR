using UnityEngine;
using System.Collections;

public class MissileCtrl : MonoBehaviour {
    public float speed;

    // Use this for initialization
    void Start () {
        GetComponent<Rigidbody>().AddForce(transform.forward * speed);
    }


    void OnCollisionEnter(Collision coll)
    {
        Destroy(gameObject);
    }
}
