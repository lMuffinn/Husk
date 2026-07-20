/*
 * DetectPlayer.cs
 * Purpose: Keep track of whether the player is within sight of the father
 * Author: Matthew Eagleman
 * Date Created: 2023ish
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectPlayer : MonoBehaviour
{
    public float checkRadius = 6;
    public Transform eyes; // this is essentially the enemys' head. it wouldn't be the same as his position.
    public LayerMask player;
    public LayerMask ignore;
    public GameObject target; // this is the player. i would probably just find them in the script nowadays.
    RaycastHit2D canSeePlayer;
    public bool seePlayer;
    // the father will chase the player a small distance after losing sight. 
    public float objectpermanance = 2; // this stores the length of time to keep chasing the player after losing sight of him
    public float timer; // this keeps track of the time since the player was last seen

    // Start is called before the first frame update
    void Start()
    {
        timer = objectpermanance;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        // first check if the player is within a seeable distance
        if (Physics2D.OverlapCircle(transform.position, checkRadius, player))
        {
            // if they are, check if there are any obstacles between this gameObject and the player
            // this casts a ray between the player and this gameobject to see if it collides with any walls
            Vector3 direction = (target.GetComponent<Transform>().position - GetComponent<Transform>().position);
            canSeePlayer = Physics2D.Raycast(GetComponent<Transform>().position, direction, checkRadius, ignore);
            Debug.DrawRay(GetComponent<Transform>().position, direction);
            // TO DO:
                // i don't think this is good, ill probably need to change this
            if (!canSeePlayer) timer = objectpermanance; //reset the timer if you can't see the player? that doesn't seem quite right
        }
        timer -= Time.deltaTime;
        if (timer < 0) seePlayer = false;
        else if (timer >= 0) seePlayer = true;
    }
    private void OnDrawGizmos()
    {
        // draw the radius the father is able to see
        Gizmos.color = Color.gray;
        Gizmos.DrawWireSphere(eyes.position,checkRadius);
        
    }
}
