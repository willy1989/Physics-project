using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Accelerator : MonoBehaviour
{
    [SerializeField] private KinematicEquations kinematicEquations;

    [SerializeField] private float acceleration;

    public float GetAcceleration()
    {
        return acceleration;
    }

    public float FinalVelocity(float initialVelocity)
    {
        return kinematicEquations.FinalVelocity_1(initialVelocity: initialVelocity, acceleration: acceleration, deltaTime: Time.deltaTime);
    }
}
