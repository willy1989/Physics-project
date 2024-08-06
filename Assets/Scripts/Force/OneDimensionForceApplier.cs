using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneDimensionForceApplier : MonoBehaviour
{
    [SerializeField] private KinematicEquations kinematicEquations;

    [SerializeField] private StopClock stopClock;

    [SerializeField] private Constant_ForceType constantForceType;

    [SerializeField] private Drag_ForceType dragForceType;

    [SerializeField] private float mass;

    private float finalVelocity;

    public float FinalVelocity => finalVelocity;

    [SerializeField] private DirectionDimension dimension;

    private void Awake()
    {
        stopClock.clockStopedEvent += LogFinalVelocity;
    }

    private void Update()
    {
        // F = m * a, so a = F/m
        float initialVelocity = finalVelocity;

        float combinedForces = constantForceType.Force() - dragForceType.Force();
        float acceleration = combinedForces / mass;

        finalVelocity = kinematicEquations.FinalVelocity_1(initialVelocity, acceleration, Time.deltaTime);

        float displacement = kinematicEquations.DeltaX_2(finalVelocity, initialVelocity, Time.deltaTime);

        transform.position += GetDirectionUnitVector() * displacement;

    }

    private void LogFinalVelocity()
    {
        Debug.Log("Final velocity: " + finalVelocity);
    }

    private Vector3 GetDirectionUnitVector()
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
