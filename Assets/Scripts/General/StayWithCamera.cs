using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StayWithCamera : MonoBehaviour
{
    private Transform cameraTransform;
    private Renderer render;
    void Start()
    {
        cameraTransform = Camera.main.transform;
        render = GetComponent<Renderer>();
    }

    void Update()
    {
        if (render.isVisible)
        {
            // Converte a posição da viewport para coordenadas do mundo
            Vector3 viewportPosition = Camera.main.WorldToViewportPoint(transform.position);

            // Mantém o objeto dentro dos limites da câmera (viewport)
            viewportPosition.x = Mathf.Clamp01(viewportPosition.x);
            viewportPosition.y = Mathf.Clamp01(viewportPosition.y);

            // Converte de volta para coordenadas do mundo
            transform.position = Camera.main.ViewportToWorldPoint(viewportPosition);
        }        
    }
}
