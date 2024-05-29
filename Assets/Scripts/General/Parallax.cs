using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    public float speed;

    private void LateUpdate()
    {
        transform.position += Vector3.forward * speed * Time.deltaTime;
    }
}
