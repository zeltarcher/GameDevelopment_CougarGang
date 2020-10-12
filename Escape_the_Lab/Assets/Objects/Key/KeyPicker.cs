using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class KeyPicker : MonoBehaviour
{
    private float coin = 0;
    private float key = 0;
    private float drug = 0;

    public TextMeshProUGUI textKey;
    public TextMeshProUGUI textDrug;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.transform.tag == "Keys")
        {
            Destroy(other.gameObject);
            key++;
            textKey.text = key.ToString();
        }

        else if(other.transform.tag == "Drugs")
        {
            Destroy(other.gameObject);
            drug++;
            textDrug.text = drug.ToString();
        }
    }
}
