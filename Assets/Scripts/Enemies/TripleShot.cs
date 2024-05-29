using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TripleShot : MonoBehaviour
{
    public Cooldown fireCooldown;
    public GameObject projectilePrefab;
    public Transform firePoint;
    public float spreadAngle = 15f;
    public float projectileSpeed;
    void Update()
    {
        if (!fireCooldown.IsCoolingDown())
        {
            FireTripeShot();
            fireCooldown.StartCooldown();
        }

    }

    public void FireTripeShot()
    {
        for (int i = 0; i < 3; i++)
        {
            var projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
            projectile.transform.Rotate(0f, spreadAngle * (i - 1), 0f);
            projectile.transform.SetParent(null);

            projectile.GetComponent<Rigidbody>().AddForce(projectile.transform.right * projectileSpeed);
        }
    }
}
