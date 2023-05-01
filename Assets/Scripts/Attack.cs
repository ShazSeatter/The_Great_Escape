using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    Collider2D attackCollider;

    public int attackDamage = 10;

    private void Awake()
    {
        attackCollider = GetComponent<Collider2D>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        // see if it can be hit
        Damageable damageable = collision.GetComponent<Damageable>();

        if(damageable != null)
        {
            // Hit the target
            bool gotHit = damageable.Hit(attackDamage);

            if(gotHit)
            {
                Debug.Log(collision.name + "hit for " + attackDamage);
            }
            
        }
    }
}
