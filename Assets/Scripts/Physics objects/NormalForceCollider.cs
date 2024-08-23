using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalForceCollider : MonoBehaviour
{
    public Vector3 NormalVector()
    {
        Vector3 result = transform.up;

        return result;
    }
}

