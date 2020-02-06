/*
HighScore.cs
by Deepti Ramani
02/05/2020

*/

using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScore : MonoBehaviour
{
    //top 10 high scores
    public int[] highScoreList = new int[10];
    //TODO: add names
    //public string[] highScoreNames = new string[10];

    public GameObject HighScoreDisplay;

    // Start is called before the first frame update
    void Start()
    {
        HighScoreDisplay.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //write the high scores to a file
    public void WriteToFile()
    {
        string highScoreString = "";
        for(int i = 0; i < highScoreList.Length; i++)
        {
            highScoreString += highScoreList[i] + "\n";
        }
        File.WriteAllText("C:\\Users\\deepti.ramani\\Documents\\DinoRun\\team-lemon-lime\\Dino Run - LemonLime\\Assets\\Scripts\\HighScoreList.txt", "" + highScoreString + "\n");
    }

    public void DisplayHighScores()
    {
        string highScoreString = "";
        //fill with info from file
        HighScoreDisplay.transform.Find("HighScoreList").gameObject.GetComponent<Text>().text = highScoreString;

        HighScoreDisplay.SetActive(true);
    }

    //add new score into the list and sort (highest to lowest)
    public void AddScore(int addedScore)
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
