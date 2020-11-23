using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBulletSpawner : MonoBehaviour
{

    public GameObject getBullet;
    public float spawnRate = 2f;
    float nextSpawn = 0.0f;

    void Start()
    {
        //InvokeRepeating("Spawn", nextSpawn, spawnRate);
    }

    void Update()
    {
        //Instantiate(getBullet, transform.position, getBullet.transform.rotation);
        if (Time.time > nextSpawn)
        {
            nextSpawn = Time.time + spawnRate;
            Instantiate(getBullet, transform.position, getBullet.transform.rotation);
        }
    }
}
