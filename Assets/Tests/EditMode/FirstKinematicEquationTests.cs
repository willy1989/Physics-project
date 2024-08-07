using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UIElements;

namespace Editor
{
    public class FirstKinematicEquationTests
    {
        protected KinematicEquations kinematicEquationComponent;

        [SetUp]
        public void SetUpTest()
        {
            GameObject someGameobject = new GameObject();

            kinematicEquationComponent = someGameobject.AddComponent<KinematicEquations>();
        }

        public class FinalVelocityMethod : FirstKinematicEquationTests
        {
            [Test]
            public void Input_InitialVelocity_00_Acceleration_01_DeltaTime_01_Output_01()
            {
                float finalVelocity = kinematicEquationComponent.FinalVelocity_1(initialVelocity: 0f, acceleration: 1f, deltaTime: 1f);

                Assert.AreEqual(finalVelocity, 1f);
            }

            [Test]
            public void Input_InitialVelocity_00_Acceleration_02_DeltaTime_01_Output_02()
            {
                float finalVelocity = kinematicEquationComponent.FinalVelocity_1(initialVelocity: 0f, acceleration: 2f, deltaTime: 1f);

                Assert.AreEqual(finalVelocity, 2f);
            }

            [Test]
            public void Input_InitialVelocity_01_Acceleration_05_DeltaTime_03_Output_16()
            {
                float finalVelocity = kinematicEquationComponent.FinalVelocity_1(initialVelocity: 1f, acceleration: 5f, deltaTime: 3f);

                Assert.AreEqual(finalVelocity, 16f);
            }

            [Test]
            public void Input_InitialVelocity_Minus01_Acceleration_01_DeltaTime_01_Output_00()
            {
                float finalVelocity = kinematicEquationComponent.FinalVelocity_1(initialVelocity: -1f, acceleration: 1f, deltaTime: 1f);

                Assert.AreEqual(finalVelocity, 0f);
            }

            [Test]
            public void Input_DeltaTime_Minus1_Output_Error()
            {
                Assert.Throws<ArgumentException>(() => kinematicEquationComponent.FinalVelocity_1(initialVelocity: 0f, acceleration: 1f, deltaTime: -1f));
            }



        }

        public class InitialVelocityMethod : FirstKinematicEquationTests
        {
            [Test]
            public void Input_FinalVelocity_01_Acceleration01_DeltaTime_01_Output_00()
            {
                float initialVelocity = kinematicEquationComponent.InitialVelocity_1(finalVelocity: 1f, acceleration: 1f, deltaTime: 1f);

                Assert.AreEqual(initialVelocity, 0f);
            }

            [Test]
            public void Input_FinalVelocity_05_Acceleration02_DeltaTime_01_Output_03()
            {
                float initialVelocity = kinematicEquationComponent.InitialVelocity_1(finalVelocity: 5f, acceleration: 2f, deltaTime: 1f);

                Assert.AreEqual(initialVelocity, 3f);
            }

            [Test]
            public void Input_FinalVelocity_00_Acceleration_Minus2_DeltaTime_02_Output_04()
            {
                float initialVelocity = kinematicEquationComponent.InitialVelocity_1(finalVelocity: 0f, acceleration: -2f, deltaTime: 2f);

                Assert.AreEqual(initialVelocity, 4f);
            }
        }




    }

}

