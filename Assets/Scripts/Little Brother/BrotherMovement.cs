using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Pathfinding;
using Cinemachine;

public class BrotherMovement : MonoBehaviour
{
    int floor = 2;
    public Transform[] tUp;
    public Transform[] tDown;
    public LayerMask up;
    public LayerMask down;
    int playerfloor;
    public GameObject player;
    //AIDestinationSetter AID;
    //AIPath path;
    Transform target;
    public GameObject cinemachine;
    public Collider2D[] boundary;
    public float lenseSize = 22;


    // Start is called before the first frame update
    void Start()
    {
        //AID = GetComponent<AIDestinationSetter>();
        //path = GetComponent<AIPath>();
        GetComponent<LoneSection>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        playerfloor = player.GetComponent<Floor>().floor;
        //Debug.Log("brother: " + floor + " player: " + playerfloor);
        floor = GetComponent<Floor>().floor;
        target = GetComponent<LoneSection>().target;
        if (player.GetComponent<Items>().littleBrotherSection)
        {
            Debug.Log(target.gameObject.GetComponent<Floor>().floor);
            //AID.target = FindTarget(target.gameObject.GetComponent<Floor>().floor, target.gameObject);
            cinemachine.GetComponent<CinemachineConfiner2D>().m_BoundingShape2D = boundary[GetComponent<Floor>().floor];
            cinemachine.GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize = lenseSize;
        }
        else
        {
            //AID.target = FindTarget(playerfloor, player);
        }
    }

    Transform GetClosestStairs(List<Transform> stairs, Transform fromThis)
    {
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
