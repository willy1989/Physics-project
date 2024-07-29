using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneDimensionMover : MonoBehaviour
{
    [SerializeField] private KinematicEquations kinematicEquations;

    [SerializeField] private float acceleration;

    [SerializeField] private DirectionDimension dimension;

    private Vector3 directionVector;

    private Transform objectToMove;

    public float FinalVelocity { get; private set; }

    private void Awake()
    {
        objectToMove = this.transform;

        directionVector = GetDirectionVector();
    }

    private void Update()
    {
        MoveObject();
    }

    private void MoveObject()
    {
        float initialVelocity = FinalVelocity;

        FinalVelocity = kinematicEquations.FinalVelocity_1(initialVelocity: initialVelocity, acceleration: acceleration, deltaTime: Time.deltaTime);

        float deltaX = kinematicEquations.DeltaX_2(finalVelocity: FinalVelocity, initialVelocity: initialVelocity, deltaTime: Time.deltaTime);

        objectToMove.position += directionVector * deltaX;
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


