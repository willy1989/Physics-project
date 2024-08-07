using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneDimensionForceApplier : MonoBehaviour
{
    [SerializeField] private KinematicEquations kinematicEquations;

    [SerializeField] private ForceType[] forceTypes;

    [SerializeField] private float mass;

    public float Mass => mass;

    private Vector3 finalVelocity;

    public Vector3 FinalVelocity => finalVelocity;

    public void SetUp(KinematicEquations _kinematicEquations, ForceType[] _forceTypes, float _mass)
    {
        kinematicEquations = _kinematicEquations;
        forceTypes = _forceTypes;
        mass = _mass;
    }


    private void Update()
    {
        // F = m * a, so a = F/m
        Vector3 initialVelocity = finalVelocity;

        Vector3 combinedForces = Vector3.zero;

        foreach (var forceType in forceTypes)
        {
            combinedForces += forceType.Force();
        }

        Vector3 acceleration = combinedForces / mass;


        float finalVelocityX = kinematicEquations.FinalVelocity_1(initialVelocity.x, acceleration.x, Time.deltaTime);
        float finalVelocityY = kinematicEquations.FinalVelocity_1(initialVelocity.y, acceleration.y, Time.deltaTime);
        float finalVelocityZ = kinematicEquations.FinalVelocity_1(initialVelocity.z, acceleration.z, Time.deltaTime);


        finalVelocity = new Vector3(finalVelocityX, finalVelocityY, finalVelocityZ);

        float displacementX = kinematicEquations.DeltaX_2(finalVelocity.x, initialVelocity.x, Time.deltaTime);
        float displacementY = kinematicEquations.DeltaX_2(finalVelocity.y, initialVelocity.y, Time.deltaTime);
        float displacementZ = kinematicEquations.DeltaX_2(finalVelocity.z, initialVelocity.z, Time.deltaTime);

        Vector3 displacement = new Vector3(displacementX, displacementY, displacementZ);

        transform.position += displacement;

    }
}
