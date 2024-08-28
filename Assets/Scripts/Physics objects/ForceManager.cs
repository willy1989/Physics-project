using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceManager : MonoBehaviour
{
    [SerializeField] private ForceCalculator forceCalculator;

    [SerializeField] private BoxCastCollisionManager boxCastCollisionManager;

    public Vector3 CombinedForces(float mass, Vector3 finalVelocity)
    {
        Vector3 zConstantForce = ConstantForce(magnitude: 3f, direction: new Vector3(0f, 0f, 1f));

        Vector3 gravityForce = ConstantForce(magnitude: 9.81f, direction: new Vector3(0f, -1f, 0f));

        Vector3 noConstraintsForces = zConstantForce + gravityForce;

        Vector3 normalForce = NormalForces(noConstraintsForces: noConstraintsForces);

        Vector3 impactForce = ImpactForces(mass: mass, finalVelocity: finalVelocity);

        Vector3 result = noConstraintsForces + normalForce + impactForce;

        //Vector3 pushForce = noConstraintsForces + normalForce + impactForce;

        //Vector3 staticFrictionForce = StaticFrictionForce(normalForce: normalForce, pushForce: pushForce);

        //Vector3 kineticFrictionForce = KineticFrictionForce(normalForce: normalForce, finalVelocity: finalVelocity);

        //Vector3 result = noConstraintsForces + normalForce + impactForce + kineticFrictionForce + staticFrictionForce;

        return result;
    }

    private Vector3 ConstantForce(float magnitude, Vector3 direction)
    {
        return forceCalculator.ConstantForce(magnitude, direction);
    }

    private Vector3 NormalForces(Vector3 noConstraintsForces)
    {
        if (boxCastCollisionManager.IsInContact == false)
            return Vector3.zero;

        Vector3 result = Vector3.zero;

        foreach (CollisionInformation item in boxCastCollisionManager.CollisionInformation)
        {
            result += forceCalculator.NormalForce(pushForce: noConstraintsForces, surfaceNormal: item.NormalVector);
        }

        return result;
    }

    private Vector3 ImpactForces(float mass, Vector3 finalVelocity)
    {
        if (boxCastCollisionManager.IsInContact == false)
            return Vector3.zero;

        Vector3 result = Vector3.zero;

        foreach (CollisionInformation item in boxCastCollisionManager.CollisionInformation)
        {
            result += forceCalculator.ImpactForce(finalVelocity: finalVelocity, mass: mass, surfaceNormal: item.NormalVector);
  
        }

        return result;
    }

    private Vector3 StaticFrictionForce(Vector3 normalForce, Vector3 pushForce)
    {
        if (boxCastCollisionManager.IsInContact == false)
            return Vector3.zero;

        Vector3 result = forceCalculator.StaticFrictionForce(normalForce: normalForce,
                                                                              staticFrictionCoefficient: boxCastCollisionManager.CollisionInformation[0].StaticFrictionCoefficient,
                                                                              pushForce: pushForce);

        return result;
    }

    private Vector3 KineticFrictionForce(Vector3 normalForce, Vector3 finalVelocity)
    {
        if (finalVelocity.magnitude == 0f)
            return Vector3.zero;

        if (boxCastCollisionManager.IsInContact == false)
            return Vector3.zero;

        Vector3 result = forceCalculator.KineticFrictionForce(kineticFrictionCoefficient: boxCastCollisionManager.CollisionInformation[0].KineticFrictionCoefficient,
                                                                            normalForce: normalForce,
                                                                            movementDirection: finalVelocity);
        return result;
    }
}
