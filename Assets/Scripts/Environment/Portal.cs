/*
 * Portal.cs
 * Purpose: Transport a character to another point on the map
 * Date Created: 2022ish
 * Authors: Matthew Eagleman
 */
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

// TO DO: 
// - get rid of tTransform and just use transform
// - use vector math instead of whatever fuck ass thing i was doing

public class Portal : MonoBehaviour
{
    Transform tTransform; // unnecessary
    public LayerMask Character; // the layers that the characters will be on
    Vector3 pos;
    public Vector2 size = new Vector2(1, 2); // the bounds that the character must enter to be transported
    public Transform exit; // position the player will be transported to
    Rect rect;
    // Start is called before the first frame update
    void Start()
    {
        tTransform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        pos = tTransform.position;
        // get the collider of the character that is within the bounds of the portal
        Collider2D col = Physics2D.OverlapArea(new Vector2(pos.x - size.x / 2, pos.y - size.y / 2), new Vector2(pos.x + size.x / 2, pos.y + size.y / 2), Character);
        // store whether the character is within the bounds of the portal. this is silly.
        bool touchingChar = Physics2D.OverlapArea(new Vector2(pos.x - size.x / 2, pos.y - size.y / 2), new Vector2(pos.x + size.x / 2, pos.y + size.y / 2), Character);
        // if the character is within the bounds of the portal, transport them to the exit location
        if (touchingChar)
        {
            col.gameObject.GetComponent<Transform>().position = new Vector2(exit.position.x, exit.position.y);
        }
    }
    private void OnDrawGizmos()
    {
        // draw the bounds of the portal
        // most of this could probably go within the DrawRect function
        rect.width = size.x;
        rect.height = size.y;
        rect.x = GetComponent<Transform>().position.x-size.x/2;
        rect.y = GetComponent<Transform>().position.y-size.y/2;
        Gizmos.color = Color.gray;
        DrawRect(rect);
    }
    void DrawRect(Rect rect)
    {
        // convert the DrawWireCube function to recognize and render Rects.
        // this should be within a library or something, there are multiple files that use it
        Gizmos.DrawWireCube(new Vector3(rect.center.x, rect.center.y, 0.01f), new Vector3(rect.size.x, rect.size.y, 0.01f));
    }
}
