using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBulletVertical : MonoBehaviour
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
        //float xv = this.transform.position.x;
        targetPoint.x = this.transform.position.x;
        targetPoint.y = this.transform.position.y - 2;
        targetPoint.z = this.transform.position.z;

        transform.position = Vector3.MoveTowards(this.transform.position, targetPoint, Time.deltaTime * fireSpeed);

        des = des - fireSpeed;

        if (des < 0.1f)
        {
            Destroy(this.gameObject);
        }

    }
}
