using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class KeyPicker : MonoBehaviour
{

    // from key picker
    //=============================

    private float coin = 0;
    public float bomb = 0;
    private float immute = 0;
    private float hp = 0;
    private float key = 0;
    private float drug = 0;
    private float superPotion = 0;

<<<<<<< HEAD
    public TextMeshProUGUI txtCoin;
    public TextMeshProUGUI txtBomb;
    public TextMeshProUGUI txtImmute;
    public TextMeshProUGUI txtHP;
    public TextMeshProUGUI txtDrug;
    public TextMeshProUGUI txtKey;
    public TextMeshProUGUI txtSuper;
    public GameObject playerDoc;
    public GameObject samplePlayerDoc;
    public GameObject waterManager;
    //==============================
    

=======
    TextMeshProUGUI txtCoin;
    TextMeshProUGUI txtBomb;
    TextMeshProUGUI txtImmute;
    TextMeshProUGUI txtHP;
    TextMeshProUGUI txtDrug;
    TextMeshProUGUI txtKey;
    TextMeshProUGUI txtSuper;
    //==============================

    public float getKeys()
    {
        return key;
    }
>>>>>>> 4889e8bde82bcef306116f1ba28d527cc6727ecb

    void Update()
    {

        khaBomb();
        makeChange();
        if (Input.GetKeyDown(KeyCode.C))
        {

            print("waterSpeed: " + gameObject.GetComponent<water>().waterSpeed);
            print("testNumber: " + gameObject.GetComponent<water>().testNumber);
        }
        
        
        
    }

<<<<<<< HEAD
    private void makeChange()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            if (superPotion > 0)
            {
                superPotion--;
                txtSuper.text = superPotion.ToString();

            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
=======
    private void OnTriggerEnter2D(Collider2D other)
>>>>>>> 4889e8bde82bcef306116f1ba28d527cc6727ecb
    {

        // from key picker
        //======================================================
        if (other.transform.tag == "Coins")
        {
            Destroy(other.gameObject);
            coin++;
            Debug.Log(coin);
            txtCoin.text = coin.ToString();
        }

        else if (other.transform.tag == "superPotion")
        {
            Destroy(other.gameObject);
            superPotion++;
            Debug.Log("superpotion counts: " + superPotion);
            txtSuper.text = superPotion.ToString();

        }

        else if (other.transform.tag == "Bombs")
        {
            Destroy(other.gameObject);
            bomb++;
            txtBomb.text = bomb.ToString();
        }
        else if (other.transform.tag == "Immutes")
        {
            Destroy(other.gameObject);
            immute++;
            txtImmute.text = immute.ToString();
        }
        else if (other.transform.tag == "HPs")
        {
            Destroy(other.gameObject);
            hp++;
            txtHP.text = hp.ToString();
        }
        else if (other.transform.tag == "Drugs")
        {
            Destroy(other.gameObject);
            drug++;
            Debug.Log("Drug counts: " + drug);
            txtDrug.text = drug.ToString();
        }
        else if (other.transform.tag == "Keys")
        {
            Destroy(other.gameObject);
            key++;
            Debug.Log("Key counts: " + key);
            txtKey.text = key.ToString();
        }
        //======================================================

    }


    
    public void khaBomb()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (bomb > 0)
            {
                bomb--;
                txtBomb.text = bomb.ToString();
                FindObjectOfType<water>().waterSpeed = 0.01f;
                //gameObject.GetComponent<water>().waterSpeed = gameObject.GetComponent<water>().waterSpeed * 10;
                //Debug.Log(gameObject.GetComponent<water>().waterSpeed);
                
            }
        }
    }


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
