using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drone : MonoBehaviour
{
    public float speed;
    public float timeToDestroy;
    public GameObject laserPrefab;
    public GameObject shootPoint;
    private GameObject laser;
    private bool canShootLaser = true;
    public bool isBossDrone = false;
    public Renderer detectPointRenderer;
    public bool haveBeenSeen = false;
    private bool delayController = false;
    public Cooldown delay;
    public Origin origin;

    void Update()
    {
        if (detectPointRenderer.isVisible)
        {
            haveBeenSeen = true;
        }

        if (haveBeenSeen && delayController == false)
        {
            delay.StartCooldown();
            Debug.Log("Drone avistado");
            delayController = true;
        }

        if (canShootLaser)
        {
            if (delayController && !delay.IsCoolingDown())
            {
                if (!isBossDrone)
                {
                    GetComponent<Parallax>().enabled = true;
                }

                Move();
            }
        }

    }

    public void ShootLaser()
    {
        //Ajusta a posição inicial do laser para não ficar em cima do drone
        Vector3 laserPosition = transform.position + transform.forward * 0.7f;

        laser = Instantiate(laserPrefab, laserPosition, shootPoint.transform.rotation);
        if (isBossDrone)
        {
            laser.GetComponent<DroneLaser>().isBossDrone = true;
        }
    }

    private void Move()
    {
        switch (origin)
        {
            case Origin.Left:
                transform.position += -transform.forward * Time.deltaTime * speed;
                if (transform.position.x >= 0.57f)
                {
                    StopAndShoot();
                }
                break;

            case Origin.Right:
                transform.position += -transform.forward * Time.deltaTime * speed;
                if (transform.position.x <= -0.57f)
                {
                    StopAndShoot();
                }
                break;

            case Origin.Bottom:
                transform.position += transform.forward * Time.deltaTime * speed;
                if (transform.position.z <= Camera.main.transform.position.z + 0.7f)
                {
                    StopAndShoot();
                }
                break;

            case Origin.Top:
                transform.position += -transform.forward * Time.deltaTime * speed;
                if (transform.position.z <= Camera.main.transform.position.z - 0.7f)
                {
                    StopAndShoot();
                }
                break;
        }
    }

    private void StopAndShoot()
    {
        speed = 0;
        canShootLaser = false;
        ShootLaser();
        Destroy(laser, timeToDestroy);
        Destroy(gameObject, timeToDestroy);
    }
    public enum Origin
    {
        Bottom,
        Top,
        Right,
        Left
    }
}
