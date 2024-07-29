using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirDrag : MonoBehaviour
{
    [SerializeField] private float airDragCoefficient;

    public float GetAirDragAcceleration(float finalVelocity)
    {
        return - finalVelocity * airDragCoefficient;
    }
}
