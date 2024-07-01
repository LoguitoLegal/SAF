using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PinguinAnimator : MonoBehaviour
{
    public float duration;

    [SerializeField] private Sprite[] spritesGood;
    [SerializeField] private Sprite[] spritesBad;
    [SerializeField] private Sprite[] spritesMedium;

    public bool isGood = true;
    public bool isMedium = false;
    public bool isBad = false;

    private Image image;
    private int index = 0;
    private float timer = 0;

    void Start()
    {
        image = GetComponent<Image>();
    }
    private void Update()
    {
        if ((timer += Time.deltaTime) >= (duration / spritesGood.Length))
        {
            timer = 0;
            if (isGood)
            {
                image.sprite = spritesGood[index];
            }
            else if(isMedium)
            {
                image.sprite = spritesMedium[index];
            }
            else if (isBad)
            {
                image.sprite = spritesBad[index];
            }
            
            index = (index + 1) % spritesGood.Length;
        }
    }
}
