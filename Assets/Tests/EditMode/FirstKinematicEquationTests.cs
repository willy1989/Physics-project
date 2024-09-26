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
        [SetUp]
        public void SetUpTest()
        {
            GameObject someGameobject = new GameObject();
        }

        public class FinalVelocityMethod : FirstKinematicEquationTests
        {
            [Test]
            public void Input_InitialVelocity_0_Acceleration_1_DeltaTime_1_Output_1()
            {
                float finalVelocity = KinematicEquations.FinalVelocity_1(initialVelocity: 0f, acceleration: 1f, deltaTime: 1f);

                Assert.AreEqual(1f, finalVelocity);
            }

            [Test]
            public void Input_InitialVelocity_0_Acceleration_2_DeltaTime_1_Output_2()
            {
                float finalVelocity = KinematicEquations.FinalVelocity_1(initialVelocity: 0f, acceleration: 2f, deltaTime: 1f);

                Assert.AreEqual(2f, finalVelocity);
            }

            [Test]
            public void Input_InitialVelocity_1_Acceleration_5_DeltaTime_3_Output_16()
            {
                float finalVelocity = KinematicEquations.FinalVelocity_1(initialVelocity: 1f, acceleration: 5f, deltaTime: 3f);

                Assert.AreEqual(16f, finalVelocity);
            }

            [Test]
            public void Input_InitialVelocity_Minus1_Acceleration_1_DeltaTime_1_Output_0()
            {
                float finalVelocity = KinematicEquations.FinalVelocity_1(initialVelocity: -1f, acceleration: 1f, deltaTime: 1f);

                Assert.AreEqual(0f, finalVelocity);
            }

            [Test]
            public void Input_DeltaTime_Minus1_Output_Error()
            {
                Assert.Throws<ArgumentException>(() => KinematicEquations.FinalVelocity_1(initialVelocity: 0f, acceleration: 1f, deltaTime: -1f));
            }
        }

        public class InitialVelocityMethod : FirstKinematicEquationTests
        {
            [Test]
            public void Input_FinalVelocity_1_Acceleration1_DeltaTime_1_Output_0()
            {
                float initialVelocity = KinematicEquations.InitialVelocity_1(finalVelocity: 1f, acceleration: 1f, deltaTime: 1f);

                Assert.AreEqual(0f, initialVelocity);
            }

            [Test]
            public void Input_FinalVelocity_5_Acceleration2_DeltaTime_1_Output_3()
            {
                float initialVelocity = KinematicEquations.InitialVelocity_1(finalVelocity: 5f, acceleration: 2f, deltaTime: 1f);

                Assert.AreEqual(3f, initialVelocity);
            }

            [Test]
            public void Input_FinalVelocity_0_Acceleration_Minus2_DeltaTime_2_Output_4()
            {
                float initialVelocity = KinematicEquations.InitialVelocity_1(finalVelocity: 0f, acceleration: -2f, deltaTime: 2f);

                Assert.AreEqual(4f, initialVelocity);
            }

            [Test]
            public void Input_DeltaTime_Minus1_Output_Error()
            {
                Assert.Throws<ArgumentException>(() => KinematicEquations.InitialVelocity_1(finalVelocity: 1f, acceleration: 1f, deltaTime: -1f));
            }
        }

        public class AccelerationMethod: FirstKinematicEquationTests
        {
            [Test]
            public void Input_InitialVelocity_0_FinalVelocity_1_DeltaTime_1_Output_1()
            {
                float acceleration = KinematicEquations.Acceleration_1(initialVelocity: 0f, finalVelocity: 1f, deltaTime: 1f);

                Assert.AreEqual(1f, acceleration);
            }

            [Test]
            public void Input_InitialVelocity_Minus1_FinalVelocity_5_DeltaTime_3_Output_2()
            {
                float acceleration = KinematicEquations.Acceleration_1(initialVelocity: -1f, finalVelocity: 5f, deltaTime: 3f);

                Assert.AreEqual(2f, acceleration);
            }

            [Test]
            public void Input_InitialVelocity_Minus1_FinalVelocity_Minus3_DeltaTime_5_Output_Minus0Point4()
            {
                float acceleration = KinematicEquations.Acceleration_1(initialVelocity: -1f, finalVelocity: -3f, deltaTime: 5f);

                Assert.AreEqual(-0.4f, acceleration);
            }

            [Test]
            public void Input_DeltaTime_Minus1_Output_Error()
            {
                Assert.Throws<ArgumentException>(() => KinematicEquations.Acceleration_1(initialVelocity: 1f, finalVelocity: 1f, deltaTime: -1f));
            }
        }

        public class DeltaTimeMethod: FirstKinematicEquationTests
        {
            [Test]
            public void Input_InitialVelocity_0_FinalVelocity_1_Acceleration_1_Output_1()
            {
                float deltaTime = KinematicEquations.DeltaTime_1(initialVelocity: 0f, finalVelocity: 1f, acceleration: 1f);
                Assert.AreEqual(1f, deltaTime);
            }

            [Test]
            public void Input_InitialVelocity_0_FinalVelocity_3_Acceleration_2_Output_1Point5()
            {
                float deltaTime = KinematicEquations.DeltaTime_1(initialVelocity: 0f, finalVelocity: 3f, acceleration: 2f);
                Assert.AreEqual(1.5f, deltaTime);
            }

            [Test]
            public void Input_InitialVelocity_6_FinalVelocity_5_Acceleration_3_Output_Error()
            {
                Assert.Throws<ArgumentException>(() => KinematicEquations.DeltaTime_1(initialVelocity: 6f, finalVelocity: 5f, acceleration: 3f));
            }
        }


    }

}

