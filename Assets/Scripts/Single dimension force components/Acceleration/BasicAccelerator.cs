using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAccelerator : MonoBehaviour
{
    [SerializeField] private KinematicEquations kinematicEquations;

    [SerializeField] private float acceleration;

    public float GetAcceleration()
    {
        return acceleration;
    }
}
