/*
 * Movement.cs
 * Purpose: Control the movement of the older brother
 * Date Created: like, sometime in fall 2022
 * Authors: Matthew Eagleman, Trevor Eagleman
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

    //Normal Movement
    Vector2 _moveInput;
    float _speed;
    float _velocityX;
    float _velocityY;
    float _acceleration = 5f;
    bool _sprinting = false;



    //stairs-------------------
    bool _isOnStairs = false;
    bool _stairsFaceRight;

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
        float stairVelocityY = 0f;
        if (_isOnStairs)
        {
            stairVelocityY = _stairsFaceRight ? _velocityX : -_velocityX;
        }
        rb.linearVelocity = new Vector2(_velocityX, _velocityY + stairVelocityY);
    }
    void Update()
    {

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
        // set variables to track if the character is on the stairs or not
        _isOnStairs = state;
        _stairsFaceRight = isRight;
    }
}
