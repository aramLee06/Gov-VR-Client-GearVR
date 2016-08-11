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
    public GameObject expEffect;
    public GameObject gun;
    GameObject aim;
    float turnspeed = 0.5f;
    //--------------------------------------

    public Transform[] wayPointList; //루트 좌표
    public Transform waypointContainer;
    public int currentWayPoint; //현재 위치
    public Transform targetWayPoint; //다음 위치

    private float speed = 0.5f; //이동 속도
    public bool bossbattle = false;
    public bool isdead = false;
    private int hp=11;

    void Start()

    {
        //Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemy"), true);
        //Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Missile"), LayerMask.NameToLayer("Player"), true);
        //Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Boss_Missile"), LayerMask.NameToLayer("Player"), true);
        //targetWayPoint = wayPointList[currentWayPoint];
        //Debug.Log(targetWayPoint);
        aim = GameObject.Find("aim");
    }

    void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.tag == "Enemy")
        {
            //Physics.IgnoreCollision(GetComponent<Collider>(), coll.gameObject.GetComponent<Collider>());
        }
        if (coll.gameObject.tag == "Missile")
        {
            this.GetComponent<Rigidbody>().isKinematic = true;
            HP();
        }
        else if (coll.gameObject.tag == "Boss_Missile")
        {
            this.GetComponent<Rigidbody>().isKinematic = true;
            HP();
            isdead = true;
            Debug.Log(isdead);
        }
        else
            this.GetComponent<Rigidbody>().isKinematic = false;
    }
    
    void HP()
    {
        hp = hp - 1;
        if (hp <= 0)
        {
            Instantiate(expEffect, transform.position, Quaternion.identity);
            isdead = true;
            Debug.Log(isdead);
            GameObject.Find("UIManager").SendMessage("UISHOW");
        }
    }

    void Update()
    {
        if(!bossbattle)
        {
            walk();
        }
        /*Vector3 ang = gun.transform.eulerAngles;
        if (ang.x > 180) ang.x -= 360;
        ang.x = Mathf.Clamp(ang.x, -15, 5);
        gun.transform.eulerAngles = ang;*/
        Vector3 aim_ang = aim.transform.localPosition;
        //aim_ang.x = aim.transform.position.x;
        //aim_ang.y = aim.transform.position.y;
        //if (aim_ang.x > 180) aim_ang.x -= 360;
        //if (aim_ang.y > 180) aim_ang.y -= 360;

        //turret.transform.localEulerAngles = new Vector3(0.0f, aim_ang.y * Mathf.Rad2Deg, 0.0f);
        //gun.transform.localEulerAngles = new Vector3(aim_ang.x * Mathf.Rad2Deg, 0.0f, 0.0f);
        // turret.transform.Rotate(new Vector3(0.0f, test.y * Mathf.Rad2Deg, 0.0f) * turnspeed * Time.deltaTime);

        //turretmove();
        //gunmove();

        //gun.transform.Rotate(new Vector3(test.x * Mathf.Rad2Deg, 0.0f, 0.0f) * turnspeed * Time.deltaTime);
        //Input.GetAxis("Horizontal") * Mathf.Rad2Deg Input.GetAxis("Vertical")
        //turret.transform.Rotate(new Vector3(0.0f, aim.transform.eulerAngles.x * Mathf.Rad2Deg, 0.0f) * turnspeed * Time.deltaTime);
        //gun.transform.Rotate(new Vector3(aim.transform.eulerAngles.y * Mathf.Rad2Deg, 0.0f, 0.0f) * turnspeed * Time.deltaTime);
        
		/*
		if (Input.GetButton("Fire1") && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Instantiate(shot, shotSpawn.transform.position, shotSpawn.transform.rotation);
        }
        */
    }
    void turretmove()
    {
        //Debug.Log();
        Vector3 lookPos = aim.transform.position - turret.transform.position;
        //lookPos.x = turret.transform.position.x;
        //lookPos.z = turret.transform.position.z;
        Quaternion rotation = Quaternion.LookRotation(lookPos);
        turret.transform.rotation = Quaternion.Slerp(turret.transform.rotation, rotation, turnspeed * Time.deltaTime);
    }

    void gunmove()
    {
        Vector3 lookPos = aim.transform.position - gun.transform.position;
        //lookPos.x = lookPos.y;
        //lookPos.y = gun.transform.position.y;
        //lookPos.z = gun.transform.position.z;
        Quaternion rotation = Quaternion.LookRotation(lookPos);
        gun.transform.rotation = Quaternion.Slerp(gun.transform.rotation, rotation, turnspeed * Time.deltaTime);
    }

    void walk()
    {
        //Debug.Log(targetWayPoint.position);
        //this.transform.Translate (new Vector3 (Input.GetAxis("Horizontal"),0,Input.GetAxis("Vertical")) * Time.deltaTime);

        transform.forward = Vector3.RotateTowards(transform.forward, targetWayPoint.position - transform.position, speed * Time.deltaTime, 0.0f);

        transform.position = Vector3.MoveTowards(transform.position, targetWayPoint.position, speed * Time.deltaTime);

        //Debug.Log("대상의위치"+targetWayPoint.position);
        // Debug.Log("현재나의위치" + transform.position);
        if (targetWayPoint.position.x - 0.3f <= transform.position.x && transform.position.x <= targetWayPoint.position.x + 0.3f && targetWayPoint.position.z - 0.3f <= transform.position.z && transform.position.z + 0.3f <= targetWayPoint.position.z + 0.3f)
        {
            currentWayPoint++;
            if (currentWayPoint >= wayPointList.Length)
            {
                bossbattle = true;
                GameObject.FindWithTag("Boss").GetComponent<BigBadBoss>().battlemod();
                return;
            }
            targetWayPoint = wayPointList[currentWayPoint];
        }
    }
}
