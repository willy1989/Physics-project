using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionTest : MonoBehaviour
{
    private List<CollisionInformation> collisionInformationList = new List<CollisionInformation>();

    public bool IsInContact
    {
        get
        {
            return collisionInformationList.Count > 0;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        CollisionInformation collisionInformation = GetCollisionInformation(collision);

        collisionInformationList.Add(collisionInformation);
    }

    private void OnCollisionExit(Collision collision)
    {
        
    }

    private CollisionInformation GetCollisionInformation(Collision collision)
    {
        FrictionCollider frictionCollider = collision.gameObject.GetComponent<FrictionCollider>();

        if (frictionCollider == null)
            return null;

        ContactPoint[] contactPoints = collision.contacts;

        CollisionInformation collisionInformation = new CollisionInformation(frictionCollider.StaticFrictionCoefficient(),
                                                                             frictionCollider.KineticFrictionCoefficient(),
                                                                             contactPoints[0].normal);

        return collisionInformation;
    }
}
