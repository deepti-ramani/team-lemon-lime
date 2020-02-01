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
    //generation is a function of distance
    public GameObject GameControl;
    public float minOffset = 2.0f;
    public float maxOffset = 5.0f;
    private float targetPosX;

    //where to generate
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
        targetPosX = transform.position.x + Random.Range(minOffset, maxOffset);
    }

    // Update is called once per frame
    void Update()
    {
        //instantiate when we've travelled the required dist
        if (transform.position.x - targetPosX <= 0.001)
        {
            //generate cacti
            if (GameControl.GetComponent<GameControl>().score < ScoreUntilBirds)
            {
                //find a random position to generate
                PosToGenerate = new Vector2(transform.position.x + 10.0f, -1.25f);

                //instantiate random cactus variation
                randObject = Random.Range(0, CactusList.Length);
                Instantiate(CactusList[randObject], PosToGenerate, transform.rotation);
            }

            //generate birds once we reach a certain score
            else if (Random.Range(0.0f, 1.0f) <= 0.33f)
            {
                //pick one of three positions (low, middle, high)
                PosToGenerate = new Vector2(transform.position.x + 10.0f, BirdYPos[Random.Range(0, 3)]);

                //instantiate
                Instantiate(Bird, PosToGenerate, transform.rotation);
            }

            //restart timer
            targetPosX = transform.position.x + Random.Range(minOffset, maxOffset);
        }
    }
}
