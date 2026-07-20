using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightStopperDep : MonoBehaviour
{
    Transform tTransform;
    Transform parentTransform;
    public float checkRadius = 0.1f;
    public LayerMask Obstacle;
    Vector3 pos;
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
        bool touchingWall = Physics2D.OverlapCircle(tTransform.position, checkRadius, Obstacle);
        if (!touchingWall)
        {
            pos = parentTransform.position;
        }
        else
        {
            tTransform.position = new Vector3(parentTransform.position.x,pos.y,parentTransform.position.z);
        }
        float distance = Distance(tTransform.position, parentTransform.position);
        if (distance > maxDistance) tTransform.position = parentTransform.position;
        //Debug.Log(Vector3.Distance(tTransform.position, parentTransform.position));
    }

    float Distance(Vector3 light, Vector3 player)
    {
        float distance;
        distance = ((light.x - player.x) * (light.x - player.x) + (light.y - player.y) * (light.y - player.y));
        return distance;
    }
}
