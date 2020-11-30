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
    public float rateOfFire = 1f;
    public float timeBetweenShooting = 2f;
    public float shootingDuration = 1f;
    public GameObject grenadeObject;
    public float throwDistance = 300f;
    public float explosionDelay = 2f;
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
    bool isShooting, isThrowing;
    float YdistanceToPlayer, distanceToPlayer;
    float gravity;
    Vector2 bulletPosition;
    Projectile projectile;
    Grenade grenade;

    private enum State
    {
        Walking,
        Chase,
        MeleeAttack,
        RangedAttack,
        Throwing,
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
        isShooting = true;
        yield return new WaitForSecondsRealtime(timeBetweenShooting);
        animate.speed = rateOfFire;
        state = State.RangedAttack;
        yield return new WaitForSecondsRealtime(shootingDuration);
        state = State.Chase;
        animate.speed = 1;
        isShooting = false;
    }
    private IEnumerator throwObject()
    {
        isThrowing = true;
        state = State.Throwing;
        animate.SetTrigger("boss idle throwing");
        yield return new WaitForSecondsRealtime(1.3f);
        state = State.Chase;
        isThrowing = false;
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

                if (Math.Abs(transform.position.y - player.position.y) < .6f && !isShooting && !isThrowing)
                {
                    StartCoroutine("shootGun");
                }

                if (Math.Abs(transform.position.y - player.position.y) > 1f && !isShooting && !isThrowing)
                    StartCoroutine("throwObject");

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

                animate.speed = 1.3f;
                myRigidBody.velocity = new Vector2(moveSpeed * 1.5f, gravity);
                break;

            case State.MeleeAttack:
                animate.speed = 1;
                playAnimation("boss idle melee attack");
                myRigidBody.velocity = new Vector2(0, gravity);
                break;

            case State.Throwing:
                myRigidBody.velocity = new Vector2(0, gravity);
                StartCoroutine("throwObject");
                break;

            case State.Death:
                playAnimation("boss dying");
                myRigidBody.velocity = new Vector2(0, gravity);
                health = 0;
                foreach (Collider c in GetComponents<Collider>())
                    c.enabled = false;
                enabled = false;
                break;

            case State.Stunned:
                myRigidBody.velocity = new Vector2(0, gravity);
                animate.enabled = false;
                sprite.sprite = stunnedSprite;
                StartCoroutine("stunned");
                break;

            case State.RangedAttack:
                animate.speed = 1;
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

    private void throwGrenade()
    {
        Bounds bounds = GetComponent<BoxCollider2D>().bounds;
        if (sprite.flipX)
        {
            bulletPosition = new Vector2(bounds.min.x, bounds.center.y + .1f);
            grenade.throwDistance = -throwDistance;
        }
        else
        {
            bulletPosition = new Vector2(bounds.max.x, bounds.center.y + .1f);
            grenade.throwDistance = throwDistance;
        }
        Instantiate(grenadeObject, bulletPosition, quaternion.identity);
    }

    void terminate() { animate.enabled = false; Destroy(gameObject); }
    void enableAttack() { polygon.enabled = true; }
    void disableAttack() { polygon.enabled = false; }
    void endHit() { state = State.Walking; }
    public void TakeDamage(int damage)
    {
        //animate.SetTrigger("boss hit");
        //state = State.Hit;
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
            //state = State.Hit;
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
        grenade = grenadeObject.GetComponent<Grenade>();

        isShooting = false;
        polygon.enabled = false;
        state = State.Walking;
        gravity = -gravitySpeed / 100;
        projectile.speed = projectileSpeed;
        projectile.damage = projectileDamage;
        grenade.throwDistance = throwDistance;
        grenade.explosionDelay = explosionDelay;

    }


    void FixedUpdate()
    {
        updateRaycast();

        if (character.p1 == true)
            player = man;
        else if (character.p2 == true)
            player = robot;

        distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (state != State.RangedAttack && health > 0)
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

        //Debug.Log(state);
        updateState();
    }
}