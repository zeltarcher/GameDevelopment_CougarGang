using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using System;

public class CheckPasscode : MonoBehaviour
{
    public Text dialogBoxText;
    public Text showPassCode;
    public GameObject Inputs;
    public GameObject Buttons;
    private InputField[] digitInputs = new InputField[4];
    private Button clear;
    private int[] values;

    //Get from Quiz File
    //private List<int> answers = new List<int> { 0, 9, 4, 1 };
    //private List<int> answers = new List<int> { 0, 9, 4, 1 };
    private int[] answers = new int[4];
    private string[] answersString = new string[4];


    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < digitInputs.Length; i++)
        {
            digitInputs[i] = Inputs.transform.GetChild(i).GetComponent<InputField>();
        }
        clear = Buttons.transform.GetChild(0).GetComponent<Button>();
        answersString[0] = "?";
        answersString[1] = "?";
        answersString[2] = "?";
        answersString[3] = "?";
        //values = FindObjectOfType<UIManager>().sendNewPasscodeList();
        //answers = values;



        //Array.Sort(answers);


    }

    // Update is called once per frame
    void Update()
    {
        DialogText();
        clear.onClick.AddListener(ClearButton);
        //answers = values;
        //answers = sortArray(answers);


        //Array.Sort(answers);



        //foreach (int i in answers)
        //{
        //Debug.Log(i);
        //}

    }

    private bool AllQuesAnswered()
    {
        foreach (int a in answers)
        {
            //Debug.Log(a);
            if (a > 9) return false;
        }
        return true;
    }

    private void DialogText()
    {
        if (!AllQuesAnswered())
        {
            dialogBoxText.text = "You Haven't Collected All 4 Passcode!";
            showPassCode.text = "";
            Inputs.SetActive(false);
            Buttons.SetActive(false);
        }
        else
        {
            dialogBoxText.text = "Please Enter The Password in Ascending Order:";
            printPasscodeString();
            Inputs.SetActive(true);
            Buttons.SetActive(true);
            
        }
    }

    private void ClearButton()
    {
        foreach (InputField n in digitInputs) n.text = "";
    }

    public bool CodePassed()
    {
        for (int i = 0; i < digitInputs.Length; i++)
        {
            if ((digitInputs[i].text) != answers[i].ToString())
                return false;
        }
        return true;
    }

    public void receiveNewPasscodeList(int int1, int int2, int int3, int int4) {
        //int[] inside = intArray;
        //values = inside;
        answers[0] = int1;
        answers[1] = int2;
        answers[2] = int3;
        answers[3] = int4;
        answers = sortArray(answers);
        //return answers;
    }
    private int[] sortArray(int[] inputArray)
    {
        int n = inputArray.Length;
        for (int i = 0; i < n; i++)
        {
            for (int j = i + 1; j < n; j++)
            {
                if (inputArray[j] < inputArray[i])
                {
                    int temp = inputArray[i];
                    inputArray[i] = inputArray[j];
                    inputArray[j] = temp;
                }
            }
        }
        foreach (int i in inputArray)
        {
            //Debug.Log("Answers is: " + i);
        }
        return inputArray;
    }
    public void receivePasscodeString(string string1, string string2, string string3, string string4) {
        answersString[0] = string1;
        answersString[1] = string2;
        answersString[2] = string3;
        answersString[3] = string4;
    }
    private void printPasscodeString() {
        /*showPassCode.text = "Hint: " + "\n" + "First Passcode: " + answersString[0] + "\n"
            + "Second Passcode: " +  answersString[1] + "\n"
            + "Third Passcode: " +  answersString[2] + "\n"
            + "Fourth Passcode: " +  answersString[3] + "\n";*/

        showPassCode.text = "Hint: " + "\n" + answersString[0] + "\t"
            + answersString[1] + "\t"
            + answersString[2] + "\t"
            + answersString[3];
    }

}
