# Introduction

This is a personal project. Its goal is to simulate how an object would behave according to the laws of physics. 

Having taken physics classes from Khan Academy, I want to apply this knowledge to a concrete project. Applying this knowledge in different situations would help cement it in my mind.

Instead of researching the inner workings of existing physics engines, I thought it would be more interesting to take that raw knowledge and apply it directly with no preconceived notions of how physics engines work. Of course, I am aware that Unity provides a built-in physics engine. And for any other project needing physics simulation, I would turn to it first. Instead, the idea was to try to come up with my own solutions for the sake of practicing. 

The system I created is rather basic. It relies mainly on kinematic equations and applying forces to objects:

- Constant forces (gravity for example)
- Normal forces (when pushing against a surface)
- Kinetic and static friction forces

For the sake of simplicity, I didn't use more advanced concepts such as energy, torque, center of gravity, etc. That being said, momentum comes into play in this scenario: a moving object with no net force acting on it. Momentum allows us to calculate a contact force when the object hits a surface.

# Project overview

This project was made with Unity and all the physics simulation was coded from scratch. I just use Unity for displaying the graphics and setting the positions of the objects.

The physics objects are able to do the following actions:

- Moving around
- Sliding against surfaces
- Being blocked by surfaces when perpendicular to it
- Being slowed down by friction or even being held in place

I created a scene to illustrate each situation: 

![](https://lh7-rt.googleusercontent.com/docsz/AD_4nXfu_YddrgXlI1xlTEyNxzbyjtYxcUB8c8LgzJOCtPG4zp8jKbg-LnAehoH01QWknteSYILfiUlPWa_fC7I-9cYL07LtyQCq5Gja8VxkPWgN3NUBMFWM4GVn-CGsPeFqOpCUKt6ZKwqzemNU9banEdfTPMfX?key=-fYGQ1yML5972xK0d2S93w)

# Code architecture 

The code base is made of 3 main modules: a physics object, a force manager and the kinematic functions library.

## The physics object

It is responsible for moving itself around the game world. To do so, it keeps track of its mass and current velocity. Each frame it computes its new velocity and displacement based on the force vector it receives from the force manager. 

## The force manager

The force manager computes all the forces and combines them into a single Vector3, which is then passed to the physics object. 

## The kinematic functions library

This library is structured around the 4 kinematic equations. 

$$v = v_0 + at$$
$$x = v_0 t + \frac{1}{2} a t^2$$
$$v^2 = v_0^2 + 2ax$$
$$x = \frac{(v + v_0)}{2} t$$

Each equation is broken down into several functions that have different input and output. The idea is to have every form of each equation available.

For instance, here are several versions of the 1st equation: 

final velocity = initial velocity  + acceleration * delta time

Based on the input that you know and the output that you are looking to get, you would pick the one you need.

```
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
```

# Forces calculations

The main challenge was to figure out in which order to calculate each force. This is because some forces are derived from other forces. For instance, friction and normal forces are computed from constant forces pushing against a surface.

```
public Vector3 CombinedForces(float mass, Vector3 finalVelocity)
{
    Vector3 combinedConstantForces = Vector3.zero;

    foreach (var constantForceController in constantsForceControllers)
    {
        combinedConstantForces += constantForceController.ConstantForce();
    }

    // Calculate normal forces individually. One per surface. Then use each to calculate each corresponding static and friction forces. 
    List<Vector3> normalForces = NormalForces(pushForce: combinedConstantForces);

    Vector3 combinedImpactForces = ImpactForces(mass: mass, finalVelocity: finalVelocity);

    Vector3 combinedNormalForces = Vector3.zero;

    if(normalForces != null)
    {
        foreach (Vector3 normalForce in normalForces)
        {
            combinedNormalForces += normalForce;
        }
    }

    Vector3 combinedKineticFrictionForces = KineticFrictionForce(pushForce: combinedConstantForces, finalVelocity: finalVelocity);

    Vector3 combinedStaticFrictionForces = StaticFrictionForce(pushForce: combinedConstantForces, finalVelocity: finalVelocity);

    Vector3 result = combinedConstantForces + combinedNormalForces + combinedImpactForces + combinedStaticFrictionForces + combinedKineticFrictionForces;

    return result;
}
```
## Normal force

In the scenario where the object is already in contact with a surface, I use the constant force pushing against it to derive the normal force. If the force is not perpendicular to the surface, we use trigonometry to have the object slide against it. It works the same for a slanted floor. The steeper the slope, the slower the physics object will climb it.

When the object is moving, but no forces are applied to it, we can’t use the normal force formula, since it requires the push force as an input. Instead we use the momentum to derive the normal force: mass * velocity, which is information that we do have.

## Friction forces

Kinetic friction force slows down an already moving object, while the static friction force holds an object in place until it is pushed with a sufficient force that overcomes the static friction force. 

One problem I couldn’t solve is the kinetic friction bringing the object to a complete halt. I couldn’t figure out how to bring the velocity down to 0. The friction force is opposite to the current direction the object is moving towards. The problem occurred when the velocity became very small, and the friction force vector would be larger. It caused the direction of the object to flip each frame, never reaching 0. This also caused problems with the static friction calculation, as it only kicks in when the velocity of the object is zero. So, in the simulation, once an object started moving it never truly came to a stop.


# Unit testing

As I started the project, I realized I was spending a great deal of time making sure the kinematic equations and the rest of the methods that rely on them were working accurately. Every change, every refactor of the code, involved running the simulation in different scenarios with different parameters to make sure the math and the object's behaviors were still correct. Here are the elements I was keeping an eye on:

- Kinematic equations: calculations for velocity, acceleration, displacement and, time
- Time taken for an object to cover a certain distance, with different forces and distances
- Different frame rates

## Edit mode tests

I wrote edit mode tests for the kinematic equations methods, as they are very straightforward and purely about math. This experience already made me realize the pitfalls and blind spots of my current code. For instance, I originally didn’t account for negative deltaTime as an input or an output. So now I throw an error whenever time is negative. Naturally I also added a test for it. Additionally, I didn’t handle division by zero.

```
[Test]
public void Input_InitialVelocity_0_Acceleration_1_DeltaTime_1_Output_1()
{
    float finalVelocity = KinematicEquations.FinalVelocity_1(initialVelocity: 0f, acceleration: 1f, deltaTime: 1f);

	Assert.AreEqual(1f, finalVelocity);
}
```

```
[Test]
public void Input_InitialVelocity_0_Acceleration_2_DeltaTime_1_Output_2()
{
    float finalVelocity = KinematicEquations.FinalVelocity_1(initialVelocity: 0f, acceleration: 2f, deltaTime: 1f);

    Assert.AreEqual(2f, finalVelocity);
}
```

## Play mode tests

I also created tests for the complete program, as I am testing the behavior of objects. I would typically test how long it takes for one object to reach a certain distance, given a certain force is applied to them. 
And even though they cover basic scenarios, and take longer to run, as you have to actually wait in real time for the object to reach its destination, because they are automated, they still allowed me to save so much time.

## Challenges

- Naming convention for tests (method names): what information should we include? Input, outputs, something else?
- Architecture for tests: how to divide classes and modules so that the tests are clearly listed in a tree on the editor, and to take advantage of reusing code (setup phase)

As should be obvious by now, writing tests for this project has one major advantage: saving a lot of time. Indirectly, it also made me more confident and more relaxed about making changes to the code. Because, if something broke, I would spot it quickly, most of the time at least. 

On the other hand, there was one small downside, and that only affected the play mode tests: classes need to be more open, to reveal more of their internal fields, properties and, methods, thus increasing overall complexity. This is because the tests create instances of the necessary modules and inject their dependencies through code. When not working with tests, I would simply drag and drop the references to the serialized fields. 


# Conclusion

While this system is far from having comprehensive features, I think I learned enough to create more than just basic character controllers. For instance, I now know how to make a character slide against a wall (normal force) or have it slow down gradually against the floor (friction force). In a way, even though it is limited, I can now create custom physics that will fit the particular goals of a game.
