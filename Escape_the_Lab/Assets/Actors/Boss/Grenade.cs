using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    public float throwDistance = 10f;
    public float explosionDelay = 2f;
    Rigidbody2D body;
    SpriteRenderer Sprite;
    SpriteRenderer childSprite;
    SpriteRenderer[] allRenderers;
    Animator animate;

    private IEnumerator explosion()
    {
        yield return new WaitForSecondsRealtime(explosionDelay);
        Sprite.enabled = false;
        childSprite.enabled = true;
        animate.enabled = true;
    }

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        animate = GetComponentInChildren<Animator>();
        allRenderers = GetComponentsInChildren<SpriteRenderer>();
        Sprite = allRenderers[0];
        childSprite = allRenderers[1];
        childSprite.enabled = false;
        animate.enabled = false;
        body.AddForce(new Vector2(throwDistance, Mathf.Abs(throwDistance) - 100));
        StartCoroutine("explosion");
    }

    void Update()
    {

    }
}
