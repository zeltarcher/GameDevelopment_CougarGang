using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (Controller2D))]
public class Player : MonoBehaviour
{
    //                          Class variables
    //====================================================================
    public float jumpHeight = 4f;
    public float jumpAcceleration = .4f;
    public float movementSpeed = 6f;
    float jumpSpeed;
    float gravity;
    float velocityXSmoothing;
    Vector3 velocity;
    Vector2 direction;
    Controller2D controller;
    Animator animate;
    SpriteRenderer sprite;


    //                          Helper methods
    //====================================================================
    private void updateAnimation()
    {
        if (direction.x == 0 && controller.collisions.below)
        {
            resetAnimations();
            animate.SetBool("Player Idle", true);
        }
        else if (controller.collisions.below)
        {
            if (direction.x < 0)
                sprite.flipX = true;
            else
                sprite.flipX = false;
            resetAnimations();
            animate.SetBool("Player Moving", true);
        }
        else if (velocity.y > 1 && controller.collisions.below == false)
        {
            if (direction.x < 0)
                sprite.flipX = true;
            else
                sprite.flipX = false;
            resetAnimations();
            animate.SetBool("Player jump loop", true);
        }
        else if (velocity.y < 1 && controller.collisions.below == false)
        {
            if (direction.x < 0)
                sprite.flipX = true;
            else
                sprite.flipX = false;
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

    //                      Run time methods
    //=====================================================================
    void Start()
    {
        controller = GetComponent<Controller2D>();
        animate = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        gravity = -(2 * jumpHeight) / Mathf.Pow(jumpAcceleration, 2);
        jumpSpeed = Mathf.Abs(gravity) * jumpAcceleration;
    }

  
    void Update()
    {
        direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if (controller.collisions.above || controller.collisions.below)
            velocity.y = 0;
   
        if (Input.GetKeyDown(KeyCode.Space) && controller.collisions.below)
        {
            resetAnimations();
            animate.SetTrigger("Player jump start");
            velocity.y = jumpSpeed;
        }
        if (Input.GetKeyUp(KeyCode.Space) && velocity.y > 0)
            velocity.y = 1f;

        updateAnimation();
        Vector3 newVelocity = calculateVelocity(ref velocity, direction);
        controller.move(newVelocity);
    }
}
