using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticFriction_ForceType : ForceType
{
    [SerializeField] private NormalForce_ForceType normalForceType;

    [SerializeField] private Constant_ForceType constantForceType;

    [SerializeField] private float staticFrictionCoefficient;

    public override Vector3 Force()
    {
        float fsMax = (normalForceType.Force() * staticFrictionCoefficient).magnitude;

        Vector3 constantForce = constantForceType.Force();

        Vector3 result = Vector3.zero;

        if (constantForce.magnitude <= fsMax)
        {
            result = -constantForce;
        }

        else
        {
            result = Vector3.zero;
        }

        return result;
    }
}
