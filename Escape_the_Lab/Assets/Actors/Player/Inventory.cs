using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Inventory : MonoBehaviour
{
    public float coin = 0;
    public float bomb = 0;
    public float immute = 0;
    public float hp = 0;
    public float key = 0;
    public float drug = 0;
    public float superPotion = 0;
    public float ammo = 0;

    public TextMeshProUGUI textCoin;
    public TextMeshProUGUI textBomb;
    public TextMeshProUGUI textImmute;
    public TextMeshProUGUI textHP;
    public TextMeshProUGUI textDrug;
    public TextMeshProUGUI textKey;
    public TextMeshProUGUI textSuper;
    public TextMeshProUGUI textAmmo;

    // Start is called before the first frame update
    void Start()
    {
        textCoin = GameObject.Find("Coin Count Text").GetComponent<TextMeshProUGUI>();
        textBomb = GameObject.Find("Bomb Count Text").GetComponent<TextMeshProUGUI>();
        //textImmute = GameObject.Find("Super Count Text").GetComponent<TextMeshProUGUI>();
        textHP = GameObject.Find("HP Potion Count Text").GetComponent<TextMeshProUGUI>();
        textDrug = GameObject.Find("Drug Count Text").GetComponent<TextMeshProUGUI>();
        textKey = GameObject.Find("Key Count Text").GetComponent<TextMeshProUGUI>();
        textSuper = GameObject.Find("Super Count Text").GetComponent<TextMeshProUGUI>();
        textAmmo = GameObject.Find("Ammo Count Text").GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        textCoin.text = coin.ToString();
        textBomb.text = bomb.ToString();
        textHP.text = hp.ToString();
        textSuper.text = superPotion.ToString();
        //textAmmo.text = ammo.ToString();

    }
}