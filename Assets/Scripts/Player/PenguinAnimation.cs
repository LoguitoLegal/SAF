using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PenguinAnimation : MonoBehaviour
{
    public float duration;

    [SerializeField] private Sprite[] spritesGood;
    [SerializeField] private Sprite[] spritesMedium;
    [SerializeField] private Sprite[] spritesBad;
    public enum Estado
    {
        Good,
        Medium,
        Bad
    }

    public Estado estadoAtual;

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
            switch (estadoAtual)
            {
                case Estado.Good:
                    image.sprite = spritesGood[index];
                    index = (index + 1) % spritesGood.Length;
                    break;

                case Estado.Medium:
                    image.sprite = spritesMedium[index];
                    index = (index + 1) % spritesMedium.Length;
                    break;

                case Estado.Bad:
                    image.sprite = spritesBad[index];
                    index = (index + 1) % spritesBad.Length;
                    break;
            }


        }
    }
}
