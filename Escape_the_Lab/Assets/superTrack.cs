using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class superTrack : MonoBehaviour
{

    public Vector2 superPosition;

    // Start is called before the first frame update
    void Start()
    {
        superPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        superPosition = transform.position;
    }
}
