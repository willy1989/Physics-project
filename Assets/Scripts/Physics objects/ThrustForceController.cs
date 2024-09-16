using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ThrustForceController : MonoBehaviour
{
    [SerializeField] private float newtons;

    [SerializeField] private float maxSteeringAngle = 30f;

    [SerializeField] private float steeringSpeed = 10f;

    private float _steeringAngle;

    private float steeringAngle
    {
        get
        {
            return _steeringAngle;
        }

        set
        {
            if (value >= maxSteeringAngle)
                _steeringAngle = maxSteeringAngle;
            else if (value < -maxSteeringAngle)
                _steeringAngle = -maxSteeringAngle;

            else
                _steeringAngle = value;
        }
    }

    private void Update()
    {
        IncrementSteeringAngle();
    }

    public Vector3 Force()
    {
        Vector3 result = Vector3.zero;

        if (Input.GetKey(KeyCode.Space))
        {
            result += transform.forward * newtons;
        }

        if(Input.GetKey(KeyCode.LeftShift))
        {
            result -= transform.forward * newtons;
        }

        result = Quaternion.Euler(0f, steeringAngle, 0f) * result;

        return result;
    }

    private void IncrementSteeringAngle()
    {
        if (Input.GetKey(KeyCode.Q))
            steeringAngle -= steeringSpeed * Time.deltaTime;

        if (Input.GetKey(KeyCode.D))
            steeringAngle += steeringSpeed * Time.deltaTime;

    }



    /*private void OnDrawGizmos()
    {
        // Set the gizmo color for the original vector
        Gizmos.color = Color.green;

        // Draw the original vector (from origin to originalVector)
        Gizmos.DrawRay(transform.position, transform.forward);

        // Set the gizmo color for the original vector
        Gizmos.color = Color.red;

        // Draw the original vector (from origin to originalVector)
        Gizmos.DrawRay(transform.position, directionVector);
    }
    */
}
