using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SlidersUI : MonoBehaviour
{
    public bool isSfx;
    public TMP_Text textoFeedbackValor;
    public Slider slider;


    void Awake()
    {
        if (isSfx)
            AtualizarTextoSliderVolumeSFX(SoundManager.Instance.PegarVolumeSFXSalvo());
        else
            AtualizarTextoSliderVolumeBG(SoundManager.Instance.PegarVolumeBGSalvo());
    }

    public void AtualizarTextoSliderVolumeBG(float valor)
    {
        textoFeedbackValor.text = (valor * 100f).ToString("F0") + "%";
        slider.value = valor;
        SoundManager.Instance.AtualizarVolumeBG(valor);
    }

    public void AtualizarTextoSliderVolumeSFX(float valor)
    {
        textoFeedbackValor.text = (valor * 100f).ToString("F0") + "%";
        slider.value = valor;
        SoundManager.Instance.AtualizarVolumeSFX(valor);

    }

}
