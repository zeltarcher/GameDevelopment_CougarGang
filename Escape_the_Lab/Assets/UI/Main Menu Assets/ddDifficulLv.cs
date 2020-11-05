using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ddDifficulLv : MonoBehaviour
{
    public Text ddDifficult;
    public Text txtDiff;

    // Start is called before the first frame update
    void Start()
    {
        //txtDiff.text = ExplainBaseOption(ddDifficult.text);
        ExplainBaseOption();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ExplainBaseOption()
    {
        switch (ddDifficult.text)
        {
            case "TUTORIAL":
                txtDiff.text = "Play Tutorial";
               break;
            case "EASY":
                txtDiff.text = "Play with Easy level";
                break;
            case "NORMAL":
                txtDiff.text = "Play with Normal level";
                break;
            case "HARD":
                txtDiff.text = "Play with Hard level";
                break;
            /*
            default:
                break;
            */
        }
    }
}
