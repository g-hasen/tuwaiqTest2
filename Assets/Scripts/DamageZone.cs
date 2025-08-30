using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageZone : MonoBehaviour
{
    public int damagePerSecond = 10;
    public float damageRadius = 3;
    private Transform player;
    private float damageTimer = 0;

    void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
            player = playerObj.transform;
    }

    void Update()
    {
        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= damageRadius)
        {
            damageTimer += Time.deltaTime;
            if (damageTimer >= 1f / damagePerSecond)
            {
                player.GetComponent<Player2>().TakeDamage(1);
                damageTimer = 0f;
            }
        }
        else
        {
            damageTimer = 0f;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, damageRadius);
    }
}
