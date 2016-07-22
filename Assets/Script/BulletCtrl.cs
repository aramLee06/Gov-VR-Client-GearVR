using UnityEngine;
using UnityEngine.Networking;

public class BulletCtrl : NetworkBehaviour {

    float speed = 30f;
    float timer;
    float waitingTime;
    GameObject temp;
    public GameObject effectPrefab;

    // Use this for initialization
    void Start()
    {
        timer = 0.0f;
        waitingTime = 0.2f;
        temp = GameObject.Find("plasma_beam_flare_red");
        temp.SetActive(false);
        Destroy(this.gameObject, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer>waitingTime)
        {
            GameObject.Find("plasma_beam_heavy_red").transform.FindChild("plasma_beam_flare_red").gameObject.SetActive(true);
        }
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    
    void OnCollisionEnter(Collision collision)
    {
        var hit = collision.gameObject;

        var effect = (GameObject)Instantiate(effectPrefab, gameObject.transform.position, gameObject.transform.rotation);
        NetworkServer.Spawn(effect);
        Destroy(effect, 1.0f);

        var hitPlayer = hit.GetComponent<Player_Tank>();
        Destroy(gameObject);
        if (hitPlayer != null)
        {
            var combat = hit.GetComponent<Combat>();
            combat.TakeDamage(1);
            //Destroy(gameObject);
        }
    }
}
