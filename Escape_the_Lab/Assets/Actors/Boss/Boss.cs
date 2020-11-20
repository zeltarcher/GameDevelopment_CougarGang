using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Timeline;

public class Boss : MonoBehaviour
{
    public float moveSpeed = 1f;
    public LayerMask detectCollisionWith;
    public float followRange = 5f;
    public float attackRange = 1f;
    public float gravitySpeed = 400f;
    public float health = 100;
    public Sprite stunnedSprite;
    public float stunnedDuration = 1f;
    public GameObject gunProjectile;
    public float projectileSpeed = 20;
    public int projectileDamage = 20;
    public int NumOfProjectiles = 5;
    Transform player, man, robot;
    Rigidbody2D myRigidBody;
    BoxCollider2D box;
    PolygonCollider2D polygon;
    Bounds colliderBounds;
    SpriteRenderer sprite;
    Vector2 leftRayCast, rightRayCast, topRaycast;
    RaycastHit2D leftHit, rightHit, stunCheck, leftWall, rightWall;
    Animator animate;
    charChange character;
    State state;
    bool ground;
    float YdistanceToPlayer, distanceToPlayer;
    float gravity;
    Vector2 bulletPosition;
    Projectile projectile;

    private enum State
    {
        Walking,
        Chase,
        MeleeAttack,
        RangedAttack,
        Hit,
        Death,
        Stunned
    }

    private IEnumerator stunned()
    {
        enabled = false;
        yield return new WaitForSecondsRealtime(stunnedDuration);
        enabled = true;
        animate.enabled = true;
        state = State.Walking;
    }

    private IEnumerator shootGun()
    {
        yield return new WaitForSecondsRealtime(1);
        state = State.RangedAttack;
        yield return new WaitForSecondsRealtime(1);
        state = State.Walking;
    }

    private void playAnimation(String name)
    {
        foreach (AnimatorControllerParameter parameter in animate.parameters)
        {
            if (parameter.type == AnimatorControllerParameterType.Bool)
            {
                animate.SetBool(parameter.name, false);
            }
        }
        animate.SetBool(name, true);
    }

    void updateState()
    {
        switch (state)
        {
            case State.Walking:
                disableAttack();
                playAnimation("boss idle");
                myRigidBody.velocity = new Vector2(0, gravity);
                break;

            case State.Chase:
                disableAttack();
                playAnimation("boss walking");

                if (Math.Abs(transform.position.y - player.position.y) < .6f)
                {
                    StartCoroutine("shootGun");
                }

                if (transform.position.x > player.position.x)
                {
                    sprite.flipX = true;
                    moveSpeed = -Mathf.Abs(moveSpeed);
                }
                else if (transform.position.x < player.position.x)
                {
                    moveSpeed = Mathf.Abs(moveSpeed);
                    sprite.flipX = false;
                }
                myRigidBody.velocity = new Vector2(moveSpeed * 1.5f, gravity);
                break;

            case State.MeleeAttack:
                playAnimation("boss idle melee attack");
                myRigidBody.velocity = new Vector2(0, gravity);
                break;

            case State.Hit:
                myRigidBody.velocity = new Vector2(0, gravity);
                break;

            case State.Death:
                playAnimation("boss dying");
                health = 0;
                foreach (Collider c in GetComponents<Collider>())
                    c.enabled = false;
                myRigidBody.velocity = new Vector2(0, gravity);
                enabled = false;
                break;

            case State.Stunned:
                myRigidBody.velocity = new Vector2(0, gravity);
                animate.enabled = false;
                sprite.sprite = stunnedSprite;
                StartCoroutine("stunned");
                break;

            case State.RangedAttack:
                playAnimation("boss idle shooting");
                myRigidBody.velocity = new Vector2(0, gravity);
                break;
        }
    }

    void updateRaycast()
    {
        colliderBounds = box.bounds;
        leftRayCast = new Vector2(colliderBounds.min.x, colliderBounds.min.y);
        rightRayCast = new Vector2(colliderBounds.max.x, colliderBounds.min.y);
        topRaycast = new Vector2(colliderBounds.center.x, colliderBounds.max.y);
        leftHit = Physics2D.Raycast(leftRayCast, Vector2.down, .1f, detectCollisionWith);
        rightHit = Physics2D.Raycast(rightRayCast, Vector2.down, .1f, detectCollisionWith);
        stunCheck = Physics2D.Raycast(topRaycast, Vector2.up, .05f, detectCollisionWith);
        leftWall = Physics2D.Raycast(new Vector2(colliderBounds.min.x, colliderBounds.center.y), Vector2.left, .05f, detectCollisionWith);
        rightWall = Physics2D.Raycast(new Vector2(colliderBounds.max.x, colliderBounds.center.y), Vector2.right, .1f, detectCollisionWith);
    }

    private void fireGun()
    {
        SpriteRenderer projectileSprite = gunProjectile.GetComponent<SpriteRenderer>();
        Bounds bounds = GetComponent<BoxCollider2D>().bounds;
        if (sprite.flipX)
        {
            projectileSprite.flipX = true;
            bulletPosition = new Vector2(bounds.min.x - 1f, bounds.center.y);
            projectile.speed = -projectileSpeed;
        }
        else
        {
            bulletPosition = new Vector2(bounds.max.x + 1f, bounds.center.y);
            projectile.speed = projectileSpeed;
            projectileSprite.flipX = false;
        }
        Instantiate(gunProjectile, bulletPosition, quaternion.identity);
    }

    void terminate() { Destroy(gameObject); }
    void enableAttack() { polygon.enabled = true; }
    void disableAttack() { polygon.enabled = false; }
    void endHit() { state = State.Walking; }
    public void TakeDamage(int damage)
    {
        animate.SetTrigger("boss hit");
        state = State.Hit;
        health -= damage;
    }
    void poisonWater() { TakeDamage(10); }

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
        animate = GetComponent<Animator>();
        polygon = GetComponentInChildren<PolygonCollider2D>();
        character = FindObjectOfType<charChange>();
        man = GameObject.Find("Man").GetComponent<Player>().transform;
        robot = GameObject.Find("Robot").GetComponent<Player>().transform;
        projectile = gunProjectile.GetComponent<Projectile>();

        polygon.enabled = false;
        state = State.Walking;
        gravity = -gravitySpeed / 100;
        projectile.speed = projectileSpeed;
        projectile.damage = projectileDamage;
    }


    void FixedUpdate()
    {
        updateRaycast();

        if (character.p1 == true)
            player = man;
        else if (character.p2 == true)
            player = robot;

        distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (state != State.Hit && state != State.RangedAttack && health > 0)
        {
            if (distanceToPlayer <= followRange && 
                distanceToPlayer >= attackRange && 
                Math.Abs(transform.position.x - player.position.x) > .5f)
                state = State.Chase;
            else if (distanceToPlayer <= attackRange)
                state = State.MeleeAttack;
            else
                state = State.Walking;
        }
        else if (health <= 0)
        {
            state = State.Death;
        }
        if (stunCheck)
            state = State.Stunned;

        updateState();
    }
}