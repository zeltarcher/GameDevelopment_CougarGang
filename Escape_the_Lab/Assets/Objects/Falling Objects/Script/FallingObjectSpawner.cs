using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingObjectSpawner : MonoBehaviour
{
    [SerializeField]
    private string[] fallObjName;
    [SerializeField]
    private Wave[] wave;

    private float currentTime;
    List<float> remainingPositions = new List<float>();
    private int waveIndex;
    float xPos = 0;
    int rand;
  
    private Vector3 velocity;          //used to check whether the player starts moving

    private Player player;
    private bool StartSpawn = false;

    AudioSource fallingObj_audiosrc;
    AudioClip fire;

    // Start is called before the first frame update
    void Start()
    {
        waveIndex = 0;
        currentTime = wave[waveIndex].delayTime;
        //player = GameObject.Find("Man").GetComponent<Player>();

        fallingObj_audiosrc = GetComponent<AudioSource>();
        fire = Resources.Load<AudioClip>("Fire_Burning");
    }

    // Update is called once per frame
    void Update()
    {
        if(FindObjectOfType<charChange>().p1 == true)
        {
            player = GameObject.Find("Man").GetComponent<Player>();
            velocity = player.GetPlayerVelocity();
        }
        else if(FindObjectOfType<charChange>().p2 == true)
        {
            player = GameObject.Find("Robot").GetComponent<Player>();
            velocity = player.GetPlayerVelocity();
        }
        
        ////======================
        if (velocity.x > 0)     //if player starts moving, then the falling objects start to spawn
        {
            StartSpawn = true;
        }

        if (StartSpawn == true)
        {
            currentTime -= Time.deltaTime;
            if (currentTime < 0)
            {
                SelectWave();

            }
        }
    }

    void SpawnObjects(float xPos)
    {
        //fallingObj_audiosrc.PlayOneShot(fire);

        int r = Random.Range(0, fallObjName.Length);    
        string FallObjType = fallObjName[r];

        GameObject FallObj = ObjectPooling.instance.GetPooledObject(FallObjType);
        FallObj.transform.position = new Vector3(xPos, Camera.main.transform.position.y + 2*Camera.main.orthographicSize, 0); 
        FallObj.SetActive(true);

    }

    void SelectWave()
    {
   
        currentTime = wave[waveIndex].delayTime;
        if (wave[waveIndex].totalSpawn > 0)
        {          
            xPos = Random.RandomRange(player.transform.position.x - 15, player.transform.position.x + 15);  //random place from where the falling object spawns
            SpawnObjects(xPos);
            wave[waveIndex].totalSpawn--;
        }
        else
        {
            if (waveIndex < wave.Length - 1)
                waveIndex++;
            else
                wave[waveIndex].totalSpawn = 10;
        }
                  
    }
}

[System.Serializable]
public class Wave
{
    public float delayTime;
    public float totalSpawn;
}
