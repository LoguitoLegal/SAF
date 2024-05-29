using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageFlash : MonoBehaviour
{
    public MeshRenderer meshRenderer;
    public Color originColor;
    public float flashTime = 0.15f;
    public Player player;
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        originColor = meshRenderer.material.color;
    }

    void Update()
    {
        if (player._tookDamage)
        {
            FlashStart();
        }
    }

    public void FlashStart()
    {
        meshRenderer.material.color = Color.red;
        Invoke("FlashStop", flashTime);
    }

    public void FlashStop()
    {
        meshRenderer.material.color = originColor;
    }
}
