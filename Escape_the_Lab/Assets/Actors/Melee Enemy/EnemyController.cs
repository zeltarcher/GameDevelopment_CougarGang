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
    public Sprite stunnedSprite;
    public float stunnedDuration = 1f;
    Transform player;
    Rigidbody2D myRigidBody;
    BoxCollider2D box;
    PolygonCollider2D polygon;
    CapsuleCollider2D capsule;
    Bounds colliderBounds, capsuleBounds;
    SpriteRenderer sprite;
    Vector2 leftRayCast, rightRayCast, topRaycast;
    RaycastHit2D leftHit, rightHit, stunCheck, leftWall, rightWall;
    Animator animate;
    State state;
    bool ground;
    float distanceToPlayer;
    float gravity;

    //healthbar
    Transform healthBar;
    float hb_max;

    //sound
    AudioSource SFX_enemySrc;
    AudioClip enemy_dieSound, enemy_hitSound, enemy_slashing;

    private enum State 
    { 
        Walking, 
        Chase, 
        Attack,
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
        switch(state)
        {
            case State.Walking:
                playAnimation("enemy walking");
                if (leftHit != rightHit && ground == true)
                {
                    moveSpeed *= -1;
                    ground = false;
                }

                if(leftWall || rightWall)
                    moveSpeed *= -1;

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

                if(!SFX_enemySrc.isPlaying)
                    SFX_enemySrc.PlayOneShot(enemy_slashing);

                break;
            case State.Hit:
                myRigidBody.velocity = new Vector2(0, gravity);
                break;
            case State.Death:
                CoUpdate();

                playAnimation("enemy death");
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
        }
    }

    IEnumerable CoUpdate()
    {
        Debug.Log("Waiting");
        if (!SFX_enemySrc.isPlaying)
            SFX_enemySrc.PlayOneShot(enemy_dieSound);
        yield return new WaitForSeconds(2);

        yield return null;
    }

    void updateRaycast()
    {
        colliderBounds = box.bounds;
        capsuleBounds = capsule.bounds;
        leftRayCast = new Vector2(colliderBounds.min.x - .1f, colliderBounds.min.y);
        rightRayCast = new Vector2(colliderBounds.max.x + .1f, colliderBounds.min.y);
        topRaycast = new Vector2(capsuleBounds.center.x, capsuleBounds.max.y);
        leftHit = Physics2D.Raycast(leftRayCast, Vector2.down, .1f, detectCollisionWith);
        rightHit = Physics2D.Raycast(rightRayCast, Vector2.down, .1f, detectCollisionWith);
        stunCheck = Physics2D.Raycast(topRaycast, Vector2.up, .05f, detectCollisionWith);
        leftWall = Physics2D.Raycast(new Vector2(colliderBounds.min.x, colliderBounds.center.y), Vector2.left, .05f, detectCollisionWith);
        rightWall = Physics2D.Raycast(new Vector2(colliderBounds.max.x, colliderBounds.center.y), Vector2.right, .1f, detectCollisionWith);
    }

    void terminate() { Destroy(gameObject); }
    void enableAttack() { polygon.enabled = true; }
    void disableAttack() { polygon.enabled = false; }
    void endHit() { state = State.Walking;}
    public void TakeDamage(int damage) 
    {
        animate.SetTrigger("enemy hit");
        state = State.Hit;
        health -= damage;

        if (health >= 0)
        {
            healthBar.localScale = new Vector3(health / hb_max, healthBar.localScale.y, healthBar.localScale.z);
        }


        if (!SFX_enemySrc.isPlaying)
            SFX_enemySrc.PlayOneShot(enemy_hitSound);

    }
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
        capsule = GetComponent<CapsuleCollider2D>();
        myRigidBody = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        /*
        if(FindObjectOfType<charChange>().p1 == true)
            player = GameObject.Find("Man").GetComponent<Player>().transform;
        else if(FindObjectOfType<charChange>().p2 == true)
            player = GameObject.Find("Robot").GetComponent<Player>().transform;
        */
        animate = GetComponent<Animator>();
        polygon = GetComponentInChildren<PolygonCollider2D>();
        polygon.enabled = false;
        state = State.Walking;
        Debug.Log(LayerMask.LayerToName(11));

        healthBar = gameObject.transform.Find("HealthBar");
        hb_max = health;

        SFX_enemySrc = GetComponent<AudioSource>();
        enemy_dieSound = Resources.Load<AudioClip>("Enemy_Death");
        enemy_slashing = Resources.Load<AudioClip>("Enemy_Sword_Attack");
        enemy_hitSound = Resources.Load<AudioClip>("Enemy_Hurt");
    }


    void FixedUpdate()
    {
        updateRaycast();

        if (FindObjectOfType<charChange>().p1 == true)
            player = GameObject.Find("Man").GetComponent<Player>().transform;
        else if (FindObjectOfType<charChange>().p2 == true)
            player = GameObject.Find("Robot").GetComponent<Player>().transform;

        distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if (state != State.Hit && health > 0)
        {
            if (distanceToPlayer <= followRange && distanceToPlayer >= attackRange)
                state = State.Chase;
            else if (distanceToPlayer <= attackRange)
                state = State.Attack;
            else
                state = State.Walking;
        }
        else if(health <= 0)
        {
            CoUpdate();
            state = State.Death;
        }

        if (stunCheck)
            state = State.Stunned;

        updateState();
    }
}


