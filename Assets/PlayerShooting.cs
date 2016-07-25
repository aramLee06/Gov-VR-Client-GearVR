using UnityEngine;
using System.Collections;

public class PlayerShooting : MonoBehaviour {

    public int damagePerShot = 20;
    public float timeBetweenBullets = 0.15f;
    public float range = 100f;
    public GameObject spot;


    float timer;
    Ray shootRay;
    RaycastHit shootHit;
    int shootableMask;
    public ParticleSystem gunParticles;
    public LineRenderer gunLine;
    float effectsDisplayTime = 0.2f;


    void Awake()
    {
        shootableMask = LayerMask.GetMask("Shootable");
        //gunParticles = GetComponent<ParticleSystem>();
        //gunLine = GetComponent<LineRenderer>();
    }


    void Update()
    {
        spot.transform.LookAt(GameObject.Find("Boss").transform);
        timer += Time.deltaTime;
        transform.Rotate(new Vector3(Input.GetAxis("Vertical") * Mathf.Rad2Deg, 0.0f, 0.0f) * Time.deltaTime);

        if (Input.GetButton("Fire1") && timer >= timeBetweenBullets && Time.timeScale != 0)
        {
            Shoot();
        }
        else
            DisableEffects();

        if (timer >= timeBetweenBullets * effectsDisplayTime)
        {
            //DisableEffects();
        }
    }


    public void DisableEffects()
    {
        gunLine.enabled = false;
        gunParticles.Stop();
    }


    void Shoot()
    {
        timer = 0f;
        
        gunParticles.Play();

        gunLine.enabled = true;
        gunLine.SetPosition(0, transform.position);

        shootRay.origin = spot.transform.position;
        shootRay.direction = spot.transform.forward;

        if (Physics.Raycast(shootRay, out shootHit, range))
        {
            Debug.DrawLine(shootRay.origin, shootHit.point);
            Debug.Log(shootHit.collider.tag);
            /*EnemyHealth enemyHealth = shootHit.collider.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damagePerShot, shootHit.point);
            }*/
            gunParticles.transform.position= shootHit.collider.transform.position;
            gunLine.SetPosition(1, shootHit.collider.transform.position);
        }
        else
        {
            gunLine.SetPosition(1, shootRay.origin + shootRay.direction * range);
        }
    }
}