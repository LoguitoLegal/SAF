using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    [SerializeField] private GameObject ProjectilePrefab;
    [SerializeField] private Transform ShootPoint;
    [SerializeField] private float speed = 0.5f;
    [SerializeField] private Cooldown Cooldown;
    private Renderer enemyRender;
    private void Start()
    {
        enemyRender = GetComponent<Renderer>();
    }
    void Update()
    {
        if (enemyRender.isVisible)
        {
            if (!Cooldown.IsCoolingDown())
            {
                GameObject projectile = Instantiate(ProjectilePrefab, ShootPoint.position, Quaternion.identity);
                projectile.GetComponent<Rigidbody>().AddForce(transform.forward * speed);
                Cooldown.StartCooldown();
            }
        }

    }
}
