using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(BoxCollider2D))]
public class Controller2D : RayCastController
{
    //                          Class variables
    //====================================================================

    public collisionInfo collisions;

    public struct collisionInfo
    {
        public bool above, below;
        public bool left, right;

        public void reset()
        {
            above = below = false;
            left = right = false;
        }
    }
    //                          Helper methods
    //====================================================================


    public void move(Vector3 velocity)
    {
        updateRaycastOrigins();
        collisions.reset();

        if (velocity.x != 0)
            horizontalCollisions(ref velocity);
        if(velocity.y != 0)
            verticalCollisions(ref velocity);

        transform.Translate(velocity);
    }

    void verticalCollisions(ref Vector3 velocity)
    {
        Vector2 rayOrigin;
        float directionY = Mathf.Sign(velocity.y);
        float rayLength = Mathf.Abs(velocity.y) + skinWidth;

        for (int i = 0; i < verticalRayCount; i++)
        {
            if (directionY == -1)
                rayOrigin = raycast.bottomLeft;
            else
                rayOrigin = raycast.topLeft;

            rayOrigin += Vector2.right * (verticalRaySpacing * i + velocity.x);
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.up * directionY, rayLength, collisionMask);

            if(hit)
            {
                velocity.y = (hit.distance - skinWidth) * directionY;
                rayLength = hit.distance;
                collisions.below = directionY == -1;
                collisions.above = directionY == 1;
            }
        }
    }
    void horizontalCollisions(ref Vector3 velocity)
    {
        Vector2 rayOrigin;
        float directionX = Mathf.Sign(velocity.x);
        float rayLength = Mathf.Abs(velocity.x) + skinWidth;

        for (int i = 0; i < horizontalRayCount; i++)
        {
            if (directionX == -1)
                rayOrigin = raycast.bottomLeft;
            else
                rayOrigin = raycast.bottomRight;

            rayOrigin += Vector2.up * (horizontalRaySpacing * i);
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.right * directionX, rayLength, collisionMask);

            if (hit)
            {
                velocity.x = (hit.distance - skinWidth) * directionX;
                rayLength = hit.distance;
                collisions.left = directionX == -1;
                collisions.right = directionX == 1;
            }
        }
    }
    public override void Start()
    {
        base.Start();
    }


}
