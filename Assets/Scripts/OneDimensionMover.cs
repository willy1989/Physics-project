using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneDimensionMover : MonoBehaviour
{
    [SerializeField] private KinematicEquations kinematicEquations;

    [SerializeField] private Accelerator accelerator;

    [SerializeField] private AirDrag airDrag;

    private float finalVelocity;

    public float FinalVelocity => finalVelocity;

    [SerializeField] private DirectionDimension dimension;

    private void Update()
    {
        MoveObject();
    }

    private void MoveObject()
    {
        float initialVelocity = finalVelocity;

        float acceleration = accelerator.GetAcceleration() + airDrag.GetAirDragAcceleration(initialVelocity);

        finalVelocity = kinematicEquations.FinalVelocity_1(initialVelocity: initialVelocity, acceleration: acceleration, deltaTime: Time.deltaTime);

        float deltaX = kinematicEquations.DeltaX_2(finalVelocity: finalVelocity, initialVelocity: initialVelocity, deltaTime: Time.deltaTime);

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


