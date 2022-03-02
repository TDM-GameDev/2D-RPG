using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] FloatVariable playerMoveSpeed;

    [SerializeField] RPG.Events.VoidEvent onInteract;


    [Header("Input Settings")]
    public PlayerInput playerInput;
    private Vector3 rawInputMovement;

    Rigidbody2D rb;
    Animator animator;
    Vector3 change;

    //Current Control Scheme
    private string currentControlScheme;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        currentControlScheme = playerInput.currentControlScheme;
        Debug.Log(currentControlScheme);
    }

    void Update()
    {
        //ProcessInput();
        UpdateAnimationAndMove();
    }

    //private void ProcessInput()
    //{
    //    var horizontalInput = Input.GetAxisRaw("Horizontal");
    //    var verticalInput = Input.GetAxisRaw("Vertical");

    //    change = Vector3.zero;
    //    change.x = horizontalInput;
    //    change.y = verticalInput;
    //}

    private void UpdateAnimationAndMove()
    {
        if (rawInputMovement != Vector3.zero)
        {
            animator.SetFloat("moveX", rawInputMovement.x);
            animator.SetFloat("moveY", rawInputMovement.y);
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
        rb.MovePosition(transform.position + rawInputMovement * playerMoveSpeed.Value * Time.fixedDeltaTime);
    }

    public void OnMovement(InputAction.CallbackContext value) {
        Vector2 inputMovement = value.ReadValue<Vector2>();
        rawInputMovement = new Vector3(inputMovement.x, inputMovement.y, 0);
    }

    public void OnInteract(InputAction.CallbackContext value) {
        if (value.started) {
            onInteract.Raise();
        }
    }






    public void OnControlsChanged() {
        if (playerInput.currentControlScheme != currentControlScheme) {
            currentControlScheme = playerInput.currentControlScheme;
            //Debug.Log(currentControlScheme);
            //var inputDevice = playerInput.GetDevice<Gamepad>();
            //var deviceType = inputDevice.description;
            //Debug.Log($"description: {inputDevice.name}");
            //Debug.Log(playerInput.devices);
        }
    }
}
