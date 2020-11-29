using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MMButtonsControl : MonoBehaviour
{
    public GameObject InfoContainer;
    private GameObject MMCanvas;

    void Awake()
    {
        MMCanvas = GameObject.Find("Panel-center");
        
        InfoContainer = GameObject.Find("Panel info");
    }
    public void StartGame(string gameplayScene)
    {
        SceneManager.LoadSceneAsync(gameplayScene);
    }

    public void InfoOpen()
    {
        InfoContainer.SetActive(true);
        MMCanvas.SetActive(false);
    }

    public void InfoClose()
    {
        InfoContainer.SetActive(false);
        MMCanvas.SetActive(true);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
