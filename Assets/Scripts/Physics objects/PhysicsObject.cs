using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PhysicsObject
{
    public class PhysicsObject : MonoBehaviour
    {
        [SerializeField] private ForceManager forceManager;

        [SerializeField] private float mass;

        public float Mass => mass;

        private Vector3 finalVelocity;

        public Vector3 FinalVelocity => finalVelocity;

        private void Update()
        {
            if (forceManager == null)
                return;

            Vector3 combinedForces = forceManager.CombinedForces(mass: mass, finalVelocity: finalVelocity);

            ApplyForces(combinedForces);
        }

        public void SetUp(ForceManager forceManager, float mass)
        {
            this.forceManager= forceManager;
            this.mass= mass;
        }

        private void ApplyForces(Vector3 combinedForces)
        {
            Vector3 acceleration = combinedForces / mass;

            Vector3 initialVelocity = finalVelocity;

            finalVelocity = ComputedFinalVelocity(initialVelocity, acceleration);

            Vector3 displacement = Displacement(initialVelocity, finalVelocity);

            transform.position += displacement;
        }

        private Vector3 ComputedFinalVelocity(Vector3 initialVelocity, Vector3 acceleration)
        {
            float finalVelocityX = KinematicEquations.FinalVelocity_1(initialVelocity.x, acceleration.x, Time.deltaTime);
            float finalVelocityY = KinematicEquations.FinalVelocity_1(initialVelocity.y, acceleration.y, Time.deltaTime);
            float finalVelocityZ = KinematicEquations.FinalVelocity_1(initialVelocity.z, acceleration.z, Time.deltaTime);

            Vector3 result = new Vector3(finalVelocityX, finalVelocityY, finalVelocityZ);

            return result;
        }

        private Vector3 Displacement(Vector3 initialVelocity, Vector3 finalVelocity)
        {
            float displacementX = KinematicEquations.DeltaX_2(finalVelocity.x, initialVelocity.x, Time.deltaTime);
            float displacementY = KinematicEquations.DeltaX_2(finalVelocity.y, initialVelocity.y, Time.deltaTime);
            float displacementZ = KinematicEquations.DeltaX_2(finalVelocity.z, initialVelocity.z, Time.deltaTime);

            Vector3 result = new Vector3(displacementX, displacementY, displacementZ);

            return result;
        }
    }
}


