using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallObject : MonoBehaviour
{
    private Player player;

    private void Start()
    {
        //player = GameObject.Find("Man").GetComponent<Player>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (FindObjectOfType<charChange>().p1 == true)
            player = GameObject.Find("Man").GetComponent<Player>();
        else if (FindObjectOfType<charChange>().p2 == true)
            player = GameObject.Find("Robot").GetComponent<Player>();

        if (other.gameObject.CompareTag("Player")
            && other.GetType().ToString() == "UnityEngine.BoxCollider2D")
        { 
            player.TakeDamage(10);
            this.gameObject.SetActive(false);
        }
    }
}
