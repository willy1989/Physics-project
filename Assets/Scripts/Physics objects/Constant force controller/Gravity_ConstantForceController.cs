using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PhysicsObject
{
    public class Gravity_ConstantForceController : ConstantForceController
    {
        [SerializeField] private PhysicsObject physicsObject;

        public void SetUp(PhysicsObject physicsObject)
        {
            this.physicsObject = physicsObject;
        }

        public override Vector3 ConstantForce()
        {
            Vector3 gravityForce = new Vector3(0f, -1f, 0f) * physicsObject.Mass * 9.81f;

            return gravityForce;
        }
    }
}


