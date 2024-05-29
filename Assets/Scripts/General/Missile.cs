using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    public GameObject target;
    public GameObject lockAim;
    public float speed;
    public float rotateSpeed;
    private bool isBack;
    public float lifetime;
    private Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        if (target == null)
        {
            target = GameObject.Find("Player");
        }

        SoundManager.Instance.TocarSFX(8);

        Destroy(gameObject, lifetime);

    }

    private void FixedUpdate()
    {
        FollowTarget(target);
    }
    void Update()
    {

        if (lockAim != null && !lockAim.activeSelf)
        {
            lockAim.SetActive(true);
        }
    }

    private void FollowTarget(GameObject target)
    {
        Vector3 direction = target.transform.position - rb.position;
        direction.Normalize();

        Vector3 amountToRotate = Vector3.Cross(direction, transform.forward) * Vector3.Angle(transform.forward, direction);

        rb.angularVelocity = -amountToRotate * rotateSpeed;

        rb.velocity = transform.forward * speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            //Play Explosion Effect before Destroy
            //lockAim.SetActive(false);
            Destroy(gameObject);
        }
    }
}
