using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UIElements;

namespace Editor
{
    public class SecondKinematicEquationTests
    {
        class DeltaX
        {
            private KinematicEquations kinematicEquationComponent;

            [SetUp]
            public void SetUpTest()
            {
                GameObject someGameobject = new GameObject();

                kinematicEquationComponent = someGameobject.AddComponent<KinematicEquations>();
            }

            [Test]
            public void Input_FinalVelocity_01_InitialVelocity_00_DeltaTime_01_Output_01()
            {
                float finalVelocity = kinematicEquationComponent.DeltaX_2(finalVelocity: 1f, initialVelocity: 0f, deltaTime: 1f);

                Assert.AreEqual(finalVelocity, 0.5f);
            }
        }
    }
}
    
