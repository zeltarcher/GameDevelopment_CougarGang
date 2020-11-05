using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class charChange : MonoBehaviour
{

    public GameObject player1;
    public GameObject player2;
    public GameObject countDown;

    public bool p1 = true;
    public bool p2 = false;
    public float transformTime = 10f;

    // Start is called before the first frame update
    void Start()
    {
        
        player1.SetActive(true);
        player2.SetActive(false);
        p1 = true;
        p2 = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void transformSuper()
    {
        if (p1 == true && p2 == false)
        {
            
            
            if (countDown)
            {
                showCountDown();
                
            }
            player2.GetComponent<Player>().currentHealth = 100;
            player1.SetActive(false);
            player2.SetActive(true);
            p1 = false;
            p2 = true;
            Vector2 newSuperPosition = new Vector2(player1.transform.position.x, player1.transform.position.y + 1);

            player2.transform.position = newSuperPosition;

        }

        StartCoroutine(waiter());
        
    }


    public void showCountDown()
    {
        Vector3 newPos = new Vector3(player2.transform.position.x - 0.2f, player2.transform.position.y + 2f);
        Instantiate(countDown, newPos, Quaternion.identity, player2.transform);
    }
    

    IEnumerator waiter()
    {
        
        yield return new WaitForSeconds(transformTime);
        backToNomal();
    }

    void backToNomal()
    {
        if (p1 == false && p2 == true)
        {
            player1.SetActive(true);
            player2.SetActive(false);
            p1 = true;
            p2 = false;
            player1.transform.position = player2.transform.position;
        }
    }
}
