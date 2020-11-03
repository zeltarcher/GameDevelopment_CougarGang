using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class water : MonoBehaviour
{
    public int testNumber = 0;
<<<<<<< HEAD
    public float waterSpeed = 100;
    TilemapCollider2D ignoreCollider;
=======
    public float waterSpeed;
>>>>>>> 4889e8bde82bcef306116f1ba28d527cc6727ecb
    private Vector3 velocity;
    SpriteRenderer sprite;
    bool checkX = false;
    bool checkY = false;
<<<<<<< HEAD
    bool hitWall = false;
    Transform cameraTransform;


=======
>>>>>>> 4889e8bde82bcef306116f1ba28d527cc6727ecb
    AudioSource water_AudioSrc;
    AudioClip water_raisingSound;
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
        waterSpeed = waterSpeed / 100000;
        InvokeRepeating("flipSpriteX", 1f, .5f); 
        InvokeRepeating("flipSpriteY", .8f, .5f);

        water_raisingSound = Resources.Load<AudioClip>("Water_Rasing_Normal");
        water_AudioSrc = GetComponent<AudioSource>();
    }

<<<<<<< HEAD
    private void Update()
    {
        
    }

    void FixedUpdate()
    {

        

        // cameraBounds = OrthographicBounds(mainCamera);
        // waterBounds = boxCollider.bounds;
        //waterBounds.max.y < cameraBounds.max.y
        //if (!hitWall)
        //  {
        
        velocity.y += waterSpeed * Time.deltaTime;
       
        // }
        // else
         // velocity.y = 0;

        //Vector3 resultingPosition = cameraTransform.position + cameraTransform.forward;
        //transform.position = new Vector3(resultingPosition.x, transform.position.y, resultingPosition.z);
=======
    void FixedUpdate()
    {
            velocity.y += waterSpeed * Time.deltaTime;
>>>>>>> 4889e8bde82bcef306116f1ba28d527cc6727ecb

        transform.Translate(velocity);

        //SFX
        if (water_AudioSrc.time >= water_raisingSound.length || water_AudioSrc.time <= 0)
        {
            //Debug.Log("Get in play water sound==========================");
            water_AudioSrc.Play();
        }

    }
}
