using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(BoxCollider2D))]
public class Controller2D : MonoBehaviour
{
    //                          Class variables
    //====================================================================
    BoxCollider2D collider2D;
    RayCastOrigins raycast;
    const float skinWidth = 0.15f;
    public int horizontalRayCount = 4;
    public int verticalRayCount = 4;
    public LayerMask collisionMask;
    private float horizontalRaySpacing;
    private float verticalRaySpacing;
    public collisionInfo collisions;

    struct RayCastOrigins
    {
        public Vector2 topLeft, topRight;
        public Vector2 bottomLeft, bottomRight;
    }

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

    void updateRaycastOrigins()
    {
        Bounds bounds = collider2D.bounds; //sets the size of the collider of each axis
        bounds.Expand(skinWidth * -2); //changes bounds of each axis inward
        raycast.bottomLeft = new Vector2(bounds.min.x, bounds.min.y); //sets the position of the raycasts
        raycast.bottomRight = new Vector2(bounds.max.x, bounds.min.y);
        raycast.topLeft= new Vector2(bounds.min.x, bounds.max.y);
        raycast.topRight = new Vector2(bounds.max.x, bounds.max.y);
    }

    void calculateRaySpacing()
    {
        Bounds bounds = collider2D.bounds;
        bounds.Expand(skinWidth * -2);
        horizontalRayCount = Mathf.Clamp(horizontalRayCount, 2, int.MaxValue); //sets min count to 2
        verticalRayCount = Mathf.Clamp(verticalRayCount, 2, int.MaxValue);
        horizontalRaySpacing = bounds.size.y / (horizontalRayCount - 1);
        verticalRaySpacing = bounds.size.x / (verticalRayCount - 1);
    }

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
            //Debug.DrawRay(rayOrigin, Vector2.up * directionY * rayLength, Color.red, Time.deltaTime);

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
            //Debug.DrawRay(rayOrigin, Vector2.right * directionX * rayLength, Color.red, Time.deltaTime);

            if (hit)
            {
                velocity.x = (hit.distance - skinWidth) * directionX;
                rayLength = hit.distance;
                collisions.left = directionX == -1;
                collisions.right = directionX == 1;
            }
        }
    }
    //                      Run time methods
    //=====================================================================
    void Start()
    {
        collider2D = GetComponent<BoxCollider2D>();
        calculateRaySpacing();
    }
}
