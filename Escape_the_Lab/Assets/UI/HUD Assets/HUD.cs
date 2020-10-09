using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUD : MonoBehaviour
{
    Camera mainCamera;
    Canvas hud;

    void Start()
    {
        mainCamera = FindObjectOfType<Camera>();
        hud = GetComponent<Canvas>();
        hud.worldCamera = mainCamera;
    }
}
