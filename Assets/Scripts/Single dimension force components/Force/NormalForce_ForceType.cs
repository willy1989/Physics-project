using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalForce_ForceType : ForceType
{
    [SerializeField] private Gravity_ForceType gravityForceType;

    public override Vector3 Force()
    {
        Vector3 result = -gravityForceType.Force();

        return result;
    }
}
