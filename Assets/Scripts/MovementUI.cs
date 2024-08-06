using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MovementUI : MonoBehaviour
{
    [SerializeField] private OneDimensionForceApplier oneDimensionForceApplier;

    [SerializeField] private TextMeshProUGUI speedMeter;

    private void Update()
    {
        UpdateSpeedMeter();
    }

    private void UpdateSpeedMeter()
    {
        speedMeter.text = oneDimensionForceApplier.FinalVelocity.ToString("F2") + " m/s";
    }
}
