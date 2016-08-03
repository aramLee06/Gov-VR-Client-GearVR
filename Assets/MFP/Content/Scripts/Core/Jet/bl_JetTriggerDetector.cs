using UnityEngine;

public class bl_JetTriggerDetector : MonoBehaviour {

    [SerializeField]private bl_JetManager Jet;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="c"></param>
    void OnTriggerEnter(Collider c)
    {
        if (c.transform.tag == bl_PlayerPhoton.PlayerTag)
        {
            Jet.OnDetectorEnter();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="c"></param>
    void OnTriggerExit(Collider c)
    {
        if (c.transform.tag == bl_PlayerPhoton.PlayerTag)
        {
            Jet.OnDetectorExit();
        }
    }
}