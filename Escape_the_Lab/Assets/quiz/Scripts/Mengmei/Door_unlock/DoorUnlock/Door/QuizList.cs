//Get 4 non-duplicated random No. of questions

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuizList : MonoBehaviour
{
    private Quiz quiz;
    public List<int> list_of_quizNO;
    private int num;

    // Start is called before the first frame update
    void Start()
    {
        quiz = FindObjectOfType<Quiz>();
        list_of_quizNO = new List<int>();

        for (int i = 0; i < 4; i++)
        {
            num = Random.Range(0, quiz.count);
            while (list_of_quizNO.Contains(num))
            {
                num = Random.Range(0, quiz.count);
            }
            list_of_quizNO.Add(num);
        }

        //foreach (int n in list_of_quizNO)
        //{
        //    Debug.Log(n);

        //}
    }
}
