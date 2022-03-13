using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField] int damage = 1;
    [SerializeField] float thrust = 25f;
    [SerializeField] float knockTime = .3f;
    [SerializeField] Transform attackPosition;

    bool knockedBack = false;

    public void OnAttack() {
        Collider2D[] attackHit = Physics2D.OverlapCircleAll(attackPosition.position, .15f);

        for (int i = 0; i < attackHit.Length; i++) {
            if (attackHit[i].TryGetComponent(out Health health)) {
                health.TakeDamage(damage);
                if (attackHit[i].gameObject.CompareTag("Enemy") && attackHit[i].gameObject.TryGetComponent(out Rigidbody2D rb)) {
                    Vector2 knockBack = (rb.transform.position - transform.position).normalized * thrust;
                    //rb.AddForce(diff, ForceMode2D.Impulse);
                    if (!knockedBack) {
                        Debug.Log("Adding force");
                        rb.AddForce(knockBack, ForceMode2D.Impulse);
                        knockedBack = true;
                    }
                    
                    StartCoroutine(nameof(KnockCo), rb);
                }
            }
        }
    }

    private IEnumerator KnockCo(Rigidbody2D rb) {
        yield return new WaitForSeconds(knockTime);
        rb.velocity = Vector2.zero;
        knockedBack = false;
        //rb.isKinematic = true;
    }

    //private void OnTriggerEnter2D(Collider2D other) {
    //    //if (other.gameObject.CompareTag(""))
    //    if (other.TryGetComponent(out Health target)) {
    //        target.TakeDamage(damage);
    //    }
    //}
}
