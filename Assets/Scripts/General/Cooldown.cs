using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Cooldown
{
    [SerializeField] private float cooldownTime = 1f;
    private float nextFireTime;

    public bool IsCoolingDown()
    {
        if (Time.time < nextFireTime)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void StartCooldown()
    {
        nextFireTime = Time.time + cooldownTime;
    }

    public void SetCoolDownTime(float cooldownTime)
    {
        this.cooldownTime = cooldownTime;
    }
}
