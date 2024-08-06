using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constant_ForceType : ForceType
{
    [SerializeField] private float force;

    [SerializeField] private DirectionDimension dimension;

    private Vector3 GetDirectionUnitVector()
    {
        if (dimension == DirectionDimension.XPOSITIVE)
            return new Vector3(1f, 0f, 0f);

        else if (dimension == DirectionDimension.YPOSITIVE)
            return new Vector3(0f, 1f, 0f);

        else if (dimension == DirectionDimension.ZPOSITIVE)
            return new Vector3(0f, 0f, 1f);

        else if (dimension == DirectionDimension.XNEGATIVE)
            return new Vector3(-1f, 0f, 0f);

        else if (dimension == DirectionDimension.YNEGATIVE)
            return new Vector3(0f, -1f, 0f);

        else if (dimension == DirectionDimension.YPOSITIVE)
            return new Vector3(0f, 0f, -1f);

        Debug.LogError("Couldn't map DirectionDimension enum to a direction.");

        return Vector3.zero;
    }

    private enum DirectionDimension
    {
        XPOSITIVE,
        YPOSITIVE,
        ZPOSITIVE,
        XNEGATIVE,
        YNEGATIVE,
        ZNEGATIVE
    }

    public override Vector3 Force()
    {
        return force * GetDirectionUnitVector();
    }
}
