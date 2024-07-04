using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu, mainMenu;
    public GameManager gameManager;
    public GameObject[] subMenus;
    public GameObject tutorial;
    public static bool isPaused;
    private bool wentByCheat = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused && !tutorial.activeSelf)
            {
                ResumeGame();
            }
            else if (isPaused && tutorial.activeSelf)
            {
                tutorial.SetActive(false);
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        if (!gameManager.endPanel.activeSelf)
        {
            pauseMenu.SetActive(true);
            foreach (GameObject submenu in subMenus)
            {
                submenu.SetActive(false);
            }
            mainMenu.SetActive(true);
            Time.timeScale = 0f;
            isPaused = true;
        }
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void DesactivateMenu(GameObject menu)
    {
        menu.SetActive(false);
    }
    public void ActivateMenu(GameObject target)
    {
        target.SetActive(true);
    }

    public void LoadScene(int scene)
    {
        SceneManager.LoadScene(scene);
        isPaused = false;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void OnButtonClick()
    {
        SoundManager.Instance.TocarSFX(7);
    }

    public void ButtonHover()
    {
        SoundManager.Instance.TocarSFX(0);
    }

    public void GoToBoss()
    {
        if (wentByCheat == false)
        {
            //Desativa todos os inimigos
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

            for (int i = 0; i < enemies.Length; i++)
            {
                enemies[i].SetActive(false);
            }

            //Teleporta
            Vector3 teleport = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, GameObject.Find("BossPoint").transform.position.z);
            Camera.main.transform.position = teleport;
            wentByCheat = true;
        }
    }

    public void UnlockAll()
    {
        GameObject.Find("Player").GetComponent<Player>().points = 9999;
    }
}
