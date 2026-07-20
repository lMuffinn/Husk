/*
 * LightStopper.cs
 * Purpose: Stop the light from being pushed into the wall. This should be attatched to a child of the player.
 * Date Created: idk, 2023ish
 * Author: Matthew Eagleman
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightStopper : MonoBehaviour
{
    Transform tTransform;
    Transform parentTransform;
    public float checkRadius = 0.1f;
    public LayerMask Obstacle; // anything we don't want the light to get stuck under
    Vector3 pos; // keeps track of the last frame the light wasn't within the wall
    public float maxDistance = 4;
    // Start is called before the first frame update
    void Start()
    {
        tTransform = GetComponent<Transform>();
        parentTransform = GetComponentInParent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        // check if the light is within the wall
        bool touchingWall = Physics2D.OverlapCircle(tTransform.position, checkRadius, Obstacle);
        if (!touchingWall)
        {
            // if it is not within the wall, stick to the player
            pos = parentTransform.position; // only write when the players position is not within the wall
        }
        else
        {
            // if it is, keep the y position set to the last position the light was not inside the wall
            tTransform.position = new Vector3(parentTransform.position.x,pos.y,parentTransform.position.z);
        }
        float distance = Distance(tTransform.position, parentTransform.position);
        if (distance > maxDistance) tTransform.position = parentTransform.position;
        //Debug.Log(Vector3.Distance(tTransform.position, parentTransform.position));
    }

    float Distance(Vector3 light, Vector3 player)
    {
        // calculate the distance between the light source and the player
        float distance;
        // jesus i didn't know how to use vectors yet
        distance = ((light.x - player.x) * (light.x - player.x) + (light.y - player.y) * (light.y - player.y));
        return distance;
    }
}
