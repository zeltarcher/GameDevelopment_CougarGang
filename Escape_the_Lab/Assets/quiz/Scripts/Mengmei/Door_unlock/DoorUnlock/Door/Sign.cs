using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sign : MonoBehaviour
{
    public GameObject dialogBox;
    //public Text dialogBoxText;
    private bool playerIsEntered;

    //Quiz
    private Quiz quiz;
    private QuizList quizList;
    private int num;
    private GameEvents gaEn;
    private GameManager gaMa;
    private string touchName;
    
    // Start is called before the first frame update
    void Start()
    {
        playerIsEntered = false;
        dialogBox.SetActive(false);
        quiz = FindObjectOfType<Quiz>();
        quizList = FindObjectOfType<QuizList>();
        gaEn = FindObjectOfType<GameEvents>();
        gaMa = FindObjectOfType<GameManager>();
        //GetNum();
        touchingName();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.E) && playerIsEntered)
        {
            //Quiz question and options
            //string Prob = quiz.FetchQuestion(num).question + "\n"
            //           + quiz.FetchQuestion(num).options;
            //dialogBoxText.text = Prob;
            //touchingName();
            gaMa.UpdateCheckSignName(touchingName());
            Debug.Log("Touch name after pressing E is: " + touchingName());
            dialogBox.SetActive(true);
            gaMa.Display();
        }
        else {
            touchName = "";
        }
    }
    public string getTouchName() {
        return touchName;
    }
    private string touchingName() {
        //string touchNameInside = "";
        if (gameObject.name == "Sign1")
        {
            //touchNameInside = "Sign1";
            touchName = "Sign1";
            //Debug.Log("Touch: Sign1 and " + "Touch name is: " + touchName);
        }
        else if (gameObject.name == "Sign2")
        {
            //touchNameInside = "Sign2";
            touchName = "Sign2";
            //Debug.Log("Touch: Sign2 and " + "Touch name is: " + touchName);
        }
        else if (gameObject.name == "Sign3")
        {
            //touchNameInside = "Sign3";
            touchName = "Sign3";
            //Debug.Log("Touch: Sign3 and " + "Touch name is: " + touchName);
        }
        else if (gameObject.name == "Sign4")
        {
            //touchNameInside = "Sign4";
            touchName = "Sign4";
            //Debug.Log("Touch: Sign4 and " + "Touch name is: " + touchName);
        }
 
        //touchName = touchNameInside;
        return touchName;
    }
    private int GetNum()
    {
        if (gameObject.name == "Sign1")
            num = quizList.list_of_quizNO[0];
        else if (gameObject.name == "Sign2")
            num = quizList.list_of_quizNO[1];
        else if (gameObject.name == "Sign3")
            num = quizList.list_of_quizNO[2];
        else if(gameObject.name == "Sign4")
            num = quizList.list_of_quizNO[3];
        return num;
    }
    //void Display() { 
        //gaEn.UpdateQuestionUI
    //}

    void OnTriggerEnter2D(Collider2D other)
    {
       /* if (other.tag == "Player")
        {
            playerIsEntered = true;
            Debug.Log("Player is entered is " + playerIsEntered);
        }*/
       
        //playerIsEntered = true;
        Debug.Log("Name in is: " + other.tag);
        Debug.Log("Player is entered is " + playerIsEntered);
       
    }

    void OnTriggerExit2D(Collider2D other)
    {
        /*if (other.tag == "Player")
        {
            playerIsEntered = false;
            dialogBox.SetActive(false);
            Debug.Log("Player is entered is " + playerIsEntered);
        }*/

        
         //playerIsEntered = false;
         //dialogBox.SetActive(false);
         Debug.Log("Name out is: " + other.tag);
         Debug.Log("Player is entered is " + playerIsEntered);
        
    }
    public void PlayerEntered() {
        playerIsEntered = true;
        //Debug.Log("Name in is: " + other.tag);
       Debug.Log("Player is entered is " + playerIsEntered);
    }
    public void PlayerNotEntered()
    {
       playerIsEntered = false;
       dialogBox.SetActive(false);
        //Debug.Log("Name in is: " + other.tag);
       Debug.Log("Player is entered is " + playerIsEntered);
    }
}
