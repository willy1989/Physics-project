using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrictionCollider : MonoBehaviour
{
    [SerializeField] private float frictionCoefficient;

    public float FrictionCoefficient()
    {
        return frictionCoefficient;
    }
}
