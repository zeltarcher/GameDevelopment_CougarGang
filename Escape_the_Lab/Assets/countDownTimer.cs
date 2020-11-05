using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class countDownTimer : MonoBehaviour
{
    float currentTime = 0f;
    float startingTime = 10f;

    [SerializeField] TextMesh countDown;


    // Start is called before the first frame update
    void Start()
    {
        currentTime = startingTime;
        Destroy(gameObject, 10f);
        
    }

    // Update is called once per frame
    void Update()
    {
        currentTime -= 1 * Time.deltaTime;
        int cov = Mathf.FloorToInt(currentTime);
        countDown.text = cov.ToString();
    }
}
