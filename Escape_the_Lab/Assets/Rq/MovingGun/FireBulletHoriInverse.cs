using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBulletHoriInverse : MonoBehaviour
{

    public float fireSpeed = 20;
    public float des;
    public Vector3 targetPoint;

    AudioSource au_sou;
    AudioClip fire_sfx;

    void Start()
    {
        au_sou = GetComponent<AudioSource>();
        fire_sfx = Resources.Load<AudioClip>("Enemy_Turret_LaserShoot");
        //au_sou.PlayOneShot(fire_sfx);
    }

    void FixedUpdate()
    {

        targetPoint.x = this.transform.position.x - 2;
        targetPoint.y = this.transform.position.y;
        targetPoint.z = this.transform.position.z;

        transform.position = Vector3.MoveTowards(this.transform.position, targetPoint, Time.deltaTime * fireSpeed);

        des = des - fireSpeed;

        if (des < 0.1f)
        {
            Destroy(this.gameObject);
        }
    }
}