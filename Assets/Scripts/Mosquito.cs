using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mosquito : MonoBehaviour
{
    public float flightSpeed = 2f;
    public float wayPointReachedDistance = 0.1f;
    public DetectionZone stabDetectionZone;
    public List<Transform> wayPoints;

    Animator animator;

    Rigidbody2D rb;

    Damageable damageable;

    Transform nextWayPoint;

    int wayPointNum = 0;


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

    public bool CanMove
    {
        get
        {
            return animator.GetBool("canMove");
        }
    }


    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        damageable = GetComponent<Damageable>();
    }


    private void Start()
    {
        nextWayPoint = wayPoints[wayPointNum];
    }
    // Update is called once per frame
    void Update()
    {
        // checking to see if there is any collideres detected if there is there is a target
        HasTarget = stabDetectionZone.detectedColliders.Count > 0;
    }

    private void FixedUpdate()
    {
        if(damageable.IsAlive)
        {
            if(CanMove)
            {
                Flight();
            } else
            {
                rb.velocity = Vector3.zero;
            }
        }
    }

    private void Flight()
    {
        // Fly to next waypoint
        Vector2 directionToWayPoint = (nextWayPoint.position - transform.position).normalized;

        // Check if we have reached waypoint already
        float distance = Vector2.Distance(nextWayPoint.position, transform.position);
        rb.velocity = directionToWayPoint * flightSpeed;

        // See if we need to switch waypoints
        if(distance <= wayPointReachedDistance)
        {
            wayPointNum++;

            if(wayPointNum >= wayPoints.Count)
            {
                // loop back to original waypoint 
                wayPointNum = 0;
            }

            nextWayPoint = wayPoints[wayPointNum];
        }
        
    }
}
