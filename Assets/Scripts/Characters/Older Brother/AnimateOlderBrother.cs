/*
 * AnimateOlderBrother.cs
 * Purpose: Handle the animation of the older brother
 * Date Created: sometime in 2022
 * Authors: Matthew Eagleman, Trevor Eagleman
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// TO DO:
// - add transitions for the walking upwards animation

public class AnimateOlderBrother : MonoBehaviour
{
    Animator animator;
    SpriteRenderer spriteRenderer;
    InputAction moveAction;
    Vector2 moveInput;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponentInParent<SpriteRenderer>();
        moveAction = GetComponentInParent<PlayerInput>().actions.FindAction("Move");
        animator = GetComponent<Animator>(); 
    }

    // Update is called once per frame
    void Update()
    {
        // this uses the new movement system! good job trevor!
        // check if the player is pressing any buttons related to movement
        moveInput = moveAction.ReadValue<Vector2>();
        if (moveInput.magnitude == 0)
            // if none of the buttons are being pressed, this sets the animation to idle
            animator.SetBool("Walking", false);
        else
        {
            // if they are being pressed, this sets the animation to the walking animation
            animator.SetBool("Walking", true);
            if (moveInput.x > 0) spriteRenderer.flipX = true; // walking right
            else if (moveInput.x < 0) spriteRenderer.flipX = false; // walking left
        }
        animator.SetFloat("VelocityY", moveInput.y); // i don't think this is actually doing anything
    }
}
