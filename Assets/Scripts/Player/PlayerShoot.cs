using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public Transform ShootPoint;
    public Transform ShootPointLeft;
    public Transform ShootPointRight;
    public float force;
    public Cooldown Cooldown;
    public GameObject projectilePrefab;
    public int shootLevel;

    void Update()
    {
        if(Input.GetKey(KeyCode.Space))
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
                    Shoot(ShootPoint);

                    Shoot(ShootPointLeft);

                    Shoot(ShootPointRight);

                    SoundManager.Instance.TocarSFX(5);
                }
            }
        }
    }

    public void Shoot(Transform shootPoint)
    {
        GameObject projectile = Instantiate(projectilePrefab, shootPoint.position, Quaternion.identity);
        projectile.GetComponent<Rigidbody>().AddForce(transform.right * force);

        Cooldown.StartCooldown();
    }
}
