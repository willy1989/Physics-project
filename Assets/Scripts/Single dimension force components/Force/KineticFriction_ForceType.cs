using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KineticFriction_ForceType : ForceType
{
    [SerializeField] private float kineticFrictionCoefficient;

    [SerializeField] private NormalForce_ForceType normalForceType;

    [SerializeField] private OneDimensionForceApplier oneDimensionForceApplier;

    public override Vector3 Force()
    {
        float kineticFrictionForceMagnitude = normalForceType.Force().magnitude * kineticFrictionCoefficient;

        Vector3 directionVector = -oneDimensionForceApplier.FinalVelocity.normalized;

        Vector3 result = directionVector * kineticFrictionForceMagnitude;

        return result;
    }
}
