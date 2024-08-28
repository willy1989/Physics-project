using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PhysicsObject
{
    public class PhysicsObject : MonoBehaviour
    {
        [SerializeField] private KinematicEquations kinematicEquations;

        [SerializeField] private ForceCalculator forceCalculator;

        [SerializeField] private BoxCastCollisionManager boxCastCollisionManager;

        [SerializeField] private float mass;

        public float Mass => mass;

        private Vector3 finalVelocity;

        public Vector3 FinalVelocity => finalVelocity;

        private List<CollisionInformation> currentCollisionInformation;

        private void Update()
        {
            currentCollisionInformation = boxCastCollisionManager.FilteredCollisionInformation();

            Vector3 noConstraintsForces = NoConstraintsForces();

            Vector3 normalForce = Vector3.zero;

            foreach (CollisionInformation collisionInformation in currentCollisionInformation)
            {
                normalForce += NormalForces(noConstraintsForces, collisionInformation.NormalVector);
            }

            Vector3 impactForce = Vector3.zero;

            foreach (CollisionInformation collisionInformation in currentCollisionInformation)
            {
                impactForce += ImpactForces(collisionInformation.NormalVector);
            }


            Vector3 pushForce = noConstraintsForces + normalForce + impactForce;

            Vector3 staticFrictionForce = StaticFrictionForce(normalForce: normalForce, pushForce: pushForce);

            Vector3 kineticFrictionForce = KineticFrictionForce(normalForce);

            Vector3 combinedForces = noConstraintsForces + normalForce + impactForce + kineticFrictionForce + staticFrictionForce;

            ApplyForces(combinedForces);
        }

        private bool IsInContact()
        {
            return currentCollisionInformation.Count > 0;
        }

        private Vector3 NoConstraintsForces()
        {
            Vector3 result = Vector3.zero;

            Vector3 zConstantForce = forceCalculator.ConstantForce(magnitude: 3f, direction: new Vector3(0f, 0f, 1f));

            Vector3 gravityForce = forceCalculator.ConstantForce(magnitude: 9.81f, direction: new Vector3(0f, -1f, 0f));

            result += zConstantForce;

            result += gravityForce;

            return result;
        }

        private Vector3 NormalForces(Vector3 noConstraintsForces, Vector3 normalVector)
        {
            // Forces with no constraints
            Vector3 result = Vector3.zero;

            // Normal forces

            if (IsInContact() == false)
                return result;

            Vector3 normalForce = forceCalculator.NormalForce(pushForce: noConstraintsForces, surfaceNormal: normalVector);
            
            result += normalForce;

            return result;
        }

        private Vector3 ImpactForces(Vector3 normalVector)
        {
            Vector3 result = Vector3.zero;
            if (IsInContact() == false)
                return result;

            Vector3 impactForce = forceCalculator.ImpactForce(finalVelocity: finalVelocity, mass: mass, surfaceNormal: normalVector);
            
            result += impactForce;

            return result;
        }

        private Vector3 KineticFrictionForce(Vector3 normalForce)
        {
            Vector3 result = Vector3.zero;

            // Friction forces
            if (FinalVelocity.magnitude == 0f)
                return result;


            if (currentCollisionInformation.Count > 0 && currentCollisionInformation[0] != null)
            {
                Vector3 kineticFrictionForce = forceCalculator.KineticFrictionForce(kineticFrictionCoefficient: currentCollisionInformation[0].KineticFrictionCoefficient, 
                                                                                    normalForce: normalForce, 
                                                                                    movementDirection: FinalVelocity);
                                                                                       
                result += kineticFrictionForce;
            }

            return result;
        }

        private Vector3 StaticFrictionForce(Vector3 normalForce, Vector3 pushForce)
        {
            Vector3 result = Vector3.zero;

            if (currentCollisionInformation.Count > 0 && currentCollisionInformation[0] != null)
            {
                Vector3 staticFrictionForce = forceCalculator.StaticFrictionForce(normalForce: normalForce, 
                                                                                  staticFrictionCoefficient: currentCollisionInformation[0].StaticFrictionCoefficient, 
                                                                                  pushForce: pushForce);

                result += staticFrictionForce;
            }

            return result;
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
            float finalVelocityX = kinematicEquations.FinalVelocity_1(initialVelocity.x, acceleration.x, Time.deltaTime);
            float finalVelocityY = kinematicEquations.FinalVelocity_1(initialVelocity.y, acceleration.y, Time.deltaTime);
            float finalVelocityZ = kinematicEquations.FinalVelocity_1(initialVelocity.z, acceleration.z, Time.deltaTime);

            Vector3 result = new Vector3(finalVelocityX, finalVelocityY, finalVelocityZ);

            return result;
        }

        private Vector3 Displacement(Vector3 initialVelocity, Vector3 finalVelocity)
        {
            float displacementX = kinematicEquations.DeltaX_2(finalVelocity.x, initialVelocity.x, Time.deltaTime);
            float displacementY = kinematicEquations.DeltaX_2(finalVelocity.y, initialVelocity.y, Time.deltaTime);
            float displacementZ = kinematicEquations.DeltaX_2(finalVelocity.z, initialVelocity.z, Time.deltaTime);

            Vector3 result = new Vector3(displacementX, displacementY, displacementZ);

            return result;
        }
    }
}


