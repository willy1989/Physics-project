using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceCalculator : MonoBehaviour
{
    public Vector3 ConstantForce(float magnitude, Vector3 direction)
    {
        return direction.normalized * magnitude;
    }

    public Vector3 NormalForce(Vector3 pushForce, Vector3 surfaceNormal)
    {
        float angleRadians = AngleInRadiansFromVectors(pushForce, -surfaceNormal);

        float anglesDegrees = Mathf.Rad2Deg * angleRadians;

        if (anglesDegrees > 90)
            return Vector3.zero;

        float normalForceMagnitude = Mathf.Cos(angleRadians) * pushForce.magnitude;

        Vector3 result = surfaceNormal * normalForceMagnitude;

        return result;
    }

    public Vector3 ImpactForce(Vector3 finalVelocity, float mass, Vector3 surfaceNormal)
    {
        if (finalVelocity.magnitude <= 0)
            return Vector3.zero;

        float angleRadians = AngleInRadiansFromVectors(finalVelocity, -surfaceNormal);

        float normalForceMagnitude = Mathf.Cos(angleRadians) * finalVelocity.magnitude;

        Vector3 normalVector = surfaceNormal * normalForceMagnitude;

        Vector3 result = normalVector * mass / Time.deltaTime;

        return result;
    }

    public Vector3 KineticFrictionForce(float kineticFrictionCoefficient, Vector3 normalForce, Vector3 movementDirection)
    {
        float kineticFrictionForceMagnitude = normalForce.magnitude * kineticFrictionCoefficient;

        Vector3 directionVector = -movementDirection.normalized;

        Vector3 result = directionVector * kineticFrictionForceMagnitude;

        return result;
    }

    public Vector3 StaticFrictionForce(float fsMax, Vector3 pushForce)
    {
        if (fsMax >= pushForce.magnitude)
            return -pushForce;
        else
            return Vector3.zero;
    }

    public float FsMax(Vector3 normalForce, float staticFrictionCoefficient)
    {
        float result = (normalForce * staticFrictionCoefficient).magnitude;

        return result;
    }

    private float AngleInRadiansFromVectors(Vector3 vectorA, Vector3 vectorB)
    {
        float dotProduct = vectorA.x * vectorB.x + vectorA.y * vectorB.y + vectorA.z * vectorB.z;

        float multipliedMagnitudes = vectorA.magnitude * vectorB.magnitude;

        float result = Mathf.Acos(dotProduct / multipliedMagnitudes);

        return result;
    }
}
