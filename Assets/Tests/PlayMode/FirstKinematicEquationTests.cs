using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class FirstKinematicEquationTests
{
    public class ConstantForce
    {
        [UnityTest]
        public IEnumerator Move_Object_Of_1kg_With_Force_1_To_00_00_1_In_2_Seconds()
        {
            KinematicEquations kinematicEquationsComponent = new GameObject().AddComponent<KinematicEquations>();

            Constant_ForceType constantForceComponent = new GameObject().AddComponent<Constant_ForceType>();

            constantForceComponent.SetUp(_force: 1f, _dimension: DirectionDimension.ZPOSITIVE);


            // Physics object
            GameObject physicsObject = new GameObject();

            OneDimensionForceApplier oneDimensionForceApplier = physicsObject.AddComponent<OneDimensionForceApplier>();

            oneDimensionForceApplier.SetUp(_kinematicEquations: kinematicEquationsComponent, _forceTypes: new ForceType[] { constantForceComponent }, _mass: 1);

            float elapsedTime = 0;
            float duration = 2f;


            while(elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            Debug.Log(physicsObject.transform.position);

            Assert.AreEqual(physicsObject.transform.position, new Vector3(0f, 0f, 1f));
        }
    }

    
}
