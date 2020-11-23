using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedGun : MonoBehaviour
{
    public GameObject[] gos1 = new GameObject[2];
    //public GameObject targetOn;
    public float speed1 = 1;
    int i = 0;
    float des;
    void Start()
    {

    }

    void Update()
    {

        des = Vector3.Distance(this.transform.position, gos1[i].transform.position);

        transform.position = Vector3.MoveTowards(this.transform.position, gos1[i].transform.position, Time.deltaTime * speed1);

        //vertical moving, need some adjustment on player

        //if ((this.transform.position.x - gos[i].transform.position.x) > 0.3f)
        //{

        //}

        //if ((gos[i].transform.position.x - this.transform.position.x) > 0.3f)
        //{

        //}

        if (des < 0.3f && i < gos1.Length)
        {
            i++;
        }

        if (i == gos1.Length)
        {

            i = 0;

        }

    }
}
