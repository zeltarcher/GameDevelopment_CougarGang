using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerCamera : MonoBehaviour
{

    CinemachineVirtualCamera obj;
    //Player player;
    public GameObject player1;
    public GameObject player2;
    Player player;



    //public GameObject chk1;
    //public GameObject chk2;

    //public bool a1;
    //public bool a2;
    void Start()
    {
        //a1 = true;
        //a2 = false;

        obj = GetComponent<CinemachineVirtualCamera>();

        player = FindObjectOfType<Player>();

        //obj.Follow = player1.transform;
        obj.Follow = player.GetComponent<Transform>();
    }

    private void Update()
    {
        /*
        if (Input.GetKeyDown(KeyCode.H))
        {
            print("Pressed");
            if(ck1 == true && ck2 == false)
            {
                print("ck1 became flase ck2 became true");
                ck1 = false;
                ck2 = true;
            }
            else if(ck1 == false && ck2 == true)
            {
                print("ck1 became true, ck2 became false");
                ck1 = true;
                ck2 = false;
            }
        }
        

        if (Input.GetKeyDown(KeyCode.J))
        {
            if (a1 == true)
            {
                a1 = false;
                a2 = true;

                obj.Follow = chk1.transform;
            }
            else if (a2 == true)
            {
                a1 = true;
                a2 = false;

                obj.Follow = chk2.transform;
            }
        }
        */
        
        /*
        if (FindObjectOfType<charChange>().p1 == true)
        {
            obj.Follow = player1.transform;
        }

        else if (FindObjectOfType<charChange>().p2 == true)
        {            
            obj.Follow = player2.transform;
        }*/
        
    }
}
