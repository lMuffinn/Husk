/*
 * CameraController.cs
 * Authors: Matthew Eagleman
 * Date Created: spring 2023ish? maybe?
 * Purpose: Control the movement of the older brother
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    GameObject GameData;
    Transform position;
    // Start is called before the first frame update
    void Start()
    {
        //I don't think that the "gameData" gameobject is very useful as is.
        //There are kinda a few different versions of it with different names
        //We should make one single gameobject for management.
        GameData = GameObject.FindGameObjectWithTag("GameData"); //finds the GameData gameobject, which i think is not really used
        position = GetComponent<Transform>(); //gets the position of the camera
    }

    // Update is called once per frame
    void Update()
    {
        int floor = GameData.GetComponent<GameData>().floor; //gets the floor? god i wish i commented my code cuz i have no idea what this does
        // I think what this is doing is setting the camera position to align with the floor that the player is on.
        // this code is stupid and bad, we should not do it this way
        position.position = new Vector3(0, (floor-2)*40, -10); 
    }
}
