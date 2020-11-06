using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalMotionPlatform : MonoBehaviour
{
    public float moveSpeed = 3f;
    bool moveUp = true;
    public float Vertical_High_Point;
    public float Vertical_Low_Point;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y > Vertical_High_Point)
        {
            moveUp = false;
        }
        if (transform.position.y < Vertical_Low_Point)
        {
            moveUp = true;
        }

        if (moveUp)
        {
            transform.position = new Vector2(transform.position.x, transform.position.y + moveSpeed * Time.deltaTime);
        }
        else
        {
            transform.position = new Vector2(transform.position.x, transform.position.y - moveSpeed * Time.deltaTime);
        }

    }
}
