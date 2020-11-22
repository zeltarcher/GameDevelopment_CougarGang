using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Shop : MonoBehaviour
{
    public GameObject greetings;
    public GameObject bombTag;
    public GameObject superTag;
    public GameObject hpTag;
    public GameObject ammoTag;

    public GameObject shopTag;


    // Start is called before the first frame update
    void Start()
    {

        greetings = GameObject.Find("Greetings");
        bombTag = GameObject.Find("Bomb Price Tag");
        superTag = GameObject.Find("Super Price Tag");
        hpTag = GameObject.Find("HP Price Tag");
        ammoTag = GameObject.Find("Ammo Price Tag");
        shopTag = GameObject.Find("Shop Price Tag");

        greetings.SetActive(true);
        bombTag.SetActive(false);
        superTag.SetActive(false);
        hpTag.SetActive(false);
        ammoTag.SetActive(false);
        shopTag.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
