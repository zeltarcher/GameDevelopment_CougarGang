using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Cinemachine;


public class KeyPicker : MonoBehaviour
{
    public Camera camera;
    public CinemachineVirtualCamera CVC;
    public CinemachineBrain CMB;

    // from key picker
    //=============================

    public bool check = true;
    private float coin = 0;
    public float bomb = 0;
    private float immute = 0;
    private float hp = 0;
    private float key = 0;
    private float drug = 0;
    public float superPotion = 0;

    public Player player;
    /*
    public TextMeshProUGUI txtCoin;
    public TextMeshProUGUI txtBomb;
    public TextMeshProUGUI txtImmute;
    public TextMeshProUGUI txtHP;
    public TextMeshProUGUI txtDrug;
    public TextMeshProUGUI txtKey;
    public TextMeshProUGUI txtSuper;
    public GameObject playerDoc;
    public GameObject samplePlayerDoc;
    */

    //==============================

    AudioSource item_audiosrc;
    AudioClip ac_coin, ac_bomb, ac_immute, ac_hp, ac_key, ac_drug, ac_super,ac_bomb_explore;

    public float getKeys()
    {
        return key;
    }
    private void Start()
    {

        camera = Camera.main;
        CMB = camera.GetComponent<CinemachineBrain>();
        CVC = CMB.ActiveVirtualCamera as CinemachineVirtualCamera;
        


        player = GameObject.Find("Man").GetComponent<Player>();
        item_audiosrc = GetComponent<AudioSource>();
        ac_coin = Resources.Load<AudioClip>("Pickup_Coin");
        ac_bomb = Resources.Load<AudioClip>("Pickup_Item");
        ac_immute = Resources.Load<AudioClip>("Pickup_Item1");
        ac_hp = Resources.Load<AudioClip>("Pickup_Item1");
        ac_key = Resources.Load<AudioClip>("Pickup_Item2");
        ac_drug = Resources.Load<AudioClip>("Pickup_Item2");
        ac_super = Resources.Load<AudioClip>("Pickup_ItemSpecial");
        ac_bomb_explore = Resources.Load<AudioClip>("bomb_explosion");
    }
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.M))
        {
            if (check == true)
            {
                CVC.m_Lens.OrthographicSize = 20f;
                check = false;
            }
            else if (check == false)
            {
                CVC.m_Lens.OrthographicSize = 7f;
                check = true;
            }
        }

        khaBomb();
        healing();
        if (Input.GetKeyDown(KeyCode.V))
        {
            print("superPotion: " + superPotion);
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            print("ok");

            if (FindObjectOfType<Inventory>().superPotion > 0)
            {
                print("then?");
                if (FindObjectOfType<charChange>().p1 == true && FindObjectOfType<charChange>().p2 == false)
                {
                    FindObjectOfType<Inventory>().superPotion--;
                    FindObjectOfType<Inventory>().textSuper.text = FindObjectOfType<Inventory>().superPotion.ToString();
                    FindObjectOfType<charChange>().transformSuper();
                }

            }
        }




    }



    private void OnTriggerEnter2D(Collider2D other)
    {
        
               

        // from key picker
        //======================================================
        if (other.transform.tag == "Coins")
        {
            item_audiosrc.PlayOneShot(ac_coin);
            Destroy(other.gameObject);
            FindObjectOfType<Inventory>().coin++;

            FindObjectOfType<Inventory>().textCoin.text = FindObjectOfType<Inventory>().coin.ToString();

        }

        else if(other.transform.tag == "Ammo")
        {
            Destroy(other.gameObject);
            FindObjectOfType<Inventory>().ammo = FindObjectOfType<Inventory>().ammo + 20;
            FindObjectOfType<Inventory>().textAmmo.text = FindObjectOfType<Inventory>().ammo.ToString();

        }

        else if(other.transform.tag == "Gun")
        {
            Destroy(other.gameObject);
            FindObjectOfType<Player>().hasGun = true;
        }

        else if (other.transform.tag == "superPotion")
        {
            item_audiosrc.PlayOneShot(ac_super);
            Destroy(other.gameObject);
            FindObjectOfType<Inventory>().superPotion++;

            FindObjectOfType<Inventory>().textSuper.text = FindObjectOfType<Inventory>().superPotion.ToString();


        }

        else if (other.transform.tag == "Bombs")
        {
            item_audiosrc.PlayOneShot(ac_bomb);
            Destroy(other.gameObject);
            FindObjectOfType<Inventory>().bomb++;

            FindObjectOfType<Inventory>().textBomb.text = FindObjectOfType<Inventory>().bomb.ToString();
        }

        else if (other.transform.tag == "Immutes")
        {
            item_audiosrc.PlayOneShot(ac_immute);
            Destroy(other.gameObject);
            FindObjectOfType<Inventory>().immute++;
            FindObjectOfType<Inventory>().textImmute.text = FindObjectOfType<Inventory>().immute.ToString();

        }
        else if (other.transform.tag == "HPs")
        {
            item_audiosrc.PlayOneShot(ac_hp);
            Destroy(other.gameObject);
            FindObjectOfType<Inventory>().hp++;

            FindObjectOfType<Inventory>().textHP.text = FindObjectOfType<Inventory>().hp.ToString();

        }
        else if (other.transform.tag == "Drugs")
        {
            item_audiosrc.PlayOneShot(ac_drug);
            Destroy(other.gameObject);
            FindObjectOfType<Inventory>().drug++;

            FindObjectOfType<Inventory>().textDrug.text = FindObjectOfType<Inventory>().drug.ToString();
        }
        else if (other.transform.tag == "Keys")
        {
            item_audiosrc.PlayOneShot(ac_key);
            Destroy(other.gameObject);
            FindObjectOfType<Inventory>().key++;

            FindObjectOfType<Inventory>().textKey.text = FindObjectOfType<Inventory>().key.ToString();
        }
        //else if (other.transform.tag == "Sign")
        //{
            //FindObjectOfType<Sign>().PlayerNotEntered();
        //}
        //======================================================
        else if(other.transform.tag == "BombTrade" && other.transform.tag == "ShopCorridor")
        {
            FindObjectOfType<Shop>().greetings.SetActive(false);
            FindObjectOfType<Shop>().bombTag.SetActive(true);
            FindObjectOfType<Shop>().superTag.SetActive(false);
            FindObjectOfType<Shop>().hpTag.SetActive(false);
            FindObjectOfType<Shop>().ammoTag.SetActive(false);
            FindObjectOfType<Shop>().shopTag.SetActive(true);
        }

        else if (other.transform.tag == "SuperTrade" && other.transform.tag == "ShopCorridor")
        {
            FindObjectOfType<Shop>().greetings.SetActive(false);
            FindObjectOfType<Shop>().bombTag.SetActive(false);
            FindObjectOfType<Shop>().superTag.SetActive(true);
            FindObjectOfType<Shop>().hpTag.SetActive(false);
            FindObjectOfType<Shop>().ammoTag.SetActive(false);
            FindObjectOfType<Shop>().shopTag.SetActive(true);
        }

        else if (other.transform.tag == "HPTrade" && other.transform.tag == "ShopCorridor")
        {
            FindObjectOfType<Shop>().greetings.SetActive(false);
            FindObjectOfType<Shop>().bombTag.SetActive(false);
            FindObjectOfType<Shop>().superTag.SetActive(false);
            FindObjectOfType<Shop>().hpTag.SetActive(true);
            FindObjectOfType<Shop>().ammoTag.SetActive(false);
            FindObjectOfType<Shop>().shopTag.SetActive(true);
        }

        else if (other.transform.tag == "AmmoTrade" && other.transform.tag == "ShopCorridor")
        {
            FindObjectOfType<Shop>().greetings.SetActive(false);
            FindObjectOfType<Shop>().bombTag.SetActive(false);
            FindObjectOfType<Shop>().superTag.SetActive(false);
            FindObjectOfType<Shop>().hpTag.SetActive(false);
            FindObjectOfType<Shop>().ammoTag.SetActive(true);
            FindObjectOfType<Shop>().shopTag.SetActive(true);
        }

        else if(other.transform.tag == "ShopCorridor")
        {
            FindObjectOfType<Shop>().greetings.SetActive(true);
            FindObjectOfType<Shop>().bombTag.SetActive(false);
            FindObjectOfType<Shop>().superTag.SetActive(false);
            FindObjectOfType<Shop>().hpTag.SetActive(false);
            FindObjectOfType<Shop>().ammoTag.SetActive(false);
            FindObjectOfType<Shop>().shopTag.SetActive(false);
        }

    }



    public void khaBomb()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            print(FindObjectOfType<Inventory>().bomb);   
            if (FindObjectOfType<Inventory>().bomb > 0)
            {
                item_audiosrc.PlayOneShot(ac_bomb_explore);
                FindObjectOfType<Inventory>().bomb--;
                FindObjectOfType<Inventory>().textBomb.text = FindObjectOfType<Inventory>().bomb.ToString();

                FindObjectOfType<water>().waterSpeed = 0.001f;
                FindObjectOfType<DoorUnlock>().khaBomb = true;
                //gameObject.GetComponent<water>().waterSpeed = gameObject.GetComponent<water>().waterSpeed * 10;
                //Debug.Log(gameObject.GetComponent<water>().waterSpeed);

            }
        }
    }

    public void healing()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            
            if (FindObjectOfType<charChange>().p1 == true && FindObjectOfType<Inventory>().hp > 0 && FindObjectOfType<Player>().currentHealth < 100)
            {
                FindObjectOfType<Inventory>().hp--;
                FindObjectOfType<Inventory>().textHP.text = FindObjectOfType<Inventory>().hp.ToString();

                FindObjectOfType<Player>().currentHealth = 100;
            }
            
        }
    }



}

/*
private void Start()
    {
        txtCoin = GameObject.Find("Coin Count Text").GetComponent<TextMeshProUGUI>();
        txtBomb = GameObject.Find("Bomb Count Text").GetComponent<TextMeshProUGUI>();
        txtImmute = GameObject.Find("Super Count Text").GetComponent<TextMeshProUGUI>();
        txtHP = GameObject.Find("HP Potion Count Text").GetComponent<TextMeshProUGUI>();
        txtDrug = GameObject.Find("Drug Count Text").GetComponent<TextMeshProUGUI>();
        txtKey = GameObject.Find("Key Count Text").GetComponent<TextMeshProUGUI>();
        //txtSuper = GameObject.Find("Coin Count Text").GetComponent<TextMeshProUGUI>();
    }
}
*/
