/*
 * BrotherMovement.cs
 * Purpose: Handle the AI for the little brothers movement, this file needs a lot of updating
 * Date Created: sometime in 2022ish
 * Authors: Matthew Eagleman
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrotherMovement : MonoBehaviour
{
    int floor = 2;
    public Transform[] tUp;
    public Transform[] tDown;
    public LayerMask up;
    public LayerMask down;
    int playerfloor;
    public GameObject player;
    Transform target;
    public GameObject cinemachine;
    public Collider2D[] boundary;
    public float lenseSize = 22;


    // Start is called before the first frame update
    void Start()
    {
        GetComponent<LoneSection>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        playerfloor = player.GetComponent<Floor>().floor;
        floor = GetComponent<Floor>().floor;
        target = GetComponent<LoneSection>().target;
        if (player.GetComponent<Items>().littleBrotherSection)
        {
            // so i had planned for there to be a section where you have to tell the younger brother what to do because you get
            // stuck under a cabinet or something. this was the start of that idea. I don't think i like it anymore, and will
            // probably get rid of this.
            Debug.Log(target.gameObject.GetComponent<Floor>().floor);
            //AID.target = FindTarget(target.gameObject.GetComponent<Floor>().floor, target.gameObject);
            //cinemachine.GetComponent<CinemachineConfiner2D>().m_BoundingShape2D = boundary[GetComponent<Floor>().floor];
            //cinemachine.GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize = lenseSize;
        }
        else
        {
            // sets the target of the A* algorithm. find the FindTarget algorithm to see how it works
            //AID.target = FindTarget(playerfloor, player);
        }
    }

    Transform GetClosestStairs(List<Transform> stairs, Transform fromThis)
    {
        // get the closest stairs from this gameObject
        // this is ridiculous. why did i keep writing distance functions?
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
        // i did some funky stuff here so that gus would target the stairs if the micheal was on a different floor than him
        // basically if micheal is on a floor above or below, this would set the A* target to be the nearest stairs to 
        // move up or down to get closer to him. otherwise it would just be micheal.
        Transform newTarget = null;
        if (level == floor)
        {
            newTarget = mainTarget.GetComponent<Transform>();
            //path.endReachedDistance = 3; 
        }
        else if (level > floor)
        {
            newTarget = GetClosestStairs(new List<Transform>(tUp), GetComponent<Transform>());
            //path.endReachedDistance = 0.2f;
        }
        else if (level < floor)
        {
            newTarget = GetClosestStairs(new List<Transform>(tDown), GetComponent<Transform>());
            //path.endReachedDistance = 0.2f;
        }
        return newTarget;
    }

}
