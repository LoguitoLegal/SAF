using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helice : MonoBehaviour
{
    public float rotationSpeed = 30f;
    public bool planeMode;
    void Update()
    {
        Girar();
    }

    public void Girar()
    {
        if (!planeMode)
        {
            transform.Rotate(0f, 0f, -rotationSpeed * Time.deltaTime);

        }
        else
        {
            transform.Rotate(0f, -rotationSpeed * Time.deltaTime, 0f);
        }
    }
}
