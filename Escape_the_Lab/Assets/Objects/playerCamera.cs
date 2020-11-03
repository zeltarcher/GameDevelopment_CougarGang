using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerCamera : MonoBehaviour
{
    CinemachineVirtualCamera obj;
    Player player;
    public GameObject player1;
    public GameObject player2;

    void Start()
    {
        
        obj = GetComponent<CinemachineVirtualCamera>();
        
        player = FindObjectOfType<Player>();
        

        obj.Follow = player.GetComponent<Transform>();
    }

    private void Update()
    {
        if(FindObjectOfType<charChange>().p1 == true)
        {
            obj.Follow = player1.transform;
        }

        else if(FindObjectOfType<charChange>().p2 == true)
        {
            obj.Follow = player2.transform;
        }
    }
}
