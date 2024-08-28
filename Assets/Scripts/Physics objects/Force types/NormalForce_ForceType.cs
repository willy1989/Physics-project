using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PhysicsObject
{
    public class NormalForce_ForceType : ForceType
    {
        private Vector3 pushForce;

        private Vector3 surfaceNormal;

        public NormalForce_ForceType(Vector3 _pushForce, Vector3 _surfaceNormal)
        {
            this.pushForce = _pushForce;
            this.surfaceNormal = _surfaceNormal;
        }

        public override Vector3 Force()
        {
            // Angle between surface normal and push force
            float angleRadians = AngleInRadiansFromVectors(pushForce, -surfaceNormal);

            float normalForceMagnitude = Mathf.Cos(angleRadians) * pushForce.magnitude;

            Vector3 result = surfaceNormal * normalForceMagnitude;

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
