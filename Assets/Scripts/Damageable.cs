using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Damageable : MonoBehaviour
{
    public UnityEvent<int, Vector2> damageableHit;

    Animator animator;
    [SerializeField]
    private int _maxHealth = 100;
    public int MaxHealth
    {
        get
        {
            return MaxHealth;
        } set
        {
            _maxHealth = value;
        }

            }

    [SerializeField]
    private int _health = 100;


    public int Health
    {
        get
        {
            return _health;
        } set
        {
            _health = value;

            // If health drops below 0, character is no longer alive
            if(_health <= 0)
            {
                IsAlive = false;
            }
        }
    }

 

    [SerializeField]
    private bool _isAlive = true;

    [SerializeField]
    private bool isInvincible = false;



    private float timeSinceHit = 0;

    public float invincibilityTime = 1f;

    public bool IsAlive
    {
        get
        {
            return _isAlive;
        } set
        {
            _isAlive = value;
            animator.SetBool("IsAlive", value);
            Debug.Log("IsAlive set " + value);
        }
    }

    public bool LockVelocity
    {
        get
        {
            return animator.GetBool("lockVelocity");
        }
        set
        {
            animator.SetBool("lockVelocity", value);
        }
    }

    public void Awake()
    {
        animator = GetComponent<Animator>();

    }

    private void Update()
    {
        if(isInvincible)
        {
            if(timeSinceHit > invincibilityTime)
            {
                // Remove invincibility
                isInvincible = false;
                timeSinceHit = 0;
            }

            timeSinceHit += Time.deltaTime;
        }
     
    }

    // Returns whether the damageable took damage or not
    public bool Hit(int damage, Vector2 knockBack)
    {
        if(IsAlive && !isInvincible)
        {
            Health -= damage;
            isInvincible = true;

            // Notify other subscribed components that the damageable was hit to handle the knockback, etc..
            animator.SetTrigger("hit");
            LockVelocity = true;
            damageableHit.Invoke(damage, knockBack);
            CharacterEvents.characterDamaged.Invoke(gameObject, damage);

            return true; 
        }

        // Unable to be hit 
        return false; 
    }

    public void Heal(int healthRestore)
    {
        // when damageable componet gets healed INVOKE event character healed 
        if(IsAlive && Health < 100)
        {
            // get the higher of the 2 values
            // if health is == or greater maxhealth, subtraction will return a negative value, default max heal to 0.
            //int maxHeal = Mathf.Max(MaxHealth - Health, 0);
            //int actualHeal = Mathf.Min(maxHeal, healthRestore);
            Health += healthRestore;

            // UI manager to create healing text 
            CharacterEvents.characterHealed(gameObject, healthRestore);

        }
    }
}
