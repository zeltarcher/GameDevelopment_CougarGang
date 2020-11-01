using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy_H_Bar : MonoBehaviour
{
    Vector2 localScale;

    public GameObject enemy;

    //EnemyController enemyControl;

    // Start is called before the first frame update
    void Start()
    {
        localScale = transform.localScale;
        //EnemyController enemyControl = enemy.GetComponents<EnemyController>();
    }
    // Update is called once per frame
    void Update()
    {

        //localScale.x = enemy.heal;
        transform.localScale = localScale;
    }
}
