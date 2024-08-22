using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PhysicsObject
{
    public class NormalForce_ForceType : ForceType
    {
        private Vector3 pushForce;

        public NormalForce_ForceType(Vector3 _pushForce)
        {
            this.pushForce = _pushForce;
        }

        public override Vector3 Force()
        {
            Vector3 result = Vector3.zero;

            if (pushForce.y < 0)
                result = new Vector3(0f, -pushForce.y, 0f);

            return result;
        }
    }
}
