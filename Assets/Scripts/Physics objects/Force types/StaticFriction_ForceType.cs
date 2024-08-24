using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PhysicsObject
{
    public class StaticFriction_ForceType : ForceType
    {
        private Vector3 normalForce; // Going opposite to the normal of the surface

        private float staticFrictionCoefficient;

        private Vector3 pushingForce; // Going perpendicular to the normal of the surface

        public StaticFriction_ForceType(Vector3 normalForce, float staticFrictionCoefficient, Vector3 pushForce)
        {
            this.normalForce = normalForce;
            this.staticFrictionCoefficient = staticFrictionCoefficient;
            this.pushingForce = pushForce;
        }

        public override Vector3 Force()
        {
            Vector3 fsMax = normalForce * staticFrictionCoefficient;

            if (fsMax.magnitude >= pushingForce.magnitude)
                return -pushingForce;
            else
                return Vector3.zero;
        }
    }
}
