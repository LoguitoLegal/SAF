using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
public class Player : MonoBehaviour
{

    [Header("Health and Level")]
    public TMP_Text health;
    public TMP_Text levelProgress;
    public int healthPoints = 100;
    public int points = 0;
    public PointsBar pointsBar;
    private bool imortality = false;
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
    public float dashTime;
    public float dashSpeed;

    [Header("Camera Limits")]
    public Transform cameraTransform;
    public float minX;
    public float maxX;
    public float minZ;
    public float maxZ;

    [Header("Others")]
    public bool canUseShield = false;
    public bool canUseFlare = false;
    private bool canTakeLaserDamage = true;
    public GameObject shield;
    public Cooldown shieldCooldown;
    public Cooldown laserDamageCooldown;
    public Cooldown barrelRollCooldown;
    public GameObject lockAim;
    public Transform playerModelTransform;
    private Animator animationComponent;
    private PowerUps powerUps;

    private void Start()
    {
        cameraTransform = Camera.main.transform;
        powerUps = GetComponent<PowerUps>();
        position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        rb = gameObject.GetComponent<Rigidbody>();
        animationComponent = gameObject.GetComponent<Animator>();
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            SoundManager.Instance.TocarBGMusic(1);
        }
        else if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            SoundManager.Instance.TocarBGMusic(3);
        }
        
    }
    private void Update()
    {
        //Barrel roll
        if (Input.GetKey(KeyCode.D) && Input.GetKeyDown(KeyCode.K) && !barrelRollCooldown.IsCoolingDown())
        {
            barrelRollCooldown.StartCooldown();
            BarrelRoll();
        }
        else if (Input.GetKey(KeyCode.A) && Input.GetKeyDown(KeyCode.K) && !barrelRollCooldown.IsCoolingDown())
        {
            barrelRollCooldown.StartCooldown();
            BarrelRoll();
        }

        PlayerMove();

        //Shield
        if (canUseShield && Input.GetKeyDown(KeyCode.J) && imortality == false)
        {
            if (!shieldCooldown.IsCoolingDown())
            {
                shield.SetActive(true);
                canUseShield = false;
                Debug.Log("Shield Activated");
            }
        }

        if (!shieldCooldown.IsCoolingDown() && imortality == false && powerUps.shieldUnlocked == true)
        {
            canUseShield = true;
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
        if (!imortality)
        {
            if (other.gameObject.tag == "Laser")
            {

                if (canTakeLaserDamage)
                {
                    this.healthPoints -= 10;
                    TookDamage();
                    canTakeLaserDamage = false;
                    laserDamageCooldown.StartCooldown();
                }
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

    void BarrelRoll()
    {
        if (Input.GetAxis("Horizontal") > 0)
        {
            animationComponent.Play("BarrelRollRight");
            StartCoroutine(Dash(2));
        }
        else
        {
            animationComponent.Play("BarrelRoll");
            StartCoroutine(Dash(2));
        }
    }

    IEnumerator Dash(float speedMultiplier)
    {
        float originalSpeed = speed;

        speed *= speedMultiplier;

        yield return new WaitForSeconds(dashTime);

        speed = originalSpeed;
    }
    
    public void ToggleImmortality()
    {
        imortality = !imortality;

        if (_isShieldActive)
        {
            shield.SetActive(false);

        }
            _canTakeDamage = !_canTakeDamage;

        
        canUseShield = !canUseShield;
    }
}
