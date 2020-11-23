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
    public GameObject dontTag;

    public GameObject buyTag;


    public Collider2D oneone;
    public Collider2D bombShop;
    public Collider2D superShop;
    public Collider2D HPShop;
    public Collider2D ammoShop;
    public Collider2D shopWay;


    private void shopEnter()
    {
        if (FindObjectOfType<charChange>().p1 == true)
        {
            oneone = GameObject.Find("Man").GetComponent<Collider2D>();
        }
        else if (FindObjectOfType<charChange>().p2 == true)
        {
            oneone = GameObject.Find("Robot").GetComponent<Collider2D>();
        }
        
        if (oneone.IsTouching(bombShop) && oneone.IsTouching(shopWay))
        {
            if (Input.GetKeyDown(KeyCode.M))
            {
                if (FindObjectOfType<Inventory>().coin >= 4)
                {
                    FindObjectOfType<Inventory>().coin = FindObjectOfType<Inventory>().coin - 4;
                    FindObjectOfType<Inventory>().bomb++;
                }
                else
                {
                    dontTag.SetActive(true);

                }
            }
            greetings.SetActive(false);
            buyTag.SetActive(true);
            bombTag.SetActive(true);
            superTag.SetActive(false);
            hpTag.SetActive(false);
            ammoTag.SetActive(false);
        }
        else if (oneone.IsTouching(superShop) && oneone.IsTouching(shopWay))
        {
            if (Input.GetKeyDown(KeyCode.M))
            {
                if (FindObjectOfType<Inventory>().coin >= 3)
                {
                    FindObjectOfType<Inventory>().coin = FindObjectOfType<Inventory>().coin - 3;
                    FindObjectOfType<Inventory>().superPotion++;
                }
                else
                {
                    dontTag.SetActive(true);
                }
            }
            greetings.SetActive(false);
            buyTag.SetActive(true);
            bombTag.SetActive(false);
            superTag.SetActive(true);
            hpTag.SetActive(false);
            ammoTag.SetActive(false);
        }
        else if (oneone.IsTouching(HPShop) && oneone.IsTouching(shopWay))
        {
            if (Input.GetKeyDown(KeyCode.M))
            {
                if (FindObjectOfType<Inventory>().coin >= 2)
                {
                    FindObjectOfType<Inventory>().coin = FindObjectOfType<Inventory>().coin - 2;
                    FindObjectOfType<Inventory>().hp++;
                }
                else
                {
                    dontTag.SetActive(true);
                }
            }
            greetings.SetActive(false);
            buyTag.SetActive(true);
            bombTag.SetActive(false);
            superTag.SetActive(false);
            hpTag.SetActive(true);
            ammoTag.SetActive(false);
        }
        else if (oneone.IsTouching(ammoShop) && oneone.IsTouching(shopWay))
        {
            if (Input.GetKeyDown(KeyCode.M))
            {
                if (FindObjectOfType<Inventory>().coin >= 1)
                {
                    FindObjectOfType<Inventory>().coin--;
                    FindObjectOfType<Inventory>().ammo = FindObjectOfType<Inventory>().ammo + 20;
                }
                else
                {
                    dontTag.SetActive(true);
                }
            }
            else
            {
                print("dont have enough money");
            }
            greetings.SetActive(false);
            buyTag.SetActive(true);
            bombTag.SetActive(false);
            superTag.SetActive(false);
            hpTag.SetActive(false);
            ammoTag.SetActive(true);
        }
        else if (oneone.IsTouching(shopWay))
        {
            if (Input.GetKeyDown(KeyCode.M))
            {
                print("IN SHOP!!!!!");
            }
            greetings.SetActive(true);
            buyTag.SetActive(false);
            bombTag.SetActive(false);
            superTag.SetActive(false);
            hpTag.SetActive(false);
            ammoTag.SetActive(false);
            dontTag.SetActive(false);
        }

    }


    // Start is called before the first frame update
    void Start()
    {
        bombShop = GameObject.Find("Bomb Trade").GetComponent<Collider2D>();
        superShop = GameObject.Find("Super Trade").GetComponent<Collider2D>();
        HPShop = GameObject.Find("HP Trade").GetComponent<Collider2D>();
        ammoShop = GameObject.Find("Ammo Trade").GetComponent<Collider2D>();
        shopWay = GameObject.Find("Shop Corridor").GetComponent<Collider2D>();


        buyTag = GameObject.Find("MtoBuy");
        greetings = GameObject.Find("greetings");
        bombTag = GameObject.Find("4coin");
        superTag = GameObject.Find("3coin");
        hpTag = GameObject.Find("2coin");
        ammoTag = GameObject.Find("1coin");
        dontTag = GameObject.Find("you dont have");

        greetings.SetActive(true);
        buyTag.SetActive(false);
        bombTag.SetActive(false);
        superTag.SetActive(false);
        hpTag.SetActive(false);
        ammoTag.SetActive(false);
        dontTag.SetActive(false);

    }
    

    // Update is called once per frame
    void Update()
    {
        shopEnter();
    }
}
