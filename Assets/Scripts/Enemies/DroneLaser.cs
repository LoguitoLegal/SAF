using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneLaser : MonoBehaviour
{
    [SerializeField] private float growthSpeed;
    [SerializeField] private float maxLaserSize;

    private float currentLaserSize = 0f;

    void Update()
    {
        // Aumenta gradualmente o tamanho do laser apenas na direção local Z
        currentLaserSize += growthSpeed * Time.deltaTime;
        currentLaserSize = Mathf.Clamp(currentLaserSize, 0f, maxLaserSize);

        // Atualiza a escala local do laser apenas na direção Z
        Vector3 newLocalScale = transform.localScale;
        newLocalScale.z = currentLaserSize;
        transform.localScale = newLocalScale;
    }
}
