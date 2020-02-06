﻿/*
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
    public Vector2 initPosToGenerate = new Vector2(26.0f, -2.0f);
    public Vector2 currPosToGenerate = Vector2.zero;

    // Start is called before the first frame update
    void Start()
    {
        currPosToGenerate = initPosToGenerate;
    }

    // Update is called once per frame
    void Update()
    {
        //generate if camera approaches edge of ground
        if(Vector2.Distance((Vector2)transform.position, currPosToGenerate) <= 15.0f)
        {
            Instantiate(Ground[Random.Range(0, 3)], currPosToGenerate, transform.rotation);
            currPosToGenerate.x += SegmentWidth;
        }
    }

    public void InstantiateInitGround()
    {
        Instantiate(Ground[Random.Range(0, 3)], new Vector2(-4.0f, -2.0f), transform.rotation);
        Instantiate(Ground[Random.Range(0, 3)], new Vector2(6.0f, -2.0f), transform.rotation);
        Instantiate(Ground[Random.Range(0, 3)], new Vector2(16.0f, -2.0f), transform.rotation);
    }
}
