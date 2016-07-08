using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PUnitMoveTemp : MonoBehaviour
{
    public GameObject turret;
    private Transform targetTr;


    // Use this for initialization
    void Start()
    {
        targetTr = GameObject.Find("blue_tank_01_Test").GetComponent<Transform>();
        //iTween.MoveTo(gameObject, iTween.Hash("path", iTweenPath.GetPath("New Path 1"), "time", 20, "easetype", iTween.EaseType.linear, "looptype", iTween.LoopType.loop, "orienttopath", true));
    }

    // Update is called once per frame
    void Update()
    {
        turret.transform.Rotate(new Vector3(0.0f, 0.0f, Input.GetAxis("Horizontal") * Mathf.Rad2Deg) * Time.deltaTime);

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
