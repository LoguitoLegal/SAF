using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageFlash : MonoBehaviour
{
    public MeshRenderer meshRenderer;
    public Color originColor;
    public float flashTime = 0.15f;
    public Player player;
    public Enemy enemy;
    public BombPlane bombPlane;
    public bool isPlayer;
    public bool isEnemy;
    public bool isBoss;

    void Start()
    {
        if (gameObject.tag == "Player")
        {
            isPlayer = true;
        }
        else if (gameObject.tag == "Enemy" || gameObject.tag == "BossGun")
        {
            isEnemy = true;
        }
        else if (gameObject.tag == "Boss")
        {
            isBoss = true;
        }
        if (meshRenderer == null)
        {
            meshRenderer = GetComponent<MeshRenderer>();
        }
        originColor = meshRenderer.material.color;
    }

    void Update()
    {
        if (isPlayer && player._tookDamage)
        {
            FlashStart();

        }
        else if (isEnemy && enemy.tookDamage)
        {
            FlashStart();
        }
        else if (isBoss && bombPlane.tookDamage)
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
