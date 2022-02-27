using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float speed = 25;
    
    Rigidbody2D rb;
    Animator animator;
    Vector3 change;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInput();
        UpdateAnimationAndMove();
    }

    private void ProcessInput()
    {
        var horizontalInput = Input.GetAxisRaw("Horizontal");
        var verticalInput = Input.GetAxisRaw("Vertical");

        change = Vector3.zero;
        change.x = horizontalInput;
        change.y = verticalInput;
    }

    private void UpdateAnimationAndMove()
    {
        if (change != Vector3.zero)
        {
            animator.SetFloat("moveX", change.x);
            animator.SetFloat("moveY", change.y);
            animator.SetBool("moving", true);
            MovePlayer();
        }
        else
        {
            animator.SetBool("moving", false);
        }
    }

    void MovePlayer()
    {
        rb.MovePosition(transform.position + change * speed * Time.fixedDeltaTime);
    }
}
