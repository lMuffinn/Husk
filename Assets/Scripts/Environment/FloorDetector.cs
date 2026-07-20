/*
 * FloorDetector.cs
 * Purpose: Update GameObjects of type Floor to be accurate to the floor they are on.
 * Right now it is attached to empty game objects representing the bounds of each floor.
 * Date Created: 2022ish
 * Authors: Matthew Eagleman
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TO DO: USE ACTUAL VECTOR MATH JEEZ

public class FloorDetector : MonoBehaviour
{
    public LayerMask Character;
    Vector3 pos;
    public Vector2 size = new Vector2(1, 2); // define the size of the floor Manually in the editor. 
    Rect rect;
    public int floor = 0; // defines which floor that this gameobject is representing. also manually in the editor.
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        pos = transform.position;
        // get a list of all the colliders of gameobjects with the Floor.cs script attached
        // again with the wacky vector math without using the built in vector math ;_;
        Collider2D[] col = Physics2D.OverlapAreaAll(new Vector2(pos.x - size.x / 2, pos.y - size.y / 2), new Vector2(pos.x + size.x / 2, pos.y + size.y / 2), Character);
        for (int i = 0; i < col.Length; i++)
        {
            // update the floor value for all the objects it found
            col[i].gameObject.GetComponent<Floor>().floor = floor;
        }
    }
    private void OnDrawGizmos()
    {
        // draw the bounds of the floor for debugging purposes
        rect.width = size.x;
        rect.height = size.y;
        rect.x = transform.position.x - size.x / 2;
        rect.y = transform.position.y - size.y / 2;
        Gizmos.color = Color.gray;
        DrawRect(rect);
    }
    void DrawRect(Rect rect)
    {
        // convert the DrawWireCube function to recognize and render Rects 
        Gizmos.DrawWireCube(new Vector3(rect.center.x, rect.center.y, 0.01f), new Vector3(rect.size.x, rect.size.y, 0.01f));
    }
}
