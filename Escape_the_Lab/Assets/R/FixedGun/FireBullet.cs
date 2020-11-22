using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBullet : MonoBehaviour
{
    //public GameObject OriginalPoint;
    //public GameObject targetPoint;
    //public GameObject targetOn;
    
    public float fireSpeed = 20;
    public float des;
    public Vector3 targetPoint;

    void Start()
    {
        //rb = GetComponent<Rigidbody>();
    }

    void Update()
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
