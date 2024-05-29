using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drone : MonoBehaviour
{
    public float speed;
    public GameObject laserPrefab;
    public GameObject shootPoint;
    private bool canShootLaser = true;

    void Start()
    {
        
    }
    void Update()
    {
        transform.position += -transform.right * Time.deltaTime * speed;
        if (canShootLaser)
        {
            if (transform.position.x <= -0.63f || transform.position.x >= 0.63f || 
                transform.position.z >= Camera.main.transform.position.z + 0.72f || transform.position.z <= Camera.main.transform.position.z -0.72f)
            {
                speed = 0;
                canShootLaser = false;
                ShootLaser();
            }
        }

    }

    public void ShootLaser()
    {
        // Ajusta a posição inicial do laser para não ficar em cima do drone
        Vector3 laserPosition = transform.position + transform.right * 0.7f;

        GameObject laser = Instantiate(laserPrefab, laserPosition, shootPoint.transform.rotation);

    }
}
