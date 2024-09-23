using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceManager : MonoBehaviour
{
    [SerializeField] private BoxCastCollisionManager boxCastCollisionManager;

    [SerializeField] private List<ConstantForceController> constantsForceControllers = new List<ConstantForceController>();

    public void SetUp(BoxCastCollisionManager boxCastCollisionManager, List<ConstantForceController> constantsForceControllers)
    {
        this.boxCastCollisionManager = boxCastCollisionManager;
        this.constantsForceControllers = constantsForceControllers;
    }


    public Vector3 CombinedForces(float mass, Vector3 finalVelocity)
    {
        Vector3 combinedConstantForces = Vector3.zero;

        foreach (var constantForceController in constantsForceControllers)
        {
            combinedConstantForces += constantForceController.ConstantForce();
        }

        // Calculate normal forces individually. One per surface. Then use each to calculate each corresponding static and friction forces. 
        List<Vector3> normalForces = NormalForces(pushForce: combinedConstantForces);

        Vector3 combinedImpactForces = ImpactForces(mass: mass, finalVelocity: finalVelocity);

        Vector3 combinedNormalForces = Vector3.zero;

        if(normalForces != null)
        {
            foreach (Vector3 normalForce in normalForces)
            {
                combinedNormalForces += normalForce;
            }
        }

        Vector3 combinedKineticFrictionForces = KineticFrictionForce(pushForce: combinedConstantForces, finalVelocity: finalVelocity);

        Vector3 combinedStaticFrictionForces = StaticFrictionForce(pushForce: combinedConstantForces, finalVelocity: finalVelocity);

        Vector3 result = combinedConstantForces + combinedNormalForces + combinedImpactForces + combinedStaticFrictionForces + combinedKineticFrictionForces;

        return result;
    }

    public Vector3 ConstantForce(float magnitude, Vector3 direction)
    {
        return ForceCalculator.ConstantForce(magnitude, direction);
    }

    private List<Vector3> NormalForces(Vector3 pushForce)
    {
        if (boxCastCollisionManager.IsInContact == false)
            return null;

        List<Vector3> result = new List<Vector3>();

        foreach (CollisionInformation item in boxCastCollisionManager.CollisionInformation)
        {
            result.Add(ForceCalculator.NormalForce(pushForce: pushForce, surfaceNormal: item.NormalVector));
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
            result += ForceCalculator.ImpactForce(finalVelocity: finalVelocity, mass: mass, surfaceNormal: item.NormalVector);
  
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
            Vector3 normalForce = ForceCalculator.NormalForce(pushForce: pushForce, surfaceNormal: item.NormalVector);

            Vector3 effectiveForce = pushForce + normalForce;

            float fsMax = ForceCalculator.FsMax(normalForce : normalForce, staticFrictionCoefficient: item.StaticFrictionCoefficient);

            Vector3 staticFrictionForce = ForceCalculator.StaticFrictionForce(fsMax: fsMax, pushForce: effectiveForce);

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
            Vector3 normalForce = ForceCalculator.NormalForce(pushForce: cachedPushForce, surfaceNormal: item.NormalVector);

            cachedPushForce += normalForce;

            Vector3 temp = ForceCalculator.KineticFrictionForce(kineticFrictionCoefficient: item.KineticFrictionCoefficient,
                                                                normalForce: normalForce,
                                                                movementDirection: finalVelocity);

            result += temp;
        }

        return result;
    }
}
