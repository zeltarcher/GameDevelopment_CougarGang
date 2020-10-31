using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System;

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
    HealthBar healthBar;
    float jumpSpeed;
    float gravity;
    float velocityXSmoothing;
    Vector3 velocity;
    Vector2 direction;
    Controller2D controller;
    Animator animate;
    SpriteRenderer sprite;
    int currentHealth;
    bool hit;
    float timer;//use to do plaer's animated Healthbar

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
        if(!hit)
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
        TakeDamage(10); }
    private void endHit() { hit = false; }

    private void OnTriggerEnter2D(Collider2D collision) 
    {
        if (collision.tag == "Water" && immuneToWater == false)
            InvokeRepeating("poisonWater", 0f, .5f);
        else if (collision.tag == "attack")
            poisonWater();
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

    //For Player to sttay on moving platform
    /*
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collied something******************");
        if (collision.gameObject.tag == "MovingPlatform")
        {
            Debug.Log("On the Moving platform===============================");
            //collision.collider.transform.SetParent(transform);
            transform.SetParent(collision.gameObject.transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "MovingPlatform")
        {

        }
    }
    */

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
    }
 
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.A))
        {
            gameObject.GetComponent<water>().testNumber = gameObject.GetComponent<water>().testNumber + 1;
            Debug.Log(gameObject.GetComponent<water>().testNumber);
        }

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
        Debug.Log(seconds);
    }

    

    


}
