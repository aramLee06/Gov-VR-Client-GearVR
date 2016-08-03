using UnityEngine;

public class bl_CarTriggerDetector : MonoBehaviour
{
    [SerializeField]
    private Type m_Type = Type.Car;
    [SerializeField]
    private bl_CarManager CarManager;
    [SerializeField]
    private bl_Passenger Passenger;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="c"></param>
    void OnTriggerEnter(Collider c)
    {
        if (c.transform.tag == bl_PlayerPhoton.PlayerTag)
        {
            if (m_Type == Type.Car)
            {
                CarManager.OnEnterDetector();
            }
            else if (m_Type == Type.Passenger)
            {
                Passenger.OnDetectorEnter();
            }
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
            if (m_Type == Type.Car)
            {
                CarManager.OnExitDetector();
            }
            else if (m_Type == Type.Passenger)
            {
                Passenger.OnDetectorExit();
            }
        }
    }

    [System.Serializable]
    public enum Type
    {
        Car,
        Passenger,
    }
}