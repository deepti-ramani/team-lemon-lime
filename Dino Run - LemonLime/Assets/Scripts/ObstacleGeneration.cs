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
    public float minOffset = 10.0f;
    public float maxOffset = 20.0f;
    public float targetPos;

    //where to generate
    public Vector2 PosToGenerate = Vector2.zero;

    //cacti
    public int randObject = 0;
    public GameObject[] CactusList = new GameObject[6];

    //birds
    public const int ScoreUntilBirds = 500;
    public float[] BirdYPos = { -1.5f, 0.0f, 1.0f };
    public GameObject Bird;

    // Start is called before the first frame update
    void Start()
    {
        GameControl = GameObject.Find("GameControl");
        targetPos = transform.position.x + Random.Range(1.0f, 3.0f);
    }

    // Update is called once per frame
    void Update()
    {
        //instantiate when we've travelled the required dist
        if (targetPos - transform.position.x <= 0.0001)
        {
            //generate cacti
            if (GameControl.GetComponent<GameControl>().score < ScoreUntilBirds)
            {
                //instantiate random cactus variation based on how far the player has already travelled (weighted)
                if (transform.position.x < 50.0f)
                {
                    randObject = Random.Range(0, 2);
                }
                else if (transform.position.x < 80.0f)
                {
                    randObject = Random.Range(0, 4);
                }
                else
                {
                    randObject = Random.Range(0, CactusList.Length);
                }

                //find a random position to generate
                PosToGenerate = new Vector2(transform.position.x + 10.0f, CactusList[randObject].transform.position.y);

                //instantiate
                Instantiate(CactusList[randObject], PosToGenerate, transform.rotation);
            }

            //generate birds once we reach a certain score
            else if (Random.Range(0.0f, 1.0f) <= 0.33f)
            {
                //pick one of three positions (low, middle, high)
                PosToGenerate = new Vector2(transform.position.x + 10.0f, BirdYPos[Random.Range(0, 3)]);

                //instantiate
                Instantiate(Bird, PosToGenerate, transform.rotation);

                //plus content animation
                if(GameControl.GetComponent<GameControl>().isPlusContent == true)
                {
                    Bird.GetComponent<Animator>().SetInteger("State", 1);
                }
            }

            //calculate new distance
            targetPos = transform.position.x + Random.Range(minOffset, maxOffset);
        }
    }
}
