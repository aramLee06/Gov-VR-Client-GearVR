using UnityEngine;
using UnityEngine.Networking;

public class Player_Tank : NetworkBehaviour
{
    //[SyncVar]
    //Transform my_position;

    public float speed = 5.0f;        // moving speed
    public float rotSpeed = 120.0f; // rotation speed
    public GameObject gunbody;       // tank gunbody object
    public GameObject gun;          // tank gun object
    public GameObject bulletPrefab;  // bullet object
    public GameObject spPoint;
    public Camera cam;
    public GameObject aim;
    public GameObject cooltime_img;
    public GameObject hp;
    public int power = 2000;  // bullet speed power
    Combat combat;
    public float fireRate = 0.5f;
    private float nextFire;
    public Animator cooltime;

    // Use this for initialization
    void Start()
    {
        
        combat = GetComponent<Combat>();
        cooltime.SetBool("fire", false);
    }

    [ClientCallback]
    void Update()
    {
        if (!isLocalPlayer)
        {
            cam.enabled = false;
            aim.SetActive(false);
            cooltime_img.SetActive(false);
            hp.SetActive(false);
            return;
        }
        float amtToRot = rotSpeed * Time.deltaTime;  // rotation angle per frame
        float ang = Input.GetAxis("Oculus_GearVR_RThumbstickX");       // L moving turret
        float ang2 = Input.GetAxis("Oculus_GearVR_RThumbstickY");       // L moving turret
        var x = Input.GetAxis("Oculus_GearVR_LThumbstickX") * Time.deltaTime * 150.0f;
        var z = Input.GetAxis("Oculus_GearVR_LThumbstickY") * Time.deltaTime * 3.0f;
        
        transform.Rotate(0, x, 0);
        transform.Translate(0, 0, z);
        gunbody.transform.Rotate(Vector3.forward * (-ang2) * amtToRot); // L moving gunbody

        //my_position.position = transform.position;

        float fire = Input.GetAxisRaw("Tank_Fire");

        if (fire != 0.0 && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            cooltime.SetBool("init", false);
            cooltime.SetBool("fire", true);
           
            CmdDoFire();
            //var shoot = gun;
            //var shootPlayer = shoot.GetComponent<Player_Tank>();
            //Debug.Log("" + shootPlayer.ToString());
            Invoke("FireSpeed", 0.20f);
        }

    }

    [Command]
    public void CmdDoFire()
    {
        var bullet = (GameObject)Instantiate(bulletPrefab, spPoint.transform.position, spPoint.transform.rotation);
        bullet.GetComponent<Rigidbody>().AddForce(spPoint.transform.forward * power);
        
        NetworkServer.Spawn(bullet);
        Destroy(bullet, 1.0f);
        
    }

    public void FireSpeed()
    {
        cooltime.SetBool("init", true);
        cooltime.SetBool("fire", false);
    }

}
