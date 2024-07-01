using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("EndScreen")]
    private GameObject player;
    public GameObject endPanel;
    public GameObject victoryPanel;
    public GameObject defeatPanel;

    private Scene currentLevel;

    [Header("Power-Ups")]
    [SerializeField] private Image imageShootSpeed;
    [SerializeField] private Image imageShootArea;
    [SerializeField] private Image imageShield;

    [Header("Others")]
    public Transform bossPoint;
    public bool reachedBossPoint = false;
    public int bossMusicID;
    public float transitionDuration = 4f;
    public BombPlane bombPlane;
    public MilitaryBuilding militaryBuilding;
    private bool isTransitioning = false;

    void Start()
    {
        player = GameObject.Find("Player").gameObject;
        Time.timeScale = 1f;
        currentLevel = SceneManager.GetActiveScene();
    }

    void Update()
    {
        if (Camera.main.transform.position.z >= bossPoint.position.z && !reachedBossPoint && !isTransitioning)
        {
            Debug.Log("Chegou no BossPoint");
            reachedBossPoint = true;
            StartCoroutine(TransitionToBossMusic());
        }

        if (currentLevel.buildIndex == 1)
        {
            if (Camera.main.transform.position.z >= 60.77 && !bombPlane.startBattle)
            {
                bombPlane.startBattle = true;
            }
        }
        else if (currentLevel.buildIndex == 2)
        {
            if (Camera.main.transform.position.z >= 50.45 && !militaryBuilding.startBattle)
            {
                militaryBuilding.startBattle = true;
                Camera.main.GetComponent<Parallax>().enabled = false;
            }
        }

        if (VerifyWin())
        {
            OnWin();
        }
        if (VerifyLose())
        {
            OnLose();
        }
    }

    private bool VerifyWin()
    {
        if (bombPlane != null && bombPlane.battleEnd || militaryBuilding == null && SceneManager.GetActiveScene().buildIndex == 2)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private bool VerifyLose()
    {
        if (player.GetComponent<Player>().healthPoints <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void OnWin()
    {
        Time.timeScale = 0f;
        endPanel.SetActive(true);
        victoryPanel.SetActive(true);
    }

    private void OnLose()
    {
        Time.timeScale = 0f;
        endPanel.SetActive(true);
        defeatPanel.SetActive(true);
        this.player.GetComponent<Player>().healthPoints = 0;
    }

    public void ResetLevel()
    {
        SceneManager.LoadScene(currentLevel.buildIndex);
    }

    private IEnumerator TransitionToBossMusic()
    {
        isTransitioning = true;
        float originalVolume = SoundManager.Instance.PegarVolumeBGSalvo();
        float currentVolume = originalVolume;

        //Diminui o volume da música atual com o tempo
        for (float t = 0; t < transitionDuration; t += Time.deltaTime)
        {
            float normalizedTime = t / transitionDuration;
            currentVolume = Mathf.Lerp(originalVolume, 0, normalizedTime);
            SoundManager.Instance.AtualizarVolumeBG(currentVolume);
            yield return null;
        }

        SoundManager.Instance.AtualizarVolumeBG(0);

        SoundManager.Instance.TocarBGMusic(bossMusicID);

        //Aumenta o volume da música de boss com o tempo
        for (float t = 0; t < transitionDuration; t += Time.deltaTime)
        {
            float normalizedTime = t / transitionDuration;
            currentVolume = Mathf.Lerp(0, originalVolume, normalizedTime);
            SoundManager.Instance.AtualizarVolumeBG(currentVolume);
            yield return null;
        }

        SoundManager.Instance.AtualizarVolumeBG(originalVolume);
        isTransitioning = false; 
    }
}
