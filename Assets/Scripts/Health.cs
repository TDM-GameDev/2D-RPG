using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] int hitPoints;
    [SerializeField] bool isItem = false;
    
    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void TakeDamage(int damage) {
        hitPoints -= damage;
        if (hitPoints <= 0) {
            if (isItem) {
                StartCoroutine(nameof(BreakItem));
            }
        }
    }

    private IEnumerator BreakItem() {
        if (anim != null) {
            anim.SetTrigger("break");
            yield return new WaitForSeconds(.3f);
            Destroy(gameObject);
        }
    }
}
