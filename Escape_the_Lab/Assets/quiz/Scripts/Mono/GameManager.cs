using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    #region Variables

    private Data data = new Data();

    [SerializeField] GameEvents events = null;

    [SerializeField] Animator timerAnimtor = null;
    [SerializeField] TextMeshProUGUI timerText = null;
    [SerializeField] Color timerHalfWayOutColor = Color.yellow;
    [SerializeField] Color timerAlmostOutColor = Color.red;
    private Color timerDefaultColor = Color.white;

    private List<AnswerData> PickedAnswers = new List<AnswerData>();
    private List<int> FinishedQuestions = new List<int>();
    private int currentQuestion = 0;

    private int timerStateParaHash = 0;

    private IEnumerator IE_WaitTillNextRound = null;
    private IEnumerator IE_StartTimer = null;
    private Sign sign;
    private string checkSignName = "";
    private List<int> passcodeList;
    private List<int> randomIndex;
    //private int[] randomIndex = new int[4];

    public List<int> getPasscodeList { get { return getPasscodeList; } }

    private void Update()
    {
        //checkSignName = UpdateCheckSignName();
        //Display();
    }

    private             bool                IsFinished
    {
        get
        {
            return (FinishedQuestions.Count < data.Questions.Length) ? false : true;
        }
    }

    #endregion

    #region Default Unity methods

    /// <summary>
    /// Function that is called when the object becomes enabled and active
    /// </summary>
    private void OnEnable()
    {
        events.UpdateQuestionAnswer += UpdateAnswers;
    }
    /// <summary>
    /// Function that is called when the behaviour becomes disabled
    /// </summary>
    private void OnDisable()
    {
        events.UpdateQuestionAnswer -= UpdateAnswers;
    }

    /// <summary>
    /// Function that is called when the script instance is being loaded.
    /// </summary>
    private void Awake()
    {
        //If current level is a first level, reset the final score back to zero.
        if (events.level == 1) { events.CurrentFinalScore = 0; }
    }

    /// <summary>
    /// Function that is called when the script instance is being loaded.
    /// </summary>
    private void Start()
    {
        sign = FindObjectOfType<Sign>();
        passcodeList = new List<int>();
        events.StartupHighscore = PlayerPrefs.GetInt(GameUtility.SavePrefKey);

        timerDefaultColor = timerText.color;

        LoadData();
        EraseRandomQuestionIndex();
        
        timerStateParaHash = Animator.StringToHash("TimerState");

        var seed = UnityEngine.Random.Range(int.MinValue, int.MaxValue);
        UnityEngine.Random.InitState(seed);



        //GetRandomQuestion();
        //UpdateCheckSignName();
        //checkSignName = UpdateCheckSignName();
        //Display();
        EraseRandomQuestionIndex();


    }

    #endregion

    /// <summary>
    /// Function that is called to update new selected answer.
    /// </summary>

    public void UpdateAnswers(AnswerData newAnswer)
    {
        if (data.Questions[currentQuestion].Type == AnswerType.Single)
        {
            foreach (var answer in PickedAnswers)
            {
                if (answer != newAnswer)
                {
                    answer.Reset();
                }
            }
            PickedAnswers.Clear();
            PickedAnswers.Add(newAnswer);
        }
        else
        {
            bool alreadyPicked = PickedAnswers.Exists(x => x == newAnswer);
            if (alreadyPicked)
            {
                PickedAnswers.Remove(newAnswer);
            }
            else
            {
                PickedAnswers.Add(newAnswer);
            }
        }
    }

    /// <summary>
    /// Function that is called to clear PickedAnswers list.
    /// </summary>
    public void EraseAnswers()
    {
        PickedAnswers = new List<AnswerData>();
    }

    /// <summary>
    /// Function that is called to display new question.
    /// </summary>
    public void Display()
    {
        EraseAnswers();


        //EraseRandomQuestionIndex();
        var question = GetRandomQuestion();

        if (events.UpdateQuestionUI != null)
        {
            events.UpdateQuestionUI(question);
        } else { Debug.LogWarning("Ups! Something went wrong while trying to display new Question UI Data. GameEvents.UpdateQuestionUI is null. Issue occured in GameManager.Display() method."); }

        if (question.UseTimer)
        {
            UpdateTimer(question.UseTimer);
        }
    }

    /// <summary>
    /// Function that is called to accept picked answers and check/display the result.
    /// </summary>
    public void Accept()
    {
        UpdateTimer(false);
        bool isCorrect = CheckAnswers();
        FinishedQuestions.Add(currentQuestion);

        //UpdateScore(isCorrect ? data.Questions[currentQuestion].AddScore : -data.Questions[currentQuestion].AddScore);
        if (isCorrect) {

            if (checkSignName == "Sign1")
            {
                UpdateScore(data.Questions[currentQuestion].AddScore);
                FindObjectOfType<UIManager>().getNewPasscodeList(data.Questions[currentQuestion].AddScore,0);
                FindObjectOfType<UIManager>().comparePasscode();
                FindObjectOfType<UIManager>().UpdateScoreUI();
            }
            else if (checkSignName == "Sign2")
            {
                UpdateScore(data.Questions[currentQuestion].AddScore);
                FindObjectOfType<UIManager>().getNewPasscodeList(data.Questions[currentQuestion].AddScore,1);
                FindObjectOfType<UIManager>().comparePasscode();
                FindObjectOfType<UIManager>().UpdateScoreUI();
            }
            else if (checkSignName == "Sign3")
            {
                UpdateScore(data.Questions[currentQuestion].AddScore);
                FindObjectOfType<UIManager>().getNewPasscodeList(data.Questions[currentQuestion].AddScore,2);
                FindObjectOfType<UIManager>().comparePasscode();
                FindObjectOfType<UIManager>().UpdateScoreUI();
            }
            else if (checkSignName == "Sign4")
            {
                UpdateScore(data.Questions[currentQuestion].AddScore);
                FindObjectOfType<UIManager>().getNewPasscodeList(data.Questions[currentQuestion].AddScore,3);
                FindObjectOfType<UIManager>().comparePasscode();
                FindObjectOfType<UIManager>().UpdateScoreUI();
            }
            else {
                Debug.Log("Hey broooooooooooooooooooooooooooooooooooooooooooooooooooooooooo");
            }
        }
        

        if (IsFinished)
        {
            events.level++;
            if (events.level > GameEvents.maxLevel)
            {
                events.level = 1;
            }
            SetHighscore();
        }

        /*var type 
            = (IsFinished) 
            ? UIManager.ResolutionScreenType.Finish 
            : (isCorrect) ? UIManager.ResolutionScreenType.Correct 
            : UIManager.ResolutionScreenType.Incorrect;*/
        var type
            = (false)
            ? UIManager.ResolutionScreenType.Finish
            : (isCorrect) ? UIManager.ResolutionScreenType.Correct
            : UIManager.ResolutionScreenType.Incorrect;

        events.DisplayResolutionScreen?.Invoke(type, data.Questions[currentQuestion].AddScore);

        AudioManager.Instance.PlaySound((isCorrect) ? "CorrectSFX" : "IncorrectSFX");

        if (type != UIManager.ResolutionScreenType.Finish)
        {
            if (IE_WaitTillNextRound != null)
            {
                StopCoroutine(IE_WaitTillNextRound);
            }
            IE_WaitTillNextRound = WaitTillNextRound();
            StartCoroutine(IE_WaitTillNextRound);
        }
    }

    #region Timer Methods

    void UpdateTimer(bool state)
    {
        switch (state)
        {
            case true:
                IE_StartTimer = StartTimer();
                StartCoroutine(IE_StartTimer);

                timerAnimtor.SetInteger(timerStateParaHash, 2);
                break;
            case false:
                if (IE_StartTimer != null)
                {
                    StopCoroutine(IE_StartTimer);
                }

                timerAnimtor.SetInteger(timerStateParaHash, 1);
                break;
        }
    }
    IEnumerator StartTimer()
    {
        var totalTime = data.Questions[currentQuestion].Timer;
        var timeLeft = totalTime;

        timerText.color = timerDefaultColor;
        while (timeLeft > 0)
        {
            timeLeft--;

            AudioManager.Instance.PlaySound("CountdownSFX");

            if (timeLeft < totalTime / 2 && timeLeft > totalTime / 4)
            {
                timerText.color = timerHalfWayOutColor;
            }
            if (timeLeft < totalTime / 4)
            {
                timerText.color = timerAlmostOutColor;
            }

            timerText.text = timeLeft.ToString();
            yield return new WaitForSeconds(1.0f);
        }
        Accept();
    }
    IEnumerator WaitTillNextRound()
    {
        yield return new WaitForSeconds(GameUtility.ResolutionDelayTime);
        Display();
    }

    #endregion

    /// <summary>
    /// Function that is called to check currently picked answers and return the result.
    /// </summary>
    bool CheckAnswers()
    {
        if (!CompareAnswers())
        {
            return false;
        }
        return true;
    }
    /// <summary>
    /// Function that is called to compare picked answers with question correct answers.
    /// </summary>
    bool CompareAnswers()
    {
        if (PickedAnswers.Count > 0)
        {
            List<int> c = data.Questions[currentQuestion].GetCorrectAnswers();
            List<int> p = PickedAnswers.Select(x => x.AnswerIndex).ToList();

            var f = c.Except(p).ToList();
            var s = p.Except(c).ToList();

            return !f.Any() && !s.Any();
        }
        return false;
    }

    /// <summary>
    /// Function that is called to load data from the xml file.
    /// </summary>
    void LoadData()
    {
        var path = Path.Combine(GameUtility.FileDir, GameUtility.FileName + events.level + ".xml");
        data = Data.Fetch(path);
    }

    /// <summary>
    /// Function that is called restart the game.
    /// </summary>
    public void RestartGame()
    {
        //If next level is the first level, meaning that we start playing a game again, reset the final score.
        if (events.level == 1) { events.CurrentFinalScore = 0; }

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    /// <summary>
    /// Function that is called to quit the application.
    /// </summary>
    public void QuitGame()
    {
        //On quit reset the current level back to the first level.
        events.level = 1;

        Application.Quit();
    }

    /// <summary>
    /// Function that is called to set new highscore if game score is higher.
    /// </summary>
    private void SetHighscore()
    {
        var highscore = PlayerPrefs.GetInt(GameUtility.SavePrefKey);
        if (highscore < events.CurrentFinalScore)
        {
            PlayerPrefs.SetInt(GameUtility.SavePrefKey, events.CurrentFinalScore);
        }
    }
    /// <summary>
    /// Function that is called update the score and update the UI.
    /// </summary>
    private void UpdateScore(int add)
    {
        //FindObjectOfType<UIManager>().getNewPasscodeList(add);
        events.CurrentFinalScore += add;
        events.ScoreUpdated?.Invoke();
    }

    #region Getters

    Question GetRandomQuestion()
    {
        randomIndex = new List<int>(4);

        randomIndex = GetRandomQuestionIndex();
        //randomIndex[0] = 0;
        //randomIndex[1] = 1;
        //randomIndex[2] = 2;
        //randomIndex[3] = 3;

        Debug.Log("checkSignName is: " + checkSignName);
        if (checkSignName == "Sign1")
        {
            //num = quizList.list_of_quizNO[0];
            Debug.Log("Hit: " + checkSignName + "Question number is: " + randomIndex[0] + " and the array size is " + randomIndex.Count()
                + " at index: " + 0);


            currentQuestion = randomIndex[0];
            //currentQuestion = 0;
        }
        else if (checkSignName == "Sign2")
        {
            //num = quizList.list_of_quizNO[1];
            Debug.Log("Hit: " + checkSignName + "Question number is: " + randomIndex[1] + " and the array size is " + randomIndex.Count()
                + " at index: " + 1);


            currentQuestion = randomIndex[1];
            //currentQuestion = 1;
        }
        else if (checkSignName == "Sign3")
        {
            //num = quizList.list_of_quizNO[2];
            Debug.Log("Hit: " + checkSignName + "Question number is: " + randomIndex[2] + " and the array size is " + randomIndex.Count()
                + " at index: " + 2);


            currentQuestion = randomIndex[2];
            //currentQuestion = 2;
        }
        else if (checkSignName == "Sign4")
        {
            //num = quizList.list_of_quizNO[3];
            Debug.Log("Hit: " + checkSignName + " Question number is: " + randomIndex[3] + " and the array size is " + randomIndex.Count()
                + " at index: " + 3);


            currentQuestion = randomIndex[3];
            //currentQuestion = 0;
        }
        else {
            Debug.Log("Nothing happened" + checkSignName + " and the array size is " + randomIndex.Count());
        }
        //currentQuestion = randomIndex;
        //Debug.Log("It can show question");
        return data.Questions[currentQuestion];
    }
    public List<int> GetRandomQuestionIndex()
    {
        //EraseRandomQuestionIndex();
        var random = 0;
        Debug.Log("This is Holy Moly the while loop");
        //EraseRandomQuestionIndex();
        for (int i = 0; i < 4; i++)
        {
            
            if (FinishedQuestions.Count < data.Questions.Length || FinishedQuestions.Count < 4)
            {
                Debug.Log("This is aboveeeeeeeeeee the while loop");
                do
                {
                    Debug.Log("This is innnnnnnnnn the while loop");
                    random = UnityEngine.Random.Range(0, data.Questions.Length);
                } while (FinishedQuestions.Contains(random) || random == currentQuestion);
                FinishedQuestions.Add(random);
            }
            Debug.Log("This is outtttttttttttttt the while loop");


            //return random;

        }
        return FinishedQuestions;
    }

    void EraseRandomQuestionIndex() {
        FinishedQuestions = new List<int>(4);     
    }

    /*private string UpdateCheckSignName() {
        string checkSignNameInside = "";
        checkSignNameInside = sign.touchingName();
        Debug.Log("UpdateCheckSignName is:                                         " + checkSignNameInside);
        return checkSignNameInside;
    }*/

    public void UpdateCheckSignName(string signNameFromSignFile) {
        var name = signNameFromSignFile;
        checkSignName = name;
    }


    #endregion
}