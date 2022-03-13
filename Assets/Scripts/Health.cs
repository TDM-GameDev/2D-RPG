using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum HealthType
{
    Enemy,
    Item
}

public class Health : MonoBehaviour
{
    [SerializeField] float destroyWaitTime = .3f;
    [SerializeField] int hitPoints;
    //[SerializeField] bool isItem = false;
    [SerializeField] HealthType healthType;
    
    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void TakeDamage(int damage) {
        hitPoints -= damage;
        if (hitPoints <= 0) {
            switch (healthType) {
                case HealthType.Item:
                    StartCoroutine(nameof(BreakItem));
                    break;
                case HealthType.Enemy:
                    StartCoroutine(nameof(KillEnemy));
                    break;
            }
        }
    }

    private IEnumerator KillEnemy() {
        if (anim != null) {
            anim.SetTrigger("death");
            yield return new WaitForSeconds(destroyWaitTime);
            anim.enabled = false;
            Destroy(gameObject);
        }
    }

    private IEnumerator BreakItem() {
        if (anim != null) {
            anim.SetTrigger("break");
            yield return new WaitForSeconds(destroyWaitTime);
            Destroy(gameObject);
        }
    }
}
