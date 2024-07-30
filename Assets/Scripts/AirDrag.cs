using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirDrag : MonoBehaviour
{
    [SerializeField] private KinematicEquations kinematicEquations;

    [SerializeField] private float airDragCoefficient;

    [SerializeField] private DirectionDimension dimension;

    [SerializeField] private OneDimensionMover oneDimensionMover;


    private void Update()
    {
        Main();
    }

    private void Main()
    {
        float initialVelocity = oneDimensionMover.FinalVelocityUI;

        float acceleration = Acceleration(initialVelocity);

        float finalVelocity = kinematicEquations.FinalVelocity_1(initialVelocity: initialVelocity, acceleration: acceleration, deltaTime: Time.deltaTime);

        float deltaX = DeltaX(finalVelocity);

        MoveObject(deltaX);
    }

    private float Acceleration(float initialVelocity)
    {
        return - initialVelocity * airDragCoefficient;
    }

    private float DeltaX(float finalVelocity)
    {
        float result = finalVelocity * Time.deltaTime;

        return result;
    }

    private void MoveObject(float deltaX)
    {
        this.transform.position += GetDirectionVector() * deltaX;
    }

    private Vector3 GetDirectionVector()
    {
        if (dimension == DirectionDimension.X)
            return new Vector3(1f, 0f, 0f);

        else if (dimension == DirectionDimension.Y)
            return new Vector3(0f, 1f, 0f);

        else if (dimension == DirectionDimension.Z)
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
