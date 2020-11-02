using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Timeline;

public class EnemyController : MonoBehaviour
{
    public float moveSpeed = 1f;
    public LayerMask detectCollisionWith;
    public float followRange = 5f;
    public float attackRange = 1f;
    public float gravitySpeed = 400f;
    public float health = 100;
    Transform player;
    Rigidbody2D myRigidBody;
    BoxCollider2D box;
    PolygonCollider2D polygon;
    Bounds colliderBounds;
    SpriteRenderer sprite;
    Vector2 leftRayCast, rightRayCast;
    RaycastHit2D leftHit, rightHit;
    Animator animate;
    State state;
    bool ground;
    float distanceToPlayer;
    float gravity;
    private enum State 
    { 
        Walking, 
        Chase, 
        Attack,
        Hit,
        Death
    }

    private void playAnimation(String name)
    {
        foreach (AnimatorControllerParameter parameter in animate.parameters)
        {
            if (parameter.type == AnimatorControllerParameterType.Bool)
                animate.SetBool(parameter.name, false);
        }
        animate.SetBool(name, true);
    }

    void updateState()
    {
        switch(state)
        {
            case State.Walking:
                playAnimation("enemy walking");
                if (leftHit != rightHit && ground == true)
                {
                    moveSpeed *= -1;
                    ground = false;
                }

                if (Mathf.Sign(moveSpeed) > 0)
                    sprite.flipX = false;
                else
                    sprite.flipX = true;
           
                myRigidBody.velocity = new Vector2(moveSpeed, gravity);
                if (leftHit == rightHit)
                    ground = true;

                break;

            case State.Chase:
                playAnimation("enemy walking");
                if (transform.position.x > player.position.x)
                {
                    sprite.flipX = true;
                    moveSpeed = -Mathf.Abs(moveSpeed);
                }
                else
                {
                    moveSpeed = Mathf.Abs(moveSpeed);
                    sprite.flipX = false;
                }
                myRigidBody.velocity = new Vector2(moveSpeed * 1.5f, gravity);
                break;

            case State.Attack:
                playAnimation("enemy attack");
                myRigidBody.velocity = new Vector2(0, gravity);
                break;
            case State.Hit:
                playAnimation("enemy hit");
                myRigidBody.velocity = new Vector2(0, gravity);
                break;
            case State.Death:
                playAnimation("enemy death");
                health = 0;
                foreach (Collider c in GetComponents<Collider>())
                    c.enabled = false;
                myRigidBody.velocity = new Vector2(0, gravity);
                enabled = false;
                break;
        }
    }

    void updateRaycast()
    {
        colliderBounds = box.bounds;
        leftRayCast = new Vector2(colliderBounds.min.x - .1f, colliderBounds.min.y);
        rightRayCast = new Vector2(colliderBounds.max.x + .1f, colliderBounds.min.y);
        leftHit = Physics2D.Raycast(leftRayCast, Vector2.down, .1f, detectCollisionWith);
        rightHit = Physics2D.Raycast(rightRayCast, Vector2.down, .1f, detectCollisionWith);
    }

    void terminate() { Destroy(gameObject); }
    void enableAttack() { polygon.enabled = true; }
    void disableAttack() { polygon.enabled = false; }
    public void TakeDamage(int damage) { health -= damage; }
    void poisonWater(){ TakeDamage(10); }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Platform" && leftHit && rightHit)
            gravity = 0;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.tag == "Platform")
            gravity += -gravitySpeed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Water")
        {
            state = State.Hit;
            InvokeRepeating("poisonWater", 0f, .5f);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Water")
        {
            state = State.Walking;
            CancelInvoke();
        }
    } 

    void Start()
    {
        box = GetComponent<BoxCollider2D>();
        myRigidBody = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        player = FindObjectOfType<Player>().transform;
        animate = GetComponent<Animator>();
        polygon = GetComponentInChildren<PolygonCollider2D>();
        polygon.enabled = false;
        state = State.Walking;
    }


    void FixedUpdate()
    {
        updateRaycast();
        distanceToPlayer = Vector3.Distance(transform.position, player.position);

            if (distanceToPlayer <= followRange && distanceToPlayer >= attackRange && state != State.Hit)
                state = State.Chase;
            else if (distanceToPlayer <= attackRange && state != State.Hit)
                state = State.Attack;
            else if(health <= 0)
                state = State.Death;
            else if(state != State.Hit)
                state = State.Walking;

        updateState();
    }
}


