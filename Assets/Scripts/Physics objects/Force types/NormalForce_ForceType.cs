using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PhysicsObject
{
    public class NormalForce_ForceType : ForceType
    {
        private Gravity_ForceType gravityForce;

        public NormalForce_ForceType(Gravity_ForceType _gravityForce)
        {
            gravityForce = _gravityForce;
        }

        public override Vector3 Force()
        {
            return -gravityForce.Force();
        }
    }
}
