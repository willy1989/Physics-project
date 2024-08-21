using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneDimensionAccelerationMover : MonoBehaviour
{
    [SerializeField] private KinematicEquations kinematicEquations;

    [SerializeField] private float acceleration;

    [SerializeField] private float airDragCoefficient;

    private float finalVelocity;

    public float FinalVelocity => finalVelocity;

    [SerializeField] private DirectionDimension dimension;

    private void Update()
    {
        float initialVelocity = finalVelocity;

        float totalAcceleration = acceleration + AirDragAcceleration(initialVelocity);

        finalVelocity = kinematicEquations.FinalVelocity_1(initialVelocity: initialVelocity, acceleration: totalAcceleration, deltaTime: Time.deltaTime);

        float deltaX = kinematicEquations.DeltaX_2(finalVelocity: finalVelocity, initialVelocity: initialVelocity, deltaTime: Time.deltaTime);

        MoveObject(deltaX);
    }

    private float AirDragAcceleration(float initialVelocity)
    {
        return -initialVelocity * airDragCoefficient;
    }

    private void MoveObject(float deltaX)
    {
        this.transform.position += GetDirectionVector() * deltaX;
    }

    private Vector3 GetDirectionVector()
    {
        if (dimension == DirectionDimension.X)
            return new Vector3(1f, 0f, 0f);

        else if(dimension == DirectionDimension.Y)
            return new Vector3(0f, 1f, 0f);

        else if(dimension == DirectionDimension.Z)
            return new Vector3(0f, 0f, 1f);

        Debug.LogError("Couldn't map DirectionDimension enum to a direction.");

        return Vector3.zero;
    }

    private enum DirectionDimension
    {
        X,
        Y,
        Z
    }
}


