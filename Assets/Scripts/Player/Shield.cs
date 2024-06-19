using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    public int integrity = 8;
    private Player player;

    private void Awake()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
    }
    private void Update()
    {
        transform.position = player.transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "EnemyProjectile")
        {
            Destroy(other.gameObject);
            integrity--;
            SoundManager.Instance.TocarSFX(10);
        }

        if (other.tag == "Enemy" || other.tag == "Boss")
        {
            SoundManager.Instance.TocarSFX(10);
            integrity -= 2;
        }

        if (other.tag == "Missile")
        {
            Destroy(other.gameObject);
            integrity = 0;
            player.TookDamage();
            player.healthPoints -= 10;
            SoundManager.Instance.TocarSFX(10);
        }

        if (integrity <= 0)
        {
            gameObject.SetActive(false);
        }
    }
    private void OnDisable()
    {
        player._isShieldActive = false;
        player.shieldCooldown.StartCooldown();
        player._canTakeDamage = true;
        SoundManager.Instance.TocarSFX(12);
    }

    private void OnEnable()
    {
        SoundManager.Instance.TocarSFX(11);
        player._canTakeDamage = false;
        player._isShieldActive = true;
        integrity = 8;
    }
}
