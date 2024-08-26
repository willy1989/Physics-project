using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxCastCollisionManager : MonoBehaviour
{
    public List<CollisionInformation> FilteredCollisionInformation()
    {
        List<CollisionInformation> rawCollisionInformation = RawCollisionInformation();

        List<CollisionInformation> result = new List<CollisionInformation>();

        foreach (CollisionInformation current in rawCollisionInformation)
        {
            bool alreadyInList = false;

            foreach (CollisionInformation item in result)
            {
                if (current.NormalVector == item.NormalVector)
                {
                    alreadyInList = true;
                    break;
                }
            }

            if (alreadyInList == false)
                result.Add(current);
        }

        return result;
    }

    private List<CollisionInformation> RawCollisionInformation()
    {
        RaycastHit[] hits = ContactColliders();

        List<CollisionInformation> result = new List<CollisionInformation>();

        // Create collision information data
        foreach (RaycastHit hit in hits)
        {
            NormalForceCollider normalForceCollider = hit.collider.GetComponent<NormalForceCollider>();

            if (normalForceCollider == null)
                continue;

            Vector3 normalDirection = normalForceCollider.NormalVector();

            FrictionCollider frictionCollider = hit.collider.GetComponent<FrictionCollider>();

            if (frictionCollider == null)
                continue;

            float kinematicFrictionCoefficient = frictionCollider.KineticFrictionCoefficient();

            float staticFrictionCoefficient = frictionCollider.StaticFrictionCoefficient();

            CollisionInformation collisionInformation = new CollisionInformation(staticFrictionCoefficient, kinematicFrictionCoefficient, normalDirection);
            result.Add(collisionInformation);
        }

        return result;
    }

    private RaycastHit[] ContactColliders()
    {
        RaycastHit[] result = Physics.BoxCastAll(center: transform.position, halfExtents: transform.localScale * 0.5f,
                                               direction: Vector3.forward, orientation: Quaternion.identity, maxDistance: 0f);

        return result;
    }

    

    
}
