using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// the : means inherited from something in this case MonoBehaviour (like extends in java)

[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirections), typeof(Damageable))]
public class PlayerController : MonoBehaviour
{
    public float walkSpeed = 5f;
    public float runSpeed = 8f;
    public float jumpImpulse = 6f;
    public float airWalkSpeed = 3f;


    Vector2 moveInput;
    TouchingDirections touchingDirections;
    Damageable damageable;
    HealthBar healthBar;

    GameManager gameManager;

    //DialogueManager dialogueManager;

    public GameObject gameOverUI;

    //public GameObject dialogueUI;

    public float CurrentMoveSpeed
    {
        get
        {
            if (CanMove)
            {
                if (IsMoving && !touchingDirections.IsOnWall)
                {
                    if (touchingDirections.IsGrounded)
                    {
                        if (IsRunning)
                        {
                            return runSpeed;
                        }
                        else
                        {
                            return walkSpeed;
                        }
                    }
                    else
                    {
                        // Air move
                        return airWalkSpeed;
                    }
                }
                else
                {
                    // idle speed is 0
                    return 0;
                }
            }
            else
            {
                // Movement locked
                return 0;
            }
        }
    }

    [SerializeField]
    private bool _isMoving = false;

    // getters and setters
    public bool IsMoving { get {
            return _isMoving;
        } private set {
            _isMoving = value;
            animator.SetBool("IsMoving", value);
        } }

    [SerializeField]
    private bool _isRunning = false;

    // this is a property
    public bool IsRunning
    {
        get
        {
            return _isRunning;
        }
        set
        {
            _isRunning = value;
            animator.SetBool("IsRunning", value);
        }
    }

    public bool _isFacingRight = true;
    public bool IsFacingRight
    {
        get
        {
            return _isFacingRight;
        }
        private set
        {
            if (_isFacingRight != value)
            {
                // Flip the local scale to make the player face the opposite direction
                transform.localScale *= new Vector2(-1, 1);
            }
            _isFacingRight = value;
        }
    }

    public bool CanMove { get
        {
            return animator.GetBool("canMove");
        }
    }

    public bool IsAlive
    {
        get
        {
            return animator.GetBool("IsAlive");
        }
    }

    Rigidbody2D rb;

    Animator animator;

    BoxCollider2D dialogueToggle;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        touchingDirections = GetComponent<TouchingDirections>();
        damageable = GetComponent<Damageable>();
        healthBar = GetComponent<HealthBar>();
        gameManager = GetComponent<GameManager>();
        GameObject dialogueTrigger = GameObject.FindGameObjectWithTag("Dialogue");
        dialogueToggle = dialogueTrigger.GetComponent<BoxCollider2D>();
    }


    private void FixedUpdate()
    {
        if (!damageable.LockVelocity)
            // creating a new vector object and assigning it to rb.velocity. takes 2 inputs, which is x and y) 
            rb.velocity = new Vector2(moveInput.x * CurrentMoveSpeed, rb.velocity.y);

        animator.SetFloat("yVelocity", rb.velocity.y);

    }

    // function that makes the character move set to true or false

    public void OnMove(InputAction.CallbackContext context)
    {
        // vector x y movement 
        moveInput = context.ReadValue<Vector2>();
        if (IsAlive)
        {
            IsMoving = moveInput != Vector2.zero;

            // facing the direction the character is moving in
            SetFacingDirection(moveInput);
        } else
        {
            IsMoving = false;
        }

    }

    private void SetFacingDirection(Vector2 moveInput)
    {
        if (moveInput.x > 0 && !IsFacingRight)
        {
            // Face the right
            IsFacingRight = true;
        } else if (moveInput.x < 0 && IsFacingRight) {
            // Face the left
            IsFacingRight = false;
        }
    }

    public void OnRun(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            IsRunning = true;
        } else if (context.canceled)
        {
            IsRunning = false;
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
       
        if (context.started && touchingDirections.IsGrounded && CanMove)
        {
            animator.SetTrigger("jump");
            rb.velocity = new Vector2(rb.velocity.x, jumpImpulse);
        }

    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            animator.SetTrigger("attack");
        }
    }

    public void OnHit(int damage, Vector2 knockback)
    {
        rb.velocity = new Vector2(knockback.x, rb.velocity.y + knockback.y);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Trap"))
        {
            damageable.IsAlive = false;
            damageable.Health = 0;
            gameOverUI.SetActive(true);

        }
    }

    public void IsDead()
    {
            //gameManager.gameOver();
            gameOverUI.SetActive(true);

    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Dialogue"))
        {
            Debug.Log("Player has triggered");
            collision.gameObject.GetComponent<DialogueManager>().StartDialogue();
            dialogueToggle.enabled = false;
        }

        
    } 
}

