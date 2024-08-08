using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class FirstKinematicEquationTests
{
    public class ConstantForce
    {
        private KinematicEquations kinematicEquationsComponent;

        private Constant_ForceType constantForceComponent;

        private GameObject physicsObject;



        [SetUp]
        public void SetUpTest()
        {
            kinematicEquationsComponent = new GameObject().AddComponent<KinematicEquations>();

            constantForceComponent = new GameObject().AddComponent<Constant_ForceType>();

            physicsObject = new GameObject();
        }

        [UnityTest]
        public IEnumerator Move_Object_1kg_With_Force_1_In_2_Seconds_FrameRate_60_To_00_00_02()
        {
            // Arrange

            constantForceComponent.SetUp(_force: 1f, _dimension: DirectionDimension.ZPOSITIVE);

            OneDimensionForceApplier oneDimensionForceApplier = physicsObject.AddComponent<OneDimensionForceApplier>();

            oneDimensionForceApplier.SetUp(_kinematicEquations: kinematicEquationsComponent, _forceTypes: new ForceType[] { constantForceComponent }, _mass: 1);

            Application.targetFrameRate = 60;

            TimerComponent timerComponent = new GameObject().AddComponent<TimerComponent>();

            yield return timerComponent.StartTimer(duration: 2f);


            // Assert

            Vector3 expectedPosition = new Vector3(0f, 0f, 2f);

            float deltaBetweenExpectedAndActualPosition = (expectedPosition - physicsObject.transform.position).magnitude;

            Assert.Less(deltaBetweenExpectedAndActualPosition, 0.1f);
        }

        [UnityTest]
        public IEnumerator Move_Object_1kg_With_Force_1_In_2_Seconds_FrameRate_30_To_00_00_02()
        {
            // Arrange

            constantForceComponent.SetUp(_force: 1f, _dimension: DirectionDimension.ZPOSITIVE);

            OneDimensionForceApplier oneDimensionForceApplier = physicsObject.AddComponent<OneDimensionForceApplier>();

            oneDimensionForceApplier.SetUp(_kinematicEquations: kinematicEquationsComponent, _forceTypes: new ForceType[] { constantForceComponent }, _mass: 1);

            TimerComponent timerComponent = new GameObject().AddComponent<TimerComponent>();

            yield return timerComponent.StartTimer(duration: 2f);

            Application.targetFrameRate = 30;


            // Assert

            Vector3 expectedPosition = new Vector3(0f, 0f, 2f);

            float deltaBetweenExpectedAndActualPosition = (expectedPosition - physicsObject.transform.position).magnitude;

            Assert.Less(deltaBetweenExpectedAndActualPosition, 0.1f);
        }

        [UnityTest]
        public IEnumerator Move_Object_2kg_With_Force_1_In_10_Seconds_FrameRate_60_To_00_00_25()
        {
            // Arrange

            constantForceComponent.SetUp(_force: 1f, _dimension: DirectionDimension.ZPOSITIVE);

            OneDimensionForceApplier oneDimensionForceApplier = physicsObject.AddComponent<OneDimensionForceApplier>();

            oneDimensionForceApplier.SetUp(_kinematicEquations: kinematicEquationsComponent, _forceTypes: new ForceType[] { constantForceComponent }, _mass: 2);

            TimerComponent timerComponent = new GameObject().AddComponent<TimerComponent>();

            yield return timerComponent.StartTimer(duration: 10f);

            Application.targetFrameRate = 60;


            // Assert

            Vector3 expectedPosition = new Vector3(0f, 0f, 25f);

            float deltaBetweenExpectedAndActualPosition = (expectedPosition - physicsObject.transform.position).magnitude;

            Assert.Less(deltaBetweenExpectedAndActualPosition, 0.1f);
        }

        [UnityTest]
        public IEnumerator Move_Object_5kg_With_Force_Minus3_In_3_Seconds_FrameRate_20_To_00_Minus02Point07_00()
        {
            // Arrange

            constantForceComponent.SetUp(_force: 3f, _dimension: DirectionDimension.YNEGATIVE);

            OneDimensionForceApplier oneDimensionForceApplier = physicsObject.AddComponent<OneDimensionForceApplier>();

            oneDimensionForceApplier.SetUp(_kinematicEquations: kinematicEquationsComponent, _forceTypes: new ForceType[] { constantForceComponent }, _mass: 5);
            
            TimerComponent timerComponent = new GameObject().AddComponent<TimerComponent>();

            yield return timerComponent.StartTimer(duration: 3f);

            Application.targetFrameRate = 60;

            // Assert

            Vector3 expectedPosition = new Vector3(0f, -2.7f, 0f);

            float deltaBetweenExpectedAndActualPosition = (expectedPosition - physicsObject.transform.position).magnitude;

            Assert.Less(deltaBetweenExpectedAndActualPosition, 0.1f);
        }
    }
}
