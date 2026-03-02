/*
 * Movement.cs
 * Authors: Matthew Eagleman, Trevor Eagleman
 * Date Created: like, sometime in fall 2022
 * Purpose: Control the movement of the older brother
 */
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
// using Cinemachine;

public class Movement : MonoBehaviour
{
    GameObject GameData;
    Rigidbody2D rb;
    SortingGroup _sortingGroup;
    
    PlayerInput _playerInput;

    [SerializeField] float _maxSpeed = 10f;
    [SerializeField] float _maxSprintSpeed = 20f;
    [SerializeField] float _accelerate = 5f;
    [SerializeField] float _decelerate = 7f;

    Vector2 _moveInput;
    float _speed;
    float _velocityX;
    float _velocityY;
    float _acceleration = 5f;
    bool _sprinting = false;



    bool _isOnStairs = false;
    bool _stairsFaceRight;

    // float speed = 10;
    // public LayerMask up;
    // public LayerMask down;
    // public bool isTouchingStairs = false;
    // Vector3 pos;
    //interaction--------------
    // public float interactionRadius;
    // public LayerMask Interactable;
    // //stairs-------------------
    // public float stairsRadius = .2f;
    // public LayerMask stairsRight;
    // public LayerMask stairsLeft;
    // float stairsBonus;
    // public Transform feet;
    // public int floor;
    //camera--------------------
    // public Collider2D[] boundary;
    // public GameObject cinemachine;

    void Awake()
    {
        GameData = GameObject.FindGameObjectWithTag("GameData");
        rb = GetComponent<Rigidbody2D>();
        _sortingGroup = GetComponent<SortingGroup>();
        _playerInput = GetComponent<PlayerInput>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        /* The following code is old and bad. It made it so when the player goes onto the stairs they automatically start moving 
         * diagonally as to look like they are actaully going up/down the stairs.
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
        This can be removed when better code is added*/

        _moveInput = _playerInput.actions["Move"].ReadValue<Vector2>();

        // Simple movement
        // _speed = _sprinting ? _maxSprintSpeed : _maxSpeed;
        // rb.linearVelocity = new Vector2(_moveInput.x * _speed, _moveInput.y * _speed);

        // Movement with acceleration
        _speed = _sprinting ? _maxSprintSpeed : _maxSpeed;
        _acceleration = _moveInput.magnitude == 0 ? _decelerate : _accelerate;
        
        _velocityX += (_moveInput.x * _speed - _velocityX) * _acceleration * Time.fixedDeltaTime;
        _velocityY += (_moveInput.y * _speed - _velocityY) * _acceleration * Time.fixedDeltaTime;

        // Add Stair velocity separately so it isn't incorporated into the movement easing
        Vector2 stairVelocity = new Vector2(0f,0f);
        if (_isOnStairs)
        {
            stairVelocity.y = _stairsFaceRight ? _moveInput.x : -_moveInput.x;
            // multiply by speed and adjust for diagonal motion (sine of 45deg)
            stairVelocity.y *= _speed * 0.7071f; 
        }
        rb.linearVelocity = new Vector2(_velocityX, _velocityY) + stairVelocity;
    }
    void Update()
    {
       // cinemachine.GetComponent<CinemachineConfiner2D>().m_BoundingShape2D = boundary[GetComponent<Floor>().floor];
    }

    public void Sprint(InputAction.CallbackContext context)
    {
        _sprinting = (!context.canceled) ? true : false;
    }

    public void ChangeFloor(int floor)
    {
        _sortingGroup.sortingOrder = floor;
    }

    public void IsOnStairs(bool state, bool isRight)
    {
        _isOnStairs = state;
        _stairsFaceRight = isRight;
    }


    // Transform GetClosestInteractable(List<Transform> interactableObjects, Transform fromThis)
    // {
    //     //This is old lame muffin coding lol. I got this off the internet so its not even mine
    //     //All it does is get the position of the nearest object? Its so simple its kinda funny i had to look it up.
    //     //This was being used in my implementation of interactable objects
    //     Transform bestTarget = null;
    //     float closestDistanceSqr = Mathf.Infinity;
    //     Vector3 currentPosition = fromThis.position;
    //     foreach (Transform potentialTarget in interactableObjects)
    //     {
    //         Vector3 directionToTarget = potentialTarget.position - currentPosition;
    //         float dSqrToTarget = directionToTarget.sqrMagnitude;
    //         if (dSqrToTarget < closestDistanceSqr)
    //         {
    //             closestDistanceSqr = dSqrToTarget;
    //             bestTarget = potentialTarget;
    //         }
    //     }
    //     return bestTarget;
    // }

}
