using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalMotionPlatform : MonoBehaviour
{
    public float moveSpeed = 3f;
    bool moveUp = true;
    public float Vertical_Start_Point;
    public float Vertical_End_Point;
    float distance;
    public bool isEndPointNegative = true;
    // Start is called before the first frame update
    void Start()
    {
        distance = Math.Abs(Vertical_End_Point - Vertical_Start_Point);
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y > Vertical_Start_Point)
        {
            moveUp = false;
        }
        if (transform.position.y < Vertical_End_Point)
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
