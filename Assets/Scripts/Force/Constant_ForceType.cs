using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constant_ForceType : ForceType
{
    [SerializeField] private float force;

    public override float Force()
    {
        return force;
    }
}
