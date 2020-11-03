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
    private float bomb = 0;
    private float immute = 0;
    private float hp = 0;
    private float key = 0;
    private float drug = 0;
    private float superPotion = 0;

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

    void Update()
    {

        khaBomb();
        
    }

    private void OnTriggerEnter2D(Collider2D other)
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
                gameObject.GetComponent<water>().testNumber = gameObject.GetComponent<water>().testNumber * 100;
                Debug.Log(gameObject.GetComponent<water>().testNumber);
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
