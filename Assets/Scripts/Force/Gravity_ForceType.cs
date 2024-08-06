using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity_ForceType : ForceType
{
    [SerializeField] private OneDimensionForceApplier oneDimensionForceApplier;

    public override Vector3 Force()
    {
        return Vector3.down * 9.81f * oneDimensionForceApplier.Mass;
    }
}
