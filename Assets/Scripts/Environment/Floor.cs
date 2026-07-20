/*
 * Floor.cs
 * Purpose: Hold the value for the floor the gameobject is on
 * Date Created: sometime 2022
 * Authors: Matthew Eagleman
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour
{
    // this is so funny to me lol. 
    // this actually doesn't seem like a bad idea. There might be a better way to do it though.
    // the reason its so simple is so that the floorDetector can just get a list of all the
    // objects of type Floor really easily.
    // that way you can just attach it to anything that needs to keep track of the floor.
    public int floor = 0;
}
