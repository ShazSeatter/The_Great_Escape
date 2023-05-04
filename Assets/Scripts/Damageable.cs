using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Damageable : MonoBehaviour
{
    public UnityEvent<int, Vector2> damageableHit;
    public UnityEvent<int, int> healthChanged;

    Animator animator;

    HealthBar healthBar;

    public GameManager gameManager;

    [SerializeField]
    public int _maxHealth = 100;
    public int MaxHealth
    {
        get
        {
            return _maxHealth;
        } set
        {
            _maxHealth = value;
  
        }

            }

    [SerializeField]
    public int _health = 100;


    public int Health
    {
        get
        {
            return _health;

        } set
        {
            //Debug.Log(value);
            _health = value;
            healthChanged?.Invoke(_health, MaxHealth);

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

            TakeDamage(damage);
            isInvincible = true;
            
            // Notify other subscribed components that the damageable was hit to handle the knockback, etc..
            animator.SetTrigger("hit");
            LockVelocity = true;
            damageableHit.Invoke(damage, knockBack);
            CharacterEvents.characterDamaged.Invoke(gameObject, damage);
         
            return true;
        }
        
        return false;
        

        // Unable to be hit 
        
    }

    public void Heal(int healthRestore)
    {
        // when damageable componet gets healed INVOKE event character healed 
        if(IsAlive)
        {

            Health += healthRestore;

            // UI manager to create healing text 
            CharacterEvents.characterHealed.Invoke(gameObject, healthRestore);

        }
        Debug.Log(healthRestore);
    }

    public void TakeDamage(int damage)
    {
        Health -= damage;

        if (!IsAlive)
        {
            gameManager.gameOver();
        }
    }

}
