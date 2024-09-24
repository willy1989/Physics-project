using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Direction_ConstantForceController : ConstantForceController
{
    [SerializeField] private Vector3 directionVector;

    [SerializeField] private float force;

    public override Vector3 ConstantForce()
    {
        return directionVector.normalized * force;
    }

    public void SetUp(Vector3 directionVector, float force)
    {
        this.directionVector = directionVector;
        this.force = force;
    }
}
