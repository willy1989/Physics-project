using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantForceController : MonoBehaviour
{
    [SerializeField] private Vector3 constantForce;

    public Vector3 ConstantForce => constantForce;
}
