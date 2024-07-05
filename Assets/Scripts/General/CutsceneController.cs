using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class CutsceneController : MonoBehaviour
{
    public float extraTime;
    public int nextScene;
    private VideoClip video;
    private float actualTime;
    void Start()
    {
        Time.timeScale = 1f;
        video = GetComponent<VideoPlayer>().clip;
        actualTime = Time.time;
        if (SceneManager.GetActiveScene().buildIndex == 5)
        {
            SoundManager.Instance.TocarBGMusic(6);
            Invoke("StopMusic", 6.1f);
        }
        else
        {
            SoundManager.Instance.TocarBGMusic(5);
        }
    }

    void Update()
    {
        if (Time.time - actualTime >= video.length + extraTime)
        {
            SceneManager.LoadScene(nextScene);
        }

        else if (Input.GetKeyDown(KeyCode.Return))
        {
            SceneManager.LoadScene(nextScene);
        }
    }

    void StopMusic()
    {
        SoundManager.Instance.TocarBGMusic(7);
    }
}
