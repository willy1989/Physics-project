using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneDimensionMover : MonoBehaviour
{
    [SerializeField] private KinematicEquations kinematicEquations;

    [SerializeField] private AirDrag airDrag;

    [SerializeField] private float accelerationA;

    private float finalVelocity;

    public float FinalVelocity => finalVelocity;

    [SerializeField] private DirectionDimension dimension;

    private Vector3 directionVector;

    private Transform objectToMove;

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
        float initialVelocity = finalVelocity;

        float deltaVA = kinematicEquations.FinalVelocity_1(initialVelocity: initialVelocity, acceleration: accelerationA, deltaTime: Time.deltaTime) - initialVelocity;

        float airDragAcceleration = airDrag.GetAirDragAcceleration(finalVelocity);

        float deltaVB = kinematicEquations.FinalVelocity_1(initialVelocity: initialVelocity, acceleration: airDragAcceleration, deltaTime: Time.deltaTime) - initialVelocity;

        finalVelocity += deltaVA + deltaVB;

        float deltaX = kinematicEquations.DeltaX_2(finalVelocity: finalVelocity, initialVelocity: initialVelocity, deltaTime: Time.deltaTime);

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


