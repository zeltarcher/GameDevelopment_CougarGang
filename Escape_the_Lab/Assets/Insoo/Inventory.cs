using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Inventory : MonoBehaviour
{
    float coin = 0;
    float bomb = 0;
    float immute = 0;
    float hp = 0;
    float key = 0;
    float drug = 0;
    float superPotion = 0;

    TextMeshProUGUI textCoin;
    TextMeshProUGUI textBomb;
    TextMeshProUGUI textImmute;
    TextMeshProUGUI textHP;
    TextMeshProUGUI textDrug;
    TextMeshProUGUI textKey;
    TextMeshProUGUI textSuper;

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
    }

    // Update is called once per frame
    void Update()
    {

    }
}