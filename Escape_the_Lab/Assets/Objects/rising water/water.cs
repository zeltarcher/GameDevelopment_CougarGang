using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class water : MonoBehaviour
{
    public float waterSpeed;
    private Vector3 velocity;
    SpriteRenderer sprite;
    bool checkX = false;
    bool checkY = false;
    AudioSource water_AudioSrc;
    AudioClip water_raisingSound;
    Rigidbody2D waterBody;
    //HealthController Health;
    //public int damageAmount = 10;
    //public float damageInterval = .5f;

    private void flipSpriteX()
    {
        if (checkX == false)
            sprite.flipX = true;
        if (checkX == true)
            sprite.flipX = false;

        if (checkX == false)
            checkX = true;
        else
            checkX = false;
    }
    private void flipSpriteY()
    {
        if (checkY == false)
            sprite.flipY = true;
        if (checkY == true)
            sprite.flipY = false;

        if (checkY == false)
            checkY = true;
        else
            checkY = false;
    }

   /* private void poisonWater()
    {
        Health.TakeDamage(damageAmount);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.name);
        if (collision.tag == "Enemy")
        {
            Debug.Log(collision.gameObject.name);
            Health = collision.gameObject.GetComponent<HealthController>();
            InvokeRepeating("poisonWater", 0f, damageInterval);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        CancelInvoke();
    } */

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        waterBody = GetComponent<Rigidbody2D>();
        waterSpeed = waterSpeed / 10;
        InvokeRepeating("flipSpriteX", 1f, .5f); 
        InvokeRepeating("flipSpriteY", .8f, .5f);

        water_raisingSound = Resources.Load<AudioClip>("Water_Rasing_Normal");
        water_AudioSrc = GetComponent<AudioSource>();
    }

    void FixedUpdate()
    {
        waterBody.velocity = new Vector2(0, waterSpeed);

        //SFX
        if (water_AudioSrc.time >= water_raisingSound.length || water_AudioSrc.time <= 0)
        {
            //Debug.Log("Get in play water sound==========================");
            water_AudioSrc.Play();
        }

    }
}
