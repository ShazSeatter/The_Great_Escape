using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public int healthRestore = 10;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // detects if a damageable character walks into its box collider zone and then it calls the heal function on the damageable component 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Damageable damageable = collision.GetComponent<Damageable>();

        if (damageable.Health < 100)
        {
            damageable.Heal(healthRestore);
            // after picking up cookie, it will be removed from game 
            Destroy(gameObject);
        }
    }
}
