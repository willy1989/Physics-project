using NUnit.Framework;
using PhysicsObject;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TestTools;

public class ForceTesting
{
    public class ConstantForceTesting
    {
        private PhysicsObject.PhysicsObject physicsObject;

        private BoxCastCollisionManager boxCastCollisionManager;

        private ForceManager forceManager;

        private List<ConstantForceController> constantForceControllers;

        [SetUp]
        public void SetUpTest()
        {
            physicsObject = new GameObject().AddComponent<PhysicsObject.PhysicsObject>();

            boxCastCollisionManager = new GameObject().AddComponent<BoxCastCollisionManager>();

            forceManager = new GameObject().AddComponent<ForceManager>();

            constantForceControllers = new List<ConstantForceController>();
        }

        private void BaseArrange(float mass, int targetFrameRate)
        {
            // Arrange

            forceManager.SetUp(boxCastCollisionManager, constantForceControllers);

            physicsObject.SetUp(forceManager, mass: mass);

            Application.targetFrameRate = targetFrameRate;
        }

        private void ArrangeGravityForceController()
        {
            Gravity_ConstantForceController gravity_ConstantForceController = new GameObject().AddComponent<Gravity_ConstantForceController>();

            gravity_ConstantForceController.SetUp(physicsObject);

            constantForceControllers.Add(gravity_ConstantForceController);
        }

        private void ArrangeDirectionForceController(Vector3 directionVector, float force)
        {
            Direction_ConstantForceController forceController = new GameObject().AddComponent<Direction_ConstantForceController>();

            forceController.SetUp(directionVector, force);

            constantForceControllers.Add(forceController);
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
            BaseArrange(mass: 1, targetFrameRate: 60);

            ArrangeGravityForceController();

            // Act
            yield return new WaitForSeconds(2f);

            // Assert
            Assert.IsTrue(AssertTest(new Vector3(0f, -19.62f, 0f)));
        }

        [UnityTest]
        public IEnumerator Move_Object_3kg_Constant_Force_0_Minus_9_Point_81_0_In_2_Seconds_From_0_0_0_To_0_Minus_19_point_62_0_FrameRate_60()
        {
            // Arrange
            BaseArrange(mass: 3, targetFrameRate: 60);

            ArrangeGravityForceController();

            // Act
            yield return new WaitForSeconds(2f);

            // Assert
            Assert.IsTrue(AssertTest(new Vector3(0f, -19.62f, 0f)));
        }

        [UnityTest]
        public IEnumerator Move_Object_10kg_Constant_Force_0_Minus_9_Point_81_0_In_3_Seconds_From_0_0_0_To_0_Minus_44_point_145_0_FrameRate_30()
        {
            // Arrange
            BaseArrange(mass: 10, targetFrameRate: 30);

            ArrangeGravityForceController();

            // Act
            yield return new WaitForSeconds(3f);

            // Assert
            Assert.IsTrue(AssertTest(new Vector3(0f, -44.145f, 0f)));
        }

        [UnityTest]
        public IEnumerator Move_Object_1kg_Constant_Force_0_0_5_In_2_Seconds_From_0_0_0_To_0_0_0_FrameRate_60()
        {
            // Arrange
            BaseArrange(mass: 1, targetFrameRate: 60);

            ArrangeDirectionForceController(directionVector: new Vector3(0f,0f,1f), 5f);

            // Act
            yield return new WaitForSeconds(2f);

            // Assert
            Assert.IsTrue(AssertTest(new Vector3(0f, 0f, 10f)));
        }

        [UnityTest]
        public IEnumerator Move_Object_2kg_Constant_Force_0_0_5_In_2_Seconds_From_0_0_0_To_0_0_0_FrameRate_30()
        {
            // Arrange
            BaseArrange(mass: 2, targetFrameRate: 30);

            ArrangeDirectionForceController(directionVector: new Vector3(0f, 0f, 1f), 5f);

            // Act
            yield return new WaitForSeconds(2f);

            // Assert
            Assert.IsTrue(AssertTest(new Vector3(0f, 0f, 5f)));
        }
    }
}
