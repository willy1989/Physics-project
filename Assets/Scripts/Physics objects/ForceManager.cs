using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceManager : MonoBehaviour
{
    [SerializeField] private ForceCalculator forceCalculator;

    [SerializeField] private BoxCastCollisionManager boxCastCollisionManager;

    public Vector3 CombinedForces(float mass, Vector3 finalVelocity)
    {
        Vector3 zConstantForce = ConstantForce(magnitude: 5f, direction: new Vector3(0f, 0f, 1f));

        Vector3 gravityForce = ConstantForce(magnitude: 9.81f, direction: new Vector3(0f, -1f, 0f));

        // Calculate normal forces individually. One per surface. Then use each to calculate each corresponding static and friction forces. 
        List<Vector3> normalForces = NormalForces(pushForce: zConstantForce + gravityForce);

        Vector3 combinedImpactForces = ImpactForces(mass: mass, finalVelocity: finalVelocity);

        // Update final velocity here?

        Vector3 combinedNormalForces = Vector3.zero;

        if(normalForces != null)
        {
            foreach (Vector3 normalForce in normalForces)
            {
                combinedNormalForces += normalForce;
            }
        }

        Vector3 combinedKineticFrictionForces = KineticFrictionForce(pushForce: zConstantForce + gravityForce, finalVelocity: finalVelocity);

        Vector3 combinedStaticFrictionForces = StaticFrictionForce(pushForce: zConstantForce + gravityForce, finalVelocity: finalVelocity);

        Vector3 result = zConstantForce + gravityForce + combinedNormalForces + combinedImpactForces + combinedKineticFrictionForces + combinedStaticFrictionForces;

        return result;
    }

    private Vector3 ConstantForce(float magnitude, Vector3 direction)
    {
        return forceCalculator.ConstantForce(magnitude, direction);
    }

    private List<Vector3> NormalForces(Vector3 pushForce)
    {
        if (boxCastCollisionManager.IsInContact == false)
            return null;

        List<Vector3> result = new List<Vector3>();

        foreach (CollisionInformation item in boxCastCollisionManager.CollisionInformation)
        {
            result.Add(forceCalculator.NormalForce(pushForce: pushForce, surfaceNormal: item.NormalVector));
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

    private Vector3 StaticFrictionForce(Vector3 pushForce, Vector3 finalVelocity)
    {
        if(boxCastCollisionManager.IsInContact == false)
           return Vector3.zero;

        if(finalVelocity.magnitude > 0f)
            return Vector3.zero;

        Vector3 result = Vector3.zero;

        foreach (CollisionInformation item in boxCastCollisionManager.CollisionInformation)
        {
            Vector3 normalForce = forceCalculator.NormalForce(pushForce: pushForce, surfaceNormal: item.NormalVector);

            Vector3 effectiveForce = pushForce + normalForce;

            float fsMax = forceCalculator.FsMax(normalForce : normalForce, staticFrictionCoefficient: item.StaticFrictionCoefficient);

            Vector3 staticFrictionForce = forceCalculator.StaticFrictionForce(fsMax: fsMax, pushForce: effectiveForce);

            result += staticFrictionForce;
        }

        return result;
    }

    private Vector3 KineticFrictionForce(Vector3 pushForce, Vector3 finalVelocity)
    {
        if (boxCastCollisionManager.IsInContact == false)
            return Vector3.zero;

        Vector3 cachedPushForce = pushForce;

        Vector3 result = Vector3.zero;

        foreach(CollisionInformation item in boxCastCollisionManager.CollisionInformation)
        {
            Vector3 normalForce = forceCalculator.NormalForce(pushForce: cachedPushForce, surfaceNormal: item.NormalVector);

            cachedPushForce += normalForce;

            Vector3 temp = forceCalculator.KineticFrictionForce(kineticFrictionCoefficient: item.KineticFrictionCoefficient,
                                                                normalForce: normalForce,
                                                                movementDirection: finalVelocity);

            result += temp;
        }

        return result;
    }
}
