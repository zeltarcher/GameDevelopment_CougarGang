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
    Transform player;
    Rigidbody2D myRigidBody;
    BoxCollider2D box;
    Bounds colliderBounds;
    SpriteRenderer sprite;
    Vector2 leftRayCast, rightRayCast;
    RaycastHit2D leftHit, rightHit;
    State state;

    private enum State { Walking, Chase, attack}

    void updateState()
    {
        switch(state)
        {
            case State.Walking:
                if (!leftHit || !rightHit)
                    moveSpeed *= -1;

                if (Mathf.Sign(moveSpeed) > 0)
                    sprite.flipX = false;
                else
                    sprite.flipX = true;

                myRigidBody.velocity = new Vector2(moveSpeed, 0f);
                break;

            case State.Chase:
                transform.position = Vector2.MoveTowards(transform.position, player.position, Mathf.Abs(moveSpeed) * Time.deltaTime);
                if(transform.position.x > player.position.x)
                    sprite.flipX = true;
                else
                    sprite.flipX = false;

                if (Vector3.Distance(transform.position, player.position) <= attackRange)
                    state = State.attack;
                break;

            case State.attack:

                break;
        }
    }

    void updateRaycast()
    {
        colliderBounds = box.bounds;
        leftRayCast = new Vector2(colliderBounds.min.x - .1f, colliderBounds.min.y);
        rightRayCast = new Vector2(colliderBounds.max.x + .1f, colliderBounds.min.y);
        leftHit = Physics2D.Raycast(leftRayCast, Vector2.down, .5f, detectCollisionWith);
        rightHit = Physics2D.Raycast(rightRayCast, Vector2.down, .5f, detectCollisionWith);
    }

    void Start()
    {
        box = GetComponent<BoxCollider2D>();
        myRigidBody = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        player = FindObjectOfType<Player>().transform;
        state = State.Walking;
    }


    void FixedUpdate()
    {
        updateRaycast();

        if (Vector3.Distance(transform.position, player.position) <= followRange)
            state = State.Chase;
        else if (Vector3.Distance(transform.position, player.position) <= attackRange)
            state = State.attack;
        else
            state = State.Walking;


        updateState();
    }
}


