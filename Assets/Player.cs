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
    [SerializeField] private Animator playerAnimator;

    private Vector3 moveDirection;
    private bool isSprinting;
    private bool isWalking;
    private float speed =0f;
    private static readonly int PlayerSpeed = Animator.StringToHash("playerSpeed");

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



    private void FixedUpdate()
    {
        if (isWalking)
        {
            if (isSprinting)
            {
                speed = 10f;
                playerAnimator.SetFloat(PlayerSpeed, speed);
            }
            else
            {
                speed = 5f;
                playerAnimator.SetFloat(PlayerSpeed, speed);
            }
            transform.Translate(moveDirection * (speed * Time.deltaTime));
        }
        else
        {
            speed = 0;
            playerAnimator.SetFloat(PlayerSpeed, speed);
        }

    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveDirection = context.ReadValue<Vector2>();
        moveDirection = new Vector3(moveDirection.x, 0, moveDirection.y);
        isWalking = context.performed;

    }

    public void OnLook(InputAction.CallbackContext context)
    {
        
    }

    public void OnFire(InputAction.CallbackContext context)
    {
        throw new NotImplementedException();
    }

    public void OnSprint(InputAction.CallbackContext context)
    {
        isSprinting = context.performed;
        Debug.Log("Shift key pressed");
    }
}
