using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ddDifficulLv : MonoBehaviour
{
    public Text ddDifficult;
    public Text txtDiff;
    private int sceneID;
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
                sceneID = 3;
               break;
            case "EASY":
                txtDiff.text = "Play with Easy level";
                break;
            case "NORMAL":
                txtDiff.text = "Play with Normal level";
                sceneID = 2;
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
    public void StartGame()
    {
        SceneManager.LoadSceneAsync(sceneID);
    }
}
