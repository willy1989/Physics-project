using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MovementUI : MonoBehaviour
{
    [SerializeField] private MoveModule moveModule;

    [SerializeField] private TextMeshProUGUI speedMeter;

    private void Update()
    {
        UpdateSpeedMeter();
    }

    private void UpdateSpeedMeter()
    {
        speedMeter.text = moveModule.FinalVelocity.ToString("F2") + " m/s";
    }
}
