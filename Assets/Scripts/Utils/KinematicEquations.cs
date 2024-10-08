using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class KinematicEquations
{
    // First kinematic equation: vf = vi + a*t

    public static float FinalVelocity_1(float initialVelocity, float acceleration, float deltaTime)
    {
        if(deltaTime< 0.0f) 
        { 
            throw new ArgumentException("'DeltaTime' cannot be negative.");
        }

        float result = initialVelocity + acceleration * deltaTime;
        return result;
    }

    public static float InitialVelocity_1(float finalVelocity, float acceleration, float deltaTime)
    {
        if (deltaTime < 0.0f)
        {
            throw new ArgumentException("'DeltaTime' cannot be negative.");
        }

        float result = finalVelocity - acceleration * deltaTime;

        return result;
    }

    public static float Acceleration_1(float initialVelocity, float finalVelocity, float deltaTime)
    {
        if (deltaTime < 0.0f)
        {
            throw new ArgumentException("'DeltaTime' cannot be negative.");
        }

        float result = (finalVelocity - initialVelocity) / deltaTime;

        return result;
    }

    public static float DeltaTime_1(float initialVelocity, float finalVelocity, float acceleration)
    {
        float result = (finalVelocity - initialVelocity) / acceleration;

        if (result < 0.0f)
        {
            throw new ArgumentException("The return value cannot be negative. " +
                                        "Negative time makes no sense.");
        }

        return result;
    }

    // Second kinematic equation: deltaX = 1/2 * (vf + vi) * t 

    public static float DeltaX_2(float finalVelocity, float initialVelocity, float deltaTime)
    {
        float result = ((finalVelocity + initialVelocity) / 2) * deltaTime;

        return result;
    }

    public static float DeltaTime_2(float deltaX, float finalVelocity, float initialVelocity)
    {
        float result = deltaX / (finalVelocity + initialVelocity) / 2;

        return result;
    }

    // Third kinematic equation: deltaX = vi * t + 1/2 * a*t�

    public static float Acceleration_3(float initialVelocity, float deltaX, float deltaTime)
    {
        float result = 2f * (deltaX - initialVelocity * deltaTime) / (deltaTime * deltaTime);

        return result;  
    }
}
