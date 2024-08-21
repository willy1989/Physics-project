using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PhysicsObject
{
    public class KineticFriction_ForceType : ForceType
    {
        private float kineticFrictionCoefficient;

        private NormalForce_ForceType normalForce;

        private PhysicsObject physicsObject;

        public KineticFriction_ForceType(float _kineticFrictionCoefficient, NormalForce_ForceType _normalForce, PhysicsObject _physicsObject)
        {
            this.kineticFrictionCoefficient = _kineticFrictionCoefficient;
            this.normalForce = _normalForce;
            this.physicsObject = _physicsObject;
        }

        public override Vector3 Force()
        {
            float kineticFrictionForceMagnitude = normalForce.Force().magnitude * kineticFrictionCoefficient;

            Vector3 directionVector = -physicsObject.FinalVelocity.normalized;

            Vector3 result = directionVector * kineticFrictionForceMagnitude;

            return result;
        }
    }
}
