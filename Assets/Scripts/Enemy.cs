using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] string enemyName;
    [SerializeField] float moveSpeed = 1.5f;
    [SerializeField] float chaseRange;
    [SerializeField] float attackRange = 1;

    Health health;
    Attack attack;
    GameObject target;
    Transform homePosition;

    [SerializeField] bool targetInRange = false;


    void Start()
    {
        attack = GetComponent<Attack>();
        health = GetComponent<Health>();

        homePosition = transform;
    }

    void Update() {
        if (targetInRange && IsOutsideOfAttackRange()) {
            ChaseTarget();
        }
        else {
            GoHome();
        }
    }

    private bool IsOutsideOfAttackRange() {
        return Vector3.Distance(transform.position, target.transform.position) > attackRange;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            target = other.gameObject;
            //Debug.Log("Player entered aggro range.");
            targetInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            target = null;
            targetInRange = false;
        }
    }

    private void ChaseTarget() {
        //Debug.Log("Chasing player");
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, moveSpeed * Time.deltaTime);
    }

    private void GoHome() {
        Debug.Log("Going home");
        transform.position = Vector3.MoveTowards(transform.position, homePosition.transform.position, moveSpeed * Time.deltaTime);
    }
}
