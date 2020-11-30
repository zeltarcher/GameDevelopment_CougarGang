using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectTrigger : MonoBehaviour
{
    public bool isLevel1;
    public bool isLevel2;

    private GameObject shark;
    private GameObject fallObj;
    private GameObject water1;
    private GameObject water2;

    // Start is called before the first frame update
    void Awake()
    {
        isLevel1 = true;
        shark = GameObject.Find("sharkSpawner");
        fallObj = GameObject.Find("ObjectSpawner");
        water1 = GameObject.Find("Rising Water");
        water2 = GameObject.Find("Rising Water 2");
    }

    private void Start()
    {
        water2.SetActive(false);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(this.gameObject.name == "FallObjOff" && other.gameObject.CompareTag("Player"))
        {
            isLevel1 = false;
            shark.SetActive(false);
            fallObj.SetActive(false);
            water1.SetActive(false);
            water2.SetActive(false);
        }

        if (this.gameObject.name == "FallObjOn" && other.gameObject.CompareTag("Player"))
        {

            shark.SetActive(true);
            fallObj.SetActive(true);
            water2.SetActive(true);
        }  
    }
}
