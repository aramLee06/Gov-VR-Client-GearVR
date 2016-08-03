using UnityEngine;

public class bl_VehicleCallback : MonoBehaviour {

    private bl_CarManager CarManager = null;
    private bl_Passenger Passenger = null;
    private bl_JetManager JetManager;

    void Awake()
    {
        if(GetComponent<bl_CarManager>() != null)
        {
            CarManager = GetComponent<bl_CarManager>();
        }
        if (GetComponent<bl_Passenger>() != null)
        {
            Passenger = GetComponent<bl_Passenger>();
        }
        if (GetComponent<bl_JetManager>() != null)
        {
            JetManager = GetComponent<bl_JetManager>();
        }
    }

    [PunRPC]
    void CarEvent(bool b)
    {
        CarManager.InCar = b;
    }

    [PunRPC]
    void PassengerEvent(bool b, string useby)
    {
        Passenger.InUse = b;
        Passenger.UseBy = useby;
    }

    [PunRPC]
    void JetEvent(bool b)
    {
        JetManager.InCar = b;
    }
}