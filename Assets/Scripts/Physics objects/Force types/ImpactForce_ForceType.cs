using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PhysicsObject
{
    public class ImpactForce_ForceType : ForceType
    {
        Vector3 finalVelocity;

        Vector3 surfaceNormal;

        float mass;

        public ImpactForce_ForceType(Vector3 _finalVelocity, float _mass, Vector3 _surfaceNormal)
        {
            this.finalVelocity = _finalVelocity;
            this.surfaceNormal = _surfaceNormal;
            this.mass = _mass;
        }

        public override Vector3 Force()
        {
            if (finalVelocity.magnitude <= 0)
                return Vector3.zero;

            // Angle between surface normal and push force
            float angleRadians = AngleInRadiansFromVectors(finalVelocity, -surfaceNormal);

            float normalForceMagnitude = Mathf.Cos(angleRadians) * finalVelocity.magnitude;

            Vector3 normalVector = surfaceNormal * normalForceMagnitude;

            Vector3 result = normalVector * mass / Time.deltaTime;

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
}
