using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(PlayerInput))]
public class Player : MonoBehaviour
{
    private PlayerActions playerActions;
    [SerializeField] private Rigidbody playerRigidbody;

    private Vector2 moveInput;
    private float speed;

    private void Awake()
    {
        playerActions = new PlayerActions();
    }

    private void OnEnable()
    {
        playerActions.PlayerMap.Enable();
    }

    private void OnDisable()
    {
        playerActions.PlayerMap.Disable();
    }

    private void OnMove()
    {
        moveInput = playerActions.PlayerMap.Move.ReadValue<Vector2>();
        speed = 5f;
    }

    private void FixedUpdate()
    {
        Vector3 moveDirection = new Vector3(moveInput.x, 0, moveInput.y);
        transform.position += moveDirection * (speed * Time.deltaTime);
    }
}
