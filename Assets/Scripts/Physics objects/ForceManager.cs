using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceManager : MonoBehaviour
{
    [SerializeField] private ForceCalculator forceCalculator;


    public Vector3 CombinedForces(bool isInContact, List<CollisionInformation> collisionInformation, float mass, Vector3 finalVelocity)
    {
        Vector3 zConstantForce = ConstantForce(magnitude: 3f, direction: new Vector3(0f, 0f, 1f));

        Vector3 gravityForce = ConstantForce(magnitude: 9.81f, direction: new Vector3(0f, -1f, 0f));

        Vector3 noConstraintsForces = zConstantForce + gravityForce;

        Vector3 normalForce = NormalForces(noConstraintsForces: noConstraintsForces,
                                                        isInContact: isInContact,
                                                        collisionInformation: collisionInformation);

        Vector3 impactForce = ImpactForces(isInContact: isInContact,
                                                        mass: mass,
                                                        finalVelocity: finalVelocity,
                                                        collisionInformation: collisionInformation);

        Vector3 pushForce = noConstraintsForces + normalForce + impactForce;

        Vector3 staticFrictionForce = StaticFrictionForce(normalForce: normalForce,
                                                                       pushForce: pushForce,
                                                                       collisionInformation: collisionInformation,
                                                                       isInContact: isInContact);

        Vector3 kineticFrictionForce = KineticFrictionForce(normalForce: normalForce,
                                                            finalVelocity: finalVelocity,
                                                            collisionInformation: collisionInformation,
                                                            isInContact: isInContact);

        Vector3 result = noConstraintsForces + normalForce + impactForce + kineticFrictionForce + staticFrictionForce;

        return result;
    }

    private Vector3 ConstantForce(float magnitude, Vector3 direction)
    {
        return forceCalculator.ConstantForce(magnitude, direction);
    }

    private Vector3 NormalForce(Vector3 noConstraintsForces, Vector3 normalVector, bool isInContact)
    {
        Vector3 result = Vector3.zero;

        if (isInContact == false)
            return result;

        Vector3 normalForce = forceCalculator.NormalForce(pushForce: noConstraintsForces, surfaceNormal: normalVector);

        result += normalForce;

        return result;
    }

    private Vector3 NormalForces(Vector3 noConstraintsForces, bool isInContact, List<CollisionInformation> collisionInformation)
    {
        Vector3 result = Vector3.zero;

        foreach (CollisionInformation item in collisionInformation)
        {
            result += NormalForce(noConstraintsForces, item.NormalVector, isInContact);
        }

        return result;
    }

    private Vector3 ImpactForce(Vector3 normalVector, bool isInContact, float mass, Vector3 finalVelocity)
    {
        Vector3 result = Vector3.zero;
        if (isInContact == false)
            return result;

        Vector3 impactForce = forceCalculator.ImpactForce(finalVelocity: finalVelocity, mass: mass, surfaceNormal: normalVector);

        result += impactForce;

        return result;
    }

    private Vector3 ImpactForces(bool isInContact, float mass, Vector3 finalVelocity, List<CollisionInformation> collisionInformation)
    {
        Vector3 result = Vector3.zero;

        foreach (CollisionInformation item in collisionInformation)
        {
            result += ImpactForce(normalVector: item.NormalVector,
                                        isInContact: isInContact,
                                        mass: mass,
                                        finalVelocity: finalVelocity);
        }

        return result;
    }

    private Vector3 StaticFrictionForce(Vector3 normalForce, Vector3 pushForce, List<CollisionInformation> collisionInformation, bool isInContact)
    {
        if (collisionInformation.Count <= 0)
            return Vector3.zero;

        if (isInContact == false)
            return Vector3.zero;


        Vector3 staticFrictionForce = forceCalculator.StaticFrictionForce(normalForce: normalForce,
                                                                              staticFrictionCoefficient: collisionInformation[0].StaticFrictionCoefficient,
                                                                              pushForce: pushForce);

        Vector3 result = staticFrictionForce;

        return result;
    }

    private Vector3 KineticFrictionForce(Vector3 normalForce, Vector3 finalVelocity, List<CollisionInformation> collisionInformation, bool isInContact)
    {
        Vector3 result = Vector3.zero;

        if (collisionInformation.Count <= 0)
            return result;

        if (collisionInformation.Count <= 0)
            return result;

        if (finalVelocity.magnitude == 0f)
            return result;

        if (isInContact == false)
            return result;

        Vector3 kineticFrictionForce = forceCalculator.KineticFrictionForce(kineticFrictionCoefficient: collisionInformation[0].KineticFrictionCoefficient,
                                                                            normalForce: normalForce,
                                                                            movementDirection: finalVelocity);
        result += kineticFrictionForce;

        return result;
    }

}
