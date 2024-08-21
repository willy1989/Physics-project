using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PhysicsObject
{
    public class PhysicsObject : MonoBehaviour
    {
        [SerializeField] private KinematicEquations kinematicEquations;

        [SerializeField] private float mass;

        public float Mass => mass;

        private Vector3 finalVelocity;

        public Vector3 FinalVelocity => finalVelocity;

        private List<ForceType> forceTypes = new List<ForceType>();

        private void Start()
        {
            Constant_ForceType zConstantForceType = new Constant_ForceType(_constantForce: 1f, new Vector3(0f, 0f, 1f));

            forceTypes.Add(zConstantForceType);

            Gravity_ForceType gravityForceType = new Gravity_ForceType(_physicsObject: this);

            forceTypes.Add(gravityForceType);

            NormalForce_ForceType normalForce = new NormalForce_ForceType(gravityForceType);

            forceTypes.Add(normalForce);

            KineticFriction_ForceType kineticFrictionForce = new KineticFriction_ForceType(_kineticFrictionCoefficient:0.05f,_normalForce: normalForce, _physicsObject: this);

            forceTypes.Add(kineticFrictionForce);

        }

        private void Update()
        {
            ApplyForces();
        }


        private void ApplyForces()
        {
            // F = m * a, so a = F/m
            

            Vector3 combinedForces = CombinedForces();

            Vector3 acceleration = combinedForces / mass;

            Vector3 initialVelocity = finalVelocity;

            finalVelocity = ComputeFinalVelocity(initialVelocity, acceleration);

            float displacementX = kinematicEquations.DeltaX_2(finalVelocity.x, initialVelocity.x, Time.deltaTime);
            float displacementY = kinematicEquations.DeltaX_2(finalVelocity.y, initialVelocity.y, Time.deltaTime);
            float displacementZ = kinematicEquations.DeltaX_2(finalVelocity.z, initialVelocity.z, Time.deltaTime);

            Vector3 displacement = new Vector3(displacementX, displacementY, displacementZ);

            transform.position += displacement;
        }

        private Vector3 CombinedForces()
        {
            Vector3 result = Vector3.zero;

            foreach (var forceType in forceTypes)
            {
                result += forceType.Force();
            }

            return result;
        }

        private Vector3 ComputeFinalVelocity(Vector3 initialVelocity, Vector3 acceleration)
        {
            float finalVelocityX = kinematicEquations.FinalVelocity_1(initialVelocity.x, acceleration.x, Time.deltaTime);
            float finalVelocityY = kinematicEquations.FinalVelocity_1(initialVelocity.y, acceleration.y, Time.deltaTime);
            float finalVelocityZ = kinematicEquations.FinalVelocity_1(initialVelocity.z, acceleration.z, Time.deltaTime);

            Vector3 result = new Vector3(finalVelocityX, finalVelocityY, finalVelocityZ);

            return result;
        }
    }
}


