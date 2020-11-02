using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

public class Projectile : MonoBehaviour
{
    public float speed = 20f;
    public int damage = 20;
    float velocityY = 0f;
    Rigidbody2D bullet;

    private void Start()
    {
        bullet = GetComponent<Rigidbody2D>();
    }

    private void  FixedUpdate()
    {
        bullet.velocity = new Vector2(speed, velocityY);
    }

    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        Debug.Log(otherCollider.gameObject.name);
        if (otherCollider.tag == "Enemy")
        {
            otherCollider.gameObject.GetComponent<EnemyController>().TakeDamage(damage);
            Destroy(gameObject);
        }
        else
            Destroy(gameObject);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.name);
    }
}
