using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class KeyPicker : MonoBehaviour
{
    private float coin = 0;
    private float bomb = 0;
    private float immute = 0;
    private float hp = 0;
    private float key = 0;
    private float drug = 0;

    public Text txtCoin;
    public Text txtBomb;
    public Text txtImmute;
    public Text txtHP;
    public Text txtDrug;
    public Text txtKey;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.transform.tag == "Coins")
        {
            Destroy(other.gameObject);
            coin++;
            Debug.Log(coin);
            txtCoin.text = coin.ToString();
        }

        else if(other.transform.tag == "Bombs")
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
            Debug.Log(drug);
            txtDrug.text = drug.ToString();
        }
        else if (other.transform.tag == "Keys")
        {
            Destroy(other.gameObject);
            key++;
            Debug.Log(drug.ToString());
            txtKey.text = key.ToString();
        }
    }
}
