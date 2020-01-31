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
    public int minNum = 1;
    public int maxNum = 4;
    public int randNum = 1;
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
                //find a random position & number of obstacles to generate
                randDist = Random.Range(minDist, maxDist);
                randNum = Random.Range(minNum, maxNum + 1);
                //set generation position
                PosToGenerate = BaseCactusPos;
                PosToGenerate.x += randDist;

                //instantiate obstacles
                for (int i = 0; i < randNum; i++)
                {
                    //pick a random object from the list of obstacles
                    randObject = Random.Range(0, CactusList.Length);
                    Instantiate(CactusList[randObject], PosToGenerate, transform.rotation);
                    //increment the position to instantiate by the prev object's width
                    PosToGenerate.x += CactusList[randObject].transform.localScale.x;
                }
            }
            //generate birds
            else if (Random.Range(0.0f, 1.0f) <= 0.33f)
            {
                PosToGenerate = BirdPos[Random.Range(0, 3)];
                Instantiate(Bird, PosToGenerate, transform.rotation);
            }
            timeRemaining = timeUntilGeneration;
        }
    }
}
