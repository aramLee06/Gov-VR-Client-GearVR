using UnityEngine;
using UnityEngine.Networking;

public class Player_Tank : NetworkBehaviour
{

    public float speed = 5.0f;        // moving speed
    public float rotSpeed = 120.0f; // rotation speed
    public GameObject gunbody;       // tank gunbody object
    public GameObject gun;          // tank gun object
    public GameObject bulletPrefab;  // bullet object
    public GameObject spPoint;
    public Camera cam;
    public int power = 2000;  // bullet speed power
    public GUIText HP;//프리팹
    Combat combat;
    public float fireRate = 0.5f;
    private float nextFire;
    


    // Use this for initialization
    void Start()
    {
        combat = GetComponent<Combat>();
    }

    [ClientCallback]
    void Update()
    {
        if (!isLocalPlayer)
        {
            cam.enabled = false;
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

        //Vector3 tempPos = gunbody.transform;
        //DisplayHP(tempPos);
        int user_num = System.Int32.Parse(Network.player.ToString());
        HP.GetComponent<GUIText>().text = "Player" + Network.player.guid[0] + ", " + combat.health;

       // HP.transform.Translate(tempPos);

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
        var bullet = (GameObject)Instantiate(bulletPrefab, spPoint.transform.position, spPoint.transform.rotation);
        bullet.GetComponent<Rigidbody>().AddForce(spPoint.transform.forward * power);
        NetworkServer.Spawn(bullet);
        Destroy(bullet, 1.0f);
    }
    
    /*
    void DisplayHP(Transform tempPos)
    {
        int user_num = System.Int32.Parse(Network.player.ToString());
        HP.GetComponent<GUIText>().text = "Player"+Network.player.guid[0] +", " + combat.health;
        Vector3 pos = cam.WorldToViewportPoint(tempPos.transform.position);
        pos.y += 0.1f;

        var hp_point = (GameObject)Instantiate(HP, pos, Quaternion.identity);
        NetworkServer.Spawn(hp_point);
    }
    */
}
