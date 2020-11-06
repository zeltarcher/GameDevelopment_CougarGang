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

    public TextMeshProUGUI textCoin;
    public TextMeshProUGUI textBomb;
    public TextMeshProUGUI textImmute;
    public TextMeshProUGUI textHP;
    public TextMeshProUGUI textDrug;
    public TextMeshProUGUI textKey;
    public TextMeshProUGUI textSuper;

    // Start is called before the first frame update
    void Start()
    {
        /*
        textCoin.text = coin.ToString();
        textBomb.text = bomb.ToString();
        textHP.text = hp.ToString();
        textDrug.text = drug.ToString();
        textSuper.text = superPotion.ToString();
        */
    }

    // Update is called once per frame
    void Update()
    {

    }
}