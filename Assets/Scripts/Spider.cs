using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirections))]
public class Spider : MonoBehaviour
{
    public float walkSpeed = 3f;
    public float walkStopRate = 0.05f;
    public DetectionZone attackZone;

    Rigidbody2D rb;

    TouchingDirections touchingDirections;
    Animator animator;

    //// SHOOTING 
    //[SerializeField]
    //private GameObject webPrefab;

    //[SerializeField]
    //private Transform spiderHead;


    // MOVEMENT 
    public enum WalkableDirection {  Right, Left};

    private WalkableDirection _walkDirection;
    private Vector2 walkDirectionVector = Vector2.right;

    public WalkableDirection WalkDirection
    {
        get { return _walkDirection; }
        set {
            if(_walkDirection != value)
            {
                //Direction flipped
                gameObject.transform.localScale = new Vector2(gameObject.transform.localScale.x * -1, gameObject.transform.localScale.y);

                if(value == WalkableDirection.Right)
                {
                    walkDirectionVector = Vector2.right;

                } else if (value == WalkableDirection.Left)
                {
                    walkDirectionVector = Vector2.left;
                }
            }
            _walkDirection = value; }
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

    private void Awake()
    {
        // these are to make reference to the components 
        rb = GetComponent<Rigidbody2D>();
        // using script from before to incorporate methods for wall check, this then needs to be added to Prefab as well
        touchingDirections = GetComponent<TouchingDirections>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        HasTarget = attackZone.detectedColliders.Count > 0;

    }

    // fixed update is for physics functions
    private void FixedUpdate()
    {
        // this conditional statement checks to make sure enemy is on the grond (so it doesnt change direction mid air and touching wall

        if(touchingDirections.IsGrounded && touchingDirections.IsOnWall)
        {
            FlipDirection();
        }
        if (CanMove)
        { rb.velocity = new Vector2(walkSpeed * walkDirectionVector.x, rb.velocity.y); }
            
        else
        { rb.velocity = new Vector2(Mathf.Lerp(rb.velocity.x, 0, walkStopRate), rb.velocity.y); }
           
        
        
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

    //public void StopAttack()
    //{
    //    animator.SetBool("attack", false);
    //}

    //public void Shoot()
    //{
    //    // this instantiates the web into the game
    //    GameObject go = Instantiate(webPrefab, spiderHead.position, Quaternion.identity);
    //    Vector3 direction = new Vector3(transform.localScale.x, 0);

    //    go.GetComponent<Projectile>().Setup(direction);

    //}

}
