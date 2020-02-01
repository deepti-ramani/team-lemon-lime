/*
GroundGeneration.cs
by Deepti Ramani
01/31/2020
This class generates three different types of ground as the camera scrolls
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundGeneration : MonoBehaviour
{
    public GameObject[] Ground = new GameObject[3];
    public float SegmentWidth = 10.0f;
    public Vector2 PosToGenerate = new Vector2(-4.0f, -2.0f);

    // Start is called before the first frame update
    void Start()
    {
        //start with three pre-generated segments
        for(int i = 0; i < 3; i++)
        {
            Instantiate(Ground[Random.Range(0, 3)], PosToGenerate, transform.rotation);
            PosToGenerate.x += SegmentWidth;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //generate if camera approaches edge of ground
        if(Vector2.Distance((Vector2)transform.position, PosToGenerate) <= 15.0f)
        {
            Instantiate(Ground[Random.Range(0, 3)], PosToGenerate, transform.rotation);
            PosToGenerate.x += SegmentWidth;
        }
    }
}
