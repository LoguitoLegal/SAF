using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Renderer bulletRender;

    private void Start()
    {
        bulletRender = GetComponent<Renderer>();
    }
    private void Update()
    {
        if (!bulletRender.isVisible)
        {
            Destroy(gameObject);
        }
    }
}
