using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int healthPoints = 10;
    public int pointsToPlayer = 5;
    public GameObject heal;
    public bool tookDamage;
    public GameObject explosionPrefab;
    private Player player;
    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
    }
    private void Update()
    {
       //transform.position -= Vector3.forward * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PlayerProjectile")
        {
            this.healthPoints -= 1;
            TookDamage();
            Destroy(other.gameObject);
        }

        if (healthPoints <= 0)
        {
            player.points += this.pointsToPlayer;
            Chance();
            SoundManager.Instance.TocarSFX(4);
            GameObject boom = Instantiate(explosionPrefab, transform.position, explosionPrefab.transform.rotation);
            Destroy(boom, 0.8f);
            Destroy(gameObject);
        }
    }

    private void Chance()
    {
        int chance = Random.Range(0, 100);
        if (chance < 23)
        {
            Instantiate(heal, transform.position, Quaternion.identity);
        }
    }

    public void TookDamage()
    {
        tookDamage = true;
        Invoke("SetTookDamageFalse", 0.15f);
        //SoundManager.Instance.TocarSFX(2); Talvez eu colkoque som pra isso
    }
    public void SetTookDamageFalse() => tookDamage = false;
}
