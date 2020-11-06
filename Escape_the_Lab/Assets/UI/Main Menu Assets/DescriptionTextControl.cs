using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DescriptionTextControl : MonoBehaviour
{
    public string input;
    public Text txtNote;//txtNoti in HUD or txtInfo iin MainMenu
    // Start is called before the first frame update
    void Start()
    {
        txtNote.text = "";
    }

    private void OnMouseOver()
    {
        //Debug.Log("Hover----------------");
        txtNote.text = input;
    }

    private void OnMouseExit()
    {
        //Debug.Log("Exit>>>>>>>>>>>>>>>>>");
        txtNote.text = "";
    }
}
