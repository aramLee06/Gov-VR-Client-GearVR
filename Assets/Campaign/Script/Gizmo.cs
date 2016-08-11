using UnityEngine;
using System.Collections;

public class Gizmo : MonoBehaviour
{
    public float explosionRadius;
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}