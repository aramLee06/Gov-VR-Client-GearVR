using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Mountain_Drone_Move : MonoBehaviour
{
    //----------------------------------
    public GameObject turret;
    public GameObject shot; //미사일
    public Transform shotSpawn; //미사일 발사 위치
    public float fireRate = 0.5f; //미사일 발사 딜레이
    private float nextFire;
    public float power = 2000;
    public GameObject gun;
    public float tilt;
    //--------------------------------------

    public Transform[] wayPointList; //루트 좌표
    public Transform waypointContainer;
    public int currentWayPoint; //현재 위치
    public Transform targetWayPoint; //다음 위치
    public Transform spotcontainer;
    public Transform[] spotpoint;

    public float speed = 0.5f; //이동 속도

    private Rigidbody rb;
    void Start()
    {
        if (spotcontainer != null)
            spotpoint = spotcontainer.GetComponentsInChildren<Transform>();
        currentWayPoint = 1;
        //rb = GetComponent<Rigidbody>();
        if (waypointContainer != null)
        {
           // wayPointList = waypointContainer.GetComponentsInChildren<Transform>();
           // this.transform.position=wayPointList[1].position;
           // this.transform.rotation = wayPointList[1].rotation;
           // targetWayPoint = wayPointList[2];
        }
        //targetWayPoint = wayPointList[currentWayPoint];
        //Debug.Log(targetWayPoint);
    }

    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            for (int bbm = 1; bbm < spotpoint.Length; bbm++)
            {
                Instantiate(shot, spotpoint[bbm].transform.position, spotpoint[bbm].transform.rotation);
            }
        }
        if (targetWayPoint == null)
            return;
        transform.forward = Vector3.RotateTowards(transform.forward, targetWayPoint.position - transform.position, 1 * Time.deltaTime, 0.0f);
        transform.position = Vector3.MoveTowards(transform.position, targetWayPoint.position, speed * Time.deltaTime);
        //transform.rotation = Quaternion.Euler(transform.rotation.z, transform.rotation.y, transform.rotation.z * -tilt);

        if (targetWayPoint.position.x - 0.3f <= transform.position.x && transform.position.x <= targetWayPoint.position.x + 0.3f && targetWayPoint.position.z - 0.3f <= transform.position.z && transform.position.z + 0.3f <= targetWayPoint.position.z + 0.3f)
        {
            Debug.Log("대상의위치" + targetWayPoint.position);
            //if (currentWayPoint < this.wayPointList.Length)
            //{
            //   if (targetWayPoint == null)
            currentWayPoint++;
            targetWayPoint = wayPointList[currentWayPoint];
        }


        /* turret.transform.Rotate(new Vector3(0.0f, Input.GetAxis("Horizontal") * Mathf.Rad2Deg, 0.0f) * Time.deltaTime);
         gun.transform.Rotate(new Vector3(Input.GetAxis("Vertical") * Mathf.Rad2Deg, 0.0f, 0.0f) * Time.deltaTime);
         if (Input.GetButton("Fire1") && Time.time > nextFire)
         {
             nextFire = Time.time + fireRate;
             Instantiate(shot, shotSpawn.transform.position, shotSpawn.transform.rotation);
         }*/
    }

    void walk()
    {

    }
}
