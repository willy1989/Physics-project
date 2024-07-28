using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveModule : MonoBehaviour
{
    [SerializeField] private KinematicEquations kinematicEquations;

    private Transform objectToMove;

    public float FinalVelocity { get; private set; }

    private const float defaultAcceleration = 1f;

    private float specificAcceleration;

    private float deltaVelocity = 0f;

    private void Awake()
    {
        objectToMove = this.transform;

        Application.targetFrameRate = 60;
    }

    private void Start()
    {
        specificAcceleration = kinematicEquations.Acceleration_3(initialVelocity: 0f, deltaX: 10f, deltaTime: 3f);
    }

    private void Update()
    {
        MoveObject(specificAcceleration);
    }

    private void MoveObject(float acceleration)
    {
        float initialVelocity = FinalVelocity;

        FinalVelocity = kinematicEquations.FinalVelocity_1(initialVelocity: initialVelocity, acceleration: acceleration, deltaTime: Time.deltaTime);

        float deltaX = kinematicEquations.DeltaX_2(finalVelocity: FinalVelocity, initialVelocity: initialVelocity, deltaTime: Time.deltaTime);

        deltaVelocity = FinalVelocity - initialVelocity;

        objectToMove.position += new Vector3(0f, 0f, deltaX);
    }
}
