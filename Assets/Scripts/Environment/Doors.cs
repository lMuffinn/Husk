/*
 * Doors.cs
 * Purpose: Handle opening and closing of doors. This is intended to be attached to the player
 * Date: 2022ish
 * Authors: Matthew Eagleman
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doors : MonoBehaviour
{
    public float doorRadius;
    public LayerMask doors;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // check if there is a door within range
        Collider2D door = Physics2D.OverlapCircle(transform.position, doorRadius, doors);
        if (Physics2D.OverlapCircle(transform.position, doorRadius, doors) && Input.GetKeyDown(KeyCode.E)) //WHY DID I TYPE OUT THE WHOLE THING TWICE???
        {
            // if the E key is pressed, and the player is overlapping with the door,
            // either activate or disable the collider and renderer of the doors.
            door.gameObject.GetComponentInChildren<SpriteRenderer>().enabled = !door.gameObject.GetComponentInChildren<SpriteRenderer>().enabled;
            door.gameObject.GetComponentInChildren<Collider2D>().isTrigger = !door.gameObject.GetComponentInChildren<Collider2D>().isTrigger;
            // i don't know if i'll actually keep this code, i think id rather make it look nice by having an
            // open and closed sprite. we shall see.
        }
    }
}
