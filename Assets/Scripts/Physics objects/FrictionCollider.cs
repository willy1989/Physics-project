using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrictionCollider : MonoBehaviour
{
    [SerializeField] private float kinematicFrictionCoefficient;

    [SerializeField] private float staticFrictionCoefficient;

    public float KineticFrictionCoefficient()
    {
        return kinematicFrictionCoefficient;
    }

    public float StaticFrictionCoefficient()
    {
        return staticFrictionCoefficient;
    }
}
