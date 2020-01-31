/*
CactusGeneration.cs
by Deepti Ramani
01/30/2020
This is class generates obstacles for the player to jump over or duck under
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CactusGeneration : MonoBehaviour
{
    //when + where to generate (time or dist)
    public float timeUntilGeneration = 5.0f;
    private float timeRemaining = 5.0f;
    public float minDist = 2.0f;
    public float maxDist = 5.0f;
    private float randDist = 2.0f;

    public Vector2 PosToGenerate = Vector2.zero;

    //cacti
    public Vector2 BaseCactusPos = new Vector2(9.5f, -1.25f);
    public int randObject = 0;
    public GameObject[] CactusList = new GameObject[5];

    //birds
    public int ScoreUntilBirds = 100;
    public Vector2[] BirdPos = { new Vector2(9.5f, -1.25f), new Vector2(9.5f, 0.0f), new Vector2(9.5f, 1.25f) };
    public GameObject Bird;

    // Start is called before the first frame update
    void Start()
    {
        timeRemaining = timeUntilGeneration;
    }

    // Update is called once per frame
    void Update()
    {
        //decrement timer for generation
        timeRemaining -= Time.deltaTime;
        if(timeRemaining <= 0)
        {
            //generate cacti
            if (Score.getScore() <= ScoreUntilBirds)
            {
                //find a random position to generate
                randDist = Random.Range(minDist, maxDist);
                PosToGenerate = BaseCactusPos;
                PosToGenerate.x += randDist;

                //instantiate
                randObject = Random.Range(0, CactusList.Length);
                Instantiate(CactusList[randObject], PosToGenerate, transform.rotation);
            }
            //generate birds once we reach a certain score
            else if (Random.Range(0.0f, 1.0f) <= 0.33f)
            {
                //pick one of three positions (low, middle, high)
                randDist = Random.Range(minDist, maxDist);
                PosToGenerate = BirdPos[Random.Range(0, 3)];
                PosToGenerate.x += randDist;

                //instantiate
                Instantiate(Bird, PosToGenerate, transform.rotation);
            }
            //reset time
            timeRemaining = timeUntilGeneration;
        }
    }
}
