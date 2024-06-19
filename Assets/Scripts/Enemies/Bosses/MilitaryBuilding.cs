using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MilitaryBuilding : MonoBehaviour
{
    public bool startBattle = false;
    public bool isPhaseTwo = false;
    public bool canTakeDamage = false;
    public bool battleEnd;
    public bool animController = false;
    private Animator animator;
    private Enemy enemy;

    [Header("GUNS")]
    public bool canShoot = false;
    public GameObject[] guns;
    public DroneCaller[] antennas;
    private void Start()
    {
        animator = GetComponent<Animator>();
        enemy = GetComponent<Enemy>();
    }
    void Update()
    {
        if (startBattle)
        {
            if (isPhaseTwo == false)
            {
                if (animController == false)
                {
                    Debug.Log("Guns 1 start");
                    StartCoroutine(weaponsWave("Guns1"));
                    for (int i = 0; i < antennas.Length; i++)
                    {
                        antennas[i].canCall = true;
                        antennas[i].callCooldown.StartCooldown();
                    }
                    Camera.main.GetComponent<Parallax>().speed = 0f;
                    animController = true;
                }
                if (canShoot)
                {
                    Debug.Log("Fase 1");
                    for (int i = 0; i < 3; i++)
                    {
                        guns[i].GetComponent<DualShoot>().canShoot = true;
                        guns[i].GetComponent<BoxCollider>().enabled = true;
                    }

                    canShoot = false;
                }
            }
            else if (isPhaseTwo == true)
            {
                if (animController == true)
                {
                    Debug.Log("Guns 2 start");
                    StartCoroutine(weaponsWave("Guns2"));
                    animController = false;
                }
                if (canShoot)
                {
                    Debug.Log("Fase 2");
                    for (int i = 3; i < 5; i++)
                    {
                        guns[i].GetComponent<DualShoot>().canShoot = true;
                        guns[i].GetComponent<BoxCollider>().enabled = true;
                    }

                    guns[5].GetComponent<AntiAircraft>().canShoot = true;
                    guns[5].GetComponent<BoxCollider>().enabled = true;

                    canShoot = false;
                }
            }
            BattleProgression();
        }

    }

    public void BattleProgression()
    {
        if (guns[0] == null && guns[1] == null && guns[2] == null)
        {
            isPhaseTwo = true;
        }

        if (guns[3] == null && guns[4] == null && guns[5] == null)
        {
            canTakeDamage = true;
            enemy.enabled = true;

            foreach (BoxCollider c in GetComponents<BoxCollider>())
            {
                c.enabled = true;
            }

            if (enemy.healthPoints <= 0)
            {
                battleEnd = true;
            }
        }
    }

    IEnumerator weaponsWave(string animacao)
    {
        animator.Play(animacao);

        yield return new WaitForSeconds(1.5f);

        Debug.Log("Gun anim ended");
        canShoot = true;
    }
}
