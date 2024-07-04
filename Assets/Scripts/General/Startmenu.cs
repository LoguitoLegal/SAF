using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Startmenu : MonoBehaviour
{
    public GameObject tutorial;
    void Start()
    {
        SoundManager.Instance.TocarBGMusic(0);
    }

    private void Update()
    {

        if (tutorial.activeSelf && Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Tutorial desativado");
            tutorial.SetActive(false);
        }
    }
}
