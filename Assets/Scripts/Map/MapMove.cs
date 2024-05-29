using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapMove : MonoBehaviour
{
    [SerializeField] float speed = 2f;
    private void LateUpdate()
    {
        transform.position -= Vector3.forward * speed * Time.deltaTime;
    }
}
