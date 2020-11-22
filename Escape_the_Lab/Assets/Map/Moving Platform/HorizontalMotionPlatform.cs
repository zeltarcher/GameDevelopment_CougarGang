using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalMotionPlatform : MonoBehaviour
{
    public float moveSpeed = 3f;
    bool moveRight = true;
    public float Horizontal_left_Point;
    public float Horizontal_right_Point;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(transform.position.x);
        if(transform.position.x > Horizontal_right_Point)
        {
            moveRight = false;
        }
        if(transform.position.x < Horizontal_left_Point)
        {
            moveRight = true;
        }

        if (moveRight)
        {
            transform.position = new Vector2(transform.position.x + moveSpeed * Time.deltaTime, transform.position.y);
        }
        else
        {
            transform.position = new Vector2(transform.position.x - moveSpeed * Time.deltaTime, transform.position.y);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("AHHHHHHHHHHHHHHHHHHH");
        if (collision.collider.tag == "Player" || collision.collider.tag == "Platform")
        {
            Debug.Log("PLAYERRRRRRRRRRRRRR");
            collision.transform.position = new Vector2(collision.transform.position.x + moveSpeed * Time.deltaTime, collision.transform.position.y);
        }
    }
}
