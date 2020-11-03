using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropTile : MonoBehaviour
{
    public float droppingDelay;
    public float stopDelay;
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame

    void OnTriggerEnter2D(Collider2D other)
    {
        Invoke("StartDropping", droppingDelay);
        //Invoke("StopDropping", stopDelay);
    }

    void StartDropping()
    {
        rb.isKinematic = false;
    }

    void StopDropping()
    {
        rb.isKinematic = true;
    }
}
