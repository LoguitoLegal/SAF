using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockAim : MonoBehaviour
{
    private Transform playerTransform;
    void Start()
    {
        playerTransform = GameObject.Find("Player").transform;
    }

    void Update()
    {
        Vector3 position = new Vector3(playerTransform.position.x, transform.position.y, playerTransform.position.z);
        transform.position = position;
    }
}
