using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelicopterShoot : MonoBehaviour
{
    [SerializeField] private GameObject ProjectilePrefab;
    [SerializeField] private Transform ShootPointRight;
    [SerializeField] private Transform ShootPointLeft;
    [SerializeField] private float speed = 0.5f;
    [SerializeField] private Cooldown Cooldown;

    void Update()
    {
        if (transform.position.x >= -0.639 && transform.position.x <= 0.639)
        {
            if (!Cooldown.IsCoolingDown())
            {
                Shoot(ShootPointRight);
                Shoot(ShootPointLeft);
            }
        }
    }

    void Shoot(Transform shootPoint)
    {
        GameObject projectile = Instantiate(ProjectilePrefab, shootPoint.position, Quaternion.identity);
        projectile.GetComponent<Rigidbody>().AddForce(transform.forward * speed);
        Cooldown.StartCooldown();
    }
}
