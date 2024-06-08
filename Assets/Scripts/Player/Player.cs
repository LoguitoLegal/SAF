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
    public PointsBar pointsBar;
    public bool _tookDamage = false;
    public bool _canTakeDamage = true;
    public bool _isShieldActive = false;

    [Header("Position and Speed")]
    private Vector3 position;
    private Rigidbody rb;
    public float speed = 5f;
    public float rotateSpeed = 3f;
    public float smooth = 5f;
    public float barrelRollForce;
    public float rotationSmooth = 2f; //Rotation
    public float tiltAngle = 30f; //Rotation

    [Header("Camera Limits")]
    public Transform cameraTransform;
    public float minX;
    public float maxX;
    public float minZ;
    public float maxZ;

    [Header("Others")]
    public bool canUseShield = false;
    public bool canUseFlare = false;
    public GameObject shieldPrefab;
    public Cooldown shieldCooldown;
    public Cooldown laserDamageCooldown;
    private bool canTakeLaserDamage = true;
    public GameObject lockAim;
    public Transform playerModelTransform;

    private void Start()
    {
        cameraTransform = Camera.main.transform;
        position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        rb = gameObject.GetComponent<Rigidbody>();
        SoundManager.Instance.TocarBGMusic(1);
    }
    private void Update()
    {
        PlayerMove();

        //Barrel roll
        //if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow) && Input.GetKeyDown(KeyCode.K))
        //{
        //    BarrelRoll(Vector3.right);
        //}
        //else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow) && Input.GetKeyDown(KeyCode.K))
        //{
        //    BarrelRoll(Vector3.left);
        //}

        //Shield
        if (canUseShield && Input.GetKeyDown(KeyCode.J))
        {
            if (!shieldCooldown.IsCoolingDown())
            {
                Instantiate(shieldPrefab, gameObject.transform);
                _isShieldActive = true;
                Debug.Log("Shield Activated");
                canUseShield = false;
            }
            else
            {
                Debug.Log("Shield is in cooldown");
            }

        }
        if (_isShieldActive)
        {
            _canTakeDamage = false;
        }
        else
        {
            _canTakeDamage = true;
        }

        //Laser Damage
        if (!laserDamageCooldown.IsCoolingDown())
        {
            canTakeLaserDamage = true;
        }

        //Missile Aim
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
        position += Vector3.forward * Time.deltaTime * Camera.main.GetComponent<Parallax>().speed;

        //Limit player to camera
        float minXLimit = cameraTransform.position.x + minX;
        float maxXLimit = cameraTransform.position.x + maxX;
        float minZLimit = cameraTransform.position.z + minZ;
        float maxZLimit = cameraTransform.position.z + maxZ;

        position.x = Mathf.Clamp(position.x, minXLimit, maxXLimit);
        position.z = Mathf.Clamp(position.z, minZLimit, maxZLimit);

        //Smooth
        transform.position = Vector3.Lerp(transform.position, position, smooth * Time.deltaTime);

        //Rotate
        float horizontalInput = Input.GetAxis("Horizontal");
        RotatePlayer(horizontalInput);
    }

    private void RotatePlayer(float horizontalInput)
    {
        float anguloAtual = transform.localEulerAngles.z;
        float anguloFinal = -Input.GetAxis("Horizontal") * tiltAngle;
        float lerpAngle = Mathf.LerpAngle(anguloAtual, anguloFinal, rotationSmooth * Time.deltaTime);
        playerModelTransform.localEulerAngles = new Vector3(0f, 90f, lerpAngle);
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
        if (_canTakeDamage)
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

            if (other.gameObject.tag == "Missile")
            {
                this.healthPoints -= 15;
                TookDamage();
            }
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
        pointsBar.SetCurrentFill(this.points);
    }

    public void TookDamage()
    {
        _tookDamage = true;
        Invoke("SetTookDamageFalse", 0.15f);
        SoundManager.Instance.TocarSFX(2);
        Debug.Log("Tomou Dano");
    }
    public void SetTookDamageFalse() => _tookDamage = false;

    void BarrelRoll(Vector3 direction)
    {
        rb.AddForce(direction * barrelRollForce, ForceMode.Force);
    }
}
