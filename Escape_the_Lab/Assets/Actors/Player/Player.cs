﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

[RequireComponent (typeof (Controller2D))]
[RequireComponent(typeof(HealthBar))]
public class Player : MonoBehaviour
{
    //                          Class variables
    //====================================================================

    // from key picker
    //=============================
    private float coin = 0;
    private float bomb = 0;
    private float immute = 0;
    private float hp = 0;
    private float key = 0;
    private float drug = 0;
    private float superPotion = 0;

    public TextMeshProUGUI txtCoin;
    public TextMeshProUGUI txtBomb;
    public TextMeshProUGUI txtImmute;
    public TextMeshProUGUI txtHP;
    public TextMeshProUGUI txtDrug;
    public TextMeshProUGUI txtKey;
    public TextMeshProUGUI txtSuper;
    //==============================



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

    AudioSource SFX_playerSrc;
    AudioClip main_jumpSound, main_dieSound, main_walkSound, main_hitSound;

    KeyPicker keypick;

    
    //                          Helper methods
    //====================================================================
    private void updateAnimation()
    {
        if (direction.x < 0)
            sprite.flipX = true;
        else if (direction.x != 0)
            sprite.flipX = false;

        if (direction.x == 0 && controller.collisions.below)
        {
            resetAnimations();
            animate.SetBool("Player Idle", true);
        }
        else if (controller.collisions.below)
        {
            resetAnimations();
            animate.SetBool("Player Moving", true);
        }
        else if (velocity.y > 1 && controller.collisions.below == false)
        {
            resetAnimations();
            animate.SetBool("Player jump loop", true);
        }
        else if (velocity.y < 1 && controller.collisions.below == false)
        {
            resetAnimations();
            animate.SetBool("Player falling", true);
        }
    }

    private void resetAnimations()
    {
        foreach (AnimatorControllerParameter parameter in animate.parameters)
        {
            if (parameter.type == AnimatorControllerParameterType.Bool)
                animate.SetBool(parameter.name, false);
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
    void TakeDamage(int damage)
    {
        currentHealth -= damage;
        //healthBar.SetHealth(currentHealth);
        //SFX
        if (currentHealth>0)
            SFX_playerSrc.PlayOneShot(main_hitSound);
        else if(currentHealth==0)
            SFX_playerSrc.PlayOneShot(main_dieSound);
    }
    private void poisonWater() { 
        TakeDamage(10); }

    private void OnTriggerEnter2D(Collider2D collision) 
    { 
        if(collision.tag == "Water" && immuneToWater == false)
            InvokeRepeating("poisonWater", 0f, .5f); 
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        if (other.tag == "Water")
            CancelInvoke();

        // from key picker
        //======================================================
        else if (other.transform.tag == "Coins")
        {
            Destroy(other.gameObject);
            coin++;
            Debug.Log(coin);
            txtCoin.text = coin.ToString();
        }

        else if (other.transform.tag == "superPotion")
        {
            Destroy(other.gameObject);
            superPotion++;
            Debug.Log("superpotion counts: " + superPotion);
            txtSuper.text = superPotion.ToString();

        }

        else if (other.transform.tag == "Bombs")
        {
            Destroy(other.gameObject);
            bomb++;
            txtBomb.text = bomb.ToString();
        }
        else if (other.transform.tag == "Immutes")
        {
            Destroy(other.gameObject);
            immute++;
            txtImmute.text = immute.ToString();
        }
        else if (other.transform.tag == "HPs")
        {
            Destroy(other.gameObject);
            hp++;
            txtHP.text = hp.ToString();
        }
        else if (other.transform.tag == "Drugs")
        {
            Destroy(other.gameObject);
            drug++;
            Debug.Log("Drug counts: " + drug);
            txtDrug.text = drug.ToString();
        }
        else if (other.transform.tag == "Keys")
        {
            Destroy(other.gameObject);
            key++;
            Debug.Log("Key counts: " + key);
            txtKey.text = key.ToString();
        }
        //======================================================

    }

    private IEnumerator playerDeath()
    {
        Time.timeScale = 0;
        resetAnimations();
        animate.SetTrigger("Player Death");
        yield return new WaitForSecondsRealtime(3f);
        Time.timeScale = 1;
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    private void stopAnimation() {
        animate.enabled = false;
    }
   
    //                      Run time methods
    //=====================================================================
    void Start()
    {
        //keypick = gameObject.AddComponent<KeyPicker>();

        healthBar = FindObjectOfType<HealthBar>();
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);

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

        khaBomb();

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
            resetAnimations();
            animate.SetTrigger("Player jump start");
            velocity.y = jumpSpeed;

            //SFX
            SFX_playerSrc.PlayOneShot(main_jumpSound);
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

        
    }

    

    public float countBomb()
    {
        return bomb;
    }

    public void khaBomb()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (bomb > 0)
            {
                bomb--;
                txtBomb.text = bomb.ToString();
            }
        }
    }


}
