using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float walkingSpeed;
    [SerializeField]
    private float runningSpeed;
    [SerializeField]
    private float jumpSpeed;
    [SerializeField]
    private float longJumpDuration;
    [SerializeField]
    private float gravity;
    [SerializeField]
    private Camera playerCamera;
    [SerializeField]
    private float lookSpeed;
    [SerializeField]
    private float lookXLimit;
    [SerializeField]
    private AudioSource footstepSound;

    private CharacterController characterController;
    private Vector3 moveDirection = Vector3.zero;
    private float jumpStartTime = 0;
    private float rotationX = 0;
    private bool canMove = true;

    void Start()
    {
        characterController = GetComponent<CharacterController>();

        // Lock cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        MovementHandler();
        RotationHandler();
        AudioHandler();
        // ClearMovementDirection();
    }

    // (Optionally) TODO: refactor with new unity input system 
    private void MovementHandler()
    {

        // We are grounded, so recalculate move direction based on axes
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        // Getting palyer's input
        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        bool isJumping = Input.GetButton("Jump");
        float curSpeedX = canMove ? (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Vertical") : 0;
        float curSpeedY = canMove ? (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Horizontal") : 0;

        // Managing the jump
        if (characterController.isGrounded && isJumping)
            jumpStartTime = Time.time;

        float currentYDirection = moveDirection.y;

        moveDirection = (forward * curSpeedX) + (right * curSpeedY);
        if (isJumping && canMove &&
            (characterController.isGrounded || Time.time - jumpStartTime <= longJumpDuration))
        {
            moveDirection.y = jumpSpeed;
        }
        else
        {
            moveDirection.y = currentYDirection;
        }

        // Apply gravity. Gravity is multiplied by deltaTime twice (once here, and once below
        // when the moveDirection is multiplied by deltaTime). This is because gravity should be applied
        // as an acceleration (ms^-2)
        if (!characterController.isGrounded)
            moveDirection.y -= gravity * Time.deltaTime;

        // Move the controller
        characterController.Move(moveDirection * Time.deltaTime);
    }

    // (Optionally) TODO: refactor with new unity input system 
    private void RotationHandler()
    {
        // Player and Camera rotation
        if (!canMove) return;

        rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
        rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
        playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
        transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
    }

    public void Reposition(Vector3 newLocation)
    {
        characterController.enabled = false;
        moveDirection = Vector3.zero;
        transform.position = newLocation;
        characterController.enabled = true;
    }

    public void Release() {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }

    public void Freeze() {
        canMove = false;
    }

    public void Unfreeze() {
        canMove = true;
    }
    
    public void AudioHandler()
    {
        bool isJumping = Input.GetButton("Jump");
        if ((Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0) && !isJumping)
        {
            footstepSound.mute = false;
        }
        else
        {
            footstepSound.mute = true;
        }
    }
}