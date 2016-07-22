using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Player_Drone : NetworkBehaviour
{
    public float speed = 3.0f;        // moving speed
    public float rotSpeed = 50.0f; // rotation speed

    public Camera cam;
    public GameObject spPoint;
    public GameObject raiserPrefab;  // bullet object

    public int power = 500;  // bullet speed power
    public float fireRate = 0.5f;
    private float nextFire;

    private Vector3 angle; // body limit angle
    private Vector3 pos;



    // Use this for initialization
    void Start () {
        transform.Translate(0, 5f, 0);
	}

    // Update is called once per frame
    [ClientCallback]
    void Update () {
        if (!isLocalPlayer)
        {
            cam.enabled = false;
            return;
        }

        var ang = Input.GetAxisRaw("Oculus_GearVR_RThumbstickX") * Time.deltaTime * rotSpeed;       // L moving turret
        var ang2 = Input.GetAxisRaw("Oculus_GearVR_RThumbstickY") * Time.deltaTime * rotSpeed;       // L moving turret
        var x = Input.GetAxisRaw("Oculus_GearVR_LThumbstickX") * Time.deltaTime * rotSpeed;
        var z = Input.GetAxisRaw("Oculus_GearVR_LThumbstickY") * Time.deltaTime * speed;

        transform.Rotate(ang, 0, 0);
        transform.Rotate(0, 0, -ang2);
        transform.Rotate(0, x, 0);
        transform.Translate(0, 0, -z);

        angle = gameObject.transform.localEulerAngles;
        pos = gameObject.transform.localPosition;

        if (angle.x > 180) angle.x -= 360;
        angle.x = Mathf.Clamp(angle.x, -15, 5);
        if (angle.z > 180) angle.z -= 360;
        angle.z = Mathf.Clamp(angle.z, -10, 10);

        
        pos.y = Mathf.Clamp(pos.y, 4.0f, 5);
        gameObject.transform.localEulerAngles = angle;
        gameObject.transform.localPosition = pos;


        float fire = Input.GetAxisRaw("Tank_Fire");

        if (fire != 0.0 && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            CmdDoFire();
        }
    }

    [Command]
    public void CmdDoFire()
    {
        var bullet = (GameObject)Instantiate(raiserPrefab, spPoint.transform.position, spPoint.transform.rotation);
        bullet.GetComponent<Rigidbody>().AddForce(spPoint.transform.forward * power);

        NetworkServer.Spawn(bullet);
        Destroy(bullet, 1.0f);

    }
}
