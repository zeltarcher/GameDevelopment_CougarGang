using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy_H_Bar : MonoBehaviour
{
    Vector2 localScale;
    EnemyController Enemy;

    // Start is called before the first frame update
    void Start()
    {
        localScale = transform.localScale;

        Enemy = GetComponent<EnemyController>();
    }

    // Update is called once per frame
    void Update()
    {

        localScale.x = Enemy.health;
        transform.localScale = localScale;
    }
}
