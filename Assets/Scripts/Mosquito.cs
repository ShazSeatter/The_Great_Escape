using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mosquito : MonoBehaviour
{   

    public DetectionZone stabDetectionZone;

    Animator animator;

    Rigidbody2D rb;

    // setting the inital value
    public bool _hasTarget = false;

    public bool HasTarget
    {
        get
        {
            return _hasTarget;
        }
        private set
        {
            _hasTarget = value;
            animator.SetBool("hasTarget", value);
        }
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // checking to see if there is any collideres detected if there is there is a target
        HasTarget = stabDetectionZone.detectedColliders.Count > 0;
    }
}
