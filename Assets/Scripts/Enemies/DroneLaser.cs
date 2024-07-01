using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneLaser : MonoBehaviour
{
    [SerializeField] private float growthSpeed;
    [SerializeField] private float maxLaserSize;
    public bool isBossDrone = false;

    private float currentLaserSize = 0f;

    private void Start()
    {
        if (!isBossDrone)
        {
            GetComponent<Parallax>().enabled = true;
        }
    }

    void Update()
    {
        // Aumenta gradualmente o tamanho do laser apenas na direção local Z
        currentLaserSize += growthSpeed * Time.deltaTime;
        currentLaserSize = Mathf.Clamp(currentLaserSize, 0f, maxLaserSize);

        // Atualiza a escala local do laser apenas na direção Z
        Vector3 newLocalScale = transform.localScale;
        newLocalScale.x = currentLaserSize;
        transform.localScale = newLocalScale;
    }
}
