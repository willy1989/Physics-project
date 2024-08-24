using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PhysicsObject
{
    public class PhysicsObject : MonoBehaviour
    {
        [SerializeField] private KinematicEquations kinematicEquations;

        [SerializeField] private ContactCollidersManager contactCollidersManager;

        [SerializeField] private float mass;

        public float Mass => mass;

        private Vector3 finalVelocity;

        public Vector3 FinalVelocity => finalVelocity;


        private void Update()
        {
            Vector3 noConstraintsForces = NoConstraintsForces();

            Vector3 normalForce = NormalForces(noConstraintsForces);

            Vector3 pushForce = noConstraintsForces + normalForce;

            Vector3 staticFrictionForce = StaticFrictionForce(normalForce: normalForce, pushForce: pushForce);

            Vector3 kineticFrictionForce = KineticFrictionForce(normalForce);

            Vector3 combinedForces = noConstraintsForces + normalForce + staticFrictionForce + kineticFrictionForce;

            ApplyForces(combinedForces);
        }

        private Vector3 NoConstraintsForces()
        {
            Vector3 result = Vector3.zero;

            Constant_ForceType zConstantForceType = new Constant_ForceType(_constantForce: 1f, _direction: new Vector3(0f, 0f, 1f));

            Gravity_ForceType gravityForceType = new Gravity_ForceType(_physicsObject: this);

            result += zConstantForceType.Force();

            result += gravityForceType.Force();

            return result;
        }

        private Vector3 NormalForces(Vector3 noConstraintsForces)
        {
            // Forces with no constraints
            Vector3 result = Vector3.zero;

            // Normal forces

            if (contactCollidersManager.IsInContact == false)
                return result;

            NormalForceCollider normalForceCollider = contactCollidersManager.GetNormalForceCollider();

            NormalForce_ForceType normalForce = new NormalForce_ForceType(_pushForce: noConstraintsForces, _surfaceNormal: normalForceCollider.NormalVector());
            result += normalForce.Force();

            return result;
        }

        private Vector3 KineticFrictionForce(Vector3 normalForce)
        {
            Vector3 result = Vector3.zero;

            // Friction forces
            if (FinalVelocity.magnitude == 0f)
                return result;

            FrictionCollider frictionCollider = contactCollidersManager.GetFrictionCollider();

            if (frictionCollider != null)
            {
                KineticFriction_ForceType kineticFriction_ForceType = new KineticFriction_ForceType(_kineticFrictionCoefficient: frictionCollider.KineticFrictionCoefficient(),
                                                                                                    _normalForce: normalForce,
                                                                                                    _direction: FinalVelocity);

                result += kineticFriction_ForceType.Force();
            }

            return result;
        }

        private Vector3 StaticFrictionForce(Vector3 normalForce, Vector3 pushForce)
        {
            Vector3 result = Vector3.zero;

            FrictionCollider frictionCollider = contactCollidersManager.GetFrictionCollider();

            if (frictionCollider != null)
            {
                StaticFriction_ForceType staticFriction_ForceType = new StaticFriction_ForceType(normalForce: normalForce,
                                                                                                 staticFrictionCoefficient: frictionCollider.StaticFrictionCoefficient(),
                                                                                                 pushForce: pushForce);

                result = staticFriction_ForceType.Force();
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


