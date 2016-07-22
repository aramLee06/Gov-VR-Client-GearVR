using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Combat : NetworkBehaviour
{
    public GameObject HPbar;
    public Sprite HPoff;
    public Sprite HPon;
    public const int maxHealth = 10;

    [SyncVar]
    public int health = maxHealth;

    void Start()
    {
    }

    public void TakeDamage(int amount)
    {
        if (!isServer)
            return;
        health -= amount;
        RpcChangeHealth(health);
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

    [ClientRpc]
    void RpcChangeHealth(int health)
    {
        int num = 10 - health;
        string numString = "HP" + num;
        HPbar.transform.FindChild(numString).GetComponent<Image>().sprite = HPoff;
        if (health <= 0)
        {
            for (int i = 1; i <= 10; i++)
            {
                numString = "HP" + i;
                HPbar.transform.FindChild(numString).GetComponent<Image>().sprite = HPon;
            }
        }
    }
}
