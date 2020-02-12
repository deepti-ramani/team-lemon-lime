/*
HighScore.cs
by Deepti Ramani
02/05/2020
This class controls the permanent high score list (write to and read from)
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
    public string highScoreString;
    //TODO: add names
    //public string[] highScoreNames = new string[10];

    public GameObject HighScoreDisplay;

    // Start is called before the first frame update
    void Start()
    {
        //hide it to start
        HighScoreDisplay.SetActive(false);

        //get prev high score list at beginning and convert to int[]
        highScoreString = File.ReadAllText(@"C:\Users\deepti.ramani\Documents\DinoRun\team-lemon-lime\Dino Run - LemonLime\Assets\Scripts\HighScoreList.txt");
        string[] integerStrings = highScoreString.Split('\n');
        for (int i = 0; i < highScoreList.Length; i++)
        {
            highScoreList[i] = int.Parse(integerStrings[i]);
        }
    }

    // Update is called once per frame
    void Update()
    {


    }

    //write the new high scores to a file
    private void WriteToFile()
    {
        highScoreString = "";
        for(int i = 0; i < highScoreList.Length; i++)
        {
            highScoreString += highScoreList[i] + "\n";
        }
        File.WriteAllText(@"C:\Users\deepti.ramani\Documents\DinoRun\team-lemon-lime\Dino Run - LemonLime\Assets\Scripts\HighScoreList.txt", "" + highScoreString);
    }

    public void DisplayHighScores()
    {
        //fill with info from file and show
        HighScoreDisplay.transform.Find("HighScoreList").gameObject.GetComponent<Text>().text = highScoreString;
        HighScoreDisplay.SetActive(true);
    }

    //add new score into the list and sort (highest to lowest)
    public void AddScore(int addedScore)
    {
        int temp;
        //add score
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
        //write the new hs list to the file
        WriteToFile();
    }
}
