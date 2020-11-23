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
        //Debug.Log("y = " + transform.position.y);
        Debug.Log(transform.position.y > Vertical_High_Point);
        Debug.Log("Move up is: " + moveUp);
        //Debug.Log("Vertical_High_Point = " + Vertical_High_Point);
        //Debug.Log("Vertical_Low_Point = " + Vertical_Low_Point);
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
            Debug.Log("Moving UPPPPPPPPPPP");
            transform.position = new Vector2(transform.position.x, transform.position.y + moveSpeed * Time.deltaTime);
        }
        else
        {
            Debug.Log("Moving Downnnnn");
            transform.position = new Vector2(transform.position.x, transform.position.y - moveSpeed * Time.deltaTime);
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("AHHHHHHHHHHHHHHHHHHH");
        if (collision.tag == "Platform")
        {
            //Debug.Log("Hit " + collision.tag);
            // collision.transform.position = new Vector2(collision.transform.position.x + moveSpeed * Time.deltaTime *-1, collision.transform.position.y);
            moveSpeed *= -1;
        }
    }
}
