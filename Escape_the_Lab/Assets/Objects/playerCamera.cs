using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerCamera : MonoBehaviour
{
    CinemachineVirtualCamera obj;
    Player player;
    void Start()
    {
        obj = GetComponent<CinemachineVirtualCamera>();
        player = FindObjectOfType<Player>();
        obj.Follow = player.GetComponent<Transform>();
    }
}
