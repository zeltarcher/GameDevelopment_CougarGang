using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBulletHorizontal : MonoBehaviour
{
    //public GameObject OriginalPoint;
    //public GameObject targetPoint;
    //public GameObject targetOn;
    
    public float fireSpeed = 20;
    public float des;
    public Vector3 targetPoint;

    AudioSource au_sou;
    AudioClip fire_sfx;

    void Start()
    {
        //rb = GetComponent<Rigidbody>();
        au_sou = GetComponent<AudioSource>();
        fire_sfx = Resources.Load<AudioClip>("Shark_jump");
        //au_sou.PlayOneShot(fire_sfx);
    }

    void FixedUpdate()
    {
        //float xv = this.transform.position.x;
        targetPoint.x = this.transform.position.x + 2;
        targetPoint.y = this.transform.position.y;
        targetPoint.z = this.transform.position.z;

        transform.position = Vector3.MoveTowards(this.transform.position, targetPoint, Time.deltaTime * fireSpeed);

        des = des - fireSpeed;

        if (des < 0.1f){
            Destroy(this.gameObject);
        }


        //des = Vector3.Distance(this.transform.position, targetPoint.transform.position);
        //transform.position = Vector3.MoveTowards(this.transform.position, targetPoint.transform.position, Time.deltaTime * fireSpeed);
        //if (des < 0.1f)
        //{
        //Destroy(this.gameObject);
        //this.gameObject.transform.position = OriginalPoint.transform.position;
        //}
    }
}
