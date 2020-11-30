using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    CircleCollider2D circle;
    Animator animate;
    SpriteRenderer sprite;

    void enableAttack() { circle.enabled = true; }
    void disableAttack() { circle.enabled = false; }
    void disableAnimator() { 
        animate.enabled = false; 
        Destroy(transform.parent.gameObject); 
    }

    void Start()
    {
        circle = GetComponent<CircleCollider2D>();
        animate = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        circle.enabled = false;
    }
}
