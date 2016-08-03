using UnityEngine;
using System.Collections;

public class bl_PlayerFootStep : MonoBehaviour {

    public AudioSource m_Source = null;
    [Space(5)]
    public AudioClip StepClip = null;

    /// <summary>
    /// 
    /// </summary>
    public void NetworkStep()
    {
        if (m_Source != null)
        {
            m_Source.clip = StepClip;
            m_Source.Play();
        }
    }
}