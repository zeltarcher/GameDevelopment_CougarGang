using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;

public class Quiz : MonoBehaviour
{
    public struct QuizQuestion
    {
        public string question;
        public string options;
        public string answer;
    }
    private string xmlPath = "Assets/Quiz.xml";
    private XmlDocument document;

    private QuizQuestion Q;
    public int count;

    // Start is called before the first frame update
    void Awake()
    {
        document = ReadXml(xmlPath);
        GetTotalNumOfQuestions();
    }

    private XmlDocument ReadXml(string path)
    {
        XmlDocument doc = new XmlDocument();
        doc.Load(path);

        return doc;
    }

    public QuizQuestion FetchQuestion(int num)
    {
        //Fetch question from xml file
        //
        XmlNode Quiz = document.SelectSingleNode("quizBank");
        XmlNodeList QuestionList = Quiz.ChildNodes;
        XmlNode Question = QuestionList[num];
        string que = Question.ChildNodes[0].InnerText;
        string opt = "";
        string[] C = { "A. ", "B. ", "C. ", "D. " };
        for (int i = 1; i <= 4; i++)
        {

            opt += C[i-1] + Question.ChildNodes[i].InnerText+"\n";
        }
        string ans = Question.ChildNodes[1].InnerText;

        //assign the all the info to a single class variable
        Q.question = que;
        Q.options = opt;
        Q.answer = ans;

        return Q;
    }

    public int GetTotalNumOfQuestions()
    {
        
        XmlNode Quiz = document.SelectSingleNode("quizBank");
        XmlNodeList QuestionList = Quiz.ChildNodes;
        count = QuestionList.Count;
        return count;
    }
}
