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

        Vector3 normalForce = NormalForces(pushForce: zConstantForce + gravityForce);

        Vector3 impactForce = ImpactForces(mass: mass, finalVelocity: finalVelocity);

        // Update final velocity here?

        Vector3 pushForce = zConstantForce + gravityForce + normalForce + impactForce;

        Vector3 kineticFrictionForce = KineticFrictionForce(normalForce: normalForce, finalVelocity: finalVelocity);

        //Vector3 staticFrictionForce = StaticFrictionForce(normalForce: normalForce, pushForce: pushForce);

        Vector3 result = zConstantForce + gravityForce + normalForce + impactForce + kineticFrictionForce;

        return result;
    }

    private Vector3 ConstantForce(float magnitude, Vector3 direction)
    {
        return forceCalculator.ConstantForce(magnitude, direction);
    }

    private Vector3 NormalForces(Vector3 pushForce)
    {
        if (boxCastCollisionManager.IsInContact == false)
            return Vector3.zero;

        Vector3 result = Vector3.zero;

        foreach (CollisionInformation item in boxCastCollisionManager.CollisionInformation)
        {
            result += forceCalculator.NormalForce(pushForce: pushForce, surfaceNormal: item.NormalVector);
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
        if (boxCastCollisionManager.IsInContact == false)
            return Vector3.zero;

        Vector3 result = Vector3.zero;

        foreach(CollisionInformation item in boxCastCollisionManager.CollisionInformation)
        {
            result += forceCalculator.KineticFrictionForce(kineticFrictionCoefficient: item.KineticFrictionCoefficient,
                                                                            normalForce: normalForce,
                                                                            movementDirection: finalVelocity);
        }

        return result;
    }
}
