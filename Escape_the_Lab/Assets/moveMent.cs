using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class moveMent : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float runSpeed = 5f;
    [SerializeField] float jumpSpeed = 5f;

    Rigidbody2D myRigidBody;
    CapsuleCollider2D bodyCollider;
    BoxCollider2D footCollider;
    float gravityScaleAtStart;


    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        bodyCollider = GetComponent<CapsuleCollider2D>();
        footCollider = GetComponent<BoxCollider2D>();
        gravityScaleAtStart = myRigidBody.gravityScale;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Run()
    {
        float controlTrhow = CrossPlatformInputManager.GetAxis("Horizontal");
        Vector2 playerVelocity = new Vector2(controlTrhow * runSpeed, myRigidBody.velocity.y);
        myRigidBody.velocity = playerVelocity;
    }

    void Jump()
    {
        if (CrossPlatformInputManager.GetButtonDown("Jump"))
        {
            if (footCollider.IsTouchingLayers(LayerMask.GetMask("Platform")))
            {
                Vector2 jumpVelocityToAdd = new Vector2(0f, jumpSpeed);
                myRigidBody.velocity += jumpVelocityToAdd;
            }
        }
    }
}
