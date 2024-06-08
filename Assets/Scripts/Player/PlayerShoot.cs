using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [Header("ETC")]
    public float force;
    public float flareForce;
    public Player player;
    public int shootLevel;
    public float spreadAngle = 15f;

    [Header("Transforms")]
    public Transform ShootPoint;
    public Transform ShootPointLeft;
    public Transform ShootPointRight;
    public Transform FlareShootPoint1;
    public Transform FlareShootPoint2;

    [Header("Prefabs")]
    public GameObject projectilePrefab;
    public GameObject flarePrefab;

    [Header("Cooldowns")]
    public Cooldown Cooldown;
    public Cooldown flareCooldown;

    void Update()
    {

        if (Input.GetKey(KeyCode.Space))
        {
            if (!PauseMenu.isPaused) //If not Paused
            {
                //Single Shot
                if (!Cooldown.IsCoolingDown() && shootLevel == 0)
                {
                    Shoot(ShootPoint);

                    SoundManager.Instance.TocarSFX(5);
                }

                //Tripe Shot Forward
                else if (!Cooldown.IsCoolingDown() && shootLevel == 1)
                {

                    Shoot(ShootPointLeft);

                    Shoot(ShootPointRight);

                    SoundManager.Instance.TocarSFX(5);
                }
                else if (!Cooldown.IsCoolingDown() && shootLevel == 2)
                {
                    FireTripeShot(ShootPoint);

                    SoundManager.Instance.TocarSFX(5);

                }
            }
        }

        //Flare
        if (player.canUseFlare && Input.GetKeyDown(KeyCode.L))
        {
            ShootFlare(FlareShootPoint1, FlareShootPoint2);
        }
    }

    public void Shoot(Transform shootPoint)
    {
        GameObject projectile = Instantiate(projectilePrefab, shootPoint.position, Quaternion.identity);
        projectile.GetComponent<Rigidbody>().AddForce(transform.right * force);

        Cooldown.StartCooldown();
    }

    public void FireTripeShot(Transform shootpoint)
    {
        for (int i = 0; i < 3; i++)
        {
            var projectile = Instantiate(projectilePrefab, shootpoint.position, shootpoint.rotation);
            projectile.transform.Rotate(0f, spreadAngle * (i - 1), 0f);
            projectile.transform.SetParent(null);

            projectile.GetComponent<Rigidbody>().AddForce(projectile.transform.right * force);

            Cooldown.StartCooldown();
        }
    }

    public void ShootFlare(Transform point1, Transform point2)
    {
        if (!flareCooldown.IsCoolingDown())
        {
            GameObject flare1 = Instantiate(flarePrefab, point1.position, Quaternion.identity);
            flare1.GetComponent<Rigidbody>().AddForce(transform.forward * flareForce, ForceMode.Impulse);

            GameObject flare2 = Instantiate(flarePrefab, point2.position, Quaternion.identity);
            flare2.GetComponent<Rigidbody>().AddForce(-transform.forward * flareForce, ForceMode.Impulse);

            flareCooldown.StartCooldown();

        }
    }
}
