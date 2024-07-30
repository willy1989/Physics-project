using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirDrag : MonoBehaviour
{
    [SerializeField] private KinematicEquations kinematicEquations;

    [SerializeField] private float airDragCoefficient;


    public float GetAirDragAcceleration(float initialVelocity)
    {
        return - initialVelocity * airDragCoefficient;
    }

    public float GetAirDragFinalVelocity(float initialVelocity)
    {
        float acceleration = GetAirDragAcceleration(initialVelocity);

        float result = kinematicEquations.FinalVelocity_1(initialVelocity: initialVelocity, acceleration: acceleration, deltaTime: Time.deltaTime);

        return result;
    }
}
