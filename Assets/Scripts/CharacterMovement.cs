using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CharacterMovement : MonoBehaviour
{

    public Vector3 gravity;
    public Vector3 velocity;
    Vector3 move;
    
    private CharacterController controller;
    private Animator animator;

    public bool grounded;
    private float jumpHeight = 1f;
    private float gravityValue = -9.81f;
    private float walkSpeed = 5;
    private float runSpeed = 8; 

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateAnimator();
        ProcessMovement();
    }

    private void UpdateAnimator()
    {
        if (move.magnitude < 0.05f)
        {
            animator.SetFloat("Velocity", 0.0f);
        }
        else if (move.magnitude > 0.05f)
        {
            if (Input.GetButton("Fire3"))   // Left shift
            {
                animator.SetFloat("Velocity", 1.0f);
            }
            else
            {
                animator.SetFloat("Velocity", 0.5f);
            }
        }
    }

    void ProcessMovement()
    { 
        // Moving the character forward according to the speed
        float speed = GetMovementSpeed();

        move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        // Turning the character
        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
        }
        // Making sure we don't have a Y velocity if we are grounded
        // controller.isGrounded tells you if a character is grounded ( IE Touches the ground)
        grounded = controller.isGrounded;
        if (grounded)
        {
            if (Input.GetButtonDown("Jump") )
            {
                gravity.y += (float) Math.Sqrt(jumpHeight * -3.0f * gravityValue);
            }
            else 
            {
                // Don't apply gravity if grounded and not jumping
                gravity.y = -1.0f;
            }
        }
        else 
        {
            // Since there is no physics applied on character controller we have this to reapply gravity
            gravity.y += gravityValue * Time.deltaTime;
        }  
        velocity = gravity * Time.deltaTime + move * Time.deltaTime * speed;
        controller.Move(velocity);
    }

    float GetMovementSpeed()
    {
        if (Input.GetButton("Fire3"))   // Left shift
        {
            return runSpeed;
        }
        else
        {
            return walkSpeed;
        }
    }
}
