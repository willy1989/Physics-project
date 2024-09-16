using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PhysicsObject
{
    public class Constant_ForceType : ForceType
    {
        private float constantForce;

        private Vector3 direction;

        public Constant_ForceType(float _constantForce, Vector3 _direction)
        {
            this.constantForce = _constantForce;
            this.direction = _direction;
        }

        public override Vector3 Force()
        {
            return direction.normalized * constantForce;
        }
    }
}
