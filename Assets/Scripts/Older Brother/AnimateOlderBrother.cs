using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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
        moveInput = moveAction.ReadValue<Vector2>();
        if (moveInput.magnitude == 0)
            animator.SetBool("Walking", false);
        else
        {
            animator.SetBool("Walking", true);
            if (moveInput.x > 0) spriteRenderer.flipX = true;
            else if (moveInput.x < 0) spriteRenderer.flipX = false;
        }
        animator.SetFloat("VelocityY", moveInput.y);
    }
}
