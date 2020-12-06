using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class RayCastController : MonoBehaviour
{
    public RayCastOrigins raycast;
    public const float skinWidth = 0.15f;
    public int horizontalRayCount = 4;
    public int verticalRayCount = 4;
    public LayerMask collisionMask;
    protected BoxCollider2D boxCollider;
    protected float horizontalRaySpacing;
    protected float verticalRaySpacing;

    public struct RayCastOrigins
    {
        public Vector2 topLeft, topRight;
        public Vector2 bottomLeft, bottomRight;
    }

    //                          Helper methods
    //====================================================================

    public void updateRaycastOrigins()
    {
        Bounds bounds = boxCollider.bounds; //sets the size of the collider of each axis
        bounds.Expand(skinWidth * -2); //changes bounds of each axis inward
        raycast.bottomLeft = new Vector2(bounds.min.x, bounds.min.y); //sets the position of the raycasts
        raycast.bottomRight = new Vector2(bounds.max.x, bounds.min.y);
        raycast.topLeft = new Vector2(bounds.min.x, bounds.max.y);
        raycast.topRight = new Vector2(bounds.max.x, bounds.max.y);
    }

    public void calculateRaySpacing()
    {
        Bounds bounds = boxCollider.bounds;
        bounds.Expand(skinWidth * -2);
        horizontalRayCount = Mathf.Clamp(horizontalRayCount, 2, int.MaxValue); //sets min count to 2
        verticalRayCount = Mathf.Clamp(verticalRayCount, 2, int.MaxValue);
        horizontalRaySpacing = bounds.size.y / (horizontalRayCount - 1);
        verticalRaySpacing = bounds.size.x / (verticalRayCount - 1);
    }

    // Start is called before the first frame update
    public virtual void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        calculateRaySpacing();
    }
}
