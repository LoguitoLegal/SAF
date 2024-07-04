using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class CutsceneController : MonoBehaviour
{
    public VideoPlayer video;
    public int nextScene;
    private double time;
    private float startTime;

    private void Start()
    {
        Time.timeScale = 1.0f;
        if (SceneManager.GetActiveScene().buildIndex == 5)
        {
            SoundManager.Instance.TocarBGMusic(6);
            Invoke("StopMusic", 6);
        }
        else
        {
            SoundManager.Instance.TocarBGMusic(5);
        }
        time = video.clip.length;
        startTime = Time.time;
    }
    void Update()
    {
        if (Time.time - startTime >= time + 2)
        {
            Debug.Log("Proxima fase");
            SceneManager.LoadScene(nextScene);
        }        

        if (Input.GetKey(KeyCode.Return))
        {
            Debug.Log("Proxima fase");
            SceneManager.LoadScene(nextScene);
        }
    }

    void StopMusic()
    {
        SoundManager.Instance.TocarBGMusic(7);
    }
}
