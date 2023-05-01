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

    public float invincibilityTime = 0.25f;

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
            damageableHit.Invoke(damage, knockBack);

            return true; 
        }

        // Unable to be hit 
        return false; 
    }
}
