using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusController : MonoBehaviour
{
    public Image Image;
    public PenguinAnimation penguinAnimation;
    public GameObject player;

    void Update()
    {
        if (player.GetComponent<Player>().healthPoints <= 40)
        {
            penguinAnimation.estadoAtual = PenguinAnimation.Estado.Bad;
        }
        else if (player.GetComponent<Player>().healthPoints <= 60)
        {
            penguinAnimation.estadoAtual = PenguinAnimation.Estado.Medium;
        }
        else
        {
            penguinAnimation.estadoAtual = PenguinAnimation.Estado.Good;
        }
    }
}
