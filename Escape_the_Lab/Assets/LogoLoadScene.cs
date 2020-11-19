using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LogoLoadScene : MonoBehaviour
{

    IEnumerator LoadLevelAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(1);
    }

    // Start is called before the first frame update
    void Start()
    {
        //SceneManager.LoadScene(1);
        StartCoroutine(LoadLevelAfterDelay(4));
    }

    // Update is called once per frame
    void Update()
    {
    }
}
