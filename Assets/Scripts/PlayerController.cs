using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// the : means inherited from something in this case MonoBehaviour (like extends in java)

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    public float walkSpeed = 5f;
    public float runSpeed = 8f;
    Vector2 moveInput;


    public float CurrentMoveSpeed { get
        {
            if(IsMoving)
            {
                if(IsRunning)
                {
                    return runSpeed;
                } else
                {
                    return walkSpeed;
                }
            } else
            {
                // idle speed is 0
                return 0;
            }
        } }

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

    Rigidbody2D rb;

    Animator animator;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        // creating a new vector object and assigning it to rb.velocity. takes 2 inputs, which is x and y) 
        rb.velocity = new Vector2(moveInput.x * CurrentMoveSpeed, rb.velocity.y);
    }

    // function that makes the character move set to true or false

    public void OnMove(InputAction.CallbackContext context)
    {
        // vector x y movement 
        moveInput = context.ReadValue<Vector2>();

        IsMoving = moveInput != Vector2.zero;
    }

    public void OnRun(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            IsRunning = true;
        } else if(context.canceled)
        {
            IsRunning = false;
        }
    }
}
