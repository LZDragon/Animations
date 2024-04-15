using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof (CharacterController))]
[RequireComponent(typeof(PlayerInput))]
public class Player : MonoBehaviour
{
    private PlayerActions playerActions;
    [SerializeField] private Rigidbody playerRigidbody;
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private CharacterController characterController;

    private Vector3 moveDirection;
    private bool isSprinting;
    private bool isWalking;
    private float speed =0f;
    private static readonly int PlayerSpeed = Animator.StringToHash("playerSpeed");
    private bool preformPushup;
    private static readonly int Pushup = Animator.StringToHash("pushup");
    private bool preformJump;
    private float gravity = -0.98f;

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
        Movement();
        playerAnimator.SetBool(Pushup,preformPushup);
        playerAnimator.SetBool("jump", preformJump);

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
    }

    public void OnPushup(InputAction.CallbackContext context)
    {
        preformPushup = context.performed;
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        preformJump = context.performed;
    }

    public void Jump() //Called in Jump Animation (import settings -> event)
    {
        
        characterController.Move(new Vector3(0,3));
        gravity = -0.1f;
    }

    private void Movement()
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
            //transform.Translate(moveDirection * (speed * Time.deltaTime));
            characterController.Move(moveDirection * (speed * Time.deltaTime));
        }
        else
        {
            speed = 0;
            playerAnimator.SetFloat(PlayerSpeed, speed);
        }

        if (characterController.isGrounded)
        {
            gravity = -0.98f;
        }
        characterController.Move(new Vector3(0,gravity));
        playerAnimator.SetBool("isGrounded", characterController.isGrounded);
    }
}
