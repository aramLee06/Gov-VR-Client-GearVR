using UnityEngine;
using UnityEngine.Networking;

public class Player_Tank : NetworkBehaviour {

    public float speed = 5.0f;        // moving speed
    public float rotSpeed = 120.0f; // rotation speed
    public GameObject gunbody;       // tank gunbody object
    public GameObject gun;          // tank gun object
    public GameObject bulletPrefab;  // bullet object
    public GameObject spPoint;
    public Camera cam;
    public int power = 2000;  // bullet speed power
                             
    // Use this for initialization
    void Start () {
    }

    [ClientCallback]
    void Update()
    {
        float amtToRot = rotSpeed * Time.deltaTime;  // rotation angle per frame
        float ang2 = Input.GetAxis("Tank");       // moving turret
        if (!isLocalPlayer)
        {
            cam.enabled = false;
            return;
        }
        else {
            
            var x = Input.GetAxis("Horizontal") * Time.deltaTime * 150.0f;
            var z = Input.GetAxis("Vertical") * Time.deltaTime * 3.0f;
            

            transform.Rotate(0, x, 0);
            transform.Translate(0, 0, z);
            gunbody.transform.Rotate(Vector3.forward * ang2 * amtToRot); // moving gunbody
            gun.transform.Rotate(Vector3.forward * ang2 * amtToRot); // moving gun
            
            if (Input.GetKeyDown(KeyCode.Space))
            {
                CmdDoFire(1.0f);
            }
        }
    }

    [Command]
    public void CmdDoFire(float lifeTime)
    {
        GameObject bullet = (GameObject)Instantiate(bulletPrefab, spPoint.transform.position, spPoint.transform.rotation);
        bullet.GetComponent<Rigidbody>().AddForce(spPoint.transform.forward * power);
        NetworkServer.Spawn(bullet);
    }
}
