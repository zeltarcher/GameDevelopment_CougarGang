using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBulletVertical : MonoBehaviour
{

    public float fireSpeed = 20;
    public float des;
    public Vector3 targetPoint;

    void Start()
    {

    }

    void Update()
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
