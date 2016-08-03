using UnityEngine;
using System.Collections;

public class bl_TilHead : MonoBehaviour
{
    [Separator("TillEffect")]
    public float smooth = 4f;
    public float tiltAngle = 6f;
    [Separator("FallEffect")]
    [Range(0.01f, 1.0f)]
    public float m_time = 0.2f;
    public float DownAmount = 8;

    private Transform m_transform;
    /// <summary>
    /// 
    /// </summary>
    void Awake()
    {
        this.m_transform = this.transform;
    }

    /// <summary>
    /// 
    /// </summary>
     void OnEnable()
     {
         //Register event
         bl_EventHandler.OnSmallImpact += this.OnSmallImpact;
     }

    /// <summary>
    /// 
    /// </summary>
     void OnDisable()
     {
         //Unregister event
         bl_EventHandler.OnSmallImpact -= this.OnSmallImpact;
     }

    /// <summary>
    /// 
    /// </summary>
    void Update()
    {
        if (bl_CoopUtils.GetCursorState)
        {
            float t_amount = -Input.GetAxis("Mouse X") * this.tiltAngle;
            t_amount = Mathf.Clamp(t_amount, -this.tiltAngle, this.tiltAngle);
            if (!Input.GetMouseButton(1))
            {
                this.m_transform.localRotation = Quaternion.Lerp(this.m_transform.localRotation, Quaternion.Euler(0, 0, t_amount), Time.deltaTime * this.smooth);
            }
            else
            {
                this.m_transform.localRotation = Quaternion.Lerp(this.m_transform.localRotation, Quaternion.Euler(0, 0, t_amount / 2), Time.deltaTime * this.smooth);
            }
        }
    }

    /// <summary>
    /// This is called by event when player impact with ground.
    /// </summary>
    void OnSmallImpact()
    {
        StartCoroutine(FallEffect());
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    IEnumerator FallEffect()
    {
        Quaternion m_default = this.transform.localRotation;
        Quaternion m_finaly = this.transform.localRotation * Quaternion.Euler(new Vector3(DownAmount, 0, 0));
        float t_rate = 1.0f / m_time;
        float t_time = 0.0f;
        while (t_time < 1.0f)
        {
            t_time += Time.deltaTime * t_rate;
            this.transform.localRotation = Quaternion.Slerp(m_default, m_finaly, t_time);
            yield return t_rate;
        }
    }
}