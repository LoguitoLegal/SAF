using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteWhenLeftBehind : MonoBehaviour
{
    void Update()
    {
        if (transform.position.z <= Camera.main.transform.position.z - 1f)
        {
            Destroy(gameObject);
        }
    }
}
