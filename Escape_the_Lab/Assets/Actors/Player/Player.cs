using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System;
using System.Net;
using Unity.Mathematics;

[RequireComponent (typeof (Controller2D))]
[RequireComponent(typeof(HealthBar))]
public class Player : MonoBehaviour
{
    //                          Class variables
    //====================================================================

    public float jumpHeight = 4f;
    public float jumpAcceleration = .4f;
    public float movementSpeed = 6f;
    public int maxHealth = 100;
    public bool immuneToWater = false;
    public GameObject gunProjectile;
    public float projectileSpeed = 20;
    public int projectileDamage = 20;
    HealthBar healthBar;
    float jumpSpeed;
    float gravity;
    float velocityXSmoothing;
    Vector3 velocity;
    Vector2 direction;
    Controller2D controller;
    Animator animate;
    SpriteRenderer sprite;
    public int currentHealth;
    bool hit;
    float timer;//use to do plaer's animated Healthbar
    bool shoot;
    Vector2 bulletPosition;
    Projectile projectile;

    AudioSource SFX_playerSrc;
    AudioClip main_jumpSound, main_dieSound, main_walkSound, main_hitSound;

    //                          Helper methods
    //====================================================================

    private void playAnimation(String name)
    {
        foreach (AnimatorControllerParameter parameter in animate.parameters)
        {
            if (parameter.type == AnimatorControllerParameterType.Bool)
                animate.SetBool(parameter.name, false);
        }
        animate.SetBool(name, true);
    }
    private void updateAnimation()
    {
        if (direction.x < 0)
            sprite.flipX = true;
        else if (direction.x != 0)
            sprite.flipX = false;
        if(!hit && !shoot)
        {
            if (direction.x == 0 && controller.collisions.below)
                playAnimation("Player Idle");
            else if (controller.collisions.below)
                playAnimation("Player Moving");
            else if (velocity.y > 1 && controller.collisions.below == false)
                playAnimation("Player jump loop");
            else if (velocity.y < 1 && controller.collisions.below == false)
                playAnimation("Player falling");
        }
        else if (!hit && shoot)
        {
            if (direction.x == 0 && controller.collisions.below)
                playAnimation("Idle gun");
            else if (controller.collisions.below)
                playAnimation("Walking gun");
            else if (velocity.y > 1 && controller.collisions.below == false)
                playAnimation("Jump gun");
            else if (velocity.y < 1 && controller.collisions.below == false)
                playAnimation("Player falling");
        }
    }

    private Vector3 calculateVelocity(ref Vector3 velocity, Vector2 direction)
    {
        float targetVelocityX = direction.x * movementSpeed;
        Vector3 accel = new Vector3(0, gravity, 0);
        velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, .05f);
        Vector3 deltaMove = (velocity + accel * Time.deltaTime * .5f) * Time.deltaTime;
        velocity.y += gravity * Time.deltaTime;
        return deltaMove;
    }


    //I changed to public to make FallObject to call it.
    public void TakeDamage(int damage)
    {
        if (!hit)
        {
            //For health bar animation
            timer = 0.0f;
            healthBar.HBar_GetHitAnimated();

            hit = true;
            playAnimation("Player Hit");
            currentHealth -= damage;
            //healthBar.SetHealth(currentHealth);
            //SFX
            if (currentHealth > 0)
                SFX_playerSrc.PlayOneShot(main_hitSound);
            else if (currentHealth == 0)
                SFX_playerSrc.PlayOneShot(main_dieSound);
        }
    }
    private void poisonWater() {
        hit = false;
        TakeDamage(10); 
    }
    private void endHit() { hit = false; }
    private void enableShooting() { shoot = true; }
    private void disableShooting() { shoot = false; }

    private void OnTriggerEnter2D(Collider2D collision) 
    {
        if (collision.tag == "Water" && immuneToWater == false)
            InvokeRepeating("poisonWater", 0f, .5f);
        else if (collision.tag == "attack")
            TakeDamage(10);
    }

    private void OnTriggerExit2D(Collider2D collision) 
    {
        if (collision.tag == "Water")
            CancelInvoke();   
    } 

    private IEnumerator playerDeath()
    {
        Time.timeScale = 0;
        playAnimation("Player Death");
        yield return new WaitForSecondsRealtime(3f);
        Time.timeScale = 1;
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    private void stopAnimation() {
        animate.enabled = false;
    }

    //Get player velocity for FallingObjectSpawners
    public Vector3 GetPlayerVelocity()
    {
        return velocity;
    }

    private void fireGun()
    {
        SpriteRenderer projectileSprite = gunProjectile.GetComponent<SpriteRenderer>();
        Bounds bounds = GetComponent<BoxCollider2D>().bounds;
        if (sprite.flipX)
        {
            projectileSprite.flipX = true;
            bulletPosition = new Vector2(bounds.min.x - .6f, bounds.center.y);
            projectile.speed = -projectileSpeed;
        }
        else
        {
            bulletPosition = new Vector2(bounds.max.x + .5f, bounds.center.y);
            projectile.speed = projectileSpeed;
            projectileSprite.flipX = false;
        }

            Instantiate(gunProjectile, bulletPosition, quaternion.identity);
    }



    //                      Run time methods
    //=====================================================================
    void Start()
    {
        
        healthBar = FindObjectOfType<HealthBar>();
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        timer = 0.0f;

        controller = GetComponent<Controller2D>();
        animate = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        gravity = -(2 * jumpHeight) / Mathf.Pow(jumpAcceleration, 2);
        jumpSpeed = Mathf.Abs(gravity) * jumpAcceleration;

        main_jumpSound = Resources.Load<AudioClip>("Main_jump");
        main_dieSound = Resources.Load<AudioClip>("Main_Die");
        main_walkSound = Resources.Load<AudioClip>("Main_Walk");
        main_hitSound = Resources.Load<AudioClip>("Main_Hurt");

        SFX_playerSrc = GetComponent<AudioSource>();
        shoot = true;
        projectile = gunProjectile.GetComponent<Projectile>();
        projectile.speed = projectileSpeed;
        projectile.damage = projectileDamage;
    }
 
    void Update()
    {

       /* if (Input.GetKeyDown(KeyCode.A))
        {
            gameObject.GetComponent<water>().testNumber = gameObject.GetComponent<water>().testNumber + 1;
            Debug.Log(gameObject.GetComponent<water>().testNumber);
        } */

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            StartCoroutine("playerDeath");
            healthBar.SetHealth(currentHealth);
        }
        else
        {
            healthBar.SetHealth(currentHealth);
        }

        direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if (controller.collisions.above || controller.collisions.below)
            velocity.y = 0;
   
        if (Input.GetKeyDown(KeyCode.Space) && controller.collisions.below)
        {
            playAnimation("Player jump start");
            velocity.y = jumpSpeed;

            //SFX
            SFX_playerSrc.PlayOneShot(main_jumpSound);

            //handle jump out of moving platform
            //transform.parent = null;
        }
        if (Input.GetKeyUp(KeyCode.Space) && velocity.y > 0)
            velocity.y = 1f;

        if (Input.GetKeyDown(KeyCode.Mouse0) && shoot)
            fireGun();

        if (Time.timeScale != 0)
        {
            updateAnimation();
            Vector3 newVelocity = calculateVelocity(ref velocity, direction);
            controller.move(newVelocity);

            //SFX
            //SFX_playerSrc.PlayOneShot(main_walkSound);
        }

        timer += Time.deltaTime;
        float seconds = timer % 60;
        if (seconds > .2f)
        {
            healthBar.HBar_Normalized();
        }
        //Debug.Log(seconds);
    }

    

    


}
