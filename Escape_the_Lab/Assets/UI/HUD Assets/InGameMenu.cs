using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameMenu : MonoBehaviour
{
    public GameObject inGameMenu;
    public bool toggle;

    private void Start()
    {
        inGameMenu.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && inGameMenu.activeSelf == false)
            Pause();
    }
    public void Resume()
    {
        Time.timeScale = 1;
        inGameMenu.SetActive(false);
    }

    public void BackToMM(string MMSceneName)
    {
        Time.timeScale = 1;
        SceneManager.LoadSceneAsync(MMSceneName);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Pause()
    {
        inGameMenu.SetActive(true);
        Time.timeScale = 0;
    }

}
