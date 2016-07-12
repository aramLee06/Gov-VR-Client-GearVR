using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PUnitMoveTemp : MonoBehaviour
{
    public GameObject turret;
    private Transform targetTr;
    public GameObject shot; //미사일
    public Transform shotSpawn; //미사일 발사 위치
    public float fireRate; //미사일 발사 딜레이
    private float nextFire;
    public float power= 2000;
    private float shaking;
    private float movetime;
    public GameObject gun;

    // Use this for initialization
    void Start()
    {
        shaking = 1f;
        movetime = 1f;
        targetTr = GameObject.Find("blue_tank_01_Test").GetComponent<Transform>();
        //iTween.MoveTo(gameObject, iTween.Hash("path", iTweenPath.GetPath("New Path 1"), "time", 20, "easetype", iTween.EaseType.linear, "looptype", iTween.LoopType.loop, "orienttopath", true));
    }

    // Update is called once per frame
    void Update()
    {
        
        transform.Translate(new Vector3(0, Input.GetAxis("Vertical"), 0) * Time.deltaTime);
        turret.transform.Rotate(new Vector3(0.0f, 0.0f, Input.GetAxis("Horizontal") * Mathf.Rad2Deg) * Time.deltaTime);
        gun.transform.Rotate(new Vector3(Input.GetAxis("Vertical") * Mathf.Rad2Deg, 0.0f, 0.0f) * Time.deltaTime);
        if (Input.GetButton("Fire1") && Time.time > nextFire)
        {
//            Debug.Log(Time.time);
            nextFire = Time.time + fireRate;
            Transform myBullet = (Transform)Instantiate(shot, shotSpawn.transform.position, shotSpawn.transform.rotation);
        }
        //turret.transform.forward = Vector3.RotateTowards(transform.forward, targetTr.position - transform.position, Time.deltaTime, 0.0f);
        /*{
            BezierTime = BezierTime + Time.deltaTime;


            if (BezierTime == 1)
 {
                BezierTime = 0;
            }
            CurveX = (((1 - BezierTime) * (1 - BezierTime)) * StartPointX) + (2 * BezierTime * (1 - BezierTime) * ControlPointX) + ((BezierTime * BezierTime) * EndPointX);
            CurveY = (((1 - BezierTime) * (1 - BezierTime)) * StartPointY) + (2 * BezierTime * (1 - BezierTime) * ControlPointY) + ((BezierTime * BezierTime) * EndPointY);
            transform.position = new Vector3(CurveX, CurveY, 0);
            ㅁㄴ
        }*/
        //this.transform.Translate (new Vector3 (0.0f, 0.0f, 0.5f) * Time.deltaTime);
        //this.transform.Rotate (new Vector3 (0.0f,Input.GetAxis ("Horizontal")*Mathf.Rad2Deg, 0.0f) * Time.deltaTime);

        //if (Input.GetKey (KeyCode.Escape)) {
        //	SceneManager.LoadScene ("02_Lobby_new");
        //}
        //this.transform.Translate (new Vector3 (Input.GetAxis("Horizontal"),0,Input.GetAxis("Vertical")) * Time.deltaTime);
        //Input.GetAxis("Horizontal"),0,Input.GetAxis("Vertical")*
    }
}
