using NUnit.Framework;
using PhysicsObject;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TestTools;

public class FirstKinematicEquationsTests_PhysicsObject
{
    public class ConstantForce
    {
        [SetUp]
        public void SetUpTest()
        {

        }


        [UnityTest]
        public IEnumerator Move_Object_1kg_Constant_Force_0_Minus_9_Point_81_0_In_2_Seconds_From_0_0_0_To_0_Minus_19_point_62_0_FrameRate_60()
        {
            // Arrange

            PhysicsObject.PhysicsObject physicsObject = new GameObject().AddComponent<PhysicsObject.PhysicsObject>();

            BoxCastCollisionManager boxCastCollisionManager = new GameObject().AddComponent<BoxCastCollisionManager>();

            Gravity_ConstantForceController gravity_ConstantForceController = new GameObject().AddComponent<Gravity_ConstantForceController>();

            ForceManager forceManager = new GameObject().AddComponent<ForceManager>();

            List<ConstantForceController> constantForceControllers = new List<ConstantForceController>();

            

            gravity_ConstantForceController.SetUp(physicsObject);
            
            constantForceControllers.Add(gravity_ConstantForceController);

            forceManager.SetUp(boxCastCollisionManager, constantForceControllers);

            physicsObject.SetUp(forceManager, mass: 1);

            Application.targetFrameRate = 60;

            // Act

            yield return new WaitForSeconds(2f);

            // Assert

            Vector3 actualFinalPosition = physicsObject.transform.position;

            Vector3 expectedFinalPosition = new Vector3(0f, -19.62f, 0f);

            float deltaBetweenExpectedAndActualPosition = (actualFinalPosition - expectedFinalPosition).magnitude;

            Assert.Less(deltaBetweenExpectedAndActualPosition, 0.5f);
        }
    }
}
