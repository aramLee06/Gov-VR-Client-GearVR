using UnityEngine;
using System.Collections;

public class LaserCtrl : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        Destroy(gameObject, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter(Collision coll)
    {
        Debug.Log("아야");
    }
}
