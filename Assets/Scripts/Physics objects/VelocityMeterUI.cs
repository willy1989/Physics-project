using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace PhysicsObject
{
    public class VelocityMeterUI : MonoBehaviour
    {
        [SerializeField] private PhysicsObject physicsObject;

        [SerializeField] private TextMeshProUGUI speedMeter;

        private void Update()
        {
            UpdateSpeedMeter();
        }

        private void UpdateSpeedMeter()
        {
            speedMeter.text = physicsObject.FinalVelocity.magnitude.ToString("F2") + " m/s";
        }
    }
}
