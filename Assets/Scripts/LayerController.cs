/*
 * LayerController.cs
 * Author: Matthew Eagleman
 * Purpose: Place every object with this script at the correct z velue, so that the player can walk around and it doesn't look weird
 * Date 9/3/2025
 */
using UnityEngine;

[ExecuteAlways]

public class LayerController : MonoBehaviour
{
    [SerializeField] float yMax = 100;
    [SerializeField] float yMin = -150;
    [SerializeField] Transform yIndicator;
    [SerializeField] bool dynamic = false;
    float range;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        range = yMax - yMin;
        if (yIndicator == null)
        {
            GameObject yIndicatorGameObject = new GameObject();
            yIndicatorGameObject.transform.SetParent(this.transform);
            yIndicator = yIndicatorGameObject.transform;
            SpriteRenderer rndr = GetComponent<SpriteRenderer>();
            if (rndr != null)
            {
                yIndicator.transform.position = new Vector3(rndr.bounds.center.x, rndr.bounds.center.y - rndr.bounds.size.y / 2, 0);
            }
            else yIndicator.transform.localPosition = new Vector3(0,0, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (dynamic || !Application.isPlaying)
        {
            this.transform.position = new Vector3(this.transform.position.x,this.transform.position.y,(yIndicator.position.y + yMin) / range);
        }
    }
}
