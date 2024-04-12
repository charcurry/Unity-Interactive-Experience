using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController2D : MonoBehaviour
{

    public float moveSpeed = 5.0f;

    Vector2 movement;

    Animator animator;

    Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate()
    {
        HandleMovement();
        HandleAnimations();
    }

    void HandleMovement()
    {
        rb.MovePosition(rb.position + moveSpeed * Time.fixedDeltaTime * movement.normalized);
    }

    void HandleAnimations()
    {
        if (movement != Vector2.zero)
        {
            animator.SetFloat("MovementX", movement.x);
            animator.SetFloat("MovementY", movement.y);
            animator.SetBool("moving", true);
        }
        else
        {
            animator.SetBool("moving", false);
        }
    }
}
