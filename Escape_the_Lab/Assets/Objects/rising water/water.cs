using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class water : MonoBehaviour
{

    public int testNumber = 0;
    public float waterSpeed;
    TilemapCollider2D ignoreCollider;
    private Vector3 velocity;
    Camera mainCamera;
    Bounds cameraBounds;
    Bounds waterBounds;
    Collider2D boxCollider;
    SpriteRenderer sprite;
    bool checkX = false;
    bool checkY = false;
    bool hitWall = false;
    Transform cameraTransform;

    AudioSource water_AudioSrc;
    AudioClip water_raisingSound;

    private Bounds OrthographicBounds(Camera camera)
    {
        float screenAspect = (float)Screen.width / (float)Screen.height;
        float cameraHeight = camera.orthographicSize * 2;
        Bounds bounds = new Bounds(
            camera.transform.position,
            new Vector3(cameraHeight * screenAspect, cameraHeight, 0));
        return bounds;
    }

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

   

    void Start()
    {
        ignoreCollider = FindObjectOfType <TilemapCollider2D>();
        mainCamera = FindObjectOfType<Camera>();
        cameraTransform = mainCamera.transform;
        boxCollider = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
        cameraBounds = OrthographicBounds(mainCamera);
        waterBounds = boxCollider.bounds;
        waterSpeed = waterSpeed / 100000;
        //Physics2D.IgnoreCollision(boxCollider, ignoreCollider);
        //Physics2D.IgnoreLayerCollision(11, 8);
        //Physics2D.IgnoreLayerCollision(11, 12);
        //InvokeRepeating("flipSpriteX", 1f, .5f); 
        //InvokeRepeating("flipSpriteY", .8f, .5f);

        water_raisingSound = Resources.Load<AudioClip>("Water_Rasing_Normal");
        water_AudioSrc = GetComponent<AudioSource>();
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

        transform.Translate(velocity);

        //SFX
        if (water_AudioSrc.time >= water_raisingSound.length || water_AudioSrc.time <= 0)
        {
            //Debug.Log("Get in play water sound==========================");
            water_AudioSrc.Play();
        }

    }
}
