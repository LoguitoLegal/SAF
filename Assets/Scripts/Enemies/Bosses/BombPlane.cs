using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BombPlane : MonoBehaviour
{
    [Header("Atributes")]
    public bool startBattle = false;
    public int health;
    public float speed;

    [Header("Attacks")]
    public Transform shootPointL;
    public Transform shootPointR;
    public Transform shootPointMissileR;
    public Transform shootPointMissileL;
    public Cooldown shootCooldown;
    public Cooldown missileCooldown;
    public GameObject bulletPrefab;
    public GameObject missilePrefab;
    public float bulletForce;
    public float rotationSpeed;
    public Cooldown cooldownToFirstMissile;

    [Header("Others")]
    public Transform playerPosition;
    public bool isPhaseTwo = false;
    public GameObject gunLeft;
    public GameObject gunRight;
    public GameObject gunMissile;


    void Start()
    {
        playerPosition = GameObject.Find("Player").transform;
        speed = GameObject.Find("Main Camera").GetComponent<Parallax>().speed;
        cooldownToFirstMissile.StartCooldown();
    }

    void Update()
    {
        //Guns look at player
        SmoothLookAt(gunLeft.transform, playerPosition.position);
        SmoothLookAt(gunRight.transform, playerPosition.position);

        //Normal weapons
        if (!shootCooldown.IsCoolingDown())
        {
            Shoot(shootPointL);
            Shoot(shootPointR);
        }

        //Missile
        if (!missileCooldown.IsCoolingDown() && !cooldownToFirstMissile.IsCoolingDown())
        {
            ShootMissile();
        }

    }

    private void LateUpdate()
    {
        transform.position += Vector3.forward * speed * Time.deltaTime;
    }

    private void SmoothLookAt(Transform gunTransform, Vector3 targetPosition)
    {
        Vector3 direction = targetPosition - gunTransform.position;
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        gunTransform.rotation = Quaternion.Slerp(gunTransform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    private void Shoot(Transform shootPoint)
    {
        GameObject projectile = Instantiate(bulletPrefab, shootPoint.position, Quaternion.identity);
        projectile.GetComponent<Rigidbody>().AddForce(shootPoint.forward * bulletForce);
        shootCooldown.StartCooldown();
    }

    private void ShootMissile()
    {
        if (!missileCooldown.IsCoolingDown())
        {
            Transform shootPosition;
            if (playerPosition.position.x < 0)
            {
                shootPosition = shootPointMissileL;
            }
            else
            {
                shootPosition = shootPointMissileR;
            }

            Vector3 spawnPosition = new Vector3(shootPosition.position.x, 0.321f, shootPosition.position.z);
            GameObject missile = Instantiate(missilePrefab, spawnPosition, shootPosition.rotation);
            missile.GetComponent<Rigidbody>().AddForce(shootPosition.forward * 20f * Time.deltaTime);
            missileCooldown.StartCooldown();
            //The missile script itself follow the player
        }
    }

    private void Move()
    {

    }
}
