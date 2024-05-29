using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("EndScreen")]
    private GameObject player;
    [SerializeField] private TMP_Text result;
    public GameObject endPanel;
    private Scene currentLevel;

    [Header("Power-Ups")]
    [SerializeField] private Image imageShootSpeed;
    [SerializeField] private Image imageShootArea;
    [SerializeField] private Image imageShield;

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

        //if (VerifyWin())
        //{
        //    OnWin();
        //}
        if (VerifyLose())
        {
            OnLose();
        }
    }

    //private bool VerifyWin()
    //{
    //    if (player.GetComponent<Player>().points >= 100)
    //    {
    //        return true;
    //    }
    //    else
    //    {
    //        return false;
    //    }
    //}

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
}
