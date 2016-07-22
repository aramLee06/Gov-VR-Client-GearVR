using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Collections;

public class Player_Tank : NetworkBehaviour
{
    //[SyncVar]
    //Transform my_position;

    
    public GameObject gunbody;       // tank gunbody object
    public GameObject gun;          // tank gun object
    public GameObject bulletPrefab;  // bullet object
    public GameObject spPoint;
    public GameObject HP;
    public Camera cam;
    
    public Image skillFilter;
    public Image skillFilter_off;

    public float speed = 1.5f;        // moving speed
    public float rotSpeed = 50.0f; // rotation speed
    public int power = 500;  // bullet speed power
    public float fireRate = 0.5f;
    public float cool = 0.5f;

    private float nextFire;
    private float currentCoolTime;
    private bool canUseSkill = true; //스킬을 사용할 수 있는지 확인하는 변수

    private Vector3 angle;
    Combat combat;
    
    public Animator wheelAnimator;





    // Use this for initialization
    void Start()
    {
        combat = GetComponent<Combat>();

        skillFilter.fillAmount = 1; //처음에 스킬 버튼을 가리지 않음
        skillFilter_off.enabled = false;
    }

    [ClientCallback]
    void Update()
    {
        if (!isLocalPlayer)
        {
            cam.enabled = false;
            skillFilter.enabled = false;
            skillFilter_off.enabled = false;
            HP.SetActive(false);
            return;
        }
        float amtToRot = rotSpeed * Time.deltaTime;  // rotation angle per frame
        float ang = Input.GetAxisRaw("Oculus_GearVR_RThumbstickX");       // L moving turret
        float ang2 = Input.GetAxisRaw("Oculus_GearVR_RThumbstickY");       // L moving turret
        var x = Input.GetAxisRaw("Oculus_GearVR_LThumbstickX") * Time.deltaTime * rotSpeed;
        var z = Input.GetAxisRaw("Oculus_GearVR_LThumbstickY") * Time.deltaTime * speed;

        

        if (x!=0.0f || z!=0.0f)
        {
            transform.Rotate(0, x, 0);
            if (z > 0.0001)
            {
                wheelAnimator.SetBool("moving_back", false);
                wheelAnimator.SetBool("moving_front", true);
                transform.Translate(0, 0, z);
            }  
            else if(z < -0.0001)
            {
                wheelAnimator.SetBool("moving_front", false);
                wheelAnimator.SetBool("moving_back", true);
                transform.Translate(0, 0, z);
            }
            else
            {
                wheelAnimator.SetBool("moving_back", false);
                wheelAnimator.SetBool("moving_front", false);
            }
                
        }
            



        gunbody.transform.Rotate(Vector3.forward * (-ang2) * amtToRot); // L moving gunbody

        angle = gun.transform.localEulerAngles ;

        if (angle.x > 180) angle.x -= 360;
        angle.x = Mathf.Clamp(angle.x, -15, 5);
        
        gun.transform.localEulerAngles = angle;

        if(ang>0.5 || ang<-0.5)
            gun.transform.Rotate(Vector3.left * (-ang) * amtToRot);
       

        float fire = Input.GetAxisRaw("Tank_Fire");
        if (fire != 0.0 && Time.time > nextFire)
        {
            skillFilter_off.enabled = true;
            UseSkill();
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
    

    public void UseSkill()
    {
        if (canUseSkill)
        {
            Debug.Log("Use Skill");
            skillFilter.fillAmount = 1; 
            StartCoroutine("Cooltime");

            currentCoolTime = cool;

            StartCoroutine("CoolTimeCounter");

            canUseSkill = false; //스킬을 사용하면 사용할 수 없는 상태로 바꿈
        }
        else
        {
            Debug.Log("아직 스킬을 사용할 수 없습니다.");
        }
    }

    IEnumerator Cooltime()
    {
        while (skillFilter.fillAmount > 0)
        {
            skillFilter.fillAmount -= 1 * Time.smoothDeltaTime / cool;

            yield return null;
        }

        canUseSkill = true; //스킬 쿨타임이 끝나면 스킬을 사용할 수 있는 상태로 바꿈
        skillFilter_off.enabled = false;
        skillFilter.fillAmount = 1; // 초기 이미지

        yield break;
    }

    //남은 쿨타임을 계산할 코르틴을 만들어줍니다.
    IEnumerator CoolTimeCounter()
    {
        while (currentCoolTime > 0)
        {
            yield return new WaitForSeconds(1.0f);

            currentCoolTime -= 1.0f;
        }

        yield break;
    }
}
