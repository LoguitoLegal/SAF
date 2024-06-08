using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    public GameObject target;
    public GameObject lockAim;
    public bool canFollow = false;
    public float speed;
    public float rotateSpeed;
    public float followDelay = 0.5f;
    public float lifetime;
    private Rigidbody rb;
    public bool isBossMissile;
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        if (target == null)
        {
            target = GameObject.Find("Player");
        }

        SoundManager.Instance.TocarSFX(8);

        StartCoroutine(EnableFollowingAfterDelay(followDelay));

        Destroy(gameObject, lifetime);

    }

    private void FixedUpdate()
    {
        transform.position += Vector3.forward * Time.deltaTime * Camera.main.GetComponent<Parallax>().speed;

        if (canFollow && target != null)
        {
            FollowTarget(target);
        }
    }
    void Update()
    {
        GameObject[] flares = GameObject.FindGameObjectsWithTag("Flare");

        //Exibe a mira no player
        if (lockAim != null && !lockAim.activeSelf)
        {
            lockAim.SetActive(true);
        }

        //Se tiver flare, o alvo é o flare
        if (flares.Length > 0)
        {
            target = GetClosestFlare(flares);
        }

        //Se não tiver, o alvo é o player
        else
        {
            target = GameObject.Find("Player");
        }
    }

    private void FollowTarget(GameObject target)
    {

        Vector3 direction = target.transform.position - rb.position;
        direction.y = 0f;
        direction.Normalize();

        Vector3 amountToRotate = Vector3.Cross(direction, transform.forward) * Vector3.Angle(transform.forward, direction);

        rb.angularVelocity = -amountToRotate * rotateSpeed * Time.deltaTime;

        rb.velocity = transform.forward * speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" || other.tag == "Flare")
        {
            //Play Explosion Effect before Destroy
            //lockAim.SetActive(false);
            SoundManager.Instance.TocarSFX(9);
            Destroy(gameObject);
        }
    }

    private GameObject GetClosestFlare(GameObject[] flares)
    {
        GameObject closestFlare = null;
        float minDistance = Mathf.Infinity;
        Vector3 currentPosition = transform.position;

        foreach (GameObject flare in flares)
        {
            float distance = Vector3.Distance(flare.transform.position, currentPosition);
            if (distance < minDistance)
            {
                closestFlare = flare;
                minDistance = distance;
            }
        }

        return closestFlare;
    }
    private IEnumerator EnableFollowingAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        canFollow = true;
    }
}
