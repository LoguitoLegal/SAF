using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusController : MonoBehaviour
{
    public Image Image;
    public Sprite statusGood;
    public Sprite statusMid;
    public Sprite statusBad;

    public GameObject player;

    void Update()
    {
        if (player.GetComponent<Player>().healthPoints <= 40)
        {
            Image.sprite = statusBad;
        }
        else if (player.GetComponent<Player>().healthPoints <= 60)
        {
            Image.sprite = statusMid;
        }
        else
        {
            Image.sprite = statusGood;
        }
    }
}
