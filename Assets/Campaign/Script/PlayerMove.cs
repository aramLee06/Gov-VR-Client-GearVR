using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerMove : MonoBehaviour
{
    //----------------------------------
    public GameObject turret;
    public GameObject shot; //미사일
    public Transform shotSpawn; //미사일 발사 위치
    public float fireRate = 0.5f; //미사일 발사 딜레이
    private float nextFire;
    public float power = 2000;
    public GameObject gun;
    //--------------------------------------

    public Transform[] wayPointList; //루트 좌표
    public Transform waypointContainer;
    public int currentWayPoint; //현재 위치
    public Transform targetWayPoint; //다음 위치

    private float speed = 0.5f; //이동 속도

    void Start()

    { 
            Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Enemy"), LayerMask.NameToLayer("Player"), true);
        //targetWayPoint = wayPointList[currentWayPoint];
        //Debug.Log(targetWayPoint);
    }

    void Update()
    {
        //Debug.Log(targetWayPoint.position);
        //this.transform.Translate (new Vector3 (Input.GetAxis("Horizontal"),0,Input.GetAxis("Vertical")) * Time.deltaTime);

        transform.forward = Vector3.RotateTowards(transform.forward, targetWayPoint.position - transform.position, speed * Time.deltaTime, 0.0f);

        transform.position = Vector3.MoveTowards(transform.position, targetWayPoint.position, speed * Time.deltaTime);

        //Debug.Log("대상의위치"+targetWayPoint.position);
        // Debug.Log("현재나의위치" + transform.position);
        if (targetWayPoint.position.x - 0.3f <= transform.position.x && transform.position.x <= targetWayPoint.position.x + 0.3f && targetWayPoint.position.z - 0.3f <= transform.position.z && transform.position.z + 0.3f <= targetWayPoint.position.z + 0.3f)
        {
            //Debug.Log("대상의위치"+targetWayPoint.position);
            //if (currentWayPoint < this.wayPointList.Length)
            //{
            //   if (targetWayPoint == null)
            currentWayPoint++;
            targetWayPoint = wayPointList[currentWayPoint];
        }

        turret.transform.Rotate(new Vector3(0.0f, Input.GetAxis("Horizontal") * Mathf.Rad2Deg, 0.0f) * Time.deltaTime);
        gun.transform.Rotate(new Vector3(Input.GetAxis("Vertical") * Mathf.Rad2Deg, 0.0f, 0.0f) * Time.deltaTime);
        if (Input.GetButton("Fire1") && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Instantiate(shot, shotSpawn.transform.position, shotSpawn.transform.rotation);
        }
    }

    void walk()
    {

    }
}
