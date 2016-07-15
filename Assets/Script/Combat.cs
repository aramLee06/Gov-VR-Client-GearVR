using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Combat : NetworkBehaviour
{
    public const int maxHealth = 100;
    float hp;

    [SyncVar(hook = "OnChangeHealth")]
    public int health = maxHealth;

    public RectTransform healthBar;

    public void TakeDamage(int amount)
    {
        if (!isServer)
            return;
        health -= amount;
        if (health <= 0)
        {
            health = maxHealth;
            // called on the server, will be invoked on the clients
            RpcRespawn();
        }
    }

    [ClientRpc]
    void RpcRespawn()
    {
        if (isLocalPlayer)
        {
            // move back to zero location
            transform.position = Vector3.zero;
        }
    }

    void OnChangeHealth(int health)
    {
        healthBar.sizeDelta = new Vector2(health, healthBar.sizeDelta.y);
    }
}
