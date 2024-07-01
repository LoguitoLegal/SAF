using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class DroneCaller : MonoBehaviour
{
    public Cooldown callCooldown;
    public bool canCall;
    public GameObject rightDrone;
    public GameObject leftDrone;
    public bool isRightDrone;
    void Update()
    {
        if (canCall && !callCooldown.IsCoolingDown())
        {
            Call();
            callCooldown.StartCooldown();
        }
    }

    void Call()
    {
        float randomPlace = Random.Range(49.9f, 50.9f);
        
        if (isRightDrone)
        {
            Vector3 spawnPoint = new Vector3(rightDrone.transform.position.x, rightDrone.transform.position.y, randomPlace);
            Instantiate(rightDrone, spawnPoint, rightDrone.transform.rotation);
        }
        else
        {
            Vector3 spawnPoint = new Vector3(leftDrone.transform.position.x, leftDrone.transform.position.y, randomPlace);
            Instantiate(leftDrone, spawnPoint, leftDrone.transform.rotation);

        }
    }
}
