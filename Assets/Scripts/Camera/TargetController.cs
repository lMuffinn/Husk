/*
 * TargetController.cs
 * Authors: Matthew Eagleman
 * Date Created: Fall 2025
 * Purpose: Control the movement of the Father
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Pathfinding;

// TO DO:
// - put find stairs into a library, as well as a distance function

public class TargetController : MonoBehaviour
{
    int floor = 0;
    public GameObject gameData;
    int playerfloor;
    Transform tTransform;
    //AIDestinationSetter AID;
    //-------------------
    public LayerMask up;
    public LayerMask down;
    Vector3 pos;
    public Transform collide;
    public float checkRadius;
    //-------------------
    public Transform[] tUp;
    public Transform[] tDown;
    public GameObject player;
    //-------------------
    public DetectPlayer detectPlayer;
    //-------------------
    GameObject[] tasks;
    int currentTask;
    bool doingTask;
    float taskTimer;
    public float taskTime = 4;
    bool taskChosen;
    public LayerMask taskLayer;
    //-------------------
    public AudioSource steps;
    //public AIPath aIPath;
    // Start is called before the first frame update
    void Start()
    {
        tasks = GameObject.FindGameObjectsWithTag("Task");
        tTransform = GetComponent<Transform>();
        //AID = GetComponent<AIDestinationSetter>();
    }
    // Update is called once per frame
    void Update()
    {
        playerfloor = player.GetComponent<Floor>().floor;
        pos = tTransform.position;
        floor = GetComponent<Floor>().floor;
        // checks if touching stairs----------------------------------------------------------------
        /*if (Physics2D.OverlapCircle(collide.position, checkRadius, up))
        {
            floor++;
            tTransform.position = new Vector3(pos.x, pos.y + 40, 0);
        }
        else if (Physics2D.OverlapCircle(collide.position, checkRadius, down))
        {
            floor--;
            tTransform.position = new Vector3(pos.x, pos.y - 40, 0);
        }*/
        // if can see player -----------------------------------------------------------------------
        if (detectPlayer.seePlayer)
        {
            steps.pitch = Mathf.Lerp(1,2.5f,1);
            //aIPath.maxSpeed = 6;
            // sets target to stairs or player depending on what floor each is on
            //AID.target = FindTarget(playerfloor,player);
        }
        //task system -------------------------------------------------------------------------------
        else if (!doingTask && !detectPlayer.seePlayer && !taskChosen)
        {
            currentTask = Random.Range(0, tasks.Length);
            taskChosen = true;
        }
        else if (!doingTask && !detectPlayer.seePlayer && taskChosen)
        {
            steps.pitch = Mathf.Lerp(2.5f, 1, 1);
            //aIPath.maxSpeed = 3;
            //Debug.Log("enemy target floor:" + tasks[currentTask].GetComponent<Floor>().floor + " enemy floor:" + floor);
            //AID.target = FindTarget(tasks[currentTask].GetComponent<Floor>().floor,tasks[currentTask]);
            if (Physics2D.OverlapCircle(collide.position, checkRadius, taskLayer))
            {
                doingTask = true;
                taskTimer = taskTime;
            }
        }
        else if (doingTask && !detectPlayer.seePlayer)
        {
            steps.pitch = 0;
            taskTimer -= Time.deltaTime;
            if (taskTimer < 0) 
            { 
                doingTask = false;
                taskChosen = false;
            }
        }
    }
    Transform GetClosestStairs(List<Transform> stairs, Transform fromThis)
    {
        //yet another thing thats been defined multiple times, we'll
        Transform bestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = fromThis.position;
        foreach (Transform potentialTarget in stairs)
        {
            Vector3 directionToTarget = potentialTarget.position - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if (dSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToTarget;
                bestTarget = potentialTarget;
            }
        }
        return bestTarget;
    }
    Transform FindTarget(int level, GameObject mainTarget)
    {
        Transform newTarget = null;
        if (level == floor) 
        { 
            newTarget = mainTarget.GetComponent<Transform>(); 
        }
        else if (level > floor)
        {
            newTarget = GetClosestStairs(new List<Transform>(tUp), GetComponent<Transform>());
        }
        else if (level < floor)
        {
            newTarget = GetClosestStairs(new List<Transform>(tDown), GetComponent<Transform>());
        }
        return newTarget;
    }
}