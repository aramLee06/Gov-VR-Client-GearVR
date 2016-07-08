using UnityEngine;
using System.Collections;

public class BulletCtrl : MonoBehaviour {

    float speed = 30f;
    // Use this for initialization
    void Start()
    {
        Destroy(this.gameObject, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
}
