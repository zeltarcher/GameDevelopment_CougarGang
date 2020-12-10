using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[Serializable()]
public struct UIManagerParameters
{
    [Header("Answers Options")]
    [SerializeField] float margins;
    public float Margins { get { return margins; } }

    [Header("Resolution Screen Options")]
    [SerializeField] Color correctBGColor;
    public Color CorrectBGColor { get { return correctBGColor; } }
    [SerializeField] Color incorrectBGColor;
    public Color IncorrectBGColor { get { return incorrectBGColor; } }
    [SerializeField] Color finalBGColor;
    public Color FinalBGColor { get { return finalBGColor; } }
}
[Serializable()]
public struct UIElements
{
    [SerializeField] RectTransform answersContentArea;
    public RectTransform AnswersContentArea { get { return answersContentArea; } }

    [SerializeField] TextMeshProUGUI questionInfoTextObject;
    public TextMeshProUGUI QuestionInfoTextObject { get { return questionInfoTextObject; } }

    [SerializeField] TextMeshProUGUI scoreText;
    public TextMeshProUGUI ScoreText { get { return scoreText; } }

    [Space]

    [SerializeField] Animator resolutionScreenAnimator;
    public Animator ResolutionScreenAnimator { get { return resolutionScreenAnimator; } }

    [SerializeField] Image resolutionBG;
    public Image ResolutionBG { get { return resolutionBG; } }

    [SerializeField] TextMeshProUGUI resolutionStateInfoText;
    public TextMeshProUGUI ResolutionStateInfoText { get { return resolutionStateInfoText; } }

    [SerializeField] TextMeshProUGUI resolutionScoreText;
    public TextMeshProUGUI ResolutionScoreText { get { return resolutionScoreText; } }

    [Space]

    [SerializeField] TextMeshProUGUI highScoreText;
    public TextMeshProUGUI HighScoreText { get { return highScoreText; } }

    [SerializeField] CanvasGroup mainCanvasGroup;
    public CanvasGroup MainCanvasGroup { get { return mainCanvasGroup; } }

    [SerializeField] RectTransform finishUIElements;
    public RectTransform FinishUIElements { get { return finishUIElements; } }
}
public class UIManager : MonoBehaviour {

    #region Variables

    public enum         ResolutionScreenType   { Correct, Incorrect, Finish }

    [Header("References")]
    [SerializeField]    GameEvents             events                       = null;

    [Header("UI Elements (Prefabs)")]
    [SerializeField]    AnswerData             answerPrefab                 = null;

    [SerializeField]    UIElements             uIElements                   = new UIElements();

    [Space]
    [SerializeField]    UIManagerParameters    parameters                   = new UIManagerParameters();

    private             List<AnswerData>       currentAnswers               = new List<AnswerData>();
    private             int                    resStateParaHash             = 0;

    private             IEnumerator            IE_DisplayTimedResolution    = null;
    //private List<int> newPasscodeList = new List<int>(4);
    private int[] newPasscodeList = new int[4];
    private string[] newPasscodeStringList = new string[4];

    public int[] printNewPasscodeList { get{ return newPasscodeList; } }
    private int checkRepeatedLocation = 5;

    #endregion

    #region Default Unity methods

    /// <summary>
    /// Function that is called when the object becomes enabled and active
    /// </summary>
    void OnEnable()
    {
        events.UpdateQuestionUI         += UpdateQuestionUI;
        events.DisplayResolutionScreen  += DisplayResolution;
        events.ScoreUpdated             += UpdateScoreUI;
    }
    /// <summary>
    /// Function that is called when the behaviour becomes disabled
    /// </summary>
    void OnDisable()
    {
        events.UpdateQuestionUI         -= UpdateQuestionUI;
        events.DisplayResolutionScreen  -= DisplayResolution;
        events.ScoreUpdated             -= UpdateScoreUI;
    }

    /// <summary>
    /// Function that is called when the script instance is being loaded.
    /// </summary>
    void Start()
    {
        
        resStateParaHash = Animator.StringToHash("ScreenState");
        //newPasscodeList = new List<int>();
        resetPasscodeList();
        resetPasscodeStringList();
        //comparePasscode();
        UpdateScoreUI();
    }
    private void Update()
    {
        //UpdateScoreUI();
        //FindObjectOfType<CheckPasscode>().receiveNewPasscodeList(newPasscodeList);
        //comparePasscode();
        //UpdateScoreUI();
        

    }

    #endregion

    /// <summary>
    /// Function that is used to update new question UI information.
    /// </summary>
    void UpdateQuestionUI(Question question)
    {
        uIElements.QuestionInfoTextObject.text = question.Info;
        CreateAnswers(question);
    }
    /// <summary>
    /// Function that is used to display resolution screen.
    /// </summary>
    void DisplayResolution(ResolutionScreenType type, int score)
    {
        UpdateResUI(type, score);
        uIElements.ResolutionScreenAnimator.SetInteger(resStateParaHash, 2);
        uIElements.MainCanvasGroup.blocksRaycasts = false;

        if (type != ResolutionScreenType.Finish)
        {
            if (IE_DisplayTimedResolution != null)
            {
                StopCoroutine(IE_DisplayTimedResolution);
            }
            IE_DisplayTimedResolution = DisplayTimedResolution();
            StartCoroutine(IE_DisplayTimedResolution);
        }
    }
    IEnumerator DisplayTimedResolution()
    {
        yield return new WaitForSeconds(GameUtility.ResolutionDelayTime);
        uIElements.ResolutionScreenAnimator.SetInteger(resStateParaHash, 1);
        uIElements.MainCanvasGroup.blocksRaycasts = true;
    }

    /// <summary>
    /// Function that is used to display resolution UI information.
    /// </summary>
    void UpdateResUI(ResolutionScreenType type, int score)
    {
        var highscore = PlayerPrefs.GetInt(GameUtility.SavePrefKey);

        switch (type)
        {
            case ResolutionScreenType.Correct:
                uIElements.ResolutionBG.color = parameters.CorrectBGColor;
                uIElements.ResolutionStateInfoText.text = "CORRECT!";
                uIElements.ResolutionScoreText.text = "Passcode is " + score;
                break;
            case ResolutionScreenType.Incorrect:
                uIElements.ResolutionBG.color = parameters.IncorrectBGColor;
                uIElements.ResolutionStateInfoText.text = "WRONG!";
                uIElements.ResolutionScoreText.text = "Retry to get the correct answer";
                break;
            case ResolutionScreenType.Finish:
                uIElements.ResolutionBG.color = parameters.FinalBGColor;
                uIElements.ResolutionStateInfoText.text = "FINAL SCORE";

                StartCoroutine(CalculateScore());
                uIElements.FinishUIElements.gameObject.SetActive(true);
                uIElements.HighScoreText.gameObject.SetActive(true);
                uIElements.HighScoreText.text = ((highscore > events.StartupHighscore) ? "<color=yellow>new </color>" : string.Empty) + "Highscore: " + highscore;
                break;
        }
    }

    /// <summary>
    /// Function that is used to calculate and display the score.
    /// </summary>
    IEnumerator CalculateScore()
    {
        if (events.CurrentFinalScore == 0) { uIElements.ResolutionScoreText.text = 0.ToString(); yield break; }

        var scoreValue = 0;
        var scoreMoreThanZero = events.CurrentFinalScore > 0; 
        while (scoreMoreThanZero ? scoreValue < events.CurrentFinalScore : scoreValue > events.CurrentFinalScore)
        {
            scoreValue += scoreMoreThanZero ? 1 : -1;
            uIElements.ResolutionScoreText.text = scoreValue.ToString();

            yield return null;
        }
    }

    /// <summary>
    /// Function that is used to create new question answers.
    /// </summary>
    void CreateAnswers(Question question)
    {
        EraseAnswers();

        float offset = 0 - parameters.Margins;
        for (int i = 0; i < question.Answers.Length; i++)
        {
            AnswerData newAnswer = (AnswerData)Instantiate(answerPrefab, uIElements.AnswersContentArea);
            newAnswer.UpdateData(question.Answers[i].Info, i);

            newAnswer.Rect.anchoredPosition = new Vector2(0, offset);

            offset -= (newAnswer.Rect.sizeDelta.y + parameters.Margins);
            uIElements.AnswersContentArea.sizeDelta = new Vector2(uIElements.AnswersContentArea.sizeDelta.x, offset * -1);

            currentAnswers.Add(newAnswer);
        }
    }
    /// <summary>
    /// Function that is used to erase current created answers.
    /// </summary>
    void EraseAnswers()
    {
        foreach (var answer in currentAnswers)
        {
            Destroy(answer.gameObject);
        }
        currentAnswers.Clear();
    }

    /// <summary>
    /// Function that is used to update score text UI.
    /// </summary>
    public void UpdateScoreUI()
    {

        //bool isListEmpty = !newPasscodeList.Any();
        //uIElements.ScoreText.text = "Score: " + events.CurrentFinalScore;
        //uIElements.ScoreText.text = "Passcode Table: " + "\n" + "1st Passcode: " + printNewPasscodeList + "\n" 
        /*if (isListEmpty)
        {
            uIElements.ScoreText.text = "Passcode Table: " + "\n\n"
                + "1st Passcode: " + "\n" + "?" + "\n\n"
                + "2nd Passcode: " + "\n" + "?" + "\n\n"
                + "3rd Passcode: " + "\n" + "?" + "\n\n"
                + "4th Passcode: " + "\n" + "?";
        }*/
        //Debug.Log("WTF: " + uIElements.ScoreText.text); 
        //Debug.Log("SH*t: " + newPasscodeStringList[0]);
        
        uIElements.ScoreText.text = "1st Quiz Passcode: " + "\n" + newPasscodeStringList[0] + "\n\n"
            + "2nd Quiz Passcode: " + "\n" + newPasscodeStringList[1] + "\n\n"
            + "3rd Quiz Passcode: " + "\n" + newPasscodeStringList[2] + "\n\n"
            + "4th Quiz Passcode: " + "\n" + newPasscodeStringList[3];
        

        //var sendPasscode = newPasscodeList;
        //FindObjectOfType<CheckPasscode>().receiveNewPasscodeList(newPasscodeList);
        // Note hereeeeeeeeeeeeeeeeeeeeeeeeeeeeee

        //FindObjectOfType<CheckPasscode>().receiveNewPasscodeList(newPasscodeList);
        //comparePasscode();


        var newList = newPasscodeList;
        FindObjectOfType<CheckPasscode>().receiveNewPasscodeList(newList[0], newList[1], newList[2], newList[3]);
        var newStringList = newPasscodeStringList;
        FindObjectOfType<CheckPasscode>().receivePasscodeString(newStringList[0], newStringList[1], newStringList[2], newStringList[3]);

    }
    public void getNewPasscodeList(int add, int location) {
        var pc = add;
        
         newPasscodeList[location] = pc;
         Debug.Log("Elements in newPassCodeList is: " + newPasscodeList[location]
                    + " With location is " + location);
         for (int i = 0; i < 4; i++)
         {
            Debug.Log(newPasscodeList[i] + "             " + i);
         }
      
      

    }
    public void resetPasscodeList() {

        newPasscodeList = new int[4];
        for (int i = 0; i< 4; i++)
        {
            newPasscodeList[i] = 100;
            //Debug.Log("Check Check Check: " + newPasscodeList.Count());
        }
        //foreach (int i in newPasscodeList) {
            //Debug.Log("Check Check Check: " + i);
        //}

    }
    
    public void resetPasscodeStringList()
    {

        for (int i = 0; i < 4; i++)
        {
            newPasscodeStringList[i] = "?";
        }

    }
    public void comparePasscode() {
        for (int i = 0; i < 4; i++) {
            if (newPasscodeList[i] != 100)
            {
                newPasscodeStringList[i] = newPasscodeList[i].ToString();
            }
            else if(newPasscodeList[i] == 100)
            {
                newPasscodeStringList[i] = "?";
            }
        }
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
            Debug.Log("Answers is: " + i);
        }
        return inputArray;
    }


}