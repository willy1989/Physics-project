using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionInformation
{
    public float StaticFrictionCoefficient { get; private set; }
    public float KineticFrictionCoefficient { get; private set; }

    public Vector3 NormalVector { get; private set; }

    public CollisionInformation(float staticFrictionCoefficient, float kineticFrictionCoefficient, Vector3 normalVector)
    {
        StaticFrictionCoefficient = staticFrictionCoefficient;
        KineticFrictionCoefficient = kineticFrictionCoefficient;
        NormalVector = normalVector;
    }
}
