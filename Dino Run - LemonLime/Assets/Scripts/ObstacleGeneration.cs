/*
ObstacleGeneration.cs
by Deepti Ramani
01/30/2020
This class generates obstacles for the player to jump over or duck under
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleGeneration : MonoBehaviour
{
    //generation is a function of time (or score?)
    public GameObject GameControl;
    public float minTime = 3.0f;
    public float maxTime = 5.0f;
    private float timeRemaining = 0.0f;

    //where to generate
    public float minDist = 2.0f;
    public float maxDist = 5.0f;
    private float randDist = 2.0f;
    public Vector2 PosToGenerate = Vector2.zero;

    //cacti
    public int randObject = 0;
    public GameObject[] CactusList = new GameObject[6];

    //birds
    public int ScoreUntilBirds = 500;
    public float[] BirdYPos = { -1.25f, 0.0f, 1.25f };
    public GameObject Bird;

    // Start is called before the first frame update
    void Start()
    {
        GameControl = GameObject.Find("GameControl");
        timeRemaining = Random.Range(minTime, maxTime);
    }

    // Update is called once per frame
    void Update()
    {
        //decrement time
        timeRemaining -= Time.deltaTime;

        //instantiate when time runs out (consider function of score)
        if (timeRemaining <= 0)
        {
            //generate cacti
            if (GameControl.GetComponent<GameControl>().score < ScoreUntilBirds)
            {
                //find a random position to generate
                randDist = Random.Range(minDist, maxDist);
                PosToGenerate = new Vector2(transform.position.x + 10.0f, -1.25f);
                PosToGenerate.x += randDist;

                //instantiate random cactus variation
                randObject = Random.Range(0, CactusList.Length);
                Instantiate(CactusList[randObject], PosToGenerate, transform.rotation);
            }

            //generate birds once we reach a certain score
            else if (Random.Range(0.0f, 1.0f) <= 0.33f)
            {
                //pick one of three positions (low, middle, high)
                randDist = Random.Range(minDist, maxDist);
                PosToGenerate = new Vector2(transform.position.x + 10.0f, BirdYPos[Random.Range(0, 3)]);
                PosToGenerate.x += randDist;

                //instantiate
                Instantiate(Bird, PosToGenerate, transform.rotation);
            }

            //restart timer
            timeRemaining = Random.Range(minTime, maxTime);
        }
    }
}
