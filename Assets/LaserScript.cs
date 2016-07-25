using UnityEngine;
using System.Collections;

public class LaserScript : MonoBehaviour
{
    public LineRenderer line;
    public Transform spotcontainer;
    public Transform[] spotpoint;


    void Start()
    {
        if (spotcontainer != null)
            spotpoint = spotcontainer.GetComponentsInChildren<Transform>();
        //line = gameObject.GetComponent<LineRenderer>();
        line.enabled = false;
    }
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            StopCoroutine("FireLaser");
            StartCoroutine("FireLaser");
        }
    }
    IEnumerator FireLaser()
    {
        line.enabled = true;

        while (Input.GetButton("Fire1"))
        {
            for (int bbm = 1; bbm < spotpoint.Length; bbm++)
            {
                Ray ray = new Ray(spotpoint[bbm].transform.position, spotpoint[bbm].transform.forward);
                RaycastHit hit;

                line.SetPosition(0, ray.origin);

                if (Physics.Raycast(ray, out hit, 100))
                {
                    line.SetPosition(1, hit.point);
                    if (hit.rigidbody)
                    {
                        hit.rigidbody.AddForceAtPosition(transform.forward * 10, hit.point);
                    }
                }
                else
                    line.SetPosition(1, ray.GetPoint(100));

                yield return null;
            }
        }

        line.enabled = false;
    }
}