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

    public Vector2 BasePos = new Vector2(9.5f, -2.0f);
    private Vector2 CurrPos = new Vector2(9.5f, -2.0f);

    //objects to generate
    public int minNum = 1;
    public int maxNum = 4;
    public int randNum = 1;

    public int randObject = 0;
    public GameObject[] ObjectsToGenerate = new GameObject[4];

    // Start is called before the first frame update
    void Start()
    {
        timeRemaining = timeUntilGeneration;
    }

    // Update is called once per frame
    void Update()
    {
        timeRemaining -= Time.deltaTime;
        if(timeRemaining <= 0)
        {
            randDist = Random.Range(minDist, maxDist);
            randNum = Random.Range(minNum, maxNum + 1);
            CurrPos = BasePos;
            CurrPos.x += randDist;

            //instantiate
            for (int i = 0; i < randNum; i++)
            {
                randObject = Random.Range(0, ObjectsToGenerate.Length);
                Instantiate(ObjectsToGenerate[randObject], CurrPos, transform.rotation);
                //CurrPos.x;
            }

            timeRemaining = timeUntilGeneration;
        }
    }
}
