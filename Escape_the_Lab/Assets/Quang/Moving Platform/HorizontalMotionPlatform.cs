using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalMotionPlatform : MonoBehaviour
{
    public GameObject thisPlatform;
    public float moveSpeed = 3f;
    bool moveRight = true;
    public float Horizontal_left_Point;
    public float Horizontal_right_Point;


    
    /*
    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Platform")
        {
            collision.collider.transform.SetParent(null);
        }
    }*/

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
}
