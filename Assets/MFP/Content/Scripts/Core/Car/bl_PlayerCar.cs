using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class bl_PlayerCar : MonoBehaviour {

    /// <summary>
    /// Objects which are deactivated when the local client entering a car.
    /// </summary>
    public GameObject LocalObjects = null;
    /// <summary>
    /// Objects which are deactivated when the Remote client entering a car.
    /// </summary>
    public GameObject RemoteObjects = null;
    public List<MonoBehaviour> PlayerScripts = new List<MonoBehaviour>();

    /// <summary>
    /// This is called when Local player enter in car
    /// you can write here if you need do something in this event.
    /// </summary>
    public void OnEnterLocal()
    {
        foreach (MonoBehaviour m in PlayerScripts)
        {
            m.enabled = false;
        }
        LocalObjects.SetActive(false);
        chararcterController.enabled = false;
    }

    /// <summary>
    /// This is called when Local player exit from car
    /// you can write here if you need do something in this event.
    /// </summary>
    public void OnExitLocal()
    {
        foreach (MonoBehaviour m in PlayerScripts)
        {
            m.enabled = true;
        }
        LocalObjects.SetActive(true);
        chararcterController.enabled = true;
    }

    /// <summary>
    /// Wait a moment after exit of vehicle for avoid
    /// collision with vehicle colliders.
    /// </summary>
    /// <returns></returns>
   IEnumerator WaitForCC()
    {
        yield return new WaitForSeconds(1f);
        chararcterController.enabled = true;
    }

    /// <summary>
    /// /// This is called when Remote player enter in car
    /// you can write here if you need do something in this event.
    /// </summary>
    void OnEnterNetwork()
    {
        RemoteObjects.SetActive(false);
        chararcterController.enabled = false;
        StopCoroutine(WaitForCC());
    }
    /// <summary>
    /// This is called when Remote player exit from car
    /// you can write here if you need do something in this event.
    /// </summary>
    void OnExitNetwork()
    {
        RemoteObjects.SetActive(true);
        StartCoroutine(WaitForCC());
    }

    private CharacterController chararcterController
    {
        get
        {
            return this.GetComponent<CharacterController>();
        }
    }

    [PunRPC]
    void NetworkCarEvent(bool e,int _type)
    {
        if (e)//when enter in a vehicle
        {
            OnEnterNetwork();
            //Not draw UI when is in jet
            if(_type == 1 || _type == 3) { GetComponent<bl_DrawName>().ShowUI(false); }
        }
        else //when exit of a vehicle
        {
            OnExitNetwork();
            //Draw again UI when exit of jet
            if (_type == 1 || _type == 3) { GetComponent<bl_DrawName>().ShowUI(true); }
        }
    }
}