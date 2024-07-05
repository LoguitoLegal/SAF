using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Startmenu : MonoBehaviour
{
    public GameObject main;
    public GameObject tutorial;
    void Start()
    {
        SoundManager.Instance.TocarBGMusic(0);
    }

    private void Update()
    {
        if (tutorial.activeSelf == true && Input.GetKeyDown(KeyCode.Escape))
        {
            tutorial.SetActive(false);
            main.SetActive(true);       
        }
    }
}
