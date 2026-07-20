/*
 * Animate.cs
 * Purpose: Control Gus Animations
 * Date Created: like, sometime in fall 2022
 * Authors: Matthew Eagleman
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// IMPORTANT NOTE: 
//  the younger brothers movement is updated with the A* pathfinding algorithm. 
// at this moment i do not have a pathfinding algorithm picked out, but the one i was using
// operated by updating the position of its given gameobject. because of this, anything with 
// movement controlled by the algorithm will not register in triggers/colliders. for it to 
// register in triggers/colliders, the movement has to be spurred by unitys built in physics 
// system, e.g. AddForce. i will have to give this some thought, im not sure i like the way
// things are working right now.

// TO DO:
// - use built in transform instead of tTransform

public class Animate : MonoBehaviour
{
    //Global Variable Declarations
    Animator animator; 
    Vector3 old;
    Vector3 recent;
    Transform tTransform;
    // Start is called before the first frame update
    void Start()
    {
        //Assigns variables to the actual components on the gameobjects.
        animator = GetComponent<Animator>();
        tTransform = GetComponent<Transform>();
        old = tTransform.position;

    }

    // Update is called once per frame
    void Update()
    {
        recent = tTransform.position; // save the position during this frame
        Vector2 vel = FindVelocity(old, recent); // the previous line seems unnecesary, i could just put transform.position directly in the function
        // when gus moves up, this sets the parameter in the animator so that the animation for 
        // walking upwards can be played.
        animator.SetBool("Up", vel.y > 0); 
        // if gus is standing still, play the idle animation
        if (vel.x == 0) animator.SetBool("Walking", false);
        // if gus' velocity is not zero, play the walking animation
        else if (vel.x > 0)
        {
            animator.SetBool("Walking", true);
            GetComponent<SpriteRenderer>().flipX = true; // walking right
        }
        else if (vel.x < 0)
        {
            animator.SetBool("Walking", true);
            GetComponent<SpriteRenderer>().flipX = false; // walking left
        }
        old = recent;
    }

    Vector2 FindVelocity(Vector3 old, Vector3 recent)
    {
        // we had to find the velocity manually because the A* pathfinding algorithm
        // updates the position of the of whatever it is attached to, without using unitys built in physics.
        Vector2 velocity;
        velocity = new Vector2((recent.x - old.x), (recent.y - old.y));
        return velocity;
    }
}
