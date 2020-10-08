using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPicker : MonoBehaviour
{
    private float coin = 0;
    private float key = 0;
    private float drug = 0;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.transform.tag == "Keys")
        {
            Destroy(other.gameObject);
        }

        else if(other.transform.tag == "Drugs")
        {
            Destroy(other.gameObject);
        }
    }
}
