using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchFallScript : MonoBehaviour
{
    public float droppingDelay;
    public float stopDelay;
    public int touch;
    public int maxTouch;
    
    Rigidbody2D rb;
    SpriteRenderer sr;
    

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        
    }

    // Update is called once per frame

    void OnTriggerEnter2D(Collider2D other)
    {

        touch++;
        sr.sprite = Resources.Load("RedUnit", typeof(Sprite)) as Sprite;
        if (touch > maxTouch) {

            Invoke("StartDropping", droppingDelay);
        }
    }

    void StartDropping()
    {
        rb.isKinematic = false;
    }

}
