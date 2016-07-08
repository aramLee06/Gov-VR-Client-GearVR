using UnityEngine;
using System.Collections;

public class Nav : MonoBehaviour
{
    private Transform targetTr;
    private Transform thisTr;
    private NavMeshAgent nvAgent;

    // Use this for initialization
    void Start()
    {
        targetTr = GameObject.Find("Cube").GetComponent<Transform>();
        nvAgent = this.gameObject.GetComponent<NavMeshAgent>();
        nvAgent.destination = targetTr.position;
    }
}