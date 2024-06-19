using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DualShoot : MonoBehaviour
{
    public bool canShoot = false;
    public bool lookAtPlayer = false;
    public float rotationSpeed;
    public Cooldown shootCooldown;
    public GameObject bulletPrefab;
    public GameObject head;
    public float bulletForce;
    public Transform shootPoint1;
    public Transform shootPoint2;
    private Transform playerTransform;

    private void Start()
    {
        playerTransform = GameObject.Find("Player").transform;
    }
    void Update()
    {
        if (canShoot)
        {
            if (!shootCooldown.IsCoolingDown())
            {
                Shoot(shootPoint1);
                Shoot(shootPoint2);
                shootCooldown.StartCooldown();
            }

        }
        if (lookAtPlayer)
        {
            SmoothLookAt(head.transform, playerTransform.position);
        }

    }

    void Shoot(Transform shootPoint)
    {
        Vector3 spawnPosition = new Vector3(shootPoint.position.x, shootPoint.position.y, shootPoint.position.z);
        GameObject projectile = Instantiate(bulletPrefab, spawnPosition, Quaternion.identity);
        projectile.GetComponent<Rigidbody>().AddForce(shootPoint.forward * bulletForce);
    }

    private void SmoothLookAt(Transform gunTransform, Vector3 targetPosition)
    {
        Vector3 direction = targetPosition - gunTransform.position;

        direction.y = 0f;

        Quaternion targetRotation = Quaternion.LookRotation(direction);

        //Suaviza a rotaçãozinha da arma
        gunTransform.rotation = Quaternion.Slerp(gunTransform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
}
