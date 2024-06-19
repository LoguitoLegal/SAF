using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[ExecuteInEditMode()]
public class PointsBar : MonoBehaviour
{
    public int maximum;
    public int current;
    public Image mask;

    // Update is called once per frame
    void Update()
    {
        GetCurrentFill();
    }

    void GetCurrentFill()
    {
        float fill = (float)current / (float)maximum;
        mask.fillAmount = fill;
    }

    public void SetCurrentFill(int value)
    {
        current = value;
    }
}
