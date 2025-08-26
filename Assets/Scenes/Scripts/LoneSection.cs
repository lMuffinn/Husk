using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Pathfinding;

public class LoneSection : MonoBehaviour
{
    public Transform crowbar;
    public Transform stop;
    public float radius;
    public LayerMask hideSpots;
    public Transform target;


    public void SetTargetCrowbar()
    {
        target = crowbar;
    }

    public void SetTargetStop()
    {
        target = stop;
    }

    public void SetTargetHide()
    {
        Collider2D[] hidingSpots = Physics2D.OverlapCircleAll(transform.position, radius, hideSpots);
        List<Transform> transformsList = new List<Transform>(hidingSpots.Length);
        for(int i = 0; i < hidingSpots.Length; i++)
        {
        transformsList.Add(hidingSpots[i].gameObject.GetComponent<Transform>());
        }
        target = GetClosestHidingSpot(transformsList, transform);
    }

    Transform GetClosestHidingSpot(List<Transform> HidingSpots, Transform fromThis)
    {
        Transform bestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = fromThis.position;
        foreach (Transform potentialTarget in HidingSpots)
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
}
