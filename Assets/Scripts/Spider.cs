using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirections), typeof(Damageable))]
public class Spider : MonoBehaviour
{
    public float walkAcceleration = 3f;
    public float maxSpeed = 3f;
    public float walkStopRate = 0.05f;
    public DetectionZone attackZone;
    public DetectionZone cliffDetectionZone;

    

    Damageable damageable;

    Rigidbody2D rb;

    TouchingDirections touchingDirections;
    Animator animator;



    // MOVEMENT 
    public enum WalkableDirection {  Right, Left};

    private WalkableDirection _walkDirection;
    
    private Vector2 walkDirectionVector = Vector2.right;
  

    public WalkableDirection WalkDirection
    {
        get { return _walkDirection; }
        set
        {
            if (_walkDirection != value)
            {
                //Direction flipped - allows the object to be flipped to walk the other way
                gameObject.transform.localScale = new Vector2(gameObject.transform.localScale.x * -1, gameObject.transform.localScale.y);
                

                if (value == WalkableDirection.Right)
                {
                    walkDirectionVector = Vector2.right;

                }
                else if (value == WalkableDirection.Left)
                {
                    walkDirectionVector = Vector2.left;
                }

            }

            _walkDirection = value;
        }
    }

    // setting the inital value
    public bool _hasTarget = false;

    public bool HasTarget { get
        {
            return _hasTarget;
        } private set
        {
            _hasTarget = value;
            animator.SetBool("hasTarget", value);
        } }

    public bool CanMove
    {
        get
        {
            return animator.GetBool("canMove");
        }
    }

    public float AttackCooldown { get
        {
            return animator.GetFloat("attackCooldown");
        } private set
        {
            animator.SetFloat("attackCooldown", Mathf.Max(value, 0));
        }
            }

    private void Awake()
    {
        // these are to make reference to the components 
        rb = GetComponent<Rigidbody2D>();
        // using script from before to incorporate methods for wall check, this then needs to be added to Prefab as well
        touchingDirections = GetComponent<TouchingDirections>();
        animator = GetComponent<Animator>();
        damageable = GetComponent<Damageable>();
    }

    // Update is called once per frame
    void Update()
    {
        HasTarget = attackZone.detectedColliders.Count > 0;
        // using time.deltatime ensures the movement is the same across all computers regardles of the fps being applied
        if(AttackCooldown > 0)
        {
            AttackCooldown -= Time.deltaTime;
        }
        
    }

    private void Start()
    {
        WalkDirection = WalkableDirection.Right;

    }

    // fixed update is for physics functions
    private void FixedUpdate()
    {
        // this conditional statement checks to make sure enemy is on the grond (so it doesnt change direction mid air and touching wall

        if(touchingDirections.IsGrounded && touchingDirections.IsOnWall)
        {
            FlipDirection();
        }

        if (!damageable.LockVelocity)
        {
            if (CanMove)

            {
                // accelerate towards max speed 
                rb.velocity = new Vector2(Mathf.Clamp(
                      rb.velocity.x + (walkAcceleration * walkDirectionVector.x * Time.fixedDeltaTime), -maxSpeed, maxSpeed), rb.velocity.y);
                    }
            
            else
            { rb.velocity = new Vector2(Mathf.Lerp(rb.velocity.x, 0, walkStopRate), rb.velocity.y); }
        }
   
    }

    private void FlipDirection()
    {
        if(WalkDirection == WalkableDirection.Right)
        {
            WalkDirection = WalkableDirection.Left;
        } else if(WalkDirection == WalkableDirection.Left)
        {
            WalkDirection = WalkableDirection.Right;
        } else
        {
            Debug.LogError("Current walkable direction is not set to legal values of right or left");
        }
    }

    public void OnHit(int damage, Vector2 knockback)
    {
        rb.velocity = new Vector2(knockback.x, rb.velocity.y + knockback.y);
    }

    // event to be triggered
    public void OnCliffDetected()
    {
        if (touchingDirections.IsGrounded)
        {
            FlipDirection();
        }
    }

}
