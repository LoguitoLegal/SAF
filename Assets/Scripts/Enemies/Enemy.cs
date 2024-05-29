using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public int healthPoints = 10;
    public int pointsToPlayer = 5;
    public GameObject heal;

    private void Update()
    {
       //transform.position -= Vector3.forward * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PlayerProjectile")
        {
            this.healthPoints -= 1;
            Destroy(other.gameObject);

            if (healthPoints <= 0)
            {
                GameObject.Find("Player").GetComponent<Player>().points += this.pointsToPlayer;
                Chance();
                SoundManager.Instance.TocarSFX(4);
                Destroy(gameObject);
            }
        }
    }

    private void Chance()
    {
        int chance = Random.Range(0, 100);
        if (chance < 30)
        {
            Instantiate(heal, transform.position, Quaternion.identity);
        }
    }
}
