using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantForceController : MonoBehaviour
{
    [SerializeField] private Vector3 direction;

    [SerializeField] private float newtons;

    public Vector3 Force()
    {
        if(Input.GetKey(KeyCode.Space))
            return direction.normalized * newtons;
        return Vector3.zero;
    }
}
