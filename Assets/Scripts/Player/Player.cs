using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
public class Player : MonoBehaviour
{

    [Header("Health and Level")]
    public TMP_Text health;
    public TMP_Text levelProgress;
    public int healthPoints = 100;
    public int points = 0;
    public bool _tookDamage = false;

    [Header("Position and Speed")]
    private Vector3 position;
    public float speed = 5f;
    public float rotateSpeed = 3f;
    public float smooth = 5f;

    [Header("Camera Limits")]
    public Transform cameraTransform;
    public float minX;
    public float maxX;
    public float minZ;
    public float maxZ;

    [Header("Others")]
    public bool canUseShield = false;
    public GameObject shieldPrefab;
    public Cooldown shieldCooldown;
    public Cooldown laserDamageCooldown;
    private bool canTakeLaserDamage = true;
    public GameObject lockAim;

    private void Start()
    {
        cameraTransform = Camera.main.transform;
        position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        SoundManager.Instance.TocarBGMusic(1);
    }
    private void Update()
    {
        PlayerMove();

        //Shield
        if (canUseShield && Input.GetKeyDown(KeyCode.J))
        {
            if (!shieldCooldown.IsCoolingDown())
            {
                Instantiate(shieldPrefab, gameObject.transform);
                Debug.Log("Shield Activated");
                canUseShield = false;
            }
            else
            {
                Debug.Log("Shield is in cooldown");
            }

        }

        //Laser
        if (!laserDamageCooldown.IsCoolingDown())
        {
            canTakeLaserDamage = true;
        }

        //Aim
        if (GameObject.FindWithTag("Missile") != null)
        {
            lockAim.SetActive(true);
        }
        else
        {
            lockAim.SetActive(false);
        }
    }

    private void LateUpdate()
    {
        RefreshStatus();
    }

    private void PlayerMove()
    {
        //Player Move
        Vector3 moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        position += moveDirection * Time.deltaTime * speed;

        //Limit player to camera
        float minXLimit = cameraTransform.position.x + minX;
        float maxXLimit = cameraTransform.position.x + maxX;
        float minZLimit = cameraTransform.position.z + minZ;
        float maxZLimit = cameraTransform.position.z + maxZ;

        position.x = Mathf.Clamp(position.x, minXLimit, maxXLimit);
        position.z = Mathf.Clamp(position.z, minZLimit, maxZLimit);

        //Smooth
        transform.position = Vector3.Lerp(transform.position, position, smooth * Time.deltaTime);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Laser")
        {

            if (canTakeLaserDamage)
            {
                this.healthPoints -= 10;
                canTakeLaserDamage = false;
                laserDamageCooldown.StartCooldown();
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Enemy")
        {
            this.healthPoints -= 10;
            TookDamage();
        }

        if (other.gameObject.tag == "EnemyProjectile")
        {
            this.healthPoints -= 5;
            Destroy(other.gameObject);
            TookDamage();
        }

        if (other.gameObject.tag == "Heal")
        {
            if (healthPoints + 30 <= 100)
            {
                this.healthPoints += 30;
            }
            else
            {
                this.healthPoints = 100;
            }

            SoundManager.Instance.TocarSFX(6);
            Destroy(other.gameObject);
        }

        RefreshStatus();
    }

    public void RefreshStatus()
    {
        this.health.text = healthPoints.ToString();
        this.levelProgress.text = points.ToString();

    }

    public void TookDamage()
    {
        _tookDamage = true;
        Invoke("SetTookDamageFalse", 0.15f);
        SoundManager.Instance.TocarSFX(2);
        Debug.Log("Tomou Dano");
    }
    public void SetTookDamageFalse() => _tookDamage = false; 
}
