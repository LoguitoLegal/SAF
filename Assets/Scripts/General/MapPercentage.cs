using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MapPercentage : MonoBehaviour
{
    public TMP_Text progressText;
    public Transform cameraTransform;
    public Transform endLineTransform;
    public Vector3 endLinePosition;
    private float fullDistance;
    private bool canContinue = true;

    void Start()
    {
        endLinePosition = endLineTransform.position;
        fullDistance = GetDistance();

    }

    void Update()
    {
        if (canContinue)
        {
            float newDistance = GetDistance();
            float progressValue = 1 - (newDistance / fullDistance);

            UpdateProgress(progressValue);
        }
        
    }

    void UpdateProgress(float value)
    {
        int percentage = Mathf.RoundToInt(value * 100);
        if (percentage >= 100)
        {
            canContinue = false;
        }
        progressText.text = percentage.ToString() + "%";
    }

    float GetDistance()
    {
        return (endLinePosition.z - Camera.main.transform.position.z);
    }
}
