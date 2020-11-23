using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathMoving : MonoBehaviour
{
    public GameObject[] gos = new GameObject[4];
    //public GameObject targetOn;
    public float speed = 1;
    int i = 0;
    float des;
    void Start()
    {

    }

    void Update()
    {

        des = Vector3.Distance(this.transform.position, gos[i].transform.position);

        transform.position = Vector3.MoveTowards(this.transform.position, gos[i].transform.position, Time.deltaTime * speed);

        //vertical moving, need some adjustment on player

        //if ((this.transform.position.x - gos[i].transform.position.x) > 0.3f)
        //{

        //}

        //if ((gos[i].transform.position.x - this.transform.position.x) > 0.3f)
        //{

        //}

        if (des < 0.3f && i < gos.Length)
        {
            i++;
        }

        if(i == gos.Length)
        {

            i = 0;

        }

    }

}
