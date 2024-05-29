using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    public int integrity = 5;

    private void Update()
    {
        Transform playerPosition = GameObject.Find("Player").transform;
        transform.position = playerPosition.position;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "EnemyProjectile")
        {
            Destroy(other.gameObject);
            integrity--;

            if (integrity <= 0)
            {
                GameObject.Find("Player").GetComponent<Player>().shieldCooldown.StartCooldown();
                GameObject.Find("Player").GetComponent<Player>().canUseShield = true;
                Destroy(gameObject);
            }
        }
    }
}
