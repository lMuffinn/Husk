using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Cinemachine;

public class Movement : MonoBehaviour
{

    float speed = 10;
    GameObject GameData;
    public LayerMask up;
    public LayerMask down;
    public bool isTouchingStairs = false;
    Vector3 pos;
    Rigidbody2D rb;
    //interaction--------------
    public float interactionRadius;
    public LayerMask Interactable;
    //stairs-------------------
    public float stairsRadius = .2f;
    public LayerMask stairsRight;
    public LayerMask stairsLeft;
    float stairsBonus;
    public Transform feet;
    public int floor;
    //camera--------------------
    public Collider2D[] boundary;
    public GameObject cinemachine;

    // Start is called before the first frame update
    void Start()
    {
        GameData = GameObject.FindGameObjectWithTag("GameData");
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //is the player on stairs
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        bool left = Physics2D.OverlapCircle(feet.position, stairsRadius, stairsLeft);
        bool right = Physics2D.OverlapCircle(feet.position, stairsRadius, stairsRight);
        if (left) stairsBonus = horizontal * -1;
        else if (right) stairsBonus = horizontal;
        else stairsBonus = 0;
        //controls movement + whatever direction the stairs are in
        rb.linearVelocity = new Vector2(horizontal * speed, vertical * speed + speed * stairsBonus);
    }
    void Update()
    {
       // cinemachine.GetComponent<CinemachineConfiner2D>().m_BoundingShape2D = boundary[GetComponent<Floor>().floor];
    }

    Transform GetClosestInteractable(List<Transform> interactableObjects, Transform fromThis)
    {
        Transform bestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = fromThis.position;
        foreach (Transform potentialTarget in interactableObjects)
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
