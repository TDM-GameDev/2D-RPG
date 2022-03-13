using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] string enemyName;
    [SerializeField] float moveSpeed = 1.5f;
    //[SerializeField] float chaseRange;
    [SerializeField] float attackRange = 1;

    Health health;
    Attack attack;
    GameObject target;
    Vector3 homePosition;
    Rigidbody2D rb;

    [SerializeField] bool targetInChaseRange = false;


    void Start()
    {
        attack = GetComponent<Attack>();
        health = GetComponent<Health>();
        rb = GetComponent<Rigidbody2D>();

        homePosition = transform.position;
    }

    void Update() {
        
    }

    private void FixedUpdate() {
        //Debug.Log("Player is outside of attack range: " + IsOutsideOfAttackRange());
        if (target != null && targetInChaseRange) {
            if (TargetIsOutsideOfAttackRange()) {
                ChaseTarget();
            }
            else {
                //attack
            }
        }
        else if (transform.position != homePosition) {
            GoHome();
        }
    }

    private bool TargetIsOutsideOfAttackRange() {
        return Vector3.Distance(transform.position, target.transform.position) > attackRange;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            target = other.gameObject;
            //Debug.Log("Player entered aggro range.");
            targetInChaseRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            //target = null;
            targetInChaseRange = false;
        }
    }

    private void ChaseTarget() {
        //Debug.Log("Chasing player");
        Vector3 move = Vector3.MoveTowards(transform.position, target.transform.position, moveSpeed * Time.deltaTime);
        rb.MovePosition(move);
    }

    private void GoHome() {
        Debug.Log("Going home");
        Vector3 move = Vector3.MoveTowards(transform.position, homePosition, moveSpeed * Time.deltaTime);
        rb.MovePosition(move);
    }
}
