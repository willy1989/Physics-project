using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PhysicsObject
{
    public class KineticFriction_ForceType : ForceType
    {
        private float kineticFrictionCoefficient;

        private Vector3 normalForce;

        private Vector3 direction;

        public KineticFriction_ForceType(float _kineticFrictionCoefficient, Vector3 _normalForce, Vector3 _direction)
        {
            this.kineticFrictionCoefficient = _kineticFrictionCoefficient;
            this.normalForce = _normalForce;
            this.direction = _direction;
        }

        public override Vector3 Force()
        {
            float kineticFrictionForceMagnitude = normalForce.magnitude * kineticFrictionCoefficient;

            Vector3 directionVector = -direction.normalized;

            Vector3 result = directionVector * kineticFrictionForceMagnitude;

            return result;
        }
    }
}
