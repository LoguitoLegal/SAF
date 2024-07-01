using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusController : MonoBehaviour
{
    public Image Image;

    public PinguinAnimator pinguinAnim;

    public GameObject player;

    void Update()
    {
        if (player.GetComponent<Player>().healthPoints <= 40)
        {
            pinguinAnim.isGood = false;
            pinguinAnim.isMedium = false;
            pinguinAnim.isBad = true;

        }
        else if (player.GetComponent<Player>().healthPoints <= 60)
        {
            pinguinAnim.isGood = false;
            pinguinAnim.isMedium = true;
            pinguinAnim.isBad = false;

        }
        else
        {
            pinguinAnim.isGood = true;
            pinguinAnim.isMedium = false;
            pinguinAnim.isBad = false;

        }
    }
}
