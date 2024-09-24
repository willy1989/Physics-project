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
        private PhysicsObject.PhysicsObject physicsObject;

        private BoxCastCollisionManager boxCastCollisionManager;

        private ForceManager forceManager;


        [SetUp]
        public void SetUpTest()
        {
            physicsObject = new GameObject().AddComponent<PhysicsObject.PhysicsObject>();

            boxCastCollisionManager = new GameObject().AddComponent<BoxCastCollisionManager>();

            forceManager = new GameObject().AddComponent<ForceManager>();
        }

        private void Arrange(float mass, int targetFrameRate)
        {
            // Arrange

            Gravity_ConstantForceController gravity_ConstantForceController = new GameObject().AddComponent<Gravity_ConstantForceController>();

            List<ConstantForceController> constantForceControllers = new List<ConstantForceController>();

            gravity_ConstantForceController.SetUp(physicsObject);

            constantForceControllers.Add(gravity_ConstantForceController);

            forceManager.SetUp(boxCastCollisionManager, constantForceControllers);

            physicsObject.SetUp(forceManager, mass: mass);

            Application.targetFrameRate = targetFrameRate;
        }

        private bool AssertTest(Vector3 expectedFinalPosition)
        {
            Vector3 actualFinalPosition = physicsObject.transform.position;

            float deltaBetweenExpectedAndActualPosition = (actualFinalPosition - expectedFinalPosition).magnitude;

            return deltaBetweenExpectedAndActualPosition <= 1f;
        }


        [UnityTest]
        public IEnumerator Move_Object_1kg_Constant_Force_0_Minus_9_Point_81_0_In_2_Seconds_From_0_0_0_To_0_Minus_19_point_62_0_FrameRate_60()
        {
            // Arrange
            Arrange(mass: 1, targetFrameRate: 60);

            // Act
            yield return new WaitForSeconds(2f);

            // Assert
            Assert.IsTrue(AssertTest(new Vector3(0f, -19.62f, 0f)));
        }

        [UnityTest]
        public IEnumerator Move_Object_3kg_Constant_Force_0_Minus_9_Point_81_0_In_2_Seconds_From_0_0_0_To_0_Minus_19_point_62_0_FrameRate_60()
        {
            // Arrange
            Arrange(mass: 3, targetFrameRate: 60);

            // Act
            yield return new WaitForSeconds(2f);

            // Assert
            Assert.IsTrue(AssertTest(new Vector3(0f, -19.62f, 0f)));
        }

        [UnityTest]
        public IEnumerator Move_Object_10kg_Constant_Force_0_Minus_9_Point_81_0_In_3_Seconds_From_0_0_0_To_0_Minus_44_point_145_0_FrameRate_30()
        {
            // Arrange
            Arrange(mass: 10, targetFrameRate: 30);

            // Act
            yield return new WaitForSeconds(3f);

            // Assert
            Assert.IsTrue(AssertTest(new Vector3(0f, -44.145f, 0f)));
        }
    }
}
