using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Credits : MonoBehaviour
{
    public AnimationClip animator;
    private float totalTime;
    private float actualTime;
    private void Start()
    {
        SoundManager.Instance.TocarBGMusic(7);
        Time.timeScale = 1f;
        actualTime = Time.time;
        totalTime = animator.length;
    }
    void Update()
    {
        if (Time.time - actualTime >= totalTime + 1.5f)
        {
            SceneManager.LoadScene(0);
        }
    }
}
