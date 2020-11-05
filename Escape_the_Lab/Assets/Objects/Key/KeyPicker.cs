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
    public float superPotion = 0;

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



    void Update()
    {

        khaBomb();
        healing();
        if (Input.GetKeyDown(KeyCode.V))
        {
            print("superPotion: " + superPotion);
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            print("ok");
            
            if(FindObjectOfType<Inventory>().superPotion > 0)
            {
                print("then?");
                if(FindObjectOfType<charChange>().p1 == true && FindObjectOfType<charChange>().p2 == false)
                {
                    FindObjectOfType<Inventory>().superPotion--;
                    FindObjectOfType<Inventory>().textSuper.text = FindObjectOfType<Inventory>().superPotion.ToString();
                    FindObjectOfType<charChange>().transformSuper();
                }
                
            }
        }
        
        
        
        
    }

    

    private void OnTriggerExit2D(Collider2D other)
    {

        // from key picker
        //======================================================
        if (other.transform.tag == "Coins")
        {
            Destroy(other.gameObject);
            FindObjectOfType<Inventory>().coin++;
            
            FindObjectOfType<Inventory>().textCoin.text = FindObjectOfType<Inventory>().coin.ToString();
            
        }

        else if (other.transform.tag == "superPotion")
        {
            Destroy(other.gameObject);
            FindObjectOfType<Inventory>().superPotion++;
         
            FindObjectOfType<Inventory>().textSuper.text = FindObjectOfType<Inventory>().superPotion.ToString();


        }

        else if (other.transform.tag == "Bombs")
        {
            Destroy(other.gameObject);
            FindObjectOfType<Inventory>().bomb++;

            FindObjectOfType<Inventory>().textBomb.text = FindObjectOfType<Inventory>().bomb.ToString();
        }

        else if (other.transform.tag == "Immutes")
        {
            Destroy(other.gameObject);
            FindObjectOfType<Inventory>().immute++;
            FindObjectOfType<Inventory>().textImmute.text = FindObjectOfType<Inventory>().immute.ToString();

        }
        else if (other.transform.tag == "HPs")
        {
            Destroy(other.gameObject);
            FindObjectOfType<Inventory>().hp++;

            FindObjectOfType<Inventory>().textHP.text = FindObjectOfType<Inventory>().hp.ToString();

        }
        else if (other.transform.tag == "Drugs")
        {
            Destroy(other.gameObject);
            FindObjectOfType<Inventory>().drug++;

            FindObjectOfType<Inventory>().textDrug.text = FindObjectOfType<Inventory>().drug.ToString();
        }
        else if (other.transform.tag == "Keys")
        {
            Destroy(other.gameObject);
            FindObjectOfType<Inventory>().key++;

            FindObjectOfType<Inventory>().textKey.text = FindObjectOfType<Inventory>().key.ToString();
        }
        //======================================================

    }


    
    public void khaBomb()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (bomb > 0)
            {
                FindObjectOfType<Inventory>().bomb--;
                FindObjectOfType<Inventory>().textBomb.text = FindObjectOfType<Inventory>().bomb.ToString();

                FindObjectOfType<water>().waterSpeed = 0.01f;
                //gameObject.GetComponent<water>().waterSpeed = gameObject.GetComponent<water>().waterSpeed * 10;
                //Debug.Log(gameObject.GetComponent<water>().waterSpeed);
                
            }
        }
    }

    public void healing()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (FindObjectOfType<charChange>().p1 == true)
            {
                FindObjectOfType<Inventory>().hp--;
                FindObjectOfType<Inventory>().textHP.text = FindObjectOfType<Inventory>().hp.ToString();

                FindObjectOfType<Player>().currentHealth = 100;
            }
        }
    }



}
