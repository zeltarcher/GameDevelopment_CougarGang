﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System;
using System.Net;
using Unity.Mathematics;
using Cinemachine;

[RequireComponent (typeof (Controller2D))]
[RequireComponent(typeof(HealthBar))]
public class Player : MonoBehaviour
{
    //                          Class variables
    //====================================================================
    public Collider2D oneone;
    public Collider2D twotwo; 
    public Camera camera;
    public CinemachineVirtualCamera CVC;
    public CinemachineBrain CMB;

    public float jumpHeight = 4f;
    public float jumpAcceleration = .4f;
    public float movementSpeed = 6f;
    public int maxHealth = 100;
    public bool immuneToWater = false;
    public GameObject gunProjectile;
    public float projectileSpeed = 20;
    public int projectileDamage = 20;
    public float rateOfFire = 2f;
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
    public bool hit, shoot, hasGun;
    float timer;
    Vector2 bulletPosition;
    Projectile projectile;
    Inventory inventory;

    AudioSource SFX_playerSrc;
    AudioClip main_jumpSound, main_dieSound, main_walkSound, main_hitSound, main_shoot_laser;

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
        if(FindObjectOfType<charChange>().p1 == true)
        {
            if (!hit && !hasGun)
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
            else if (!hit && hasGun)
            {
                if (direction.x == 0 && controller.collisions.below)
                    playAnimation("Idle gun");
                else if (controller.collisions.below)
                    playAnimation("Walking gun");
                else if (velocity.y > 1 && controller.collisions.below == false)
                    playAnimation("Jump gun");
                //else if (velocity.y < 1 && controller.collisions.below == false)
                //playAnimation("Player falling");
            }
        }

        else if(FindObjectOfType<charChange>().p2 == true)
        {
            if (direction.x == 0 && controller.collisions.below)
                animate.SetBool("Running", false);
            else if (controller.collisions.below)
                animate.SetBool("Running", true);
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

    public void TakeDamage(int damage)
    {
        if (!hit)
        {
            timer = 0.0f;
            healthBar.HBar_GetHitAnimated();

            hit = true;
            playAnimation("Player Hit");
            currentHealth -= damage;

            if (currentHealth > 0)
            {
                if (!SFX_playerSrc.isPlaying)
                    SFX_playerSrc.PlayOneShot(main_hitSound);
            }
            else if (currentHealth <= 0)
                SFX_playerSrc.PlayOneShot(main_dieSound);
        }
    }
    private void poisonWater() {
        hit = false;
        TakeDamage(10); 
    }
    public void endHit() { hit = false; }
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
        enabled = false;
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

    public Vector3 GetPlayerVelocity()
    {
        return velocity;
    }

    private IEnumerator fireGun()
    {
        shoot = false;
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
        yield return new WaitForSecondsRealtime(1f / rateOfFire);
        shoot = true;
    }

    private void shopEnter()
    {
        if(FindObjectOfType<charChange>().p1 == true)
        {
            oneone = GameObject.Find("Man").GetComponent<Collider2D>();
        }
        else if(FindObjectOfType<charChange>().p2 == true)
        {
            oneone = GameObject.Find("Robot").GetComponent<Collider2D>();
        }

        twotwo = GameObject.Find("Shop Corridor").GetComponent<Collider2D>();
        
        if (oneone.IsTouching(twotwo))
        {
            CVC.m_Lens.OrthographicSize = 15f;
        }
        else
        {
            CVC.m_Lens.OrthographicSize = 7f;
        }

    }



    //                      Run time methods
    //=====================================================================
    void Start()
    {
        //collider = GetComponent<BoxCollider2D>();

        healthBar = FindObjectOfType<HealthBar>();
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        timer = 0.0f;

        controller = GetComponent<Controller2D>();
        animate = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        inventory = FindObjectOfType<Inventory>();
        gravity = -(2 * jumpHeight) / Mathf.Pow(jumpAcceleration, 2);
        jumpSpeed = Mathf.Abs(gravity) * jumpAcceleration;

        main_jumpSound = Resources.Load<AudioClip>("Main_jump");
        main_dieSound = Resources.Load<AudioClip>("Main_Die");
        main_walkSound = Resources.Load<AudioClip>("Main_Walk");
        main_hitSound = Resources.Load<AudioClip>("Main_Hurt");
        main_shoot_laser = Resources.Load<AudioClip>("Main_LaserShoot");

        SFX_playerSrc = GetComponent<AudioSource>();
        shoot = true;
        hasGun = false;
        projectile = gunProjectile.GetComponent<Projectile>();
        projectile.speed = projectileSpeed;
        projectile.damage = projectileDamage;
    }
 
    void Update()
    {
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
            animate.SetTrigger("Player jump start");
            velocity.y = jumpSpeed;

            SFX_playerSrc.PlayOneShot(main_jumpSound);
        }
        if (Input.GetKeyUp(KeyCode.Space) && velocity.y > 0)
            velocity.y = 1f;

        if (Input.GetKeyDown(KeyCode.Mouse1) && shoot && hasGun && inventory.ammo > 0)
        {
            inventory.ammo--;
            StartCoroutine("fireGun");
            SFX_playerSrc.PlayOneShot(main_shoot_laser);
        }
            
        updateAnimation();
        Vector3 newVelocity = calculateVelocity(ref velocity, direction);
        controller.move(newVelocity);

        
        shopEnter();

        timer += Time.deltaTime;
        float seconds = timer % 60;
        if (seconds > .2f)
        {
            healthBar.HBar_Normalized();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Key"))
        {
            Destroy(other.gameObject);
        }
    }
}
