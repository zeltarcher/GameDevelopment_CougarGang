using UnityEngine;

public class SharkSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject shark;
    [SerializeField]
    private Waves[] wave;
    private float currentTime;
    private int waveIndex;
    private Player player;
    private Vector3 velocity;          //used to check whether the player starts moving
    private bool StartSpawn = false;
    private float xPos = 0.0f;
    private GameObject risingWater;
    private ObjectTrigger objTrigger;

    AudioSource au_sou;
    AudioClip fire_sfx;

    //private ObjectTrigger 
    // Start is called before the first frame update
    void Start()
    {
        fire_sfx = Resources.Load<AudioClip>("Shark_jump");
        au_sou = GetComponent<AudioSource>();

        waveIndex = 0;
        currentTime = wave[waveIndex].delayTime;

    }

    // Update is called once per frame
    void Update()
    {
        objTrigger = GameObject.Find("FallObjOff").GetComponent<ObjectTrigger>();

        if (objTrigger.isLevel1 == true)
        {
            risingWater = GameObject.Find("Rising Water");
        }
        else
        {
            risingWater = GameObject.Find("Rising Water 2");
        }

        if (FindObjectOfType<charChange>().p1 == true)
        {
            player = GameObject.Find("Man").GetComponent<Player>();
            velocity = player.GetPlayerVelocity();
        }
        else if (FindObjectOfType<charChange>().p2 == true)
        {
            player = GameObject.Find("Robot").GetComponent<Player>();
            velocity = player.GetPlayerVelocity();
        }

        if (velocity.x > 0)     
        {
            StartSpawn = true;
        }

        if (StartSpawn == true)
        {
            currentTime -= Time.deltaTime;
            if (currentTime < 0)
                SelectWave();
        }
    }

    void SpawnSharks(float x)
    { 
        GameObject obj = Instantiate(shark);
        float y = risingWater.transform.position.y + 2 * risingWater.transform.localScale.y;
        obj.transform.position = new Vector3(x, y, 0);
    }

    void SelectWave()
    {
        currentTime = wave[waveIndex].delayTime;
        if (wave[waveIndex].totalSpawn > 0)
        {
            xPos = Random.Range(risingWater.transform.position.x - risingWater.transform.localScale.x,
                risingWater.transform.position.x + risingWater.transform.localScale.x);
            SpawnSharks(xPos);
            wave[waveIndex].totalSpawn--;

            au_sou.PlayOneShot(fire_sfx);
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
public class Waves
{
    public float delayTime;
    public float totalSpawn;
}