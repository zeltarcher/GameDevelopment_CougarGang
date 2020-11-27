using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shark : MonoBehaviour
{
    public float velocity_x = -0.1f;
    public float velocity_y = 0.15f;
    public float gravity = -0.05f;
    private int flip;

    private Player player;
    private SpriteRenderer sprite;
    private Animator anim;

    private GameObject risingWater;
    private ObjectTrigger objTrigger;

    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        flip = Random.Range(0,2);
        if (flip == 1)
        {
            sprite.flipX = true;
            velocity_x = -velocity_x;
        }
        anim = GetComponent<Animator>();
        anim.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        velocity_y += gravity * Time.deltaTime;
        transform.Translate(velocity_x, velocity_y, 0);

        objTrigger = GameObject.Find("FallObjOff").GetComponent<ObjectTrigger>();

        if (objTrigger.isLevel1 == true)
        {
            risingWater = GameObject.Find("Rising Water");
        }
        else
        {
            risingWater = GameObject.Find("Rising Water 2");
        }

        if (transform.position.y < risingWater.transform.position.y - 2 * risingWater.transform.localScale.y)
            Destroy(this.gameObject);
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (FindObjectOfType<charChange>().p1 == true)
            player = GameObject.Find("Man").GetComponent<Player>();
        else if (FindObjectOfType<charChange>().p2 == true)
            player = GameObject.Find("Robot").GetComponent<Player>();

        if (other.gameObject.CompareTag("Player"))
        {
            anim.enabled = true;
            player.TakeDamage(10);
            Debug.Log(player.currentHealth);
        }
    }
}
