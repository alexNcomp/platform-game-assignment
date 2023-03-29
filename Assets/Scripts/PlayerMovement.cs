using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    public float walkSpeed = 5;
    public float runSpeed  = 8;

    private Vector3 moveZDirection;
    private Vector3 moveXDirection;
    private Vector3 velocity;

    public bool isGrounded;
    public float groundCheckDistance = 0.2f;
    public LayerMask groundMask;
    public float gravity = -9.81f;

    public float jumpHeight = 2f;
    private int jumps = 0;

    private CharacterController controller;
    private Animator animator; 

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
    }

    private void Update() 
    {
        Move();
    }

    private void Move()
    {
        isGrounded = Physics.CheckSphere(transform.position, groundCheckDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
            animator.SetBool("IsGrounded", true);
            jumps = 0;
        }

        float moveZ = Input.GetAxis("Vertical");
        float moveX = Input.GetAxis("Horizontal");

        moveZDirection = new Vector3(0, 0, moveZ);
        moveZDirection = transform.TransformDirection(moveZDirection);

        moveXDirection = new Vector3(moveX, 0, 0);
        moveXDirection = transform.TransformDirection(moveXDirection);

        if (moveZDirection != Vector3.zero && !Input.GetKey(KeyCode.LeftShift))         { Walk(); } 
        else if (moveXDirection != Vector3.zero && !Input.GetKey(KeyCode.LeftShift))    { Strafe(); } 
        else if (moveZDirection != Vector3.zero && Input.GetKey(KeyCode.LeftShift))     { Run(); }
        else if (moveZDirection == Vector3.zero && moveXDirection == Vector3.zero)      { Idle(); }

        moveZDirection *= moveSpeed;
        moveXDirection *= moveSpeed;

        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {   
            Jump();
            jumps = 1;
        }
        else if (GameManager.instance.powerUp && Input.GetKeyDown(KeyCode.Space) && jumps < 2)
        {
            Jump();
            jumps = 2;
        } 
        
        controller.Move(moveZDirection * Time.deltaTime);
        controller.Move(moveXDirection * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    private void Idle() 
    {
        animator.SetFloat("Speed", 0, 0.15f, Time.deltaTime);
    }

    private void Walk() 
    {
        moveSpeed = walkSpeed;
        animator.SetFloat("Speed", 0.5f, 0.08f, Time.deltaTime);
    }

    private void Strafe() 
    {
        moveSpeed = walkSpeed;
        animator.SetFloat("Speed", 0.5f, 0.08f, Time.deltaTime);
    }

    private void Run()
    {
        moveSpeed = runSpeed;
        animator.SetFloat("Speed", 1, 0.1f, Time.deltaTime);
    }

    private void Jump()
    {
        if (jumps == 0)
        {
            Debug.Log(jumps);
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
            animator.SetBool("IsGrounded", false);
        }
        else if (jumps == 1)
        {
            Debug.Log(jumps);
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
            animator.SetTrigger("Jump");
        }
    }
}
