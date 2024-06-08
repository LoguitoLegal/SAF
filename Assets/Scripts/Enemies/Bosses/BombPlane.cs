using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BombPlane : MonoBehaviour
{
    [Header("Atributes")]
    public bool startBattle = false;
    public bool isPhaseTwo = false;
    public bool canTakeDamage = false;
    public bool battleEnd;
    public int health;
    public float speed;
    public bool tookDamage;

    [Header("Attacks")]
    public Transform shootPointMiddle;
    public Transform shootPointL;
    public Transform shootPointR;
    public Transform shootPointMissileR;
    public Transform shootPointMissileL;
    public GameObject bulletPrefab;
    public GameObject missilePrefab;
    public float bulletForce;
    public float missileStartForce;
    public float rotationSpeed;

    [Header("Cooldowns")]
    public Cooldown shootCooldown;
    public Cooldown shootCooldownMiddle;
    public Cooldown missileCooldown;
    public Cooldown cooldownToFirstMissile;

    [Header("Others")]
    public Transform playerPosition;
    public GameObject gunLeft;
    public GameObject gunRight;
    public GameObject gunMissile;
    public GameObject[] weapons;

    void Start()
    {
        playerPosition = GameObject.Find("Player").transform;
        speed = GameObject.Find("Main Camera").GetComponent<Parallax>().speed;
        cooldownToFirstMissile.StartCooldown();
    }

    void Update()
    {
        if (startBattle)
        {
            //Guns look at player
            if (gunLeft != null)
            {
                SmoothLookAt(gunLeft.transform, playerPosition.position);
            }
            if (gunRight != null)
            {
                SmoothLookAt(gunRight.transform, playerPosition.position);
            }
            if (gunMissile != null)
            {
                SmoothLookAt(gunMissile.transform, playerPosition.position);

            }

            //Normal weapons
            if (!shootCooldown.IsCoolingDown())
            {
                if (gunMissile != null)
                {
                    Shoot(shootPointMiddle, shootCooldownMiddle);
                }
                if (shootPointL != null)
                {
                    Shoot(shootPointL, shootCooldown);

                }
                if (shootPointR != null)
                {
                    Shoot(shootPointR, shootCooldown);
                }
            }

            if (isPhaseTwo)
            {
                //Missile
                if (!missileCooldown.IsCoolingDown() && !cooldownToFirstMissile.IsCoolingDown())
                {
                    ShootMissile(shootPointMissileL, shootPointMissileR);
                }
            }
        }
    }

    private void LateUpdate()
    {
        if (startBattle)
        {
            transform.position += Vector3.forward * speed * Time.deltaTime;

            BattleProgression();
        }
    }

    private void BattleProgression()
    {
        //Fase 2 (misseis)
        if (gunMissile == null || gunRight == null || gunLeft == null)
        {
            isPhaseTwo = true;
        }

        //Fase 3 (destruir o aviao)
        if (gunMissile == null && gunRight == null && gunLeft == null)
        {
            canTakeDamage = true;
        }
    }

    private void SmoothLookAt(Transform gunTransform, Vector3 targetPosition)
    {
        Vector3 direction = targetPosition - gunTransform.position;
        direction.y = 0f; //Faz ela não girar pra todo lado, só pelas laterais
        Quaternion targetRotation = Quaternion.LookRotation(direction);

        //Suaviza a rotaçãozinha da arma
        gunTransform.rotation = Quaternion.Slerp(gunTransform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    private void Shoot(Transform shootPoint, Cooldown cooldown)
    {
        Vector3 spawnPosition = new Vector3(shootPoint.position.x, shootPoint.position.y, shootPoint.position.z);
        GameObject projectile = Instantiate(bulletPrefab, spawnPosition, Quaternion.identity);
        projectile.GetComponent<Rigidbody>().AddForce(shootPoint.forward * bulletForce);
        cooldown.StartCooldown();
    }

    private void ShootMissile(Transform shootPoint, Transform shootPoint2)
    {
        if (!missileCooldown.IsCoolingDown())
        {
            //Transform shootPosition;
            //if (playerPosition.position.x < 0)
            //{
            //    shootPosition = shootPointMissileL;
            //}
            //else
            //{
            //    shootPosition = shootPointMissileR;
            //}

            // Missile Left
            Vector3 spawnPosition = new Vector3(shootPoint.position.x, shootPoint.position.y, shootPoint.position.z);
            GameObject missile = Instantiate(missilePrefab, spawnPosition, shootPoint.rotation);
            missile.GetComponent<Rigidbody>().AddForce(shootPoint.forward * missileStartForce, ForceMode.Impulse);

            // Missile Right
            Vector3 spawnPosition2 = new Vector3(shootPoint2.position.x, shootPoint2.position.y, shootPoint2.position.z);
            GameObject missile2 = Instantiate(missilePrefab, spawnPosition2, shootPoint2.rotation);
            missile2.GetComponent<Rigidbody>().AddForce(shootPoint2.forward * missileStartForce, ForceMode.Impulse);

            missileCooldown.StartCooldown();
            //The missile script itself follow the player
        }
    }

    public void TookDamage()
    {
        this.tookDamage = true;
        Invoke("SetTookDamageFalse", 0.15f);
        //SoundManager.Instance.TocarSFX(2); Talvez eu colkoque som pra isso
    }
    public void SetTookDamageFalse() => this.tookDamage = false;

    private void OnTriggerEnter(Collider other)
    {
        if (canTakeDamage && other.tag == "PlayerProjectile")
        {
            this.health -= 1;
            TookDamage();
            Destroy(other.gameObject);
            if (health <= 0)
            {
                battleEnd = true;
                gameObject.SetActive(false);
            }
        }
    }
}
