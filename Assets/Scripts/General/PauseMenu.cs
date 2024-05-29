using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu, mainMenu;
    public GameManager gameManager;
    public GameObject[] subMenus;
    public static bool isPaused;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
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
}
