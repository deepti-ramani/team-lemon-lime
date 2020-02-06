/*
HighScore.cs
by Deepti Ramani
02/05/2020

*/

using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine;

public class HighScore : MonoBehaviour
{
    //top 10 high scores
    public int[] highScoreList = { 15, 14, 13, 12, 11, 10, 9, 8, 7, 6};
    //TODO: add names

    // Start is called before the first frame update
    void Start()
    {
        //test addScore
        AddScore(10);
        AddScore(16);
        AddScore(5);
        Debug.Log("Sorted list: ");
        for(int i = 0; i < highScoreList.Length; i++)
        {
            Debug.Log(highScoreList[i]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //write the high scores to a file
    public void WriteToFile()
    {
        //File.WriteAllText("C:\\Users\\deepti.ramani\\Documents\\DinoRun\\team-lemon-lime\\Dino Run - LemonLime\\Assets\\Scripts\\HighScoreList.txt", "" + highScore + "\n");
    }

    public void WriteToScene()
    {

    }

    //add new score into the list and sort
    private void AddScore(int addedScore)
    {
        int temp;
        for(int i = 0; i < highScoreList.Length; i++)
        {
            //if added score is greater than current score
            if(addedScore >= highScoreList[i])
            {
                //set current score to added score
                temp = highScoreList[i];
                highScoreList[i] = addedScore;
                //move current score down by one
                addedScore = temp;
                //repeat
            }
        }
    }
}
