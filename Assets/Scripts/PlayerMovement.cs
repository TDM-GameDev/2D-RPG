using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using UnityEditor.Animations;

public class PlayerMovement : MonoBehaviour
{
    private const string attackString = "attack";
    private const string movingString = "moving";
    private const string AttackingStateName = "Attacking";
    private const string WalkingStateName = "Walking";
    private const string IdleStateName = "Idle";

    [SerializeField] GameObject swordCollider;

    [SerializeField] FloatVariable playerMoveSpeed;

    [SerializeField] RPG.Events.VoidEvent onInteract;

    [SerializeField] AnimatorController controller;

    [Header("Input Settings")]
    public PlayerInput playerInput;
    private Vector3 rawInputMovement;

    Rigidbody2D rb;
    Animator animator;
    Attack attack;
    
    Vector3 change;

    //private enum Directions { Left, Right, Up, Down };
    //private Directions facing = Directions.Down;

    //Current Control Scheme
    private string currentControlScheme;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        currentControlScheme = playerInput.currentControlScheme;
        attack = GetComponent<Attack>();
        //Debug.Log(currentControlScheme);
    }

    void Update()
    {
        RotateSword();
        UpdateAnimationAndMove();
    }

    private void UpdateAnimationAndMove()
    {
        //animator.GetCurrentAnimatorStateInfo(0).IsName(AttackingStateName);
        if (rawInputMovement != Vector3.zero && !animator.GetCurrentAnimatorStateInfo(0).IsName(AttackingStateName))
        {
            animator.SetFloat("moveX", rawInputMovement.x);
            animator.SetFloat("moveY", rawInputMovement.y);
            animator.SetBool(movingString, true);
            MovePlayer();
        }
        else
        {
            animator.SetBool(movingString, false);
        }
    }

    void MovePlayer()
    {
        rb.MovePosition(transform.position + rawInputMovement * playerMoveSpeed.Value * Time.fixedDeltaTime);
    }

    public void OnMovement(InputAction.CallbackContext value) {
        Vector2 inputMovement = value.ReadValue<Vector2>();
        rawInputMovement = new Vector3(inputMovement.x, inputMovement.y, 0).normalized;
    }

    public void OnAttack(InputAction.CallbackContext value) {
        if (value.started) {
            animator.SetTrigger(attackString);
            attack.OnAttack();
        }
    }

    public void OnInteract(InputAction.CallbackContext value) {
        if (value.started) {
            onInteract.Raise();
        }
    }


    private void RotateSword() {
        if (swordCollider == null) return;

        //if (Input.GetKeyDown(KeyCode.Alpha1)) {
        //    Debug.Log("UpArrow");
        //    swordCollider.transform.rotation = new Quaternion(0, 0, 180, 0);
        //}
        //if (Input.GetKeyDown(KeyCode.Alpha2)) {
        //    Debug.Log("LeftArrow");
        //    swordCollider.transform.rotation = new Quaternion(0, 0, -90, 0);
        //}
        //if (Input.GetKeyDown(KeyCode.Alpha3)) {
        //    Debug.Log("RightArrow");
        //    swordCollider.transform.rotation = new Quaternion(0, 0, 90, 0);
        //}
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
