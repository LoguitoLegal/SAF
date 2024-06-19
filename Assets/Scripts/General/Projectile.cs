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
        transform.position += Vector3.forward * Time.deltaTime * Camera.main.GetComponent<Parallax>().speed;

        if (gameObject.transform.position.x <= -0.61f || gameObject.transform.position.x >= 0.61f)
        {
            Destroy(gameObject);
        }
        
        else if (!bulletRender.isVisible)
        {
            Destroy(gameObject);
        }
    }
}
