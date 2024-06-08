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
    public BombPlane Boss1;
    [SerializeField] private TMP_Text result;
    public GameObject endPanel;
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
    private bool isTransitioning = false;

    //public GameObject enemy;
    void Start()
    {
        player = GameObject.Find("Player").gameObject;
        Time.timeScale = 1f;
        currentLevel = SceneManager.GetActiveScene();
    }

    // Update is called once per frame
    void Update()
    {
        if (Camera.main.transform.position.z >= bossPoint.position.z && !reachedBossPoint && !isTransitioning)
        {
            Debug.Log("Chegou no BossPoint");
            reachedBossPoint = true;
            StartCoroutine(TransitionToBossMusic());
        }

        if (Camera.main.transform.position.z >= 60.77 && !bombPlane.startBattle)
        {
            bombPlane.startBattle = true;
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
        if (Boss1 != null && Boss1.battleEnd)
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
        result.text = "VITÓRIA!";
        result.color = Color.green;
        endPanel.SetActive(true);
        this.player.GetComponent<Player>().points = 100;
    }

    private void OnLose()
    {
        Time.timeScale = 0f;
        result.text = "DERROTA...";
        result.color = Color.red;
        endPanel.SetActive(true);
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
