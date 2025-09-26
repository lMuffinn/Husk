using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animate : MonoBehaviour
{
    Animator animator;
    Vector3 old;
    Vector3 recent;
    Transform tTransform;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        tTransform = GetComponent<Transform>();
        old = tTransform.position;

    }

    // Update is called once per frame
    void Update()
    {
        recent = tTransform.position;
        Vector2 vel = FindVelocity(old, recent);
        animator.SetBool("Up", vel.y > 0);
        if (vel.x == 0) animator.SetBool("Walking", false);
        else if (vel.x > 0)
        {
            animator.SetBool("Walking", true);
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else if (vel.x < 0)
        {
            animator.SetBool("Walking", true);
            GetComponent<SpriteRenderer>().flipX = false;
        }
        old = recent;
    }

    Vector2 FindVelocity(Vector3 old, Vector3 recent)
    {
        Vector2 velocity;
        velocity = new Vector2((recent.x - old.x), (recent.y - old.y));
        return velocity;
    }
}
