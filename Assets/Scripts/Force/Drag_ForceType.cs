using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drag_ForceType : ForceType
{
    [SerializeField] private OneDimensionForceApplier oneDimensionForceApplier;

    [SerializeField] private float dragCoefficient;

    public override float Force()
    {
        return oneDimensionForceApplier.FinalVelocity * dragCoefficient;
    }
}