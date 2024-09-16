using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PhysicsObject
{
    public class Gravity_ForceType : ForceType
    {
        private PhysicsObject physicsObject;

        public Gravity_ForceType(PhysicsObject _physicsObject)
        {
            physicsObject = _physicsObject;
        }

        public override Vector3 Force()
        {
            Vector3 result = physicsObject.Mass * Vector3.down * 9.81f;

            return result;
        }
    }
}
