using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateOlderBrother : MonoBehaviour
{

    Rigidbody2D rb;
    Animator animator;
    SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponentInParent<Rigidbody2D>();
        spriteRenderer = GetComponentInParent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("Up", rb.linearVelocity.y > 0);
        if (rb.linearVelocity == new Vector2(0, 0)) animator.SetBool("walking", false);
        else
        {
            animator.SetBool("walking", true);
            if (rb.linearVelocity.x > 0) spriteRenderer.flipX = true;
            else if (rb.linearVelocity.x < 0) spriteRenderer.flipX = false;
        }
    }
}
