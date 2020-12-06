using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlatformBoss : RayCastController
{
    public Vector2 moveSpeed;
    public LayerMask passengerMask;
    public float travelDistance = 5f;
    Vector3 startingDistance;
    bool isReturnToStart;
    public Boolean bossDeath;

    void movePassengers(Vector2 velocity)
    {
        HashSet<Transform> movedPassengers = new HashSet<Transform>();
        float directionX = Mathf.Sign(velocity.x);
        float directionY = Mathf.Sign(velocity.y);

        //vertical collision
        if(velocity.y != 0)
        {
            Vector2 rayOrigin;
            float rayLength = Mathf.Abs(velocity.y) + skinWidth;

            for (int i = 0; i < verticalRayCount; i++)
            {
                if (directionY == -1)
                    rayOrigin = raycast.bottomLeft;
                else
                    rayOrigin = raycast.topLeft;

                rayOrigin += Vector2.right * (verticalRaySpacing * i);
                RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.up * directionY, rayLength, passengerMask);

                if (hit)
                {
                    if (!movedPassengers.Contains(hit.transform))
                    {
                        movedPassengers.Add(hit.transform);
                        float pushY = velocity.y - (hit.distance - skinWidth) * directionY;
                        float pushX = (directionY == 1) ? velocity.x : 0;

                        hit.transform.Translate(new Vector3(pushX, pushY));
                    }
                }
            }
        }
        //horizontal collision
        if(velocity.x != 0)
        {
            Vector2 rayOrigin;
            float rayLength = Mathf.Abs(velocity.x) + skinWidth;
            for (int i = 0; i < horizontalRayCount; i++)
            {
                if (directionX == -1)
                    rayOrigin = raycast.bottomLeft;
                else
                    rayOrigin = raycast.bottomRight;

                rayOrigin += Vector2.up * (horizontalRaySpacing * i);
                RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.right * directionX, rayLength, passengerMask);

                if (hit)
                {
                    if (!movedPassengers.Contains(hit.transform))
                    {
                        movedPassengers.Add(hit.transform);
                        float pushY = 0;
                        float pushX = velocity.x - (hit.distance - skinWidth) * directionX;

                        hit.transform.Translate(new Vector3(pushX, pushY));
                    }
                }
            }
        }
        //if platform moving down or moving right/left
        if(directionY == -1 || velocity.y == 0 && velocity.x != 0)
        {
            Vector2 rayOrigin;
            float rayLength = skinWidth * 2;

            for (int i = 0; i < verticalRayCount; i++)
            {
                rayOrigin = raycast.topLeft + Vector2.right * (verticalRaySpacing * i);
                RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.up, rayLength, passengerMask);

                if (hit)
                {
                    if (!movedPassengers.Contains(hit.transform))
                    {
                        movedPassengers.Add(hit.transform);
                        float pushY = velocity.y;
                        float pushX = velocity.x;

                        hit.transform.Translate(new Vector3(pushX, pushY));
                    }
                }
            }
        }
    }

    void checkbounds()
    {
        if (!isReturnToStart)
        {
            float currentDistance = Vector3.Distance(startingDistance, transform.position);
            if (currentDistance > travelDistance && moveSpeed.y == 0 && moveSpeed.x != 0)
            {
                moveSpeed.x *= -1;
                isReturnToStart = true;
            }
            else if (currentDistance > travelDistance && moveSpeed.y != 0 && moveSpeed.x == 0)
            {
                moveSpeed.y *= -1;
                isReturnToStart = true;
            }
            else if (currentDistance > travelDistance && moveSpeed.y != 0 && moveSpeed.x != 0)
            {
                moveSpeed.x *= -1;
                moveSpeed.y *= -1;
                isReturnToStart = true;
            }
        }
        else if (isReturnToStart)
        {
            float currentDistance = Vector3.Distance(startingDistance, transform.position);
            if (currentDistance < 1 && moveSpeed.y == 0 && moveSpeed.x != 0)
            {
                moveSpeed.x *= -1;
                isReturnToStart = false;
            }
            else if (currentDistance < 1 && moveSpeed.y != 0 && moveSpeed.x == 0)
            {
                moveSpeed.y *= -1;
                isReturnToStart = false;
            }
            else if (currentDistance < 1 && moveSpeed.y != 0 && moveSpeed.x != 0)
            {
                moveSpeed.x *= -1;
                moveSpeed.y *= -1;
                isReturnToStart = false;
            }
        }
    }

    public override void Start()
    {
        base.Start();
        startingDistance = transform.position;
        isReturnToStart = false;
        bossDeath = FindObjectOfType<Boss>().bossDeath;
    }

    void FixedUpdate()
    {

        if (bossDeath)
        {

            updateRaycastOrigins();
            checkbounds();
            Vector2 velocity = moveSpeed * Time.deltaTime;
            movePassengers(velocity);
            transform.Translate(velocity);
            bossDeath = true;
        }
        else
        {
            bossDeath = FindObjectOfType<Boss>().bossDeath;
        }
    }
    /*void Start()
    {
        //var bossHealth = FindObjectOfType<Boss>().health;
        //bossDeath = false;
        bossDeath = FindObjectOfType<Boss>().bossDeath;
    }*/
    /*void Update()
    {
        //var bossHealth = FindObjectOfType<Boss>().health;
        //bossDeath = FindObjectOfType<Boss>().bossDeath;
        if (bossDeath)
        {
            //Debug.Log("y = " + transform.position.y);
            //Debug.Log(transform.position.y > Vertical_High_Point);
            //Debug.Log("Move up is: " + moveUp);
            //Debug.Log("Vertical_High_Point = " + Vertical_High_Point);
            //Debug.Log("Vertical_Low_Point = " + Vertical_Low_Point);
            startingDistance = transform.position;
            isReturnToStart = false;
            bossDeath = true;
        }
        else
        {
            bossDeath = FindObjectOfType<Boss>().bossDeath;
        }

    }*/
}
