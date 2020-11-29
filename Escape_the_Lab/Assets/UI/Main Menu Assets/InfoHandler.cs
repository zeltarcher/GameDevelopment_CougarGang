using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoHandler : MonoBehaviour
{
    private Transform childObj;
    private GameObject MMCanvas;
    void Awake()
    {
        MMCanvas = GameObject.Find("Panel-center");
    }
    public void OpenInfo()
    {
        MMCanvas.SetActive(false);
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(true);
        }
    }

    public void CloseInfo()
    {
        MMCanvas.SetActive(true);
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
    }
}
