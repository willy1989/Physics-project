using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContactCollidersManager : MonoBehaviour
{
    private List<Collider> contactColliders = new List<Collider>();

    public bool IsInContact
    {
        get
        {
            return contactColliders.Count > 0;
        }
    }

    public FrictionCollider GetFrictionCollider()
    {
        FrictionCollider frictionCollider = null;

        foreach (Collider collider in contactColliders)
        {
            frictionCollider = collider.GetComponent<FrictionCollider>();
        }

        return frictionCollider;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other != null)
            contactColliders.Add(other);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other != null)
        {
            int index = contactColliders.IndexOf(other);
            contactColliders.RemoveAt(index);
        }
    }
}
