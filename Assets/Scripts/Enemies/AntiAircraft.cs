using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class AntiAircraft : MonoBehaviour
{
    public float speed;
    public float rotationSpeed;
    public float missileStartForce;
    private bool haveBeenSeen = false;
    private bool shootControl = false;
    public bool isBossGun = false;
    public bool canShoot = true;
    public Cooldown shootCooldown;
    public Transform shootPoint;
    public GameObject tankArm;
    public GameObject missilePrefab;
    private Renderer render;
    private Transform playerTransform;

    private void Start()
    {
        render = GetComponent<Renderer>();
        playerTransform = GameObject.Find("Player").transform;
    }
    void Update()
    {
        SmoothLookAt(tankArm.transform, playerTransform.position);

        if (haveBeenSeen == false && render.isVisible)
        {
            haveBeenSeen = true;
            shootControl = true;
            shootCooldown.StartCooldown();
        }

        if (canShoot)
        {
            if (shootControl == true)
            {
                ShootMissile(shootPoint);
            }
        }

    }

    private void SmoothLookAt(Transform gunTransform, Vector3 targetPosition)
    {
        Vector3 direction = targetPosition - gunTransform.position;
        if (isBossGun)
        {
            direction.y = 0f;
        }
        Quaternion targetRotation = Quaternion.LookRotation(direction);

        //Suaviza a rotaçãozinha da arma
        gunTransform.rotation = Quaternion.Slerp(gunTransform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
    private void ShootMissile(Transform shootPoint)
    {
        if (!shootCooldown.IsCoolingDown())
        {

            //Missile Left
            Vector3 spawnPosition = new Vector3(shootPoint.position.x, shootPoint.position.y, shootPoint.position.z);
            GameObject missile = Instantiate(missilePrefab, spawnPosition, shootPoint.rotation);
            missile.GetComponent<Rigidbody>().AddForce(shootPoint.forward * missileStartForce, ForceMode.Impulse);

            shootCooldown.StartCooldown();
            //The missile script itself follow the player
        }
    }
}
